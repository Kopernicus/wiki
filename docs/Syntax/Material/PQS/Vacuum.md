# PQS Vacuum Material

**Shader:** `Terrain/PQS/Sphere Projection SURFACE QUAD` &nbsp;·&nbsp; **`materialType = Vacuum`**

The PQS `Material { }` drives the shader that paints a body's **near-surface terrain** — the real, subdivided quad-sphere you walk on and fly over from low orbit down to the ground. This is the counterpart to the [ScaledVersion materials](/Syntax/Material/Scaled/Vacuum), which paint only the distant low-LOD sphere; the PQS material takes over as the camera descends, and KSP cross-fades the two across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes. `Vacuum` is the base sphere-projection terrain shader, and is the type you reach for on **airless bodies**.
Like the rest of the [PQS Main family](/Syntax/Material/PQS/Main), this shader is **world-space triplanar**: it projects each texture down the three world axes and blends them by the surface normal, so there are no UV seams and a handful of tiling textures can clothe an entire planet. It blends a stack of textures by **elevation**, layers a separate texture on **steep slopes**, and crossfades between a fine near tiling and a coarse far tiling by **camera distance**. Compared with [Main](/Syntax/Material/PQS/Main) it is in some ways larger (four elevation bands instead of three) and in some ways simpler — there are **no normal maps** and **no atmospheric fog stage** at all, which is exactly why it suits vacuum worlds. Its sibling PQS shaders are [Main](/Syntax/Material/PQS/Main), [Optimized](/Syntax/Material/PQS/Optimized), [Extra](/Syntax/Material/PQS/Extra), [MainFastBlend](/Syntax/Material/PQS/MainFastBlend), [OptimizedFastBlend](/Syntax/Material/PQS/OptimizedFastBlend) and [MainTriplanarZoomRotation](/Syntax/Material/PQS/MainTriplanarZoomRotation).

We shall first see how to enable the material, then how the shader assembles the surface, and finally discuss each group of properties in turn.

## Enabling

Select the material by setting `materialType = Vacuum` in the [`PQS { }`](/Syntax/PQS) node. The `Material { }` node then accepts the properties listed below.

```
PQS
{
    materialType = Vacuum
    Material
    {
        // Colour grading of the per-vertex colour map
        saturation = 1
        contrast   = 1
        tintColor  = 1, 1, 1, 0

        // Texture strength and the near/far crossfade distance
        texTiling      = 1000
        texPower       = 0.5
        multiPower     = 0.5
        groundTexStart = 2000
        groundTexEnd   = 10000

        // Deep (lowest) elevation band — near base + far multi texture
        deepTex         = MyMod/PluginData/regolith_color.dds
        deepMultiTex    = MyMod/PluginData/regolith_far.dds
        deepMultiFactor = 10

        // Main (mid) elevation band
        mainTex         = MyMod/PluginData/dust_color.dds
        mainMultiTex    = MyMod/PluginData/dust_far.dds
        mainMultiFactor = 10

        // High elevation band
        highTex         = MyMod/PluginData/rock_color.dds
        highMultiTex    = MyMod/PluginData/rock_far.dds
        highMultiFactor = 10

        // Snow (top) elevation band
        snowTex         = MyMod/PluginData/ice_color.dds
        snowMultiTex    = MyMod/PluginData/ice_far.dds
        snowMultiFactor = 10

        // Where each band fades into the next (on normalised vertex elevation)
        deepStart  = 0
        deepEnd    = 0.3
        mainLoStart = 0
        mainLoEnd   = 0.5
        mainHiStart = 0.3
        mainHiEnd   = 0.5
        hiLoStart  = 0.6
        hiLoEnd    = 0.6
        hiHiStart  = 0.6
        hiHiEnd    = 0.9
        snowStart  = 0.9
        snowEnd    = 1

        // Steep-slope cliff overlay
        steepTex      = MyMod/PluginData/cliff_color.dds
        steepTiling   = 1
        steepPower    = 1
        steepTexStart = 20000
        steepTexEnd   = 30000
    }
}
```

## How the surface is built

The shader is a triplanar terrain surface assembled per pixel. There is no single colour map; instead a base colour, four elevation textures (each with a paired distant "multi" texture), and a steep overlay are combined in the following order:

