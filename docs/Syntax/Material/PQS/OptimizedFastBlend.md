# PQS Main – Optimised With Fast Blend

**Shader:** `Terrain/PQS/PQS Main - Optimised With Fast Blend` &nbsp;·&nbsp; **`materialType = OptimizedFastBlend`**

The PQS `Material { }` drives the shader that paints a body's **near-surface terrain** — the real, subdivided quad-sphere you see from low orbit down to the ground. This is the counterpart to the [ScaledVersion materials](/Syntax/Material/Scaled/Vacuum), which paint only the distant low-LOD sphere; the PQS material takes over as the camera descends and the two are cross-faded across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes. `Terrain/PQS/PQS Main - Optimised With Fast Blend` is the lightest member of the "PQS Main" family.

Like the rest of the family it builds the surface procedurally and **world-space triplanar** — projecting tiling textures down the three world axes and blending them by the surface normal, so there are no UV seams. It blends three textures by **elevation**, layers a separate texture on **steep slopes**, crossfades between a fine and a coarse tiling by **camera distance**, and (on bodies with an atmosphere) folds in aerial-perspective haze and underwater fog.

This variant combines two economies. Like [Optimized](/Syntax/Material/PQS/Optimized), **only the mid elevation band carries a normal map** (the low and high bands are flat-shaded), giving a lighter texture budget. And like [MainFastBlend](/Syntax/Material/PQS/MainFastBlend) it uses the 1.8 "Fast Blend" path and drops the inline `Gradient` option for the fog ramp. Stock Kerbin falls back to this kind of build at the lowest terrain-detail settings. For per-band normal maps with the same fast blend, see [MainFastBlend](/Syntax/Material/PQS/MainFastBlend); for the pre-1.8 equivalent of this cut-down build, see [Optimized](/Syntax/Material/PQS/Optimized).

We shall first see how to enable the material, then how the shader assembles the surface, and finally discuss each group of properties in turn.

## Enabling

Select the material by setting `materialType = OptimizedFastBlend` in the [`PQS { }`](/Syntax/PQS) node. The `Material { }` node then accepts the properties listed below.

```cfg
PQS
{
    materialType = OptimizedFastBlend
    Material
    {
        // Colour grading of the per-vertex colour map
        saturation = 1
        contrast   = 1
        tintColor  = 1, 1, 1, 0

        // How strongly the textures show, and the near/far crossfade distance
        powerNear      = 0.5
        powerFar       = 0.5
        groundTexStart = 2000
        groundTexEnd   = 10000

        // Elevation bands (only the mid band takes a normal map)
        lowTex          = MyMod/PluginData/sand_color.dds
        lowNearTiling   = 1000
        lowMultiFactor  = 10

        midTex          = MyMod/PluginData/rock_color.dds
        midBumpMap      = MyMod/PluginData/rock_normal.dds
        midNearTiling   = 1000
        midMultiFactor  = 10

        highTex         = MyMod/PluginData/snow_color.dds
        highNearTiling  = 1000
        highMultiFactor = 10

        // Where each band fades to the next (on normalised vertex elevation)
        lowStart  = 0
        lowEnd    = 0.3
        highStart = 0.8
        highEnd   = 1

        // Steep-slope overlay
        steepTex      = MyMod/PluginData/cliff_color.dds
        steepBumpMap  = MyMod/PluginData/cliff_normal.dds
        steepPower    = 1

        // Atmospheric haze and underwater fog (only on bodies with an atmosphere)
        globalDensity    = 1
        fogColorRamp     = MyMod/PluginData/fog_ramp.dds
        oceanFogDistance = 1000
    }
}
```

## How the surface is built

The shader is a triplanar terrain surface assembled per pixel. There is no single colour map; instead a base colour, three elevation textures, a steep overlay, and fog are combined in the following order:

