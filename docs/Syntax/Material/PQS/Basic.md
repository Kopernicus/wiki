# PQS Sphere Projection — Surface Quad (AP)

**Shader:** `Terrain/PQS/Sphere Projection SURFACE QUAD (AP) ` &nbsp;·&nbsp; **`materialType = Basic`**

The PQS `Material { }` drives the shader that paints a body's **near-surface terrain** — the real, subdivided quad-sphere you see from low orbit down to the ground. This is the counterpart to the [ScaledVersion materials](/Syntax/Material/Scaled/Vacuum), which paint only the distant low-LOD sphere; the PQS material takes over as the camera descends and the two are cross-faded across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes. `Terrain/PQS/Sphere Projection SURFACE QUAD (AP)` is the **aerial-perspective** member of the "Sphere Projection" family — the same projected surface as the vacuum variant, plus an atmospheric distance fog.

Unlike the scaled-space shaders, which sample a single pre-baked colour map, this shader builds the surface procedurally from a small set of tiling textures. It is **triplanar**, but it does *not* project down the world axes the way the [Main](/Syntax/Material/PQS/Main) family does. Instead it takes the mesh's **object-space surface direction** — packed by the PQS into the texture coordinates — and uses that normalised direction both as the triplanar UV source and as the per-axis projection weight. For the PQS quad-sphere this is the outward radial direction, so the projection follows the sphere itself. It then blends **four** textures by elevation (deep / main / high / snow), layers a separate texture on **steep slopes**, crossfades between a fine **near** and a coarse **far** tiling by camera distance, and folds in aerial-perspective fog at the end.

The "(AP)" suffix is the distinguishing feature: this is the variant for bodies **with an atmosphere**. It is the same surface-projection pipeline as the vacuum (non-AP) Sphere Projection shader, with an extra fog stage that drives distant terrain toward the atmosphere's scattering colour. If your body has no atmosphere, use the vacuum variant — the fog stage here will simply do nothing once its runtime atmosphere inputs read zero.

This family is the **simpler** projection-based terrain shader: note that, despite blending four albedo bands and a steep overlay, it carries **no normal maps at all** and applies **no specular** term. If you want the richer, world-space, normal-mapped terrain look, see the [Main](/Syntax/Material/PQS/Main) family and its variants — [Optimized](/Syntax/Material/PQS/Optimized), [Extra](/Syntax/Material/PQS/Extra), [MainFastBlend](/Syntax/Material/PQS/MainFastBlend), [OptimizedFastBlend](/Syntax/Material/PQS/OptimizedFastBlend) and [MainTriplanarZoomRotation](/Syntax/Material/PQS/MainTriplanarZoomRotation).

We shall first see how to enable the material, then how the shader assembles the surface, and finally discuss each group of properties in turn.

## Enabling

Select the material by setting `materialType = Basic` in the [`PQS { }`](/Syntax/PQS) node. The `Material { }` node then accepts the properties listed below.

