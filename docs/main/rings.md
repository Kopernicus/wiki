<!-- TITLE: Rings -->
<!-- SUBTITLE: Rosie, you have been surrounded. -->

# Explanation
TBA

# Example

```text
@Kopernicus
{
  Body
  {
    Rings
    {
      Ring
      {
        innerRadius = Single // Radius of the innermost edge of the ring, measured in meters from the CENTER of the body.
        outerRadius = Single // Radius of the outermost edge of the ring, measured in meters from the CENTER of the body.
        InnerRadiusMultiplier = FloatCurve // A multiplier for innerRadius, defined by the angle in degrees and the scaling factor.[1]
        OuterRadiusMultiplier = FloatCurve // A multiplier for outerRadius, defined by the angle in degrees and the scaling factor.[1]
        thickness = Single // Thickness of the ring in milliradii.[2]
        angle = Single // Angle of the ring plane to the planet's equator. (inclination)
        longitudeOfAscendingNode = Single // Angle between the absolute reference direction and the ascending node. It works just like the corresponding property on celestial bodies. 
        texture = Texture2D // The texture applied to the ring.
        color = Color // Unknown, experiments are welcome!
        lockRotation = Boolean // SHould be lock the rotatin of our ring?
        rotationPeriod = Single // Rotation period of the ring in seconds. Defaults to that of the parent body if lockRotation is false. If true, the ring will stand perfectly still
        unlit = Boolean // Whether an "Unlit" shader should be applied to the ring
        useNewShader = Boolean // Use the custom ring shader instead of the built-in Unity shaders
        penumbraMultiplier = Single // Parameter of custom ring shader
        steps = Integer // Number of vertices that the ring mesh has
        tiles = Integer // Nummber of times the texture should be wrapped around the ring
        innerShadeTexture = Texture2D // This texture's opaque pixels cast shadows on our inner surface.
        innerShadeTiles - Integer // How much is the innerShadeTexture tiled? optional.
        innerShadeRotationPeriod = Single // How many seconds does it take for the inner shade texture to rotate once
      }
    }
  }
}
```
