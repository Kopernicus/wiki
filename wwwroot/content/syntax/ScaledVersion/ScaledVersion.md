The `ScaledVersion { }` node in a configuration file for Kopernicus describes a less-detailed model of your planet that appears in the map view and from large distances.

## Subnodes {#Subnodes}
* [Material { }]({{ site.baseurl }}{% link content/ScaledVersion/Material.md %}) = Updates to textures and atmosphere rims.
* [OnDemand { }]({{ site.baseurl }}{% link content/ScaledVersion/OnDemand.md %}) = Used for textures that should be loaded OnDemand.
* [Light { }]({{ site.baseurl }}{% link content/ScaledVersion/Light.md %}) = Used for making stars.
* [Coronas { }]({{ site.baseurl }}{% link content/ScaledVersion/Corona.md %}) = Used for making stars.

## Example {#Example}
```
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
|fadeEnd|Decimal|Altitude, in meters, at which the transition to ScaledSpace ends.|
|sphericalModel|Boolean|Whether the ScaledSpace should be represented as a sphere instead of the actual PQS. Default is `false`.|
|deferMesh|Boolean|Whether to not generate a new mesh every time KSP is run. Default is `false`.|
|invisible|Boolean|Whether the ScaledSpace should be invisible. Also sets deferMesh to the same value. Defaults to `false`.|