```
PQS
{
    materialType = Basic
    Material
    {
        // Colour grading of the per-vertex colour map
        saturation = 1
        contrast   = 1
        tintColor  = 1, 1, 1, 0

        // How strongly the textures show, and the near/far crossfade distance
        texPower       = 0.5
        multiPower     = 0.5
        texTiling      = 1000
        groundTexStart = 2000
        groundTexEnd   = 10000

        // Deep elevation band (lowest)
        deepTex         = MyMod/PluginData/sand_color.dds
        deepMultiTex    = MyMod/PluginData/sand_far.dds
        deepMultiFactor = 10

        // Main elevation band
        mainTex         = MyMod/PluginData/rock_color.dds
        mainMultiTex    = MyMod/PluginData/rock_far.dds
        mainMultiFactor = 10

        // High elevation band
        highTex         = MyMod/PluginData/scree_color.dds
        highMultiTex    = MyMod/PluginData/scree_far.dds
        highMultiFactor = 10

        // Snow elevation band (highest)
        snowTex         = MyMod/PluginData/snow_color.dds
        snowMultiTex    = MyMod/PluginData/snow_far.dds
        snowMultiFactor = 10

        // Band transition windows (on normalised vertex elevation)
        deepStart  = 0
        deepEnd    = 0.3
        mainLoStart = 0
        mainLoEnd   = 0.5
        mainHiStart = 0.3
        mainHiEnd   = 0.5
        hiLoStart   = 0.6
        hiLoEnd     = 0.6
        hiHiStart   = 0.6
        hiHiEnd     = 0.9
        snowStart   = 0.9
        snowEnd     = 1

        // Steep-slope overlay
        steepTex      = MyMod/PluginData/cliff_color.dds
        steepTiling   = 1
        steepPower    = 1
        steepTexStart = 20000
        steepTexEnd   = 30000

        // Aerial-perspective fog
        fogColor      = 0, 0, 1, 1
        globalDensity = 1
        heightFallOff = 1
        atmosphereDepth = 1
        fogColorRamp  = MyMod/PluginData/fog_ramp.dds
    }
}
```

## How the surface is built

The shader is a triplanar terrain surface assembled per pixel. There is no single colour map; a base colour, four elevation textures, a steep overlay, and fog are combined in the following order (this is the order the fragment program follows, identical across the forward, deferred and legacy-deferred passes):

1. **Per-vertex colour grading** — the PQS supplies a colour per vertex (`COLOR.rgb`, usually from a colour map or [LandControl](/Syntax/PQSMods/LandControl/LandControl)). The shader desaturates that colour toward its luma by `saturation`, mixes in `tintColor` (whose **alpha** is the mix amount), and scales the result by `contrast`. This grading happens in the vertex stage, and the output is the **base colour** that the band textures then tint. Note the property is labelled "Colour Unsaturation (A = Factor)" in the shader — its alpha is the tint/unsaturation factor, not an opacity.
2. **Four elevation bands** — `deepTex`, `mainTex`, `highTex` and `snowTex` are blended by the vertex's normalised elevation, which the PQS height build writes into `COLOR.w` (0 at the lowest point of the height range, 1 at the highest). Each band's weight is a smoothstep window: the **deep** band is `1 - smoothstep(deepStart, deepEnd)`; the **main** band is `smoothstep(mainLoStart, mainLoEnd) − smoothstep(mainHiStart, mainHiEnd)`; the **high** band is `smoothstep(hiLoStart, hiLoEnd) − smoothstep(hiHiStart, hiHiEnd)`; the **snow** band is `smoothstep(snowStart, snowEnd)`. So deep covers the bottom of the height range, main and high the two middle windows, and snow the top.
3. **Near/far tiling crossfade** — every band is sampled at **two** tiling scales and crossfaded by camera distance. The fine **near** sample uses the shared `texTiling` for all four bands; the coarse **far** ("multi") sample uses each band's own `*MultiFactor`, drawn from `deepMultiTex` / `mainMultiTex` / `highMultiTex` / `snowMultiTex`. The crossfade runs across the view-space distances `groundTexStart` → `groundTexEnd`. `texPower` and `multiPower` set how strongly the near and far tiers show.
4. **Texture vs. flat colour** — the blended band textures do not replace the base colour, they *modulate* it: the final albedo is `lerp(baseColour, baseColour × bandTextures, texMix)`, where `texMix` is driven by `texPower`/`multiPower` and the near/far mix. Where it falls to zero — far away, or with both powers at 0 — you see the flat graded colour map; where it is high you see fully textured terrain. (Note the near textures all share one tiling, `texTiling`, whereas Main gives each band its own near tiling.)
5. **Steep-slope overlay** — `steepTex` is blended over the result on steep terrain. The per-vertex steepness (supplied by the PQS in `TEXCOORD1.y`) is multiplied by `steepPower` and clamped, so a higher `steepPower` pushes the cliff texture onto gentler slopes. The overlay has its own distance fade between `steepTexStart` and `steepTexEnd`, and its own tiling `steepTiling`.
6. **No normal maps, diffuse lighting** — unlike the Main family, **none of the bands carry a normal map and there is no steep bump map** — only albedo. The surface is lit with a plain diffuse (Lambert) term plus spherical-harmonic ambient; there is **no specular highlight** (the deferred pass writes zero specular and zero smoothness).
7. **Aerial-perspective fog** — last, the atmospheric haze is mixed in. The fog **colour** is read from the 1-D `fogColorRamp` indexed by the **sun angle** (the lookup coordinate is the dot of the runtime sun direction with the per-vertex surface direction, so the fog can be tinted differently toward and away from the sun). Its **strength** grows with distance as `1 − exp(−viewZ × globalDensity × density)`, where `density` is the per-frame value KSP computes from `heightFallOff` and `atmosphereDepth`. As distance increases the albedo is driven toward the fog colour.

