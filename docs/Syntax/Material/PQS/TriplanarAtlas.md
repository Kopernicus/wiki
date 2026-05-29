# PQS Triplanar Zoom Rotation — Texture Array

**Shader:** `Terrain/PQS/PQS Triplanar Zoom Rotation Texture Array` &nbsp;·&nbsp; **`materialType = TriplanarAtlas`**

The PQS `Material { }` drives the shader that paints a body's **near-surface terrain** — the real, subdivided quad-sphere you see from low orbit down to the ground. This is the counterpart to the [ScaledVersion materials](/Syntax/Material/Scaled/Vacuum), which paint only the distant low-LOD sphere; the PQS material takes over as the camera descends and the two are cross-faded across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes. `Terrain/PQS/PQS Triplanar Zoom Rotation Texture Array` is the **texture-array ("atlas") variant** of the zoom-rotation triplanar shader — the KSP 1.9 shader that stock Kerbin uses at the **Ultra** terrain-detail setting.

Like the rest of the family it builds the surface procedurally and **world-space triplanar** — projecting tiling textures down the three world axes and blending them by the surface normal, so there are no UV seams — and it carries the same physically-based (PBR specular) lighting and zoom-rotation tiling cascade as [`MainTriplanarZoomRotation`](/Syntax/Material/PQS/MainTriplanarZoomRotation). What sets it apart is how the ground textures are stored and chosen:

* **All ground detail comes from a Texture2DArray pair, not separate elevation bands.** Instead of distinct `lowTex` / `midTex` / `highTex` band textures, every surface type is packed as a slice into a single albedo `Texture2DArray` (`AtlasTex`) and a matching normal `Texture2DArray` (`NormalTex`). The shader samples **four** slices per pixel and blends them by weights baked into the mesh — so there are no `low*`/`mid*`/`high*` properties and no `lowStart`/`highEnd`-style elevation windows here at all.
* **Slice selection is driven by the PQS build, not by config.** The four slice indices and their blend weights are packed into a secondary mesh UV channel (the TEXCOORD2 set) by KSP's PQS system (typically from [LandControl](/Syntax/PQSMods/LandControl/LandControl)) and decoded per vertex. You supply the texture array and a master tiling; *which* slices appear where is decided by the terrain, not by anything in `Material { }`.

For the band-based PBR sibling that uses the very same zoom-rotation cascade and lighting, see [`MainTriplanarZoomRotation`](/Syntax/Material/PQS/MainTriplanarZoomRotation). A related non-"PQS Main" zoom-rotation shader is documented at [`AtmosphericTriplanarZoomRotation`](/Syntax/Material/AtmosphericTriplanarZoomRotation). For the distance-crossfade band siblings, see [Main](/Syntax/Material/PQS/Main), [Optimized](/Syntax/Material/PQS/Optimized), [Extra](/Syntax/Material/PQS/Extra), [MainFastBlend](/Syntax/Material/PQS/MainFastBlend) and [OptimizedFastBlend](/Syntax/Material/PQS/OptimizedFastBlend).

We shall first see how to enable the material, then how the shader assembles the surface, and finally discuss each group of properties in turn.

## Enabling

Select the material by setting `materialType = TriplanarAtlas` in the [`PQS { }`](/Syntax/PQS) node. The `Material { }` node then accepts the properties listed below. The albedo and normal arrays are built from `AtlasTex { }` and `NormalTex { }` collection nodes — each line inside is one slice texture, added to the array in order; the slice **index** that ends up at each vertex comes from the PQS build (see Notes), so the order you list them in must match the indices your terrain assigns.

```
PQS
{
    materialType = TriplanarAtlas
    Material
    {
        // Albedo slices (Texture2DArray) — order = slice index
        AtlasTex
        {
            Texture = MyMod/PluginData/grass_color.dds
            Texture = MyMod/PluginData/sand_color.dds
            Texture = MyMod/PluginData/rock_color.dds
            Texture = MyMod/PluginData/snow_color.dds
        }
        // Matching DXT5nm normal slices, same order
        NormalTex
        {
            Texture = MyMod/PluginData/grass_normal.dds
            Texture = MyMod/PluginData/sand_normal.dds
            Texture = MyMod/PluginData/rock_normal.dds
            Texture = MyMod/PluginData/snow_normal.dds
        }
        atlasTiling       = 100000
        colorLerpModifier = 1

        // Zoom-rotation tiling cascade
        factor            = 10
        factorBlendWidth  = 0.1
        factorRotation    = 30

        // Colour grading and PBR lighting
        saturation        = 1
        contrast          = 1
        tintColor         = 1, 1, 1, 0
        specularColor     = 0.2, 0.2, 0.2, 0.2
        albedoBrightness  = 2

        // Steep-slope overlay
        steepTex      = MyMod/PluginData/cliff_color.dds
        steepBumpMap  = MyMod/PluginData/cliff_normal.dds
        steepPower    = 1
        steepTexStart = 20000
        steepTexEnd   = 30000

        // Atmospheric haze and underwater fog (only on bodies with an atmosphere)
        globalDensity    = 1
        fogColorRamp     = MyMod/PluginData/fog_ramp.dds
        oceanFogDistance = 1000
    }
}
```

