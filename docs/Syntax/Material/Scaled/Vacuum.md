# Vacuum Material

**Shader:** `Terrain/Scaled Planet (Simple)`

The Vacuum `Material { }` drives the `Terrain/Scaled Planet (Simple)` shader — the plainest of the scaled-space planet shaders, and the type used by most airless bodies in the stock system. It paints the planet's distant, low-LOD sphere from a single **colour map** and lights it with a standard Blinn-Phong specular model.

In the stock [KittopiaTech dumps](https://github.com/Kopernicus/kittopia-dumps), the bodies set to `type = Vacuum` are Moho, Gilly, the Mun, Minmus, Ike, Dres, Vall, Tylo, Bop, and Pol. It is not, however, used by *every* airless body: Eeloo has no atmosphere yet is set to `AtmosphericStandard`.

It is the airless counterpart to the [Atmospheric material](/Syntax/Material/Scaled/Atmospheric): both build the surface the same way, but the atmospheric shader (`Terrain/Scaled Planet (RimAerial)`) adds a view-angle rim glow for the atmosphere's limb, whereas this one has no rim at all. If your body builds its appearance procedurally from cloud bands instead of a painted map, see the [Gas Giant material](/Syntax/Material/Scaled/GasGiant).

We shall first see how to enable the material, then how the shader lights the surface, and finally discuss each group of properties in turn.

## Enabling

Select the vacuum material by setting `type = Vacuum` in the [`ScaledVersion { }`](/Syntax/ScaledVersion) node. The `Material { }` node then accepts the properties listed below.

```
ScaledVersion
{
    type = Vacuum
    Material
    {
        // Surface maps
        texture = MyMod/PluginData/Mun_color.dds
        normals = MyMod/PluginData/Mun_normal.dds

        // Colour and specular response
        color     = 1, 1, 1, 1
        specColor = 0.5, 0.5, 0.5, 1
        shininess = 0.078125

        // Resource-scanner overlay (optional)
        resourceMap = MyMod/PluginData/Mun_resources.dds
    }
}
```

## How the surface is lit

The vacuum shader is essentially Unity's BumpedSpecular (Blinn-Phong) lighting with three KSP-specific pieces layered on top:

1. **Colour and gloss** — `texture` is the colour map. Its **RGB** channels are the surface albedo (multiplied by `color`), and its **alpha** channel is a per-pixel *specular intensity mask*: bright areas catch the sun's highlight strongly, dark areas stay matte. This lets a single texture vary glossiness across the surface (e.g. shiny crater glass against dull regolith).
2. **Surface relief** — `normals` is a tangent-space normal map that perturbs the lighting per pixel, so the body keeps fine surface detail when viewed from orbit even though the scaled-space mesh is smooth.
3. **Resource overlay** — `resourceMap` is the resource-scanner overlay. It is composited additively (`rgb × a`) on top of the lit surface, so the default all-black map contributes nothing and it only shows once a scan reveals it.

The highlight itself is shaped by `specColor` (its tint) and `shininess` (how tight it is). There is no atmosphere, emission, or rim term — what you paint is what you get, lit by the local star.

## Properties

The properties fall into four groups, which we shall take in turn:

1. **Surface maps** — the colour and normal textures that define the body's appearance.
2. **Colour and specular** — the tints and highlight sharpness applied during lighting.
3. **Resource overlay** — the optional scanner map composited on top.
4. **Opacity** — the scaled-space fade scalar.

### Surface maps

| Property | Format | Description |
|----------|--------|-------------|
| `texture` | File Path | The scaled-space colour map. **RGB** = surface albedo (multiplied by `color`); **alpha** = per-pixel specular intensity mask. Also accepts the alias `mainTex`. Default "white". |
| `mainTexScale` / `mainTexOffset` | Vector2 | Tiling and offset of the colour map. |
| `normals` | File Path | Tangent-space normal map, stored in DXT5nm packing (X in the alpha channel, Y in green, Z reconstructed). Also accepts the alias `bumpMap`. Default "bump" (flat). |
| `bumpMapScale` / `bumpMapOffset` | Vector2 | Tiling and offset of the normal map. |

### Colour and specular

| Property | Format | Description |
|----------|--------|-------------|
| `color` | Color | Master diffuse tint multiplied into the colour map's RGB before lighting. The alpha is unused. Default (1, 1, 1, 1). |
| `specColor` | Color | Specular highlight tint, scaled by the star's light colour and further modulated by the colour map's alpha mask. Set to black to disable specular entirely. Default (0.5, 0.5, 0.5, 1). |
| `shininess` | Decimal | Highlight sharpness. The value maps to a Blinn-Phong specular exponent of `shininess × 128` — smaller values give a broad, soft highlight, larger values a tight, sharp one. The shader declares the valid range as `[0.03, 1]`. Default 0.078125. |

### Resource overlay

| Property | Format | Description |
|----------|--------|-------------|
| `resourceMap` | File Path | Resource-scanner overlay texture, composited additively as `rgb × a` over the lit surface. Default "black" (contributes nothing). |
| `resourceMapScale` / `resourceMapOffset` | Vector2 | Tiling and offset of the resource map. |

### Opacity

| Property | Format | Description |
|----------|--------|-------------|
| `opacity` | Decimal | Master opacity of the scaled-space material, range `[0, 1]`. Default 1. |

## Notes

* `opacity` is marked `[PerRendererData]` and is driven at runtime by the scaled-space fader, which cross-fades the body between its scaled-space and local (PQS) representations as the camera crosses the [`fadeStart` and `fadeEnd`](/Syntax/ScaledVersion) altitudes. A static config value is therefore normally left at its default of 1.
* `resourceMap` is likewise `[PerRendererData]`: KSP's surface-resource visualisation layer sets it per-renderer when a scan is active. The default black texture means no overlay is shown.
* The shader requires mesh **tangents** for its normal mapping — the scaled mesh Kopernicus generates already provides them. A custom mesh without tangents will render with broken normals.
* If the shader fails to compile on the target platform it falls back to Unity's `Standard` shader.
