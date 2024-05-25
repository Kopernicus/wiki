PQS Mods, or Procedural Quad Sphere Modifiers, are various tools beyond applying a height map to a planet. They can modify an object's surface as well as its color, and they are even responsible for the various anomalies on planets. They can be used to both create a planet completely from scratch like [Hodor](https://github.com/Kopernicus/KopernicusExamples/blob/master/KopernicusExamples/Creating%20New%20Bodies/ProceduralBody/Hodor.cfg) or smooth out existing height maps and eliminate the grid-like pattern.  
They come in a wrapper node, `Mods { }`, which contains all of the PQSMod nodes and is a subnode of the `PQS { }` node.
Each PQSMod subnode contains `name`, `order`, and `enabled` keys, as described below.

|Property|Format|Description|
|--------|------|-----------|
|name|Text|The set name of the PQSMod. Used for differentiating between two PQSMods of the same type being used in the same config.|
|enabled|Boolean|Whether the PQSMod should be enabled.|
|order|Integer|The order that the PQSMod should be processed in. PQSMods are processed in increasing `order` value, so a mod with `order` 20 would be applied before a mod with order `100`.|

## PQSMods {#pqsmods} (This is a non-exhaustive list, so contributions are especially welcome in this area!)
+ [LandControl](/Syntax/PQSMods/LandControl)
+ [HeightColorMap](/Syntax/PQSMods/HeightColorMap)
+ [HeightColorMap2](/Syntax/PQSMods/HeightColorMap2)
+ [VertexColorMap](/Syntax/PQSMods/VertexColorMap)
+ [VertexColorMapBlend](/Syntax/PQSMods/VertexColorMapBlend)
+ [VertexHeightMap](/Syntax/PQSMods/VertexHeightMap)
+ [VertexHeightNoise](/Syntax/PQSMods/VertexHeightNoise)
+ [VertexHeightNoiseVertHeightCurve2](/Syntax/PQSMods/VertexHeightNoiseVertHeightCurve2)
+ [VertexSimplexHeight](/Syntax/PQSMods/VertexSimplexHeight)
+ [VertexSimplexHeightAbsolute](/Syntax/PQSMods/VertexSimplexHeightAbsolute)
+ [VertexSimplexNoiseColor](/Syntax/PQSMods/VertexSimplexNoiseColor)
