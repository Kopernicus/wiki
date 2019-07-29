---
layout: default
title: Body
---

The `Body` node is a wrapper node for the aspects of a body and contains the essential components for making a body. `Body` is a subnode of the `@Kopernicus` node.

**Subnodes**
- `Template`
- `Properties`
- `Orbit`
- `ScaledVersion`
- `Atmosphere`
- `PQS`
- `Ocean`
- `Rings`
- `Particles`
- `HazardousBody`
- `SpaceCenter`
- `Debug`
- `PostSpawnOrbit`

**Example**
```
Body
{
  name = Pear
  cacheFile = Fruits/Cache/Pear.bin
  barycenter = false
  identifier = Fruits/Pear
  randomMainMenuBody = true
  contractWeight = 15
  
  Template
  {
  ...
  }
  Properties
  {
  ...
  }
  Orbit
  {
  ...
  }
  ScaledVersion
  {
  ...
  }
  PQS
  {
  ...
  }
  Ocean
  {
  ...
  }
  Debug
  {
  ...
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|name|String|The name of the body.|
|cacheFile|File Path|The path to the cache file for the body.|
|barycenter|Boolean|Whether the body should act as a barycenter. Also makes the body unselectable.|
|cbNameLater|String|(Deprecated, use `Properties/displayName` to change the name instead.) Applies a name change after loading the body.|
|identifier|String|The Unique Body Identifier (UBI) for the body. Used in the [Interstellar Consortium](https://forum.kerbalspaceprogram.com/index.php?/topic/177439-kopernicus-interstellar-consortium/) and follows the format `System/Body`.|
|finalizeOrbit|Boolean|Whether the orbit of the body should be finalized.|
|randomMainMenuBody|Boolean|Whether the body should have a chance at being displayed on the Main Menu.|
|contractWeight|Integer|How often contracts should be generated for a body. Default is 30.|
