---
layout: default
title: ScaledVersion
subtitle: The view of Earth from space looks... like a gray sphere?!
---

The `ScaledVersion { }` node in a configuration file for Kopernicus describes a less-detailed model of your planet that appears in the map view and from large distances.


**Subnodes**
<<<<<<< HEAD
* Material - Updates to textures and atmosphere rims. (WIP)
* [OnDemand](/main/ScaledVersion/OnDemand.html) - Used for textures that should be loaded OnDemand.
* [Light](/main/ScaledVersion/Light.html) - Used for making stars.
* [Coronas](/main/ScaledVersion/Coronas.html) - Used for making stars.
=======
* [Material { }]({{ "/main/ScaledVersion/Material.html" | relative_url }}) = Updates to textures and atmosphere rims.
* [OnDemand { }]({{ "/main/ScaledVersion/OnDemand.html" | relative_url }}) = Used for textures that should be loaded OnDemand.
* [Light { }]({{ "/main/ScaledVersion/Light.html" | relative_url }}) = Used for making stars.
* [Coronas { }]({{ "/main/ScaledVersion/Corona.html" | relative_url }}) = Used for making stars.
>>>>>>> 6cbc7b0905671099df9f086e8d826c504d710fec

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
|sphericalModel|Boolean|Whether the ScaledSpace should be represented as a sphere instead of the actual PQS. Default is `false`.|
|deferMesh|Boolean|Whether to not generate a new mesh every time KSP is run. Default is `false`.|
|invisible|Boolean|Whether the ScaledSpace should be invisible. Also sets deferMesh to the same value. Defaults to `false`.|