1. **Per-vertex colour grading** — the PQS supplies a colour per vertex (`COLOR.rgb`, usually from a [colour map](/Syntax/PQSMods/VertexColorMap) or [LandControl](/Syntax/PQSMods/LandControl/LandControl)). The shader desaturates that colour toward its luma by `saturation`, mixes in `tintColor` (whose **alpha** is the mix amount, acting as an unsaturation pivot), and scales it by `contrast`. The result is the **base colour** that the band textures then tint.
2. **Four elevation bands** — `deepTex`, `mainTex`, `highTex` and `snowTex` are blended by the vertex's normalised elevation (written into `COLOR.w` by the PQS height build). Each band's weight is a pair of smoothsteps: deep is `1 − smoothstep(deepStart, deepEnd)`; main and high are each *isolated mid ranges* computed as `smoothstep(loStart, loEnd) − smoothstep(hiStart, hiEnd)`; and snow is `smoothstep(snowStart, snowEnd)`. So deep covers the bottom of the height range, main and high the middle, and snow the top. **Note the naming:** despite the "snow" label this is just the top-elevation band texture — on an airless body it is whatever you want the peaks to be, not necessarily snow.
3. **Near/far tiling crossfade** — each band is sampled at two scales and crossfaded by camera distance. The **near** tier uses the band's base texture (`deepTex`/`mainTex`/`highTex`/`snowTex`) at the shared `texTiling` scale for close-up detail; the **far** tier uses a *separate* texture per band (`deepMultiTex` … `snowMultiTex`), each at its own `*MultiFactor` tiling, for the distance. The crossfade runs between the `groundTexStart` and `groundTexEnd` view distances. `texPower` and `multiPower` set how strongly the near and far tiers show. Because the triplanar coordinates are world position scaled by 10⁻⁵, the tiling numbers are large (the near default is 1000).
4. **Texture vs. flat colour** — the blended band texture does not replace the base colour, it *modulates* it: the final albedo is `lerp(baseColour, baseColour × bandTexture, blendW)`, where `blendW` is driven by `texPower`/`multiPower` and the near/far mix. Where it falls to zero — with both powers at 0 — you see the flat graded colour map; where it is high you see fully textured terrain.
5. **Steep-slope overlay** — `steepTex` is blended over the result on steep terrain, sampled triplanar at its own `steepTiling`. The per-vertex steepness (`TEXCOORD1.y`) is multiplied by `steepPower` and clamped to 0–1, so higher `steepPower` makes the cliff texture reach onto gentler slopes. The overlay has its own distance fade between `steepTexStart` and `steepTexEnd` — the overlay is at full strength close to the camera and fades **out** with distance (`steepFade = 1 − smoothstep(steepTexStart, steepTexEnd, depth)`), so it behaves like the near tier of the ground textures. There is **no** separate near/far tiling for the steep overlay and **no** steep normal map.
6. **Lighting** — the surface is lit with a plain diffuse (Lambert) `NdotL` term against the local star plus spherical-harmonic ambient. There is **no normal mapping** on this shader (unlike [Main](/Syntax/Material/PQS/Main), every band of which carries a bump map) and **no specular highlight** — the world normal used for lighting comes straight from the mesh tangent frame, not from a texture. The forward-add pass folds in additional point/spot/directional lights, and the deferred and light-prepass paths reproduce the same triplanar composite for Unity's deferred renderer.

The triplanar projection means the band textures need **no UV mapping** on the mesh. The shader still constructs a tangent frame and therefore expects mesh **tangents**, which the PQS quad-sphere provides. Because the colour, elevation and slope all come from the PQS build, the final look depends as much on your [PQSMods](/Syntax/PQSMods/) (height, colour, LandControl) as on the textures set here.

## Properties

The properties fall into four groups, which we shall take in turn:

1. **Colour grading** — how the per-vertex colour map is processed before texturing.
2. **Texture blend & distance** — the near/far strengths and the crossfade distances.
3. **Elevation bands** — the four height textures, their distant multi textures, and the transition windows.
4. **Steep-slope overlay & opacity** — the cliff texture and the PQS fade scalar.

### Colour grading

| Property | Format | Description |
|----------|--------|-------------|
| `saturation` | Decimal | Saturation of the per-vertex colour map: `0` collapses it to greyscale (luma), `1` leaves it unchanged, higher oversaturates. Applied per-vertex before contrast and tint. Default 1. |
| `contrast` | Decimal | Contrast multiplier applied to the graded colour. Default 1. |
| `tintColor` | Color | A tint mixed into the base colour; the **alpha** is the mix amount, acting as an unsaturation pivot (0 = no tint). Default (1, 1, 1, 0) — i.e. no tint. |

