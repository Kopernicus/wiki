# Gas Giant Material

**Shader:** `Terrain/Gas Giant` &nbsp;·&nbsp; **Runtime component:** `GasGiantMaterialControls`

The Gas Giant `Material { }` drives the `Terrain/Gas Giant` shader — the animated, banded scaled-space surface used by Jool. Unlike the [Vacuum](/Syntax/Material/Scaled/Vacuum) and [Atmospheric](/Syntax/Material/Scaled/Atmospheric) materials, which paint the planet from a single colour map, the gas giant builds its appearance procedurally from two **swirling cloud band layers**:

* A **far (base) layer** that defines the overall banding and is visible at any distance.
* A **detail (near) layer** of higher-frequency cloud structure that fades in as the camera approaches.

The bands scroll and swirl over time. We shall first see how to enable the material, then walk through how those two layers are assembled, and finally discuss each group of properties in turn.

> If you would rather have gas giants generated for you than author every map by hand, see [Procedural Gas Giants](/Syntax/Expansion/ProceduralGasGiants).

## Enabling

Select the gas giant material by setting `type = GasGiant` in the [`ScaledVersion { }`](/Syntax/ScaledVersion/Material) node. The `Material { }` node then accepts the properties listed below.

```cfg
ScaledVersion
{
    type = GasGiant
    Material
    {
        // Far (base) band layer
        cloudPatternMap = MyMod/PluginData/Jool_pattern.dds
        colorMap        = MyMod/PluginData/Jool_ramp1.dds
        colorMap2       = MyMod/PluginData/Jool_ramp2.dds
        normalMap       = MyMod/PluginData/Jool_normal.dds

        // Detail (near) band layer
        detailCloudPatternMap = MyMod/PluginData/Jool_detail_pattern.dds
        detailColorMap        = MyMod/PluginData/Jool_detail_ramp.dds
        detailNormalMap       = MyMod/PluginData/Jool_detail_normal.dds
        detailTiling          = 10

        // Detail fade by camera distance
        nearDetailDistance = 50
        nearDetailStrength = 0.8
        farDetailDistance  = 200
        farDetailStrength  = 0

        // Animation control maps
        movementControlTexture = MyMod/PluginData/Jool_movement.dds
        swirlControlTexture    = MyMod/PluginData/Jool_swirl.dds

        // Animation speeds (clamped at runtime)
        bandMovementSpeed       = 0.05
        swirlRotationSpeed      = 1
        swirlRotationSwirliness = 1
        swirlPanSpeed           = -0.05
    }
}
```

## How the layers combine

The cloud bands are not a single painted texture — they are assembled at runtime:

1. `cloudPatternMap` is sampled as a greyscale control map. Its **red** channel indexes the two colour ramps (`colorMap`, `colorMap2`), and its **green** channel blends between them. This is what gives a gas giant its characteristic two-tone band palette.
2. `swirlControlTexture` and `movementControlTexture` distort and scroll the UV coordinates the patterns are read at, producing the slow swirling and east/west band drift.
3. The `detail*` maps repeat this process at a finer scale (`detailTiling`) and are mixed in based on camera distance, so close-up views gain extra cloud structure.

The movement itself is driven by the stock `GasGiantMaterialControls` component, which Kopernicus attaches to the scaled body automatically whenever this material is used. Every frame it writes the `_GasGiantTime` shader global and clamps the speed properties below to their valid ranges.

Every texture also has matching `…Scale` and `…Offset` properties (a `Vector2` tiling/offset pair), omitted from the example for brevity.

## Properties

The properties fall into six groups, which we shall take in turn:

1. **Far (base) layer** — the always-visible banding.
2. **Detail (near) layer** — the high-frequency clouds that fade in up close.
3. **Detail fade** — how that detail layer is mixed in by camera distance.
4. **Animation control maps** — the textures that shape where bands sit and how they swirl.
5. **Animation speeds** — the scalars that set how fast all of that moves.
6. **Lighting and rim glow** — the specular response and the atmospheric halo at the limb.

### Far (base) layer

This is the layer you see from orbit: the broad bands that read at any distance. The pattern map is a control map rather than a picture — it tells the shader *which* of the two colour ramps to show at each point, and how to blend them.

| Property | Format | Description |
|----------|--------|-------------|
| `cloudPatternMap` | File Path | Far band pattern. **Red** channel = the band pattern; **green** channel = blend between `colorMap` and `colorMap2`. |
| `cloudPatternMapScale` / `cloudPatternMapOffset` | Vector2 | Tiling and offset of the cloud pattern. |
| `colorMap` | File Path | First far colour ramp, sampled by the pattern's red channel. Its alpha channel also contributes to surface smoothness. |
| `colorMapScale` / `colorMapOffset` | Vector2 | Tiling and offset of the first colour ramp. |
| `colorMap2` | File Path | Second far colour ramp, blended against `colorMap` by the pattern's green channel. |
| `colorMap2Scale` / `colorMap2Offset` | Vector2 | Tiling and offset of the second colour ramp. |
| `normalMap` | File Path | Far tangent-space normal map (DXT5nm), sampled at the same swirled UVs as the pattern. Also accepts the aliases `normals` and `bumpMap`. |
| `normalMapScale` / `normalMapOffset` | Vector2 | Tiling and offset of the far normal map. |

### Detail (near) layer

The detail layer mirrors the far layer at a finer scale (set by `detailTiling`), adding cloud structure that is only worth showing when the camera is close. Its pattern is read more simply — only the red channel matters.

