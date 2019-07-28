---
layout: default
title: VertexHeightMap
subtitle: Reaching new heights, one map at a time!
---

The `VertexHeightMap { }` PQSMod is a mod that sets the terrain to a given height map.

**Example**
```
PQS
{
	Mods
	{
		VertexHeightMap
		{
			map = Fruits/PluginData/Banana_heightmap.dds
			offset = -1000
			deformity = 12500
			scaleDeformityByRadius = false
			order = 20
			enabled = true
		}
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|map|File Path|The texture containing the height map in greyscale. Black is the `offset` height, and White is the `deformity` height.|
|offset|Double|The offset of the height map from the body's radius.|
|deformity|Double|The deformity of the height map (difference between lowest and highest point).|
|scaleDeformityByRadius|Boolean|Whether to multiply the deformity by the planet's radius (in case the deformity is in radii).|