1. **Per-vertex colour grading** — the PQS supplies a colour per vertex (`COLOR.rgb`, usually from a colour map or [LandControl](/Syntax/PQSMods/LandControl/LandControl)). The shader desaturates that colour toward its luma by `saturation`, mixes in `tintColor` (whose alpha is the mix amount), and scales it by `contrast`. The result is the **base colour** that the band textures then tint.
2. **Three elevation bands** — `lowTex`, `midTex` and `highTex` are blended by the vertex's normalised elevation (written into `COLOR.w` by the PQS height build). `lowStart`/`lowEnd` set the elevation window where the low band fades up into the mid band, and `highStart`/`highEnd` the window where the mid band fades into the high band — so low covers the bottom of the height range, mid the middle, high the top.
3. **Near/far tiling crossfade** — each band is sampled at two tiling scales and crossfaded by camera distance: a fine **near** tiling (`*NearTiling`) for close-up detail and a coarse **far** tiling (`*MultiFactor`) for the distance, blended between the `groundTexStart` and `groundTexEnd` view distances. `powerNear` and `powerFar` set how strongly the near and far textures show. Because the triplanar coordinates are world position scaled by 10⁻⁵, the tiling numbers are large (the defaults are 1000 near, 10 far).
4. **Texture vs. flat colour** — the blended band texture does not replace the base colour, it *modulates* it: the final albedo is `lerp(baseColour, baseColour × bandTexture, texMix)`, where `texMix` is driven by `powerNear`/`powerFar` and the near/far mix. Where it falls to zero — far away, or with both powers at 0 — you see the flat graded colour map; where it is high you see fully textured terrain.
5. **Steep-slope overlay** — `steepTex` and `steepBumpMap` are blended over the result on steep terrain. The per-vertex steepness is multiplied by `steepPower` and clamped, so higher `steepPower` makes the cliff texture reach onto gentler slopes. The overlay has its own distance fade between `steepTexStart` and `steepTexEnd`, and its own `steepNearTiling` / `steepTiling`.
6. **Normal mapping** — as in the [Optimized](/Syntax/Material/PQS/Optimized) shader, **only the mid band** (`midBumpMap`) and the steep overlay (`steepBumpMap`) carry normal maps; the low and high bands are flat-shaded by the geometric mesh normal. The blended normal is transformed into world space through the mesh's tangent frame, and the surface is lit with a plain diffuse (Lambert) term plus spherical-harmonic ambient — there is **no specular highlight**.
7. **Aerial-perspective & ocean fog** — on a body with an atmosphere, a distance haze is mixed in last. Its colour is read from `fogColorRamp` indexed by the sun angle, and its strength grows with distance and `globalDensity`. A second, underwater **ocean fog** term reads the same ramp at a different row and fades over the `oceanFogDistance`. On an airless body the whole fog stage is compiled out and these properties do nothing.

The triplanar projection means the band textures need **no UV mapping** on the mesh, but the normal mapping still needs mesh **tangents**, which the PQS quad-sphere provides. Because the colour, elevation and slope all come from the PQS build, the final look depends as much on your [PQSMods](/Syntax/PQSMods/) (height, colour, LandControl) as on the textures set here.

## Properties

The properties fall into five groups, which we shall take in turn:

1. **Colour grading** — how the per-vertex colour map is processed before texturing.
2. **Texture blend & distance** — the near/far strengths and the crossfade distances.
3. **Elevation bands** — the three height textures (only the mid band normal-mapped) and their transition windows.
4. **Steep-slope overlay** — the cliff texture for steep terrain.
5. **Fog & opacity** — the atmospheric haze ramp, ocean-fog distance and the PQS fade scalar.

### Colour grading

| Property | Format | Description |
|----------|--------|-------------|
| `saturation` | Decimal | Saturation of the per-vertex colour map: `0` collapses it to greyscale (luma), `1` leaves it unchanged, higher oversaturates. Default 1. |
| `contrast` | Decimal | Contrast multiplier applied to the graded colour. Default 1. |
| `tintColor` | Color | A tint mixed into the base colour; the **alpha** is the mix amount (0 = no tint). Default (1, 1, 1, 0) — i.e. no tint. |

### Texture blend & distance

| Property | Format | Description |
|----------|--------|-------------|
| `powerNear` | Decimal | Strength of the fine **near** texture tier. Drops the textures out toward the flat colour map as it approaches 0. Default 0.5. |
| `powerFar` | Decimal | Strength of the coarse **far** texture tier. Default 0.5. |
| `groundTexStart` | Decimal | View distance (m) at which the near→far tiling crossfade begins. Default 2000. |
| `groundTexEnd` | Decimal | View distance (m) at which only the far tiling remains. Default 10000. |

### Elevation bands

Each band has an albedo texture with near/far tiling; only the **mid** band additionally carries a normal map (with a single tiling). The band weights come from the vertex elevation and the four transition values at the end of the table.

| Property | Format | Description |
|----------|--------|-------------|
| `lowTex` | File Path | Albedo for the **low** elevation band. Triplanar, world-tiled. Default "white". |
| `lowTexScale` / `lowTexOffset` | Vector2 | Tiling and offset of `lowTex`. |
| `lowNearTiling` | Decimal | World tiling of the low albedo at the near tier. Default 1000. |
| `lowMultiFactor` | Decimal | World tiling of the low albedo at the far tier. Default 10. |
| `midTex` | File Path | Albedo for the **mid** elevation band. Default "white". |
| `midTexScale` / `midTexOffset` | Vector2 | Tiling and offset of `midTex`. |
| `midBumpMap` | File Path | DXT5nm normal map for the mid band — the only band that has one. Default "bump" (flat). |
| `midBumpMapScale` / `midBumpMapOffset` | Vector2 | Tiling and offset of `midBumpMap`. |
| `midNearTiling` | Decimal | World tiling of the mid albedo at the near tier. Default 1000. |
| `midMultiFactor` | Decimal | World tiling of the mid albedo at the far tier. Default 10. |
| `midBumpNearTiling` | Decimal | World tiling of the mid normal map. Default 1. |
| `highTex` | File Path | Albedo for the **high** elevation band. Default "white". |
| `highTexScale` / `highTexOffset` | Vector2 | Tiling and offset of `highTex`. |
| `highNearTiling` | Decimal | World tiling of the high albedo at the near tier. Default 1000. |
| `highMultiFactor` | Decimal | World tiling of the high albedo at the far tier. Default 10. |
| `lowStart` | Decimal | Elevation (normalised 0–1) at which the low band begins fading into the mid band. Default 0. |
| `lowEnd` | Decimal | Elevation at which the low band has fully given way to the mid band. Default 0.3. |
| `highStart` | Decimal | Elevation at which the mid band begins fading into the high band. Default 0.8. |
| `highEnd` | Decimal | Elevation at which the high band fully takes over. Default 1. |

### Steep-slope overlay

| Property | Format | Description |
|----------|--------|-------------|
| `steepTex` | File Path | Albedo blended over steep slopes (cliffs). Default "white". |
| `steepTexScale` / `steepTexOffset` | Vector2 | Tiling and offset of `steepTex`. |
| `steepBumpMap` | File Path | DXT5nm normal map for the steep overlay. Default "bump". |
| `steepBumpMapScale` / `steepBumpMapOffset` | Vector2 | Tiling and offset of `steepBumpMap`. |
| `steepPower` | Decimal | Multiplier on the vertex steepness before it is clamped to the overlay weight. Higher values push the steep texture onto gentler slopes; 0 disables it. Default 1. |
| `steepTexStart` | Decimal | View distance (m) at which the steep overlay's near→far tiling fade begins. Default 20000. |
| `steepTexEnd` | Decimal | View distance (m) at which the steep overlay reaches its far tiling. Default 30000. |
| `steepNearTiling` | Decimal | World tiling of the steep overlay at the near tier. Default 1. |
| `steepTiling` | Decimal | World tiling of the steep overlay at the far tier. Default 1. |

### Fog & opacity

| Property | Format | Description |
|----------|--------|-------------|
| `globalDensity` | Decimal | Density multiplier for the aerial-perspective haze. Only has an effect on bodies with an atmosphere; higher values thicken the distance fog. Default 1. |
| `fogColorRamp` | File Path | 1-D colour ramp for the fog. The aerial haze is read from it indexed by the sun angle (so the fog can be tinted differently toward and away from the sun); a second row supplies the underwater ocean-fog colour. Default "white". |
| `fogColorRampScale` / `fogColorRampOffset` | Vector2 | Tiling and offset of the fog ramp. |
| `oceanFogDistance` | Decimal | Falloff distance (m) of the underwater ocean fog — the distance over which the surface fades into the ocean-fog colour when seen from below the waterline. Default 1000. |
| `planetOpacity` | Decimal | Opacity of the PQS material, used by KSP to fade the terrain against the scaled-space sphere across the `fadeStart`/`fadeEnd` transition. Normally left at 1. Default 1. |

## Notes

* These are the body's **near-surface** terrain materials. The body simultaneously has a [ScaledVersion material](/Syntax/Material/Scaled/Vacuum) for the distant sphere; KSP cross-fades between the two across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes, so the two should be tuned to match where they meet.
* This is the leanest of the family: only the mid band is normal-mapped, and (being a "Fast Blend" build) it does **not** offer the inline `Gradient` form of the fog ramp. If the low or high bands need surface relief, use [MainFastBlend](/Syntax/Material/PQS/MainFastBlend).
* The textures are **world-space triplanar**, so they need no UVs and the tiling values are in world units scaled by 10⁻⁵ (hence the large defaults). The shader still requires mesh **tangents** for its normal mapping, which the PQS quad-sphere provides.
* The band colour, the per-vertex elevation that drives the band blend, and the slope that drives the steep overlay all come from the PQS build — your [height](/Syntax/PQSMods/VertexHeightMap), [colour](/Syntax/PQSMods/VertexColorMap) and [LandControl](/Syntax/PQSMods/LandControl/LandControl) mods shape the input this shader paints.
* The fog stage (`globalDensity`, `fogColorRamp`, `oceanFogDistance`) is only compiled in for bodies with an atmosphere; on an airless body it is removed entirely and those properties have no effect.
* Several shader inputs are driven by KSP at runtime and are not config properties: a floating-origin offset that keeps the world-space triplanar coordinates stable as the game shifts the origin around the craft, and the sun direction, camera altitude and viewer air density used by the fog.
* The surface is lit with a diffuse (Lambert) term only — there is no specular response on this shader.
* `materialType = OptimizedFastBlend` is also accepted under its older alias `AtmosphericOptimizedFastBlend`, which you may see in existing configs; the two select the same shader.
