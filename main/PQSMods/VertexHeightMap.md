---
layout: default
title: VertexHeightMap
subtitle: Reaching new heights, one map at a time
---

The `VertexHeightMap` PQSMod is a mod that *adds* a given height map to the terrain. This means that height mods are additive, i.e. heightmaps don't set a fixed height.

**Example**
```
PQS
{
  Mods
  {
    VertexHeightMap
    {
      map = Fruits/PluginData/Banana_heightmap-base.dds
      offset = -1000
      deformity = 12500
      scaleDeformityByRadius = false
      order = 20
      enabled = true
    }
    VertexHeightMap
    {
      map = Fruits/PluginData/Banana_heightmap-add.dds
      offset = 0
      deformity = 2000
      scaleDeformityByRadius = false
      order = 21
      enabled = true
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|map|File Path|The texture containing the height map in greyscale. Black is the `offset` height, and White is the `deformity + offset` height.|
|offset|Float|The offset of the height map from the body's radius.|
|deformity|Float|The deformity of the height map (difference between lowest and highest point).|
|scaleDeformityByRadius|Boolean|Whether to multiply the deformity by the planet's radius (in case the deformity is in radii or something).|
