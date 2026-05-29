# Ocean Surface Quad

**Shader:** `Terrain/PQS/Ocean Surface Quad` &nbsp;·&nbsp; **selected as the `Material { }` of the body's [`Ocean { }`](/Syntax/Ocean) node (not via `materialType`)**

A body's ocean is itself a Procedural Quad-Sphere — a second [PQS](/Syntax/PQS) that sits at sea level and renders the water surface. The `Terrain/PQS/Ocean Surface Quad` shader is what paints that water. It is the default (and only built-in) surface material for the [`Ocean { }`](/Syntax/Ocean) node, applied to every quad tile of the ocean sphere as the camera descends toward the surface.

This is a **near-surface** material, the same way the [PQS Main family](/Syntax/Material/PQS/Main) clothes the solid terrain. It is not one of the [ScaledVersion materials](/Syntax/Material/Scaled/Vacuum) — those paint the distant low-LOD sphere of the *planet*, while this shader animates the real ocean tiles you fly down to. Where the PQS Main shader builds dry land from elevation-banded tiling textures, this one builds moving water: it triplanar-samples two animated water textures, fades to a "from space" colour with distance, folds in atmospheric-perspective fog and a sun-direction fog tint, and lights the result with a Blinn-Phong specular so the sun glints off the swell.

