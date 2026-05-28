# AtmosphericStandard Material

**Shader:** `Terrain/Scaled Planet (RimAerial) Standard`

The AtmosphericStandard `Material { }` drives the `Terrain/Scaled Planet (RimAerial) Standard` shader — the scaled-space planet shader for atmospheric bodies, lit with Unity's **Standard** physically-based shading model. Like the plainer [Atmospheric material](/Syntax/Material/Scaled/Atmospheric) it paints the planet's distant, low-LOD sphere from a single **colour map** and rings the limb with a **view-angle rim glow** that stands in for the soft halo of an atmosphere seen edge-on (the "aerial perspective"). The two materials accept exactly the same properties and draw the same rim; what differs is how the surface itself is shaded — here it is energy-conserving PBR specular, with image-based ambient and reflections supplied by Unity, rather than the older hand-rolled Blinn-Phong highlight.

In the stock system this material is used by Kerbin and Eeloo. (Eeloo has no atmosphere of its own, yet is still assigned this type; a body that wants no visible halo simply leaves its rim dark — see `rimBlend` and `rimColorRamp` below.)

If your body has no atmosphere, you can also use the plainer [Vacuum material](/Syntax/Material/Scaled/Vacuum) — it builds the surface the same way but has no rim at all. If you want a procedurally-banded gas giant rather than a painted surface, see the [Gas Giant material](/Syntax/Material/Scaled/GasGiant), which has its own, brighter rim model.

> The Standard scaled-planet shaders ship only with **KSP 1.9 and later**. On older KSP versions this type is unavailable; use [`type = Atmospheric`](/Syntax/Material/Scaled/Atmospheric) instead.

We shall first see how to enable the material, then how the shader lights the surface and draws the rim, and finally discuss each group of properties in turn.

## Enabling

Select the material by setting `type = AtmosphericStandard` in the [`ScaledVersion { }`](/Syntax/ScaledVersion/) node. The `Material { }` node then accepts the properties listed below.

```
ScaledVersion
{
    type = AtmosphericStandard
    Material
    {
        // Surface maps
        texture = MyMod/PluginData/Kerbin_color.dds
        normals = MyMod/PluginData/Kerbin_normal.dds

        // Colour and specular response
        color     = 1, 1, 1, 1
        specColor = 0.5, 0.5, 0.5, 1
        shininess = 0.078125

        // Atmospheric rim
        rimPower     = 3
        rimBlend     = 1
        rimColorRamp = MyMod/PluginData/Kerbin_rim.dds

        // Resource-scanner overlay (optional)
        resourceMap = MyMod/PluginData/Kerbin_resources.dds
    }
}
```

## How the surface is lit

The shader is a Unity **Standard (specular workflow)** surface shader with two KSP-specific pieces layered on top — one that adds surface relief, and one that paints the atmosphere. Because it is a true PBR model, the engine also supplies spherical-harmonic ambient, reflection probes, and proper energy conservation for free, rather than the single hand-rolled highlight of the legacy shaders:

1. **Albedo and smoothness** — `texture` is the colour map. Its **RGB** channels are the surface albedo (multiplied by `color`), and its **alpha** channel is a per-pixel *smoothness* mask: bright areas read as glossy, dark areas as rough and matte. The global `shininess` value scales that alpha, so the final per-pixel smoothness is `texture.alpha × shininess`. This lets a single texture vary glossiness across the surface (e.g. shiny seas against dull land).
2. **Surface relief** — `normals` is a tangent-space normal map that perturbs the lighting per pixel, so the body keeps fine surface detail when viewed from orbit even though the scaled-space mesh is smooth.
3. **Atmospheric rim** — a glow that fades in toward the planet's silhouette. Where the surface faces the camera head-on it contributes nothing; as the surface curves away toward the limb it brightens, reaching full strength right at the edge (`rimFalloff = (1 − N·V)^rimPower`). `rimPower` controls how tightly the glow hugs the limb and `rimBlend` scales its overall brightness. Its colour is read from `rimColorRamp`, a 1-D ramp **indexed by the day/night terminator**, so the sunlit limb and the dark-side limb can be tinted differently (a bright blue dayside haze fading to a dim nightside edge, say). The finished rim is then scaled by `opacity`.

Because this is a physically-based specular workflow, `specColor` is the surface's **specular reflectance at normal incidence** (its F0 colour) rather than a free-floating highlight tint — Unity uses it, together with the smoothness above, to drive both the direct specular highlight and the image-based reflections and ambient that the Standard model adds. Setting `specColor` to black removes the specular reflection entirely. There is no emission term beyond the rim — what you paint is what you get, lit by the local star and its surroundings.

A `resourceMap` overlay shares the rim's emission slot: the scanner colour is composited as `lerp(rim, resourceRGB, resourceAlpha)`, so where the resource map is opaque it replaces the rim glow at that point, and where it is transparent the rim shows through untouched. The lit surface beneath is always added on top, so the overlay never hides the planet itself. The default all-black map contributes nothing.

## Properties

The properties fall into five groups, which we shall take in turn:

1. **Surface maps** — the colour and normal textures that define the body's appearance.
2. **Colour and specular** — the tints and surface smoothness applied during PBR lighting.
3. **Atmospheric rim** — the view-angle halo and the ramp that colours it.
4. **Resource overlay** — the optional scanner map composited on top.
5. **Opacity** — the rim fade scalar.

### Surface maps

| Property | Format | Description |
|----------|--------|-------------|
| `texture` | File Path | The scaled-space colour map. **RGB** = surface albedo (multiplied by `color`); **alpha** = per-pixel smoothness mask (scaled by `shininess`). Also accepts the alias `mainTex`. Default "white". |
| `mainTexScale` / `mainTexOffset` | Vector2 | Tiling and offset of the colour map. |
| `normals` | File Path | Tangent-space normal map, stored in DXT5nm packing (X in the alpha channel, Y in green, Z reconstructed). Also accepts the alias `bumpMap`. Default "bump" (flat). |
| `bumpMapScale` / `bumpMapOffset` | Vector2 | Tiling and offset of the normal map. |

### Colour and specular

| Property | Format | Description |
|----------|--------|-------------|
| `color` | Color | Master diffuse tint multiplied into the colour map's RGB before lighting. The alpha is unused. Default (1, 1, 1, 1). |
| `specColor` | Color | Specular reflectance at normal incidence (the PBR **F0** colour), used together with the surface smoothness to drive both the direct highlight and the image-based reflections. Set to black to remove specular reflection entirely. Default (0.5, 0.5, 0.5, 1). |
| `shininess` | Decimal | Global smoothness scale. The final per-pixel smoothness is `texture.alpha × shininess`, so `1` passes the texture's gloss mask through unchanged and lower values make the whole surface rougher and more matte. The shader declares the valid range as `[0.03, 1]`. Default 0.078125. |

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

* `opacity` is marked `[PerRendererData]` and is driven at runtime by the scaled-space fader, which Kopernicus attaches to every non-star body with its `floatName` bound to `_Opacity`. As the camera crosses the [`fadeStart` and `fadeEnd`](/Syntax/ScaledVersion/) altitudes the fader sweeps this value, fading the atmospheric halo with the transition. A static config value is therefore normally left at its default of 1.
* `localLightDirection` is likewise `[PerRendererData]` and rewritten each frame, so although the loader exposes it as a config field, setting it has no lasting effect — it exists so KSP can keep the rim ramp tracking the real Sun as the planet rotates and orbits.
* Unlike the plain [Atmospheric](/Syntax/Material/Scaled/Atmospheric) shader, whose rim is keyed to the body's smooth **geometric** normal, this build computes the rim's view-angle term against the **normal-mapped** surface normal — so surface relief feeds the limb glow as well as the lit surface, though the effect is subtle at the grazing angles where the rim is brightest.
* The shader requires mesh **tangents** for its normal mapping — the scaled mesh Kopernicus generates already provides them. A custom mesh without tangents will render with broken normals.
* The shader has no shadow-casting pass of its own; it delegates shadow casting to its `Standard` fallback (which also takes over wholesale if the shader fails to compile). It supports only Unity's modern single-pass deferred path, not the legacy two-step prepass — invisible to config authors, but the reason the lit result matches across rendering paths.