| Property | Format | Description |
|----------|--------|-------------|
| `detailCloudPatternMap` | File Path | High-frequency band pattern. Only the **red** channel is read. |
| `detailCloudPatternMapScale` / `detailCloudPatternMapOffset` | Vector2 | Tiling and offset of the detail pattern. |
| `detailColorMap` | File Path | Colour ramp for the detail layer, sampled by the detail pattern's red channel. |
| `detailColorMapScale` / `detailColorMapOffset` | Vector2 | Tiling and offset of the detail colour ramp. |
| `detailNormalMap` | File Path | Detail tangent-space normal map (DXT5nm). |
| `detailNormalMapScale` / `detailNormalMapOffset` | Vector2 | Tiling and offset of the detail normal map. |
| `detailTiling` | Decimal | UV multiplier applied to the far UVs to produce the detail UVs. Higher values give finer detail. Default 10. |

### Detail fade (camera distance)

A gas giant should reveal crisp cloud structure as you fly in, then melt back to smooth bands when viewed from orbit. These four properties describe that transition as a straight line: within `nearDetailDistance` the detail layer sits at full `nearDetailStrength`; beyond `farDetailDistance` it sits at `farDetailStrength`; between the two distances its strength is linearly interpolated.

| Property | Format | Description |
|----------|--------|-------------|
| `nearDetailDistance` | Decimal | Camera distance at or below which detail is at full `nearDetailStrength`. Default 50. |
| `nearDetailStrength` | Decimal | Detail influence when zoomed in (at/below `nearDetailDistance`). `1` fully replaces the base layer; `0` hides detail. Default 0.8. |
| `farDetailDistance` | Decimal | Camera distance at or beyond which detail is at full `farDetailStrength`. Default 200. |
| `farDetailStrength` | Decimal | Detail influence when zoomed out (at/beyond `farDetailDistance`). Set `0` to make detail vanish at distance. Default 0. |

### Animation control maps

Where the speeds below say *how fast* the clouds move, these two textures say *where* and *how* they move. Both are static control maps whose individual channels each carry a different piece of the animation.

| Property | Format | Description |
|----------|--------|-------------|
| `movementControlTexture` | File Path | Static control map for band movement. **Red/green** drive left/right band movement (black = no movement, white = full speed, then scaled by `bandMovementSpeed`); **blue** blends between the two band reads; **alpha** controls swirl pan. Sampled without panning, so it defines where the bands permanently live. |
| `movementControlTextureScale` / `movementControlTextureOffset` | Vector2 | Tiling and offset of the movement control map. |
| `swirlControlTexture` | File Path | Control map shaping the swirls. **Red/green** = UV position of the centre of rotation; **blue** = swirl rotation amount. Sampled at a panned UV, so the swirl pattern itself drifts over time. |
| `swirlControlTextureScale` / `swirlControlTextureOffset` | Vector2 | Tiling and offset of the swirl control map. |

### Animation speeds

These scalars set the pace of everything above. They are clamped to their valid ranges at runtime by `GasGiantMaterialControls`, so a value outside the listed range is simply pulled back to the nearest limit.

| Property | Format | Description |
|----------|--------|-------------|
| `bandMovementSpeed` | Decimal | East/west band scroll speed. Negative moves bands westward, positive eastward. Clamped to `[-10, 10]`. Default 0.05. |
| `swirlRotationSpeed` | Decimal | How fast the swirl rotation evolves. Clamped to `[0, 10]`. Default 1. |
| `swirlRotationSwirliness` | Decimal | Final angular scale of the swirl. Negative inverts the swirl direction; values near zero flatten the swirls to straight bands. Clamped to `[-10, 10]`. Default 1. |
| `swirlPanSpeed` | Decimal | How fast the swirl control map drifts along the U axis. Clamped to `[-10, 10]`. Default -0.05. |

### Lighting and rim glow

Surface lighting uses a standard PBR specular workflow, and a view-angle rim emission produces the atmospheric halo around the planet's limb.

| Property | Format | Description |
|----------|--------|-------------|
| `specColor` | Color | Specular F0 colour for the PBR lighting. Also accepts the alias `specularColor`. Default (0.5, 0.5, 0.5, 1). |
| `surfaceColorIntoEmissiveMultiplier` | Decimal | Fraction of the lit band colour bled into emission so the night side still glows faintly. `0` gives a pure dark night side. Range `[0, 1]`. Default 0.5. |
| `rimPower` | Decimal | View-angle rim falloff exponent. Larger values concentrate the rim glow into a thinner ring at grazing angles. Default 3. |
| `rimBlend` | Decimal | Multiplicative scale on the rim ramp colour — brightens or dims the whole rim glow. Default 1. |
| `rimColorRamp` | File Path | 1D ramp tinting the rim emission, indexed by the day/night terminator so the rim is coloured differently on the lit and dark sides. |
| `rimColorRampScale` / `rimColorRampOffset` | Vector2 | Tiling and offset of the rim colour ramp. |
| `Gradient` | Gradient | The `rimColorRamp`, but defined explicitly as a gradient instead of a texture. Works the same way as the [atmospheric material's `Gradient`](/Syntax/ScaledVersion/Material): the left value is the position from 0 (lit side) to 1 (dark side), and the right value is the colour there. |

```cfg
Material
{
    specColor = 0.5, 0.5, 0.5, 1
    surfaceColorIntoEmissiveMultiplier = 0.5
    rimPower = 3
    rimBlend = 1
    rimColorRamp = MyMod/PluginData/Jool_rim.dds
}
```

The shader also has `_Opacity` and `_localLightDirection` properties, but they are marked `[PerRendererData]` and overwritten every frame by KSP's scaled-space lighting controller, so they are not exposed as config fields.

## Notes

* The shader requires mesh **tangents** for its normal mapping — the scaled mesh Kopernicus generates already provides them.
* Animation only runs in-game; the bands are static in the editor preview because `_GasGiantTime` is supplied at runtime.
