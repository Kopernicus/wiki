# PQS Triplanar Zoom Rotation

**Shader:** `Terrain/PQS/PQS Triplanar Zoom Rotation` &nbsp;·&nbsp; **`materialType = Triplanar`**

The PQS `Material { }` drives the shader that paints a body's **near-surface terrain** — the real, subdivided quad-sphere you see from low orbit down to the ground. This is the counterpart to the [ScaledVersion materials](/Syntax/Material/Scaled/Vacuum), which paint only the distant low-LOD sphere; the PQS material takes over as the camera descends and the two are cross-faded across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes. `Terrain/PQS/PQS Triplanar Zoom Rotation` is the 1.8-era zoom-rotation terrain shader, and the **plainest** of the zoom-rotation surfaces.

Like the rest of the PQS terrain shaders it builds the surface procedurally and **world-space triplanar** — projecting tiling textures down the three world axes and blending them by the surface normal, so there are no UV seams — and it blends three textures by **elevation** with a separate texture on **steep slopes**. Instead of crossfading a fixed near/far tiling pair by distance, it runs a continuous **zoom-rotation cascade**: as the camera pulls away the triplanar coordinates are repeatedly scaled down by `factor` and rotated by `factorRotation`, with adjacent zoom levels blended over a `factorBlendWidth` band. The rotation scrambles the regular tile pattern that would otherwise become obvious as you zoom out, so one set of textures stays convincing from the ground all the way to orbit. The surface is lit with a physically-based specular model (`specularColor` as the F0 reflectance, plus reflection probes) and scaled by an `albedoBrightness` multiplier.

It shares that zoom-rotation tiling idea with the [PQS Main – Triplanar Zoom Rotation](/Syntax/Material/PQS/MainTriplanarZoomRotation) variant, but it is the **plainer** surface: it does **not** carry the full "PQS Main" band normal-map set. Where the PQS-Main variant gives every elevation band its own normal map, this shader exposes a **single band normal map** — `midBumpMap` — which the shader applies to all three bands (the low, mid and high band weights sum to one and all multiply that one mid sample). The steep overlay also works differently here: it has a near/far tiling pair (`steepNearTiling` / `steepTiling`) that fades by distance between `steepTexStart` and `steepTexEnd`, and it is projected in a sun-aligned local direction frame rather than through the zoom cascade. The colour-grading, PBR-lighting and fog stages are otherwise the same.

For the richer PBR zoom-rotation sibling see [MainTriplanarZoomRotation](/Syntax/Material/PQS/MainTriplanarZoomRotation), and for the texture-array zoom-rotation sibling see [TriplanarAtlas](/Syntax/Material/PQS/TriplanarAtlas); for the distance-crossfade members of the family see [Main](/Syntax/Material/PQS/Main), [Optimized](/Syntax/Material/PQS/Optimized), [Extra](/Syntax/Material/PQS/Extra), [MainFastBlend](/Syntax/Material/PQS/MainFastBlend) and [OptimizedFastBlend](/Syntax/Material/PQS/OptimizedFastBlend).

We shall first see how to enable the material, then how the shader assembles the surface, and finally discuss each group of properties in turn.

## Enabling

Select the material by setting `materialType = Triplanar` in the [`PQS { }`](/Syntax/PQS) node. The `Material { }` node then accepts the properties listed below.

```
PQS
{
    materialType = Triplanar
    Material
    {
        // Zoom-rotation tiling cascade
        factor           = 10
        factorBlendWidth = 0.1
        factorRotation   = 30

        // Colour grading and PBR lighting
        saturation       = 1
        contrast         = 1
        tintColor        = 1, 1, 1, 0
        specularColor    = 0.2, 0.2, 0.2, 0.2
        albedoBrightness = 2

        // Elevation bands (single tiling each; only the mid band has a normal map)
        lowTex      = MyMod/PluginData/sand_color.dds
        lowTiling   = 100000

        midTex        = MyMod/PluginData/rock_color.dds
        midTiling     = 100000
        midBumpMap    = MyMod/PluginData/rock_normal.dds
        midBumpTiling = 100000

        highTex     = MyMod/PluginData/snow_color.dds
        highTiling  = 100000

        // Where each band fades to the next (on normalised vertex elevation)
        lowStart  = 0
        lowEnd    = 0.3
        highStart = 0.8
        highEnd   = 1

        // Steep-slope overlay (near/far tiling fade by distance)
        steepTex        = MyMod/PluginData/cliff_color.dds
        steepBumpMap    = MyMod/PluginData/cliff_normal.dds
        steepPower      = 1
        steepNearTiling = 1
        steepTiling     = 1
        steepTexStart   = 20000
        steepTexEnd     = 30000

        // Atmospheric haze and underwater fog (only on bodies with an atmosphere)
        globalDensity    = 1
        fogColorRamp     = MyMod/PluginData/fog_ramp.dds
        oceanFogDistance = 1000
    }
}
```

