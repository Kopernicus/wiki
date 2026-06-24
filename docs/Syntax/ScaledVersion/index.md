# ScaledVersion

The `ScaledVersion { }` node in a configuration file for Kopernicus describes a less-detailed model of your planet that appears in the map view and from large distances.

## Subnodes
* [OnDemand { }](/Syntax/ScaledVersion/OnDemand) = Used for textures that should be loaded OnDemand.
* [Light { }](/Syntax/ScaledVersion/Light) = Used for making stars.
* [Coronas { }](/Syntax/ScaledVersion/Corona) = Used for making stars.

## Material
There are a number of different materials KSP provides for scaled space bodies. These control how the scaled space model of the body looks and each corresponds to a different shader. You can choose among the materials that KSP provides by using the `type` property, or you can explicitly set `shader = X` to override the shader.

The types you can use are:
- [`Vacuum`](./../Material/Scaled/Vacuum.md)
- [`Atmospheric`](./../Material/Scaled/Atmospheric.md)
- [`AtmosphericStandard`](./../Material/Scaled/AtmosphericStandard.md)
- [`GasGiant`](./../Material/Scaled/GasGiant.md)
- [`Star`](./../Material/Scaled/Star.md)
- `Custom` - this one needs you to provide your own shader.

## Example
```cfg
ScaledVersion
{
  type = Vacuum
  fadeStart = 70000
  fadeEnd = 80000
  Material
  {
  ...
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|type|Text|Either `Vacuum`, `Atmospheric`, or `Star`, depending on whether the body is a star or a planet/moon that has an atmosphere or not. (Can also use `AtmosphericStandard`, but function is not yet known.)|
|fadeStart|Decimal|Altitude, in meters, at which the transition to ScaledSpace starts.|
|fadeEnd|Decimal|Altitude, in meters, at which the transition to ScaledSpace ends. Should be higher than fadeStart.|
|sphericalModel|Boolean|Whether the ScaledSpace should be represented as a sphere instead of the actual PQS. Default is `false`.|
|deferMesh|Boolean|Whether to not generate a new mesh every time KSP is run. Default is `false`.|
|invisible|Boolean|Whether the ScaledSpace should be invisible. Also sets deferMesh to the same value. Defaults to `false`.|