## How the surface is built

The shader is a triplanar terrain surface assembled per pixel. There is no single colour map and no per-elevation band textures; instead a graded base colour, four atlas slices, a steep overlay and fog are combined in the following order:

1. **Per-vertex colour grading** — the PQS supplies a colour per vertex (`COLOR.rgb`, usually from a colour map or [LandControl](/Syntax/PQSMods/LandControl/LandControl)). The shader desaturates that colour toward its luma by `saturation`, mixes in `tintColor` (whose alpha is the mix amount), and scales it by `contrast`. The result is the **base colour** that the atlas albedo then tints.
2. **Slice decode** — each vertex carries four atlas **slice indices** and four **blend weights**, packed into a secondary mesh UV channel (the TEXCOORD2 set) by KSP and decoded in the vertex shader. These pick *which* four slices of `AtlasTex` / `NormalTex` are sampled at that point and how strongly each contributes. None of this is set from config — it is produced by the PQS terrain build.
3. **Triplanar planar coordinates** — the triplanar source is the vertex's world position (plus a runtime floating-origin offset) multiplied by `atlasTiling`. The same coordinate feeds all four slices and the steep overlay. Because it is world position scaled, the tiling number is large (default 100000).
4. **Zoom-rotation tiling** — this is the shader's signature. A loop multiplies a running scale by `factor` until it exceeds the view-space depth, choosing a zoom tier; the planar coordinates are sampled at that tier and at the next coarser tier, and the two are blended across a band whose width is `factorBlendWidth`. Each tier also **rotates** the planar coordinates by an extra `factorRotation` degrees about the surface normal — the "swirl" that scrambles the tile pattern so it does not read as obvious repetition as the camera pulls back. The far tier rotates one full `factorRotation` step beyond the near tier.
5. **Four-slice triplanar accumulation** — for each of the two tiers, all four slices are triplanar-sampled from the array (with that tier's rotation) and summed by the decoded slice weights; the near and far results are then lerped by the blend factor. The blended atlas albedo modulates the graded base colour, mixed by `colorLerpModifier` (0 = pure vertex colour, 1 = vertex colour × atlas albedo).
6. **Steep-slope overlay** — `steepTex` and `steepBumpMap` are blended over the result on steep terrain. The per-vertex steepness is multiplied by `steepPower` and clamped, so higher `steepPower` makes the cliff texture reach onto gentler slopes; the overlay fades out with distance across `steepTexStart` → `steepTexEnd`. **Note:** the overlay is tiled at the *same* zoom-tier scale as the atlas — the declared `steepNearTiling` / `steepTiling` properties are **not used** by the shader (see Notes).
7. **Normal mapping** — each slice and the steep overlay carry a DXT5nm normal map, accumulated triplanar and transformed into world space through the mesh's tangent frame. Because the zoom cascade rotates the coordinates, the decoded normals are counter-rotated to match — which is why changing `factorRotation` shifts how relief catches the light.
8. **PBR lighting** — the blended albedo is scaled by `albedoBrightness` and lit with a physically-based specular (GGX) model: `specularColor` is the surface's specular reflectance at normal incidence (its F0), used together with the engine's box-projected reflection probes for image-based reflections, on top of the direct light and spherical-harmonic ambient. Setting `specularColor` to black removes the specular term.
9. **Aerial-perspective & ocean fog** — on a body with an atmosphere, a distance haze is mixed in last. Its colour is read from `fogColorRamp` indexed by the sun angle, and its strength grows with distance and `globalDensity`. A second, underwater **ocean fog** term reads the same ramp at a different row and fades over `oceanFogDistance`, gated by camera altitude (only above the waterline). On an airless body the fog stage has no visible effect.

The triplanar projection means the atlas textures need **no UV mapping** for colour, but the normal mapping still needs mesh **tangents**, which the PQS quad-sphere provides. Because the base colour, the slice selection/weights and the slope all come from the PQS build, the final look depends as much on your [PQSMods](/Syntax/PQSMods/) — [height](/Syntax/PQSMods/VertexHeightMap), [colour](/Syntax/PQSMods/VertexColorMap), [LandControl](/Syntax/PQSMods/LandControl/LandControl) — as on the textures set here.

## Properties

The properties fall into five groups, which we shall take in turn:

1. **Texture arrays & tiling** — the albedo/normal slice arrays, the master tiling and the colour-mix amount.
2. **Zoom & rotation** — the tiling cascade that replaces a near/far crossfade.
3. **Colour & lighting** — the colour grading and the PBR specular response.
4. **Steep-slope overlay** — the cliff texture for steep terrain.
5. **Fog & opacity** — the atmospheric haze ramp, ocean-fog distance and the PQS fade scalar.

### Texture arrays & tiling

| Property | Format | Description |
|----------|--------|-------------|
| `AtlasTex` | File Path | A **collection** of albedo textures; each entry becomes one slice of the albedo `Texture2DArray`, in the order listed. There are no `*Scale`/`*Offset` rows — the array is built as a whole and tiled by the cascade. Default "white". |
| `NormalTex` | File Path | A **collection** of DXT5nm normal maps; each entry becomes one slice of the normal `Texture2DArray`, matching the corresponding `AtlasTex` slice. Default "white". |
| `atlasTiling` | Decimal | Master triplanar tiling multiplier applied to the world-space coordinate that feeds every slice. World-scaled, so the value is large. Default 100000. |
| `colorLerpModifier` | Decimal | How strongly the atlas albedo tints the graded vertex colour: 0 = pure vertex colour, 1 = vertex colour × atlas albedo. Default 1. |

### Zoom & rotation

| Property | Format | Description |
|----------|--------|-------------|
| `factor` | Decimal | Geometric zoom step between adjacent tiling tiers — the view-space depth is bracketed between consecutive powers of this value. Larger values mean fewer, coarser zoom transitions across the visible range. **Do not set it to 1**, which collapses the cascade. Default 10. |
| `factorBlendWidth` | Decimal | Width of the smooth transition between adjacent zoom tiers (declared range 0–0.5). Small values give crisp, near-discrete tier changes; larger values blend them more softly. Default 0.1. |
| `factorRotation` | Decimal | Additional rotation, in degrees, applied to the triplanar coordinates at each successive zoom tier. Breaks up visible tile repetition as the camera zooms out (the "swirl"); because it rotates the normal maps too, it also shifts how surface relief catches the light. Default 30. |

### Colour & lighting

| Property | Format | Description |
|----------|--------|-------------|
| `saturation` | Decimal | Saturation of the per-vertex colour map: `0` collapses it to greyscale (luma), `1` leaves it unchanged, higher oversaturates. Default 1. |
| `contrast` | Decimal | Contrast multiplier applied to the graded colour. Default 1. |
| `tintColor` | Color | A tint mixed into the base colour; the **alpha** is the mix amount (0 = no tint). The shader labels it "Colour Unsaturation (A = Factor)". Default (1, 1, 1, 0) — i.e. no tint. |
| `specularColor` | Color | Specular reflectance at normal incidence (the PBR **F0** colour), used with the scene's reflection probes for the highlight and image-based reflections. Set to black to remove specular. Default (0.2, 0.2, 0.2, 0.2). |
| `albedoBrightness` | Decimal | Multiplier applied to the blended surface colour before lighting — raises or lowers the overall terrain brightness. Default 2. |

### Steep-slope overlay

| Property | Format | Description |
|----------|--------|-------------|
| `steepTex` | File Path | Albedo blended over steep slopes (cliffs). Triplanar; tiled by the zoom cascade, not by `steepTiling`. Default "white". |
| `steepTexScale` / `steepTexOffset` | Vector2 | Tiling and offset of `steepTex`. |
| `steepBumpMap` | File Path | DXT5nm normal map for the steep overlay. Default "bump" (flat). |
| `steepBumpMapScale` / `steepBumpMapOffset` | Vector2 | Tiling and offset of `steepBumpMap`. |
| `steepPower` | Decimal | Multiplier on the vertex steepness before it is clamped to the overlay weight. Higher values push the steep texture onto gentler slopes; 0 disables it. Default 1. |
| `steepTexStart` | Decimal | View distance (m) at which the steep overlay begins fading out. Default 20000. |
| `steepTexEnd` | Decimal | View distance (m) beyond which the steep overlay is fully suppressed. Default 30000. |
| `steepNearTiling` | Decimal | Declared for material-asset compatibility with the other PQS shaders but **not consumed** by this shader — the steep textures are tiled by the active zoom tier instead. Default 1. |
| `steepTiling` | Decimal | As `steepNearTiling`: present in the material but **unused** by the shader. Default 1. |

### Fog & opacity

| Property | Format | Description |
|----------|--------|-------------|
| `globalDensity` | Decimal | Density multiplier for the aerial-perspective haze. Only has an effect on bodies with an atmosphere; higher values thicken the distance fog. Default 1. |
| `fogColorRamp` | File Path | 1-D colour ramp for the fog. The aerial haze is read from it indexed by the sun angle (so the fog can be tinted differently toward and away from the sun); a second row supplies the underwater ocean-fog colour. Default "white". |
| `fogColorRampScale` / `fogColorRampOffset` | Vector2 | Tiling and offset of the fog ramp. |
| `oceanFogDistance` | Decimal | Falloff distance (m) of the underwater ocean fog — the distance over which the surface fades into the ocean-fog colour. Active only when the camera is above the waterline. Default 1000. |
| `planetOpacity` | Decimal | Opacity of the PQS material, used by KSP to fade the terrain against the scaled-space sphere across the `fadeStart`/`fadeEnd` transition. Normally left at 1. Default 1. |

## Notes

* These are the body's **near-surface** terrain materials. The body simultaneously has a [ScaledVersion material](/Syntax/Material/Scaled/Vacuum) for the distant sphere; KSP cross-fades between the two across the [`fadeStart` / `fadeEnd`](/Syntax/PQS) altitudes, so the two should be tuned to match where they meet.
* This is the **KSP 1.9 atlas shader** — stock Kerbin selects it at the **Ultra** terrain-detail setting. It shares the zoom-rotation cascade and PBR specular lighting of [MainTriplanarZoomRotation](/Syntax/Material/PQS/MainTriplanarZoomRotation) (the 1.8-era *band* variant), differing chiefly in how ground textures are stored and chosen.
* **Ground detail is a Texture2DArray, not elevation bands.** There are therefore **no** `lowTex`/`midTex`/`highTex`, no per-band normal maps, no `lowTiling`/`midTiling`/`highTiling`, and no `lowStart`/`lowEnd`/`highStart`/`highEnd` elevation-window properties — all of which exist on the PQS Main family. The atlas is supplied through the `AtlasTex` and `NormalTex` collection nodes; the loader cannot read slice names back out, so these are write-only (Kittopia hides them).
* **Slice selection is runtime-driven, not config.** Each vertex carries four atlas slice indices and four blend weights packed into a secondary mesh UV channel (the TEXCOORD2 set) by KSP's PQS system (typically driven by [LandControl](/Syntax/PQSMods/LandControl/LandControl)). They are decoded in the vertex shader and are **not** material properties — you control which textures *can* appear by populating the arrays, but where each appears is decided by the terrain build.
* `steepNearTiling` and `steepTiling` are accepted by the loader and live on the material for compatibility with the other PQS shaders, but **this shader does not use them**: the steep overlay is tiled by the active zoom-tier scale, exactly like the atlas. Setting them has no effect.
* The textures are **world-space triplanar**, so they need no UVs and `atlasTiling` is in world units (hence the large default). The shader still requires mesh **tangents** for its normal mapping, which the PQS quad-sphere provides.
* The PBR lighting uses a GGX specular BRDF plus box-projected reflection probes; `albedoBrightness` and `specularColor` control it. A black `specularColor` disables the specular contribution.
* **Opacity convention is inverted.** The forward passes use a `OneMinusSrcAlpha SrcAlpha` blend, so internally alpha = 1 is fully transparent and alpha = 0 fully opaque. `planetOpacity` is folded into that and is driven by KSP at runtime for the scaled-space fade — normally leave it at 1.
* The fog stage (`globalDensity`, `fogColorRamp`, `oceanFogDistance`) only has a visible effect on bodies with an atmosphere; the ocean-fog term additionally requires the camera to be above the waterline. See [Ocean](/Syntax/Ocean) for the matching ocean material.
* Several shader inputs are driven by KSP at runtime and are **not** config properties: the floating-origin offset that keeps the world-space triplanar coordinates stable as the game shifts the origin around the craft, the sun direction, the camera altitude, and the viewer air density used by the fog.
* The shader declares `Fallback "Diffuse"`, so on a platform where it fails to compile Unity falls back to the built-in Diffuse shader.
* `materialType = TriplanarAtlas` is also accepted under its older alias `AtmosphericTriplanarZoomRotationTextureArray`, which you may see in existing configs; the two select the same shader.
