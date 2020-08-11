---
layout: default
title: PQS Mods
---

PQS Mods, or Procedural Quad Sphere Modifiers, are various tools beyond applying a height map to a planet. They can modify an object's surface as well as its color, and they are even responsible for the various anomalies on planets. They can be used to both create a planet completely from scratch like [Hodor](https://github.com/Kopernicus/KopernicusExamples/blob/master/KopernicusExamples/Creating%20New%20Bodies/ProceduralBody/Hodor.cfg) or smooth out existing height maps and eliminate the grid-like pattern.  
They come in a wrapper node, `Mods { }`, which contains all of the PQSMod nodes and is a subnode of the `PQS { }` node.
Each PQSMod subnode contains `name`, `order`, and `enabled` keys, as described below.

|Property|Format|Description|
|--------|------|-----------|
|name|String|The set name of the PQSMod. Used for differentiating between two PQSMods of the same type being used in the same config.|
|enabled|Boolean|Whether the PQSMod should be enabled.|
|order|Integer|The order that the PQSMod should be processed in. PQSMods are processed in increasing `order` value, so a mod with `order` 20 would be applied before a mod with order `100`.|

**PQSMods**
+ [LandControl]({{ site.baseurl }}{% link main/PQSMods/LandControl/LandControlLanding.md %})
+ [HeightColorMap]({{ site.baseurl }}{% link main/PQSMods/HeightColorMap.md %})
+ [HeightColorMap2]({{ site.baseurl }}{% link main/PQSMods/HeightColorMap2.md %})
+ [VertexColorMap]({{ site.baseurl }}{% link main/PQSMods/VertexColorMap.md %})
+ [VertexColorMapBlend]({{ site.baseurl }}{% link main/PQSMods/VertexColorMapBlend.md %})
+ [VertexHeightMap]({{ site.baseurl }}{% link main/PQSMods/VertexHeightMap.md %})
+ [VertexHeightNoise]({{ site.baseurl }}{% link main/PQSMods/VertexHeightNoise.md %})
+ [VertexHeightNoiseVertHeightCurve2]({{ site.baseurl }}{% link main/PQSMods/VertexHeightNoiseVertHeightCurve2.md %})
+ [VertexSimplexHeight]({{ site.baseurl }}{% link main/PQSMods/VertexSimplexHeight.md %})
+ [VertexSimplexHeightAbsolute]({{ site.baseurl }}{% link main/PQSMods/VertexSimplexHeightAbsolute.md %})
+ [VertexSimplexNoiseColor]({{ site.baseurl }}{% link main/PQSMods/VertexSimplexNoiseColor.md %})