Unlike the PQS Main materials, the ocean material is **not** chosen with a `materialType` / `type` keyword. The [`Ocean { }`](/Syntax/Ocean) node always uses this one shader; you simply fill in its `Material { }` block. There is also a separate, much simpler **Ocean Surface Quad (Fallback)** shader behind the sibling `FallbackMaterial { }` block (used on low terrain-shader-quality settings) — that one is out of scope here and is only mentioned in the [Notes](#notes).

We shall first see how to enable the material, then how the shader assembles the water, and finally discuss each group of properties in turn.

## Enabling

You do not select this material with a type keyword. Inside the body's [`Ocean { }`](/Syntax/Ocean) node, the `Material { }` block *is* the Ocean Surface Quad material — Kopernicus binds it to the ocean PQS automatically, defaulting the shader to `Terrain/PQS/Ocean Surface Quad`. (A `shader = ...` line inside `Material { }` is honoured if you want to force a specific shader, but it defaults to this one, so it is normally omitted.) Just provide the properties below.

```
Ocean
{
    ocean = True
    oceanColor = 0, 0, 1, 1
    density = 1.25
    minLevel = 1
    maxLevel = 6

    Material
    {
        // Near vs. far ocean colour
        color          = 1, 1, 1, 1
        colorFromSpace = 0.1, 0.2, 0.35, 1

        // Animated water textures (triplanar, world-tiled)
        waterTex   = MyMod/PluginData/water_a.dds
        waterTex1  = MyMod/PluginData/water_b.dds
        tiling     = 1

        // Wave motion
        displacement    = 1
        texDisplacement = 1
        dispFreq        = 1

        // Specular glint
        specColor = 1, 1, 1, 1
        shininess = 0.078125
        gloss     = 0.078125

        // Edge transparency and the depth fade to "from space"
        oceanOpacity = 1
        falloffPower = 1
        falloffExp   = 2
        fadeStart    = 1
        fadeEnd      = 1

        // Atmospheric-perspective fog
        fogColor      = 0, 0, 1, 1
        globalDensity = 1
        fogColorRamp  = MyMod/PluginData/ocean_fog_ramp.dds
    }
}
```

## How the surface is built

The shader is a triplanar, animated water surface assembled per pixel. The ocean PQS feeds each vertex a *packed surface normal* (in `TEXCOORD0.xy` and `TEXCOORD1.x`), which the shader uses both as the triplanar projection axes and as the sun-ramp lookup coordinate. The stages below are taken from the shader's forward-base pass:

1. **Triplanar water albedo** — `waterTex` is projected down the three axes of the packed normal at the `tiling` scale and blended by the normal's magnitude, so it tiles seamlessly with no UVs. A per-frame displacement driven by `_SinTime.x × displacement` is added to the lookup. The first sample's own colour, swizzled, is fed back as a small offset, then `waterTex1` is sampled again with a time-scrolled, animated UV (driven by `dispFreq`) and `texDisplacement` to layer a second, shifting water detail on top. The net result is a moving, interlocking water albedo.
2. **Depth fade to "from space"** — the textured water is cross-faded toward a flat `colorFromSpace` by a smoothstep on view depth between `fadeStart` and `fadeEnd`. Near the camera you see the animated `color`-tinted water; with distance it settles to the single `colorFromSpace` colour. **Note the unusual default:** both `fadeStart` and `fadeEnd` default to `1`, a degenerate zero-width window — to get a meaningful near/far transition you must set them to a real distance range.
3. **Atmospheric-perspective (AP) fog** — the result is then lerped toward `fogColor` by a height-density exponential fog factor (`1 − exp2(depth × density × …)`). `globalDensity` scales how quickly that fog washes in with distance. This is the same family of aerial-perspective haze the terrain shaders use.
4. **Sun-direction fog ramp** — a 1-D `fogColorRamp` is sampled at `u = dot(sunDirection, surfaceNormal)`, `v = 0.5`, and mixed on top, weighted by the same atmospheric factor. This lets the ocean horizon be tinted differently toward and away from the sun. The ramp can be supplied as a texture (`fogColorRamp`) or inline as a gradient (`FogColorRamp`).
5. **Lighting** — the water is lit with a diffuse `N·L` term plus a Blinn-Phong specular highlight (exponent `shininess × 128`, tinted by `specColor`, scaled by `gloss`). The specular is what produces the sun glint on the water. The directional light is attenuated by a light-probe occlusion sample (1.0 when no probe volume is bound), and extra lights are accumulated in a separate additive pass.
6. **Output alpha** — the fragment alpha is a fresnel-like edge falloff, `pow((1 − N·V) × falloffPower, falloffExp)`, blended against `oceanOpacity` by the depth fade and then multiplied by `(1 − planetOpacity)`. With the standard `SrcAlpha OneMinusSrcAlpha` blend this fades the water out at grazing angles and lets KSP dissolve the whole ocean against the planet via `planetOpacity`.

A few properties from the loader are **declared but inert** — the compiled fragment never reads them. They exist only so the material round-trips with KSP's stock ocean material binary. These are `bTiling`, `bumpMap` (and its scale/offset), `mix`, `heightFallOff`, `atmosphereDepth`, `normalXYFudge`, and `normalZFudge`. In particular there is **no functional normal map**: despite the `bumpMap` key, the wave normals come entirely from the animated triplanar albedo, not from a bump texture. The AP "height fall-off" is likewise driven by a runtime global, not by the `heightFallOff` key (see [Notes](#notes)).

The triplanar projection means the water textures need **no UV mapping**, but the shader still relies on the mesh's **tangents** and the PQS-supplied packed normal; the ocean quad-sphere Kopernicus builds provides both.

## Properties

The properties fall into six groups, which we shall take in turn:

1. **Colour** — the near tint and the far "from space" colour.
2. **Water textures & motion** — the two animated textures, their tiling, and the wave-animation controls.
3. **Specular** — the highlight tint and sharpness.
4. **Opacity & depth fade** — the edge falloff, base opacity, and the near/far fade window.
5. **Atmospheric-perspective fog** — the fog colour, density, and sun-direction ramp.
6. **Compatibility / inert** — keys the loader accepts but the shader does not consume.

### Colour

| Property | Format | Description |
|----------|--------|-------------|
| `color` | Color | Near-distance tint multiplied into the animated water albedo when the camera is close (depth fade = 0). The alpha is unused. Default (1, 1, 1, 1). |
| `colorFromSpace` | Color | Far-distance ocean colour the water fades toward across the `fadeStart`/`fadeEnd` depth window — the colour the ocean shows from orbit. The alpha is unused. Default (1, 1, 1, 1). |

### Water textures & motion

| Property | Format | Description |
|----------|--------|-------------|
| `waterTex` | File Path | Primary water albedo (RGB), triplanar-sampled at `tiling`. Its swizzled colour also drives the displacement fed into `waterTex1`. Default "white". |
| `waterTexScale` / `waterTexOffset` | Vector2 | Tiling and offset of `waterTex`. |
| `waterTex1` | File Path | Secondary water albedo (RGB), triplanar-sampled with the displaced, time-animated UVs to layer extra shifting detail. Default "white". |
| `waterTex1Scale` / `waterTex1Offset` | Vector2 | Tiling and offset of `waterTex1`. |
| `tiling` | Decimal | World-space triplanar tiling scale for the water textures; larger values tile the water more finely. Default 1. |
| `displacement` | Decimal | Wave-motion amplitude. Scales `_SinTime.x` into the per-axis UV offset added to the near water sample. Default 1. |
| `texDisplacement` | Decimal | Texture-displacement scale: scales the first water sample (its displaced albedo — the sample value plus the swizzle delta, scaled by the wave-strength factor) before it offsets the second sample's UV, controlling cross-texture distortion. Default 1. |
| `dispFreq` | Decimal | Animation frequency for the cosine/sine time offsets that scroll the `waterTex1` UV. Higher values cycle the waves faster. Default 1. |

### Specular

| Property | Format | Description |
|----------|--------|-------------|
| `specColor` | Color | Specular highlight tint, modulated by the star's light colour, `gloss`, and `(1 − planetOpacity)`. Default (1, 1, 1, 1). |
| `shininess` | Decimal | Highlight sharpness: the Blinn-Phong exponent is `shininess × 128`, so smaller values give a broad glint and larger values a tight one. The shader declares the valid range as `[0.01, 1]`. Default 0.078125. |
| `gloss` | Decimal | Specular intensity multiplier. Scales the highlight alongside `(1 − planetOpacity)`. The shader declares the valid range as `[0.01, 1]`. Default 0.078125. |

### Opacity & depth fade

| Property | Format | Description |
|----------|--------|-------------|
| `oceanOpacity` | Decimal | Base opacity of the water. The fresnel falloff term blends against this, the depth fade blends that toward 1, and the result is scaled by `(1 − planetOpacity)`. Default 1. |
| `falloffPower` | Decimal | Multiplier applied to `(1 − N·V)` before the fresnel exponent — scales the strength of the grazing-angle transparency falloff. Default 1. |
| `falloffExp` | Decimal | Exponent of the fresnel-like edge falloff. Higher values push the transparency more sharply toward the silhouette, keeping the centre opaque. Default 2. |
| `fadeStart` | Decimal | Near edge (view depth) of the smoothstep that blends between the textured water and `colorFromSpace`. Default 1. |
| `fadeEnd` | Decimal | Far edge of that depth smoothstep; beyond it only `colorFromSpace` shows. Default 1. Note that with both defaults at 1 the window is degenerate — set a real range to use the fade. |
| `planetOpacity` | Decimal | Planet-vs-background fade weight. The output alpha is multiplied by `(1 − planetOpacity)`, so KSP can dissolve the ocean against the planet. Normally left at 1; runtime-driven. Default 1. |

### Atmospheric-perspective fog

| Property | Format | Description |
|----------|--------|-------------|
| `fogColor` | Color | Aerial-perspective fog tint (RGB) lerped on top of the depth-faded water by the height-density exponent — typically the planet's atmosphere colour. The alpha is unused. Default (0, 0, 1, 1). |
| `globalDensity` | Decimal | Density scalar inside the AP exp2 fog blend; larger values thicken the fog wash at distance. Default 1. |
| `fogColorRamp` | File Path | 1-D colour ramp sampled at `u = dot(sunDirection, surfaceNormal)`, `v = 0.5`, mixed on top of the post-fog colour — lets the horizon be tinted toward vs. away from the sun. Default "white". |
| `fogColorRampScale` / `fogColorRampOffset` | Vector2 | Tiling and offset of the fog ramp. |
| `FogColorRamp` | Gradient | The `fogColorRamp` supplied inline as a gradient instead of as a texture file. Sets the same underlying ramp. |

### Compatibility / inert

These keys are accepted by the loader (so stock ocean materials round-trip), but the compiled shader does not read them. Setting them has no visual effect.

| Property | Format | Description |
|----------|--------|-------------|
| `bumpMap` | File Path | Declared normal map slot, but **not sampled** — the water normals come from the animated triplanar albedo, not a bump texture. Default "bump". |
| `bumpMapScale` / `bumpMapOffset` | Vector2 | Tiling and offset of the (unused) `bumpMap`. |
| `bTiling` | Decimal | Declared normal-map tiling; unused by the fragment. Default 1. |
| `mix` | Decimal | Declared mix factor; unused by the fragment. Default 1. |
| `heightFallOff` | Decimal | Declared "AP Height Fall Off"; the actual height fall-off is read from a runtime global, not this key. Default 1. |
| `atmosphereDepth` | Decimal | Declared "AP Atmosphere Depth"; not sampled by the fragment. Default 1. |
| `normalXYFudge` | Decimal | Declared X/Y normal-map fudge for legacy paths; not consumed. Default 0.1. |
| `normalZFudge` | Decimal | Declared Z normal-map fudge for legacy paths; not consumed. Default 1.1. |

## Notes

* **This is the near-surface ocean material.** It clothes the ocean's own [PQS](/Syntax/PQS) sphere, parallel to how the [PQS Main family](/Syntax/Material/PQS/Main) clothes the solid terrain — both are distinct from the [ScaledVersion materials](/Syntax/Material/Scaled/Vacuum) that paint the planet's distant sphere.
* **No `materialType`.** The [`Ocean { }`](/Syntax/Ocean) node has no shader-selector keyword; its `Material { }` is always this shader. (Kopernicus does read an optional `shader = ...` line inside the block, defaulting to `Terrain/PQS/Ocean Surface Quad`, but you normally leave it out.)
* **Fallback material is separate.** The sibling `FallbackMaterial { }` block in the [`Ocean { }`](/Syntax/Ocean) node drives a different, simpler shader, *Ocean Surface Quad (Fallback)*, used on lower terrain-shader-quality settings. It accepts fewer keys (no fog or wave-motion controls, and no fog ramp — i.e. no `displacement`, `texDisplacement`, `dispFreq`, `fogColor`, `globalDensity`, `heightFallOff`, `atmosphereDepth` or `fogColorRamp`) but keeps the colour, water-texture, tiling, specular (`specColor`/`shininess`/`gloss`), fade and `planetOpacity` keys. It is documented separately — do not expect the properties on this page to apply to it.
* **No functional normal map.** Despite the `bumpMap` key, the shader has no bump-mapping path; the moving wave detail is entirely in the two animated triplanar albedo textures. The `bumpMap`, `bTiling`, `mix`, `heightFallOff`, `atmosphereDepth`, `normalXYFudge` and `normalZFudge` keys are inert (see the Compatibility group above).
* **Degenerate fade defaults.** Both `fadeStart` and `fadeEnd` default to 1, which collapses the depth-fade window. Until you set a real near/far range, the near-water vs. `colorFromSpace` blend will not behave as a gradual transition.
* **Runtime-driven inputs (not config).** Several shader inputs are supplied by KSP each frame and are *not* properties of this material: the sun direction used for the fog ramp; the viewer's air density / height-density factor that drives the AP fog (this — not `heightFallOff` — is the real height fall-off); the animation time (`_Time` / `_SinTime`) that moves the water; the light colour, light position and light-probe occlusion used for shading; and `planetOpacity`, which the engine animates to fade the ocean against the planet.
* **No UVs, but tangents required.** The water textures are world-space triplanar, so the mesh needs no UV mapping, but the shader still uses the mesh **tangents** and the PQS-supplied packed surface normal (in `TEXCOORD0.xy` / `TEXCOORD1.x`). The ocean quad-sphere Kopernicus generates provides both; a generic Unity mesh with ordinary UVs in `TEXCOORD0` will not render correctly.
* **Fallback shader.** If `Terrain/PQS/Ocean Surface Quad` fails to compile on the target platform, Unity falls back to its built-in `Standard` shader.