Because the triplanar coordinates come from the PQS-packed surface direction rather than a UV layout, the band textures need **no UV mapping** on the mesh. The colour, the per-vertex elevation that drives the band blend, and the steepness that drives the overlay all come from the PQS build, so the final look depends as much on your [PQSMods](/Syntax/PQSMods/) (height, colour, LandControl) as on the textures set here.

## Properties

The properties fall into five groups, which we shall take in turn:

1. **Colour grading** — how the per-vertex colour map is processed before texturing.
2. **Texture blend & distance** — the near/far strengths and the crossfade distances.
3. **Elevation bands** — the four height textures with their far-tier "multi" textures, plus the transition windows.
4. **Steep-slope overlay** — the cliff texture for steep terrain.
5. **Aerial-perspective fog & opacity** — the atmosphere haze parameters and the PQS fade scalar.

### Colour grading

| Property | Format | Description |
|----------|--------|-------------|
| `saturation` | Decimal | Saturation of the per-vertex colour map: `0` collapses it to greyscale (luma), `1` leaves it unchanged, higher oversaturates. Default 1. |
| `contrast` | Decimal | Contrast multiplier applied to the graded colour. Default 1. |
| `tintColor` | Color | A tint mixed into the base colour; the **alpha** is the mix/unsaturation factor (0 = no tint). Labelled "Colour Unsaturation (A = Factor)" in the shader. Default (1, 1, 1, 0) — i.e. no tint. |

### Texture blend & distance

| Property | Format | Description |
|----------|--------|-------------|
| `texPower` | Decimal | Strength of the fine **near** texture tier ("Near Blend"). Drops the textures out toward the flat colour map as it approaches 0. Default 0.5. |
| `multiPower` | Decimal | Strength of the coarse **far** ("multi") texture tier ("Far Blend"). Default 0.5. |
| `texTiling` | Decimal | Triplanar tiling shared by the **near** sample of all four band textures ("Near Tiling"). Default 1000. |
| `groundTexStart` | Decimal | View distance at which the near→far tiling crossfade begins. Default 2000. |
| `groundTexEnd` | Decimal | View distance at which only the far tiling remains. (The shader labels both edges "NearFar Start"; this is the **end** edge.) Default 10000. |

### Elevation bands

Each band has an albedo texture and a matching far-tier "multi" texture with its own tiling factor. The band weights come from the vertex elevation and the smoothstep windows at the end of the table.

