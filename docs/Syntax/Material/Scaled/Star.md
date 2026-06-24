# Star Material

**Shader:** `Emissive Multi Ramp Sunspots` &nbsp;·&nbsp; **Runtime component:** `SunShaderController`

The Star `Material { }` drives the `Emissive Multi Ramp Sunspots` shader — the scaled-space surface used by stars. Unlike the [Vacuum](/Syntax/Material/Scaled/Vacuum), [Atmospheric](/Syntax/Material/Scaled/Atmospheric) and [Gas Giant](/Syntax/Material/Scaled/GasGiant) materials, which paint a planet's surface and *light* it with the local star, a star **is** the light source — so this shader is fundamentally **emissive** (self-illuminated) and **animated**. It builds a slowly boiling heat-map surface by scrolling a four-channel noise texture through a heat-colour ramp, darkens it inside a static **sunspot** mask, and finally washes the camera-facing face of the disc with a tint colour.

In the stock system this material is used by the Sun (Kerbol) — the only star in the stock game.

Selecting `type = Star` does more than pick a shader: it also tells Kopernicus that this body is a star. The body's [`ScaledVersion { }`](/Syntax/ScaledVersion/) node then additionally accepts a `Light { }` block (a `LightShifter` controlling the star's lighting) and a `Coronas { }` collection (the flat corona billboards around the disc), and Kopernicus attaches the runtime components that make a star behave like one — the `SunShaderController` that animates this material, plus `ScaledSun` and `StarComponent`. Those star-wide pieces are out of scope here; this page documents only the surface material.

We shall first see how to enable the material, then how the shader builds and animates the surface, and finally discuss each group of properties in turn.

## Enabling

Select the star material by setting `type = Star` in the [`ScaledVersion { }`](/Syntax/ScaledVersion/) node. The `Material { }` node then accepts the properties listed below.

```cfg
ScaledVersion
{
    type = Star
    Material
    {
        // Animated heat-map surface
        noiseMap   = MyMod/PluginData/Sun_noise.dds
        emitColor0 = 0.6, 0.1, 0.0, 1   // colour where heat = 0 (cool)
        emitColor1 = 1.0, 0.9, 0.4, 1   // colour where heat = 1 (hot)

        // Sunspot overlay
        sunspotTex   = MyMod/PluginData/Sun_sunspots.dds
        sunspotColor = 0.2, 0.05, 0.0, 1
        sunspotPower = 1

        // Face-on colour wash (see note on "rim" naming below)
        rimColor = 1, 1, 1, 1
        rimPower = 0.2
        rimBlend = 0.2
    }
}
```

## How the surface is built

The shader is a self-illuminated surface assembled in three stages — there is no painted colour map and no atmospheric limb glow:

1. **Animated heat map** — `noiseMap` is a four-channel scrolling noise texture. Each of its four channels is offset by its own runtime-driven scroll value and looked up in `rampMap`, a 1-D **heat ramp** read as a horizontal gradient from cool (left) to hot (right). The four lookups are averaged into a single `0..1` *heat* value per pixel, which then blends the surface colour between `emitColor0` (where heat is 0) and `emitColor1` (where heat is 1). Because the four channels scroll at different speeds, the heat pattern churns and never quite repeats — this is what gives a star its slow "boiling" motion.
2. **Sunspots** — `sunspotTex` is a static greyscale mask. Its red channel, scaled by `sunspotPower`, blends the surface toward `sunspotColor` (a dark umbra colour), stamping cooler spots over the boiling emission. The mask is sampled at its own tiling, independent of the noise, so spots can be sized separately from the heat pattern.
3. **Face-on colour wash** — finally the surface is blended toward `rimColor` by `saturate(N·V)^rimPower × rimBlend`, where `N·V` is how directly the surface faces the camera. **Despite the "rim" naming this is not a limb glow** — the term is strongest where the surface faces the camera head-on (the centre of the disc) and falls to zero at the silhouette, so `rimColor` tints the *centre* of the disc and the pure heat colours show through cleanest at the edge. This is the reverse of the limb-hugging rim on the [atmospheric](/Syntax/Material/Scaled/Atmospheric) shaders.

The finished colour is emitted directly: a star lights itself and does not depend on an external sun. (It still has an ordinary Lambert lit term, so a star *can* be lit by another star, but in practice the emission dominates.)

One important runtime detail: the `rampMap` you set in config is **regenerated and replaced at load** by the `SunShaderController` Kopernicus attaches to the star (see Notes). It is the same component that drives the scrolling offsets each frame. So of the maps below, `noiseMap` and `sunspotTex` are the two you author; `rampMap` is effectively controlled by the star component, not the material node.

## Properties

The properties fall into three groups, which we shall take in turn:

1. **Heat-map surface** — the noise, the heat ramp, and the two emission colours it blends between.
2. **Sunspots** — the overlay mask and the colour it stamps.
3. **Face-on colour wash** — the camera-facing tint (named "rim" in the shader).

### Heat-map surface

| Property | Format | Description |
|----------|--------|-------------|
| `noiseMap` | File Path | Four-channel scrolling noise texture. Each channel is offset by its own runtime scroll value, looked up in `rampMap`, and the four results averaged into the per-pixel heat factor. Default "white". |
| `noiseMapScale` / `noiseMapOffset` | Vector2 | Tiling and offset of the noise texture. |
| `rampMap` | File Path | The 1-D heat colour ramp the noise is looked up in, read as a horizontal gradient from cool (left) to hot (right). **Overwritten at runtime** by the `SunShaderController` from its animation curves, so a config value has no lasting effect (see Notes). Default "white". |
| `rampMapScale` / `rampMapOffset` | Vector2 | Tiling and offset of the ramp. |
| `emitColor0` | Color | Emission colour where the heat factor is 0 (the cool end of the surface). Default (1, 1, 1, 1). |
| `emitColor1` | Color | Emission colour where the heat factor is 1 (the hot end of the surface). Default (1, 1, 1, 1). |

### Sunspots

| Property | Format | Description |
|----------|--------|-------------|
| `sunspotTex` | File Path | Static sunspot mask; only the **red** channel is read. Multiplied by `sunspotPower` it gives the blend weight toward `sunspotColor`, so the mask should be **black where there are no spots**. Note the shader's own default of "white" reads as a full-strength spot everywhere, so a real star must supply this map. |
| `sunspotTexScale` / `sunspotTexOffset` | Vector2 | Tiling and offset of the sunspot mask. |
| `sunspotColor` | Color | The (typically dark, cool) colour the surface is blended toward inside the sunspot mask. Default (0, 0, 0, 0). |
| `sunspotPower` | Decimal | Scalar multiplier on the sunspot mask's red channel before it is used as the blend weight. Values above 1 make spots more opaque; 0 disables the overlay. Default 1. |

### Face-on colour wash

The shader names these three properties "Rimlight", but as described above the effect tints the **camera-facing centre** of the disc and fades to nothing at the silhouette — it is not an atmospheric limb glow.

| Property | Format | Description |
|----------|--------|-------------|
| `rimColor` | Color | Colour the surface is blended toward where it faces the camera head-on. With the default white it gently brightens the centre of the disc. Default (1, 1, 1, 1). |
| `rimPower` | Decimal | Exponent applied to `saturate(N·V)`. Low values (the default) spread the wash broadly across the visible face; higher values pull it into a tighter spot at the very centre. Default 0.2. |
| `rimBlend` | Decimal | Overall strength of the wash — the final blend weight toward `rimColor` is `saturate(N·V)^rimPower × rimBlend`. Set to 0 to disable it entirely. Default 0.2. |

## Notes

* The surface animation is driven by Kopernicus's `SunShaderController` component (a `SharedSunShaderController`, the shared-material variant of the stock class), which it attaches to every star automatically. Each frame it writes four scrolling offset globals that scroll the four `noiseMap` channels at different rates, producing the boiling motion.
* That same component **regenerates `rampMap` at load**: it bakes a 256-pixel ramp texture from four animation curves and assigns it to the material, overwriting whatever `rampMap` the config set. The curves are not exposed through the `Material { }` node, so the heat ramp is effectively fixed by the star component rather than authored as a texture here.
* Unlike the planet shaders, this material has **no `opacity` and no `localLightDirection`** — a star is not cross-faded to a local (PQS) representation, so Kopernicus does not attach the scaled-space fader to it, and the shader has no rim that tracks the Sun.
* The shader uses the body's geometric normal directly and has **no normal map**, so it imposes no tangent requirement on the mesh.
* If the shader fails to compile on the target platform it falls back to Unity's `Diffuse` shader.