### Texture blend & distance

| Property | Format | Description |
|----------|--------|-------------|
| `texTiling` | Decimal | World tiling of the **near** base textures (all four bands share this scale). Default 1000. |
| `texPower` | Decimal | Strength of the **near** texture tier; drops the textures out toward the flat colour map as it approaches 0. Default 0.5. |
| `multiPower` | Decimal | Strength of the **far** (multi) texture tier. Default 0.5. |
| `groundTexStart` | Decimal | View distance (m) at which the near→far crossfade begins. Default 2000. |
| `groundTexEnd` | Decimal | View distance (m) at which only the far tier remains. Default 10000. (The loader and shader both label this "NearFar Start", which is a mislabel — it is the **end** of the near/far crossfade.) |

### Elevation bands

Each band has a near base texture and a separate far ("multi") texture with its own tiling factor. The band weights come from the vertex elevation and the transition values at the end of the table. There are **no** normal maps on any band.

| Property | Format | Description |
|----------|--------|-------------|
| `deepTex` | File Path | Near base albedo for the **deep** (lowest) elevation band. Triplanar, world-tiled at `texTiling`. Default "white". |
| `deepTexScale` / `deepTexOffset` | Vector2 | Tiling and offset of `deepTex`. |
| `deepMultiTex` | File Path | Far (distant) texture for the deep band. Default "white". |
| `deepMultiTexScale` / `deepMultiTexOffset` | Vector2 | Tiling and offset of `deepMultiTex`. |
| `deepMultiFactor` | Decimal | World tiling of the deep band's far texture. Default 1. |
| `mainTex` | File Path | Near base albedo for the **main** (mid) elevation band. Default "white". |
| `mainTexScale` / `mainTexOffset` | Vector2 | Tiling and offset of `mainTex`. |
| `mainMultiTex` | File Path | Far texture for the main band. Default "white". |
| `mainMultiTexScale` / `mainMultiTexOffset` | Vector2 | Tiling and offset of `mainMultiTex`. |
| `mainMultiFactor` | Decimal | World tiling of the main band's far texture. Default 1. |
| `highTex` | File Path | Near base albedo for the **high** elevation band. Default "white". |
| `highTexScale` / `highTexOffset` | Vector2 | Tiling and offset of `highTex`. |
| `highMultiTex` | File Path | Far texture for the high band. Default "white". |
| `highMultiTexScale` / `highMultiTexOffset` | Vector2 | Tiling and offset of `highMultiTex`. |
| `highMultiFactor` | Decimal | World tiling of the high band's far texture. Default 1. |
| `snowTex` | File Path | Near base albedo for the **snow** (top) elevation band. Despite the name, simply the top-elevation texture. Default "white". |
| `snowTexScale` / `snowTexOffset` | Vector2 | Tiling and offset of `snowTex`. |
| `snowMultiTex` | File Path | Far texture for the snow band. Default "white". |
| `snowMultiTexScale` / `snowMultiTexOffset` | Vector2 | Tiling and offset of `snowMultiTex`. |
| `snowMultiFactor` | Decimal | World tiling of the snow band's far texture. Default 1. |
| `deepStart` | Decimal | Elevation (normalised 0–1) below which the deep band is fully active. Default 0. |
| `deepEnd` | Decimal | Elevation at which the deep band has fully faded out. Default 0.3. |
| `mainLoStart` | Decimal | Lower edge of the main band's rising smoothstep. Default 0. |
| `mainLoEnd` | Decimal | Upper edge of the main band's rising smoothstep. Default 0.5. |
| `mainHiStart` | Decimal | Lower edge of the main band's falling smoothstep (main weight = rising − falling, isolating the mid range). Default 0.3. |
| `mainHiEnd` | Decimal | Upper edge of the main band's falling smoothstep. Default 0.5. |
| `hiLoStart` | Decimal | Lower edge of the high band's rising smoothstep. Default 0.6. |
| `hiLoEnd` | Decimal | Upper edge of the high band's rising smoothstep. Default 0.6. |
| `hiHiStart` | Decimal | Lower edge of the high band's falling smoothstep (high weight = rising − falling). Default 0.6. |
| `hiHiEnd` | Decimal | Upper edge of the high band's falling smoothstep. Default 0.9. |
| `snowStart` | Decimal | Elevation at which the snow band begins to appear. Default 0.9. |
| `snowEnd` | Decimal | Elevation at which the snow band reaches full weight. Default 1. |

