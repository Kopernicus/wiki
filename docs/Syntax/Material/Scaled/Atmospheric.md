# Atmospheric Material

**Shader:** `Terrain/Scaled Planet (RimAerial)`

The Atmospheric `Material { }` drives the `Terrain/Scaled Planet (RimAerial)` shader — the scaled-space planet shader for bodies that have an atmosphere. It paints the planet's distant, low-LOD sphere from a single **colour map**, lights it with a standard Blinn-Phong specular model, and then adds one ingredient the airless planet shaders lack: a **view-angle rim glow** around the planet's limb that stands in for the soft halo of an atmosphere seen edge-on (the "aerial perspective").

In the stock [KittopiaTech dumps](https://github.com/Kopernicus/kittopia-dumps), the bodies set to `type = Atmospheric` are Eve, Duna, and Laythe. (Jool is listed as `Atmospheric` in the dumps as well, but in the live game it actually ships with the [Gas Giant material](/Syntax/Material/Scaled/GasGiant) — one of the few places the dumps diverge from the running game.) Kerbin and Eeloo instead use the closely related `AtmosphericStandard` type (the `Terrain/Scaled Planet (RimAerial) Standard` shader), which shares the same rim concept but lights the surface through Unity's Standard PBR model.

If your body has no atmosphere, use the plainer [Vacuum material](/Syntax/Material/Scaled/Vacuum) instead — it is identical but for the missing rim. If you want a procedurally-banded gas giant rather than a painted surface, see the [Gas Giant material](/Syntax/Material/Scaled/GasGiant), which has its own, brighter rim model.

We shall first see how to enable the material, then how the shader lights the surface and draws the rim, and finally discuss each group of properties in turn.

## Enabling

Select the atmospheric material by setting `type = Atmospheric` in the [`ScaledVersion { }`](/Syntax/ScaledVersion) node. The `Material { }` node then accepts the properties listed below.

```
ScaledVersion
{
    type = Atmospheric
    Material
    {
        // Surface maps
        texture = MyMod/PluginData/Laythe_color.dds
        normals = MyMod/PluginData/Laythe_normal.dds

        // Colour and specular response
        color     = 1, 1, 1, 1
        specColor = 0.5, 0.5, 0.5, 1
        shininess = 0.078125

        // Atmospheric rim
        rimPower     = 3
        rimBlend     = 1
        rimColorRamp = MyMod/PluginData/Laythe_rim.dds

        // Resource-scanner overlay (optional)
        resourceMap = MyMod/PluginData/Laythe_resources.dds
    }
}
```

## How the surface is lit

The shader is essentially Unity's BumpedSpecular (Blinn-Phong) lighting with three KSP-specific pieces layered on top — two that build the lit surface, and one that paints the atmosphere:

1. **Colour and gloss** — `texture` is the colour map. Its **RGB** channels are the surface albedo (multiplied by `color`), and its **alpha** channel is a per-pixel *specular intensity mask*: bright areas catch the sun's highlight strongly, dark areas stay matte. This lets a single texture vary glossiness across the surface (e.g. shiny seas against dull land).
2. **Surface relief** — `normals` is a tangent-space normal map that perturbs the lighting per pixel, so the body keeps fine surface detail when viewed from orbit even though the scaled-space mesh is smooth.
3. **Atmospheric rim** — a glow that fades in toward the planet's silhouette. Where the surface faces the camera head-on it contributes nothing; as the surface curves away toward the limb it brightens, reaching full strength right at the edge (`rimFalloff = (1 − N·V)^rimPower`). `rimPower` controls how tightly the glow hugs the limb and `rimBlend` scales its overall brightness. Its colour is read from `rimColorRamp`, a 1-D ramp **indexed by the day/night terminator**, so the sunlit limb and the dark-side limb can be tinted differently (a bright blue dayside haze fading to a dim nightside edge, say). The finished rim is then scaled by `opacity`.

The highlight itself is shaped by `specColor` (its tint) and `shininess` (how tight it is); setting `specColor` to black drops the specular entirely. There is no emission term beyond the rim — what you paint is what you get, lit by the local star.

A `resourceMap` overlay shares the rim's emission slot: the scanner colour is composited as `lerp(rim, resourceRGB, resourceAlpha)`, so where the resource map is opaque it replaces the rim glow at that point, and where it is transparent the rim shows through untouched. The lit surface beneath is always added on top, so the overlay never hides the planet itself. The default all-black map contributes nothing.

## Properties

The properties fall into five groups, which we shall take in turn:

1. **Surface maps** — the colour and normal textures that define the body's appearance.
2. **Colour and specular** — the tints and highlight sharpness applied during lighting.
3. **Atmospheric rim** — the view-angle halo and the ramp that colours it.
4. **Resource overlay** — the optional scanner map composited on top.
5. **Opacity** — the rim fade scalar.

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

### Atmospheric rim

| Property | Format | Description |
|----------|--------|-------------|
| `rimPower` | Decimal | View-angle rim falloff exponent. Larger values concentrate the glow into a thinner ring at the limb; smaller values spread it across a broader band. Default 3. |
| `rimBlend` | Decimal | Master brightness multiplier on the sampled rim colour, applied before the `opacity` fade. Set to 0 to suppress the rim entirely. Default 1. |
| `rimColorRamp` | File Path | 1-D colour ramp that tints the rim, indexed across the day/night terminator so the lit and dark limbs can be coloured differently. Default "white". |
| `rimColorRampScale` / `rimColorRampOffset` | Vector2 | Tiling and offset of the rim colour ramp. |
| `Gradient` | Gradient | The `rimColorRamp`, but defined explicitly as a gradient instead of a texture. Each entry's left value is the position along the terminator from 0 (lit side) to 1 (dark side), and the right value is the rim colour there. |
| `localLightDirection` | Vector4 | Object-space sun direction used to compute the terminator coordinate for the ramp. Marked `[PerRendererData]` and overwritten every frame by KSP's scaled-space lighting controller, so a config value has no lasting effect. Default (1, 0, 0, 0). |

### Resource overlay

| Property | Format | Description |
|----------|--------|-------------|
| `resourceMap` | File Path | Resource-scanner overlay texture. Composited into the emission as `lerp(rim, resourceRGB, resourceAlpha)`: opaque pixels replace the rim glow, transparent pixels leave it alone. Default "black" (contributes nothing). |
| `resourceMapScale` / `resourceMapOffset` | Vector2 | Tiling and offset of the resource map. |

### Opacity

| Property | Format | Description |
|----------|--------|-------------|
| `opacity` | Decimal | Master strength of the rim **emission**, range `[0, 1]`. It scales the rim glow only — not the lit surface (which always renders opaque), nor the resource overlay (which composites at full strength regardless). Default 1. |

## Notes

* `opacity` is marked `[PerRendererData]` and is driven at runtime by the scaled-space fader, which Kopernicus attaches to every non-star body with its `floatName` bound to `_Opacity`. As the camera crosses the [`fadeStart` and `fadeEnd`](/Syntax/ScaledVersion) altitudes the fader sweeps this value, fading the atmospheric halo with the transition. A static config value is therefore normally left at its default of 1.
* `localLightDirection` is likewise `[PerRendererData]` and rewritten each frame, so although the loader exposes it as a config field, setting it has no lasting effect — it exists so KSP can keep the rim ramp tracking the real Sun as the planet rotates and orbits.
* The rim's view-angle term uses the body's smooth **geometric** normal, not the normal-mapped surface normal — so surface relief shapes the lit surface and specular highlight, but not the limb glow.
* The shader requires mesh **tangents** for its normal mapping — the scaled mesh Kopernicus generates already provides them. A custom mesh without tangents will render with broken normals.
* If the shader fails to compile on the target platform it falls back to Unity's `Standard` shader.