| Property | Format | Description |
|----------|--------|-------------|
| `deepTex` | File Path | Albedo for the **deep** (lowest) elevation band. Triplanar. Default "white". |
| `deepTexScale` / `deepTexOffset` | Vector2 | Tiling and offset of `deepTex`. |
| `deepMultiTex` | File Path | Far-tier (low-frequency) texture for the deep band. Default "white". |
| `deepMultiTexScale` / `deepMultiTexOffset` | Vector2 | Tiling and offset of `deepMultiTex`. |
| `deepMultiFactor` | Decimal | Triplanar tiling of the deep band's far-tier texture. Default 1. |
| `mainTex` | File Path | Albedo for the **main** (mid-low) elevation band. Default "white". |
| `mainTexScale` / `mainTexOffset` | Vector2 | Tiling and offset of `mainTex`. |
| `mainMultiTex` | File Path | Far-tier texture for the main band. Default "white". |
| `mainMultiTexScale` / `mainMultiTexOffset` | Vector2 | Tiling and offset of `mainMultiTex`. |
| `mainMultiFactor` | Decimal | Triplanar tiling of the main band's far-tier texture. Default 1. |
| `highTex` | File Path | Albedo for the **high** (mid-high) elevation band. Default "white". |
| `highTexScale` / `highTexOffset` | Vector2 | Tiling and offset of `highTex`. |
| `highMultiTex` | File Path | Far-tier texture for the high band. Default "white". |
| `highMultiTexScale` / `highMultiTexOffset` | Vector2 | Tiling and offset of `highMultiTex`. |
| `highMultiFactor` | Decimal | Triplanar tiling of the high band's far-tier texture. Default 1. |
| `snowTex` | File Path | Albedo for the **snow** (highest) elevation band. Default "white". |
| `snowTexScale` / `snowTexOffset` | Vector2 | Tiling and offset of `snowTex`. |
| `snowMultiTex` | File Path | Far-tier texture for the snow band. Default "white". |
| `snowMultiTexScale` / `snowMultiTexOffset` | Vector2 | Tiling and offset of `snowMultiTex`. |
| `snowMultiFactor` | Decimal | Triplanar tiling of the snow band's far-tier texture. Default 1. |
| `deepStart` | Decimal | Lower edge of the deep-band weight smoothstep (on normalised elevation). Default 0. |
| `deepEnd` | Decimal | Upper edge of the deep-band smoothstep; above this the deep band fades out. Default 0.3. |
| `mainLoStart` | Decimal | Lower edge of the main band's lower transition. Default 0. |
| `mainLoEnd` | Decimal | Upper edge of the main band's lower transition. Default 0.5. |
| `mainHiStart` | Decimal | Lower edge of the main band's upper transition. Default 0.3. |
| `mainHiEnd` | Decimal | Upper edge of the main band's upper transition. Default 0.5. |
| `hiLoStart` | Decimal | Lower edge of the high band's lower transition. Default 0.6. |
| `hiLoEnd` | Decimal | Upper edge of the high band's lower transition. Default 0.6. |
| `hiHiStart` | Decimal | Lower edge of the high band's upper transition. Default 0.6. |
| `hiHiEnd` | Decimal | Upper edge of the high band's upper transition. Default 0.9. |
| `snowStart` | Decimal | Lower edge of the snow-band weight smoothstep. Default 0.9. |
| `snowEnd` | Decimal | Upper edge of the snow-band smoothstep (full snow). Default 1. |

### Steep-slope overlay

| Property | Format | Description |
|----------|--------|-------------|
| `steepTex` | File Path | Albedo blended over steep slopes (cliffs). Triplanar; no normal map. Default "white". |
| `steepTexScale` / `steepTexOffset` | Vector2 | Tiling and offset of `steepTex`. |
| `steepTiling` | Decimal | Triplanar tiling of the steep overlay ("Steep Tiling"). Default 1. |
| `steepPower` | Decimal | Multiplier on the per-vertex steepness before it is clamped to the overlay weight. Higher values push the steep texture onto gentler slopes; 0 disables it. Default 1. |
| `steepTexStart` | Decimal | View distance at which the steep overlay's distance fade begins. Default 20000. |
| `steepTexEnd` | Decimal | View distance at which the steep overlay is fully faded out. Default 30000. |

### Aerial-perspective fog & opacity

