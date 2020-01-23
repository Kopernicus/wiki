---
layout: default
title: VertexHeightMap16
subtitle: A heightmap PQSMod that lets you use 16-bpp encoded textures.
---

A replacement for the `VertexHeightMap` PQSMod that allows you to use textures encoded with 16 bpp. [What is "bpp"?](https://en.wikipedia.org/wiki/Color_depth)
Any textures that are to be used with VertexHeightMap16 need to be passed through a converter, which can be found along with VHM16 and the other KopernicusExpansion packages at [https://github.com/StollD/KopernicusExpansion-Continued/releases](https://github.com/StollD/KopernicusExpansion-Continued/releases)
In order to work with the converter the textures:
* Need to be greyscale .raw files with only one channel containing data
* Need to have a width/height ratio of 2:1
* Need to have been exported with Macintosh byte order

 
**Example**
```
PQS
{
  Mods
  {
    VertexHeightMap16
    {
      map = Fruits/PluginData/Tomato_heightmap16.dds
      offset = -5000
      deformity = 15000
      scaleDeformityByRadius = false
      enabled = true
      order = 20
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|map|File Path|A 16-bpp encoded texture containing the body's heightmap in greyscale. Black is the `offset` height, and White is the `deformity + offset` height.|
|offset|Double|The offset of the height map from the body's radius.|
|deformity|Double|The deformity of the height map (difference between lowest and highest point).|
|scaleDeformityByRadius|Boolean|Whether to multiply the deformity by the planet's radius.|