---
layout: default
title: VertexHeightMap
---

The `VertexHeightMap` PQSMod is a mod that *adds* a given height map to the terrain. This means that height mods are additive, i.e. heightmaps don't set a fixed height.

**Notes on Heightmaps**
A common desire for planets with oceans is to set the ocean to a certain height relative to the terrain to make spots like shorelines be in the correct places. This is simple to do in 2 main steps:

1. Make sure your heghtmap is normalized (darkest value is 0 and highest is 255).
2. Set `offset` in VertexHeightMap to `-1 * (deformity * travel)`, where `travel` is the percentage of the deformity up from the lowest point of the height map that you want the sea surface to be at. For example, a `travel` of 0.5 would mean the ocean surface is at 50% grey on the height map. 

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
|offset|Decimal|The offset of the height map.|
|deformity|Decimal|The deformity of the height map (difference between lowest and highest point).|
|scaleDeformityByRadius|Boolean|Unknown|