| Property | Format | Description |
|----------|--------|-------------|
| `fogColor` | Color | Editor-facing aerial-perspective fog colour ("AP Fog Color"). Read by KSP's atmosphere driver, **not** by the GPU directly — the fragment samples `fogColorRamp` instead. Default (0, 0, 1, 1). |
| `heightFallOff` | Decimal | Atmospheric height falloff ("AP Height Fall Off"). Combined with `atmosphereDepth` in C# each frame to produce the runtime density uniform the GPU uses. Default 1. |
| `globalDensity` | Decimal | Aerial-perspective density multiplier ("AP Global Density"), consumed directly by the GPU. Higher values thicken the distance fog. Default 1. |
| `atmosphereDepth` | Decimal | Atmosphere thickness ("AP Atmosphere Depth"). Combined with `heightFallOff` in C# to produce the runtime density uniform. Default 1. |
| `fogColorRamp` | File Path | 1-D colour ramp for the fog. The haze colour is read from it indexed by the **sun angle**, so the fog can be tinted differently toward and away from the sun. Default "white". |
| `fogColorRampScale` / `fogColorRampOffset` | Vector2 | Tiling and offset of the fog ramp. |
| `FogColorRamp` | Gradient | The `fogColorRamp`, defined inline as a gradient instead of as a texture. Same underlying property as `fogColorRamp`. |
| `planetOpacity` | Decimal | Opacity of the PQS material, used by KSP to fade the terrain against the scaled-space sphere across the `fadeStart`/`fadeEnd` transition. Normally left at 1. Default 1. |

## Notes

* This is the body's **near-surface** terrain material. The body simultaneously has a [ScaledVersion material](/Syntax/Material/Scaled/Vacuum) for the distant sphere; KSP cross-fades between the two across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes, so the two should be tuned to match where they meet.
* This is the **aerial-perspective ("AP")** variant, for bodies **with** an atmosphere. The vacuum (non-AP) Sphere Projection shader builds the identical projected surface without the fog stage and is the one to use on airless bodies. The two share every band/steep/colour property; only the fog group (`fogColor`, `heightFallOff`, `globalDensity`, `atmosphereDepth`, `fogColorRamp`) is extra here.
* `materialType = Basic` is also accepted under its older alias `AtmosphericBasic`, which you may see in existing configs; the two select the same shader.
* The textures are **triplanar**, sampled from the PQS-packed object-space surface direction (the radial direction on the quad-sphere). They therefore need **no UV mapping** on the mesh. This shader carries **no normal maps** for any band or for the steep overlay — only albedo.
* The band colour, the per-vertex elevation that drives the four-band blend, and the slope that drives the steep overlay all come from the PQS build — your [height](/Syntax/PQSMods/VertexHeightMap), [colour](/Syntax/PQSMods/VertexColorMap) and [LandControl](/Syntax/PQSMods/LandControl/LandControl) mods shape the input this shader paints.
* Several inputs are **runtime-driven and are not config properties**: the sun direction used as the fog-ramp lookup, and the per-frame atmospheric density at the viewer (derived in C# from `heightFallOff` and `atmosphereDepth`). The user-facing `fogColor` is likewise read only by the C# atmosphere driver, which feeds the GPU-sampled `fogColorRamp`. If those runtime inputs read zero, the fog stage produces no visible haze.
* The surface is lit with a diffuse (Lambert) term plus spherical-harmonic ambient only — there is **no specular response** on this shader, and the deferred pass writes zero specular and smoothness.
* `planetOpacity` feeds the forward and legacy-deferred passes, which blend with `OneMinusSrcAlpha SrcAlpha`. In that blend the alpha is **inverted**: alpha = 0 is fully opaque and alpha = 1 is fully transparent. KSP drives this during the scaled/PQS fade, so a static config value is normally left at the default of 1.
* If the shader fails to compile on the target platform it falls back to Unity's `Diffuse` shader.
