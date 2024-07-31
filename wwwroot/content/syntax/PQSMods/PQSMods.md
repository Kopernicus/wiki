PQS Mods, or Procedural Quad Sphere Modifiers, are various tools beyond applying a height map to a planet. They can modify an object's surface as well as its color, and they are even responsible for the various anomalies on planets. They can be used to both create a planet completely from scratch like [Hodor](https://github.com/Kopernicus/KopernicusExamples/blob/master/KopernicusExamples/Creating%20New%20Bodies/ProceduralBody/Hodor.cfg) or smooth out existing height maps and eliminate the grid-like pattern.  
They come in a wrapper node, `Mods { }`, which contains all of the PQSMod nodes and is a subnode of the `PQS { }` node.
Each PQSMod subnode contains `name`, `order`, and `enabled` keys, as described below.

|Property|Format|Description|
|--------|------|-----------|
|name|Text|The set name of the PQSMod. Used for differentiating between two PQSMods of the same type being used in the same config.|
|enabled|Boolean|Whether the PQSMod should be enabled.|
|order|Integer|The order that the PQSMod should be processed in. PQSMods are processed in increasing `order` value, so a mod with `order` 20 would be applied before a mod with order `100`.|

## PQSMods {#PQSMods} (This is a non-exhaustive list, so contributions are especially welcome in this area!)

Note: **name** is the name you should be using in your configs. **Internal Name** is the name of the C# class that corresponds to this PQSMod. **To remove a specific mod in the Template node, refer to it by its internal name**.

|Name|Internal Name|
|----|-------------|
|[AerialPerspectiveMaterial](/Syntax/PQSMods/AerialPerspectiveMaterial)|PQSMod_AerialPerspectiveMaterial|
|[HeightColorMap](/Syntax/PQSMods/HeightColorMap)|PQSMod_HeightColorMap|
|[HeightColorMap2](/Syntax/PQSMods/HeightColorMap2)|PQSMod_HeightColorMap2|
|[LandControl](/Syntax/PQSMods/LandControl)|PQSLandControl|
|[VertexColorMap](/Syntax/PQSMods/VertexColorMap)|PQSMod_VertexColorMap|
|[VertexColorMapBlend](/Syntax/PQSMods/VertexColorMapBlend)|PQSMod_VertexColorMapBlend|
|[VertexHeightMap](/Syntax/PQSMods/VertexHeightMap)|PQSMod_VertexHeightMap|
|[VertexHeightNoise](/Syntax/PQSMods/VertexHeightNoise)|PQSMod_VertexHeightNoise|
|[VertexHeightNoiseVertHeightCurve2](/Syntax/PQSMods/VertexHeightNoiseVertHeightCurve2)|PQSMod_VertexHeightNoiseVertHeightCurve2|
|[VertexSimplexHeight](/Syntax/PQSMods/VertexSimplexHeight)|PQSMod_VertexSimplexHeight|
|[VertexSimplexHeightAbsolute](/Syntax/PQSMods/VertexSimplexHeightAbsolute)|PQSMod_VertexSimplexHeightAbsolute|
|[VertexSimplexNoiseColor](/Syntax/PQSMods/VertexSimplexNoiseColor)|PQSMod_VertexSimplexNoiseColor|

## Community PQSMods {#Community_PQSMods}
|Name|Internal Name|
|----|-------------|
|[VertexMitchellNetravaliHeightMap](/Syntax/PQSMods/Community/VertexMitchellNetravaliHeightMap)|PQSMod_VertexMitchellNetravaliHeightMap|
