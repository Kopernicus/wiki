---
layout: default
title: Ocean
---

The `Ocean { }` subnode contains all of the information needed to produce an ocean for the specified body.

**Subnodes**
* Material { }
* FallbackMaterial { }
* HazardousOcean { }
* Fog { }

**Example**
```
Ocean
{
  maxQuadLengthsPerFrame = 0.03
  minLevel = 2
  maxLevel = 12
  minDetailDistance = 8
  oceanColor = #8a0303
  Material
  {
  ...
  }
  FallbackMaterial
  {
  ...
  }
  Mods
  {
  ...
  }
  Fog
  {
  ...
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|ocean|Boolean|Whether the ocean is enabled.|
|oceanColor|Color|The color of the ocean on the map.|
|oceanHeight|Double|The height of the ocean in meters.|
|density|Double|The density of the ocean in g/m3. 1 is the density of actual water.|
|minLevel|Integer|The PQS minimum level of detail.|
|maxLevel|Integer|The PQS maximum level of detail.|
|minDetailDistance|Double|The minimum detail distance of ???.|
|maxQuadLengthsPerFrame|Single|?|