---
layout: default
title: ScaledVersion
---

The `ScaledVersion { }` node in a configuration file for Kopernicus describes a less-detailed model of your planet that appears in the map view and from large distances.


**Subnodes**
* [Material { }]({{ "/main/ScaledVersion/Material.html" | relative_url }}) = Updates to textures and atmosphere rims.
* [OnDemand { }]({{ "/main/ScaledVersion/OnDemand.html" | relative_url }}) = Used for textures that should be loaded OnDemand.
* [Light { }]({{ "/main/ScaledVersion/Light.html" | relative_url }}) = Used for making stars.
* [Coronas { }]({{ "/main/ScaledVersion/Corona.html" | relative_url }}) = Used for making stars.

**Example**
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
|type|String|Either `Vacuum`, `Atmospheric`, or `Star`, depending on whether the body is a star or a planet/moon that has an atmosphere or not|
|fadeStart|Double|Altitude, in meters, at which the transition to ScaledSpace starts|
|fadeEnd|Double|Altitude, in meters, at which the transition to ScaledSpace ends|
|sphericalModel|Boolean|?Does? Default is `false`.|
|deferMesh|Boolean|?Does? Default is `false`.|
|invisible|Boolean|Whether the ScaledSpace should be invisible. Also sets deferMesh to the same value. Defaults to `false`.|