### Steep-slope overlay & opacity

| Property | Format | Description |
|----------|--------|-------------|
| `steepTex` | File Path | Albedo blended over steep slopes (cliffs), triplanar at `steepTiling`. Default "white". |
| `steepTexScale` / `steepTexOffset` | Vector2 | Tiling and offset of `steepTex`. |
| `steepTiling` | Decimal | World tiling of the steep overlay. Default 1. |
| `steepPower` | Decimal | Multiplier on the vertex steepness before it is clamped to the overlay weight. Higher values push the cliff texture onto gentler slopes; 0 disables it. Default 1. |
| `steepTexStart` | Decimal | View distance (m) at which the steep overlay's distance fade begins. The overlay is at full strength nearer than this and fades out beyond it. Default 20000. |
| `steepTexEnd` | Decimal | View distance (m) beyond which the steep overlay is fully faded out. Default 30000. |
| `planetOpacity` | Decimal | Opacity of the PQS material (shader uniform `_PlanetOpacity`), used by KSP to fade the terrain against the scaled-space sphere across the `fadeStart`/`fadeEnd` transition. Normally left at 1. Default 1. |

## Notes

* This is the body's **near-surface** terrain material. The body simultaneously has a [ScaledVersion material](/Syntax/Material/Scaled/Vacuum) for the distant sphere; KSP cross-fades between the two across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes, so the two should be tuned to match where they meet.
* The textures are **world-space triplanar**, so they need no UVs and the tiling values are in world units scaled by 10⁻⁵ (hence the large near default of 1000). The shader still constructs a tangent frame from the mesh, so the PQS quad-sphere must provide **tangents**; a custom mesh without them will light incorrectly.
* The band colour, the per-vertex elevation that drives the band blend (`COLOR.w`), the per-vertex steepness that drives the overlay (`TEXCOORD1.y`), and `COLOR.rgb` base tint all come from the PQS build — your [height](/Syntax/PQSMods/VertexHeightMap), [colour](/Syntax/PQSMods/VertexColorMap) and [LandControl](/Syntax/PQSMods/LandControl/LandControl) mods shape the input this shader paints.
* Unlike [Main](/Syntax/Material/PQS/Main), this shader has **no normal maps** (no `*BumpMap` properties on any band or on the steep overlay) and **no atmospheric/ocean fog stage** — there are no `globalDensity` or `fogColorRamp` properties. The lighting is plain diffuse with SH ambient and **no specular** term.
* The far tier here is a *separate texture per band* (`deepMultiTex` … `snowMultiTex`), each with its own `*MultiFactor` tiling. This differs from the [Main](/Syntax/Material/PQS/Main) family, where the far tier reuses the band's own albedo at a coarser `*MultiFactor` tiling rather than supplying a distinct texture.
* The config key names differ from the Main family: near/far strengths are `texPower` / `multiPower` (Main uses `powerNear` / `powerFar`), the near base tiling is the single shared `texTiling`, and the band-transition keys are `deepStart`/`deepEnd`, `mainLoStart`…`mainHiEnd`, `hiLoStart`…`hiHiEnd`, `snowStart`/`snowEnd`. Each ParserTarget on this loader is unique — there are **no config aliases** (in particular, no `Atmospheric…` alias; the token is simply `Vacuum`).
* The steep overlay's distance fade runs full strength up close and fades the cliff texture **out** with distance (`steepFade = 1 − smoothstep(steepTexStart, steepTexEnd)`), so it is visible near the camera and hidden far away — matching the near tier of the ground textures.
* `planetOpacity` drives the per-pixel alpha emitted by all forward and deferred passes. Because the passes blend with `OneMinusSrcAlpha SrcAlpha`, KSP uses it to fade the planet surface in and out at the scaled-space transition; a static config value is normally left at its default of 1.
* The `_floatingOriginOffset` shader input is **not** a config property — KSP sets it at runtime so the world-space triplanar coordinates stay numerically stable as the game shifts the floating origin around the active craft.
* If none of the passes compile on the target platform, the shader falls back to Unity's built-in `Diffuse` shader.
