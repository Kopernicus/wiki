---
layout: default
title: ScaledVersion
---

The `ScaledVersion { }` node in a configuration file for Kopernicus describes a less-detailed model of your planet that appears in the map view and from large distances.


**Subnodes**
* [Material { }]({{ site.baseurl }}{% link main/ScaledVersion/Material.md %}) = Updates to textures and atmosphere rims.
* [OnDemand { }]({{ site.baseurl }}{% link main/ScaledVersion/OnDemand.md %}) = Used for textures that should be loaded OnDemand.
* [Light { }]({{ site.baseurl }}{% link main/ScaledVersion/Light.md %}) = Used for making stars.
* [Coronas { }]({{ site.baseurl }}{% link main/ScaledVersion/Coronas.md %}) = Used for making stars.

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
|type|String|Either _Vacuum_ or _Atmospheric_, depending on whether the body has an atmosphere or not|
|fadeStart|Double|Altitude, in meters, at which the transition to ScaledSpace starts|
|fadeEnd|Double|Altitude, in meters, at which the transition to ScaledSpace ends|
|sphericalModel|Boolean|?Does?|
|deferMesh|Boolean|?Does?|