## How the surface is built

The shader is a triplanar terrain surface assembled per pixel. There is no single colour map; instead a base colour, three elevation textures, a steep overlay, PBR lighting and fog are combined in the following order:

1. **Per-vertex colour grading** — the PQS supplies a colour per vertex (`COLOR.rgb`, usually from a colour map or [LandControl](/Syntax/PQSMods/LandControl/LandControl)). The shader desaturates that colour toward its luma by `saturation`, mixes in `tintColor` (whose alpha is the mix amount), and scales it by `contrast`. The result is the **base colour** that the band textures then tint.
2. **Three elevation bands** — `lowTex`, `midTex` and `highTex` are blended by the vertex's normalised elevation (written into `COLOR.w` by the PQS height build). `lowStart`/`lowEnd` set the elevation window where the low band fades up into the mid band, and `highStart`/`highEnd` the window where the mid band fades into the high band — so low covers the bottom of the height range, mid the middle, high the top. The blended band texture multiplies the graded base colour.
3. **Zoom-rotation tiling** — this is the shader's signature. The view distance is bracketed between consecutive powers of `factor` to pick a zoom level; the band textures are sampled at that level's tiling and at the next level's, and the two are blended across a transition band whose width is `factorBlendWidth`. Each successive zoom level also rotates the triplanar coordinates by an extra `factorRotation` degrees, which scrambles the tile pattern so it does not read as obvious repetition as the camera pulls back. Because the triplanar coordinates are world position scaled by 10⁻⁵, the per-band tiling numbers are large (the default is 100000).
4. **Steep-slope overlay** — `steepTex` and `steepBumpMap` are blended over the result on steep terrain. The per-vertex steepness is multiplied by `steepPower` and clamped, so higher `steepPower` makes the cliff texture reach onto gentler slopes. Unlike the band textures, the steep overlay does **not** go through the zoom cascade: it is triplanar in a sun-aligned **local direction** frame (built from the mesh's UVs), and it has two tiling tiers — `steepNearTiling` close to the camera and `steepTiling` far away — crossfaded by view distance between `steepTexStart` and `steepTexEnd`.
5. **Normal mapping** — the band relief uses a **single** normal map, `midBumpMap`. The shader samples it once (through the zoom cascade) and applies it to the low, mid and high bands together — there are no separate low/high normal maps. The steep overlay carries its own `steepBumpMap`, sampled in the same local-direction frame as `steepTex`. All normals are accumulated triplanar (per-face whiteout reprojection) and transformed into world space through the mesh's tangent frame; because the band coordinates rotate with the zoom cascade, the band normal rotates too, which is why changing `factorRotation` shifts how relief catches the light.
6. **PBR lighting** — the blended albedo is scaled by `albedoBrightness` and lit with a physically-based specular model: `specularColor` is the surface's specular reflectance at normal incidence (its F0) with its alpha as smoothness, used together with the engine's reflection probes for image-based reflections, on top of the direct light and spherical-harmonic ambient.
7. **Aerial-perspective & ocean fog** — on a body with an atmosphere, a distance haze is mixed in last. Its colour is read from `fogColorRamp` indexed by the sun angle, and its strength grows with distance and `globalDensity`. A second, underwater **ocean fog** term reads the same ramp at a different row and fades over the `oceanFogDistance`. On an airless body the whole fog stage is compiled out and these properties do nothing.

The triplanar projection means the band textures need **no UV mapping** on the mesh, but the normal mapping (and the steep overlay's local-direction frame) still needs the mesh's **tangents and UVs**, which the PQS quad-sphere provides. Because the colour, elevation and slope all come from the PQS build, the final look depends as much on your [PQSMods](/Syntax/PQSMods/) (height, colour, LandControl) as on the textures set here.

## Properties

The properties fall into five groups, which we shall take in turn:

1. **Zoom & rotation** — the tiling cascade that replaces the near/far crossfade.
2. **Colour & lighting** — the colour grading and the PBR specular response.
3. **Elevation bands** — the three height textures, the single (mid) normal map, their tiling and transition windows.
4. **Steep-slope overlay** — the cliff texture for steep terrain, with its near/far tiling fade.
5. **Fog & opacity** — the atmospheric haze ramp, ocean-fog distance and the PQS fade scalar.

### Zoom & rotation

| Property | Format | Description |
|----------|--------|-------------|
| `factor` | Decimal | Geometric zoom step between adjacent tiling levels — the view distance is bracketed between consecutive powers of this value. Larger values mean fewer, coarser zoom transitions across the visible range. **Do not set it to 1**, which collapses the cascade and locks up the game. Default 10. |
| `factorBlendWidth` | Decimal | Width of the smooth transition between adjacent zoom levels, as a fraction of one level's range (valid range 0–0.5). Small values (≈0.05) give crisp, near-discrete level changes; larger values blend them more softly. Default 0.1. |
| `factorRotation` | Decimal | Additional rotation, in degrees, applied to the triplanar coordinates at each successive zoom level. Breaks up visible tile repetition as the camera zooms out; because it rotates the band normal map too, it also shifts how surface relief catches the light. Default 30. |

### Colour & lighting

| Property | Format | Description |
|----------|--------|-------------|
| `saturation` | Decimal | Saturation of the per-vertex colour map: `0` collapses it to greyscale (luma), `1` leaves it unchanged, higher oversaturates. Default 1. |
| `contrast` | Decimal | Contrast multiplier applied to the graded colour. Default 1. |
| `tintColor` | Color | A tint mixed into the base colour; the **alpha** is the mix amount (0 = no tint). The shader header labels this "Colour Unsaturation (A = Factor)". Default (1, 1, 1, 0) — i.e. no tint. |
| `specularColor` | Color | Specular reflectance at normal incidence (the PBR **F0** colour), with its **alpha** as the surface smoothness; used with the scene's reflection probes for the highlight and image-based reflections. Set to black to remove specular. Default (0.2, 0.2, 0.2, 0.2). |
| `albedoBrightness` | Decimal | Multiplier applied to the blended surface colour before lighting — raises or lowers the overall terrain brightness. Default 2. |

### Elevation bands

Each band has an albedo texture with a single world tiling (the zoom cascade supplies the multi-scale detail). **Only the mid band carries a normal map** (`midBumpMap`); it is applied to all three bands, so there are no `lowBumpMap` or `highBumpMap` keys. The band weights come from the vertex elevation and the four transition values at the end of the table.

| Property | Format | Description |
|----------|--------|-------------|
| `lowTex` | File Path | Albedo for the **low** elevation band. Triplanar, world-tiled. Default "white". |
| `lowTexScale` / `lowTexOffset` | Vector2 | Tiling and offset of `lowTex`. |
| `lowTiling` | Decimal | World tiling of the low albedo. Default 100000. |
| `midTex` | File Path | Albedo for the **mid** elevation band. Default "white". |
| `midTexScale` / `midTexOffset` | Vector2 | Tiling and offset of `midTex`. |
| `midTiling` | Decimal | World tiling of the mid albedo. Default 100000. |
| `midBumpMap` | File Path | DXT5nm normal map for the bands. This is the **only** band normal map — it is applied to the low, mid and high bands alike. Default "bump" (flat). |
| `midBumpMapScale` / `midBumpMapOffset` | Vector2 | Tiling and offset of `midBumpMap`. |
| `midBumpTiling` | Decimal | World tiling of the band normal map. Default 100000. |
| `highTex` | File Path | Albedo for the **high** elevation band. Default "white". |
| `highTexScale` / `highTexOffset` | Vector2 | Tiling and offset of `highTex`. |
| `highTiling` | Decimal | World tiling of the high albedo. Default 100000. |
| `lowStart` | Decimal | Elevation (normalised 0–1) at which the low band begins fading into the mid band. Default 0. |
| `lowEnd` | Decimal | Elevation at which the low band has fully given way to the mid band. Default 0.3. |
| `highStart` | Decimal | Elevation at which the mid band begins fading into the high band. Default 0.8. |
| `highEnd` | Decimal | Elevation at which the high band fully takes over. Default 1. |

### Steep-slope overlay

The steep texture is sampled outside the zoom cascade, in a sun-aligned local direction frame, with two tiling tiers crossfaded by view distance.

| Property | Format | Description |
|----------|--------|-------------|
| `steepTex` | File Path | Albedo blended over steep slopes (cliffs). Default "white". |
| `steepTexScale` / `steepTexOffset` | Vector2 | Tiling and offset of `steepTex`. |
| `steepBumpMap` | File Path | DXT5nm normal map for the steep overlay, sampled in the same local-direction frame as `steepTex`. Default "bump". |
| `steepBumpMapScale` / `steepBumpMapOffset` | Vector2 | Tiling and offset of `steepBumpMap`. |
| `steepNearTiling` | Decimal | World tiling of the steep overlay close to the camera (used below `steepTexStart`). Default 1. |
| `steepTiling` | Decimal | World tiling of the steep overlay far from the camera (used above `steepTexEnd`). Default 1. |
| `steepTexStart` | Decimal | View distance (m) at which the steep overlay's near→far tiling fade begins. Default 20000. |
| `steepTexEnd` | Decimal | View distance (m) at which the steep overlay reaches its far tiling. Default 30000. |
| `steepPower` | Decimal | Multiplier on the vertex steepness before it is clamped to the overlay weight. Higher values push the steep texture onto gentler slopes; 0 disables it. Default 1. |

### Fog & opacity

| Property | Format | Description |
|----------|--------|-------------|
| `globalDensity` | Decimal | Density multiplier for the aerial-perspective haze. Only has an effect on bodies with an atmosphere; higher values thicken the distance fog. Default 1. |
| `fogColorRamp` | File Path | 1-D colour ramp for the fog. The aerial haze is read from it indexed by the sun angle (so the fog can be tinted differently toward and away from the sun); a second row supplies the underwater ocean-fog colour. Default "white". |
| `fogColorRampScale` / `fogColorRampOffset` | Vector2 | Tiling and offset of the fog ramp. |
| `oceanFogDistance` | Decimal | Falloff distance (m) of the underwater ocean fog — the distance over which the surface fades into the ocean-fog colour when seen from below the waterline (see [Ocean](/Syntax/Ocean)). Default 1000. |
| `planetOpacity` | Decimal | Opacity of the PQS material, used by KSP to fade the terrain against the scaled-space sphere across the `fadeStart`/`fadeEnd` transition. Normally left at 1. Default 1. |

## Notes

* These are the body's **near-surface** terrain materials. The body simultaneously has a [ScaledVersion material](/Syntax/Material/Scaled/Vacuum) for the distant sphere; KSP cross-fades between the two across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes, so the two should be tuned to match where they meet.
* This shader has only **one band normal map**, `midBumpMap`, shared by all three elevation bands — there are no `lowBumpMap`/`highBumpMap` keys, and consequently no separate `lowBumpTiling`/`highBumpTiling`. If you want per-band normal maps with this kind of tiling cascade, use [MainTriplanarZoomRotation](/Syntax/Material/PQS/MainTriplanarZoomRotation) instead. A practical consequence is that giving the whole surface a normal map often means keeping the terrain mostly on the **mid** band (e.g. by pushing `lowEnd` low and `highStart` high), since the low and high bands reuse the mid normal regardless.
* Detail at distance comes from the **zoom-rotation cascade** (`factor`, `factorBlendWidth`, `factorRotation`), not a near/far tiling crossfade. Each band therefore has a **single** tiling (`lowTiling`, `midTiling`, `highTiling`, `midBumpTiling`); there is no `powerNear`/`powerFar`, `groundTexStart`/`groundTexEnd`, or per-band near/far tiling pair. The exception is the steep overlay, which keeps a near/far tiling pair of its own (`steepNearTiling`/`steepTiling`) faded between `steepTexStart` and `steepTexEnd`.
* The band textures are **world-space triplanar**, so they need no UVs and the tiling values are in world units scaled by 10⁻⁵ (hence the large defaults). The **steep overlay** is instead projected in a sun-aligned local-direction frame derived from the mesh's UVs, and the normal mapping needs mesh **tangents** — both of which the PQS quad-sphere provides.
* The band colour, the per-vertex elevation that drives the band blend, and the slope that drives the steep overlay all come from the PQS build — your [height](/Syntax/PQSMods/VertexHeightMap), [colour](/Syntax/PQSMods/VertexColorMap) and [LandControl](/Syntax/PQSMods/LandControl/LandControl) mods shape the input this shader paints.
* The fog stage (`globalDensity`, `fogColorRamp`, `oceanFogDistance`) is only compiled in for bodies with an atmosphere; on an airless body it is removed entirely and those properties have no effect.
* `planetOpacity` (the shader's `_PlanetOpacity`) is normally **driven by KSP at runtime** to fade the planet across the scaled-space transition, under a `OneMinusSrcAlpha SrcAlpha` blend. It is hidden in the material inspector and there is rarely any reason to set it by hand; leave it at 1.
* Several other shader inputs are driven by KSP at runtime and are **not** config properties: a floating-origin offset that keeps the world-space triplanar coordinates stable as the game shifts the origin around the craft, the sun direction (used for the steep frame and the fog ramp), and the camera altitude and viewer air density used by the fog.
* The shader falls back to Unity's built-in `Diffuse` shader on hardware that cannot run it.
* `materialType = Triplanar` is also accepted under its older alias `AtmosphericTriplanarZoomRotation`, which you may see in existing configs; the two select the same shader.
