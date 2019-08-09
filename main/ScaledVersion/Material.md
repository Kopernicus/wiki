---
layout: default
title: Material
subtitle: Ay-yi-yi, textures galore!
---

The `Material { }` is a very complex subnode of the `ScaledVersion { }` node. It regulates the body's ScaledSpace material, and thus, is a crucial element in the formation of a body. Unfortunately, the subnode comes in three different formats, each depending on the value set in `ScaledVersion/type`. Therefore, three different examples will be given and types will be marked in the table.

**Example**
```
// Vacuum planet/moon
Material
{
	texture = Fruits/PluginData/Banana_colormap.dds
	normals = Fruits/PluginData/Banana_normalmap.dds
	color = 0.6, 0.7, 0.2, 1
	specColor = 0.5, 0.8, 0.7, 1
	shininess = 0.15
	
	resourceMap = Fruits/PluginData/Banana_resourcemap.dds
}

// Atmospheric planet/moon
Material
{
	texture = Fruits/PluginData/Blueberry_colormap.dds
	normals = Fruits/PluginData/Blueberry_normalmap.dds
	color = 0.2, 0.3, 0.65, 1
	specColor = 0.15, 0.2, 0.5, 1
	shininess = 0.215
	
	rimPower = 2.25
	rimBlend = 0.875
	Gradient
	{
		0.0 = #4dcff0
		0.25 = #13a1ed
		0.5 = #1f67b5
		0.75 = #132abf
		1.0 = #160c75
	}
	
	resourceMap = Fruits/PluginData/Blueberry_resourcemap.dds
}

// Star
Material
{
	noiseMap = Fruits/PluginData/Watermelon_noisemap.dds
	emitColor0 = 0.245, 0.825, 0.675, 1
	emitColor1 = 0.36275, 0.75, 0.47365, 1
	sunspotTex = Fruits/PluginData/Watermelon_sunspotmap.dds
	sunspotPower = 0.75
	sunspotColor = 0.2875, 0.315, 0.0565, 1
	rimColor = 0.2875, 0.9085, 0.75, 1
	rimPower = 0.7925
	rimBlend = 2.25
}
```

NOTE: `Vacuum = "V"`, `Atmospheric = "A"`, and `Star = "S"`.

|Property|Format|Applies to Type(s)|Description|
|--------|------|------------------|-----------|
|color|Color|V, A|The main color of the body. Default is (1, 1, 1, 1).|
|specColor|Color|V, A|The specular color. Default is (0.5, 0.5, 0.5, 1).|
|shininess|Single|V, A|The shininess of the planet. Default is 0.078125.|
|texture|File Path|V, A|(Also `mainTex`) The texture containing the ScaledSpace color map. Default is "White".|
|mainTexScale|Vector2|V, A|The scale of the color map.|
|mainTexOffset|Vector2|V, A|The offset of the color map.|
|normals|File Path|V, A|(Also `bumpMap`) The texture containing the normal map. Default is "bump".|
|bumpMapScale|Vector2|V, A|The scale of the normal map.|
|bumpMapOffset|Vector2|V, A|The offset of the normal map.|
|opacity|Single|V, A|The opacity of the ScaledSpace material. Default is 1.|
|rampMap|File Path|S|The texture containing the star's ramp map. Default is "White".|
|rampMapScale|Vector2|S|The scale of the ramp map.|
|rampMapOffset|Vector2|S|The offset of the ramp map.|
|noiseMap|File Path|S|The texture containing the star's noise map. Default is "White".|
|noiseMapScale|Vector2|S|The scale of the noise map.|
|noiseMapOffset|Vector2|S|The offset of the noise map.|
|emitColor0|Color|S|The first emission color. Default is (1, 1, 1, 1).|
|emitColor1|Color|S|The second emission color. Default is (1, 1, 1, 1).|
|sunspotTex|File Path|S|The texture containing the star's sunspots. Default is "White".|
|sunspotTexScale|Vector2|S|The scale of the sunspot texture.|
|sunspotTexOffset|Vector2|S|The offset of the sunspot texture.|
|sunspotPower|Single|S|The power of the sunspots. Default is 1.|
|sunspotColor|Color|S|The color of the sunspots. Default is (0, 0, 0, 0).|
|rimColor|Color|S|The rim color. Default is (1, 1, 1, 1).|
|rimPower|Single|A, S|How far from the rim of the sphere/planet the atmosphere rim will go. The lower the number, the greater the coverage. The higher the number, the closer to the edge of the sphere it will cling to. Default for "A" is 3, "S" is 0.2.|
|rimBlend|Single|A, S|The blend between the atmosphere and the rim. Default for "A" is 1, "S" is 0.2.|
|rimColorRamp|File Path|A|The texture containing the atmosphere's rim color ramp. Default is "White".|
|rimColorRampScale|Vector2|A|The scale of the rim color ramp.|
|rimColorRampOffset|Vector2|A|The offset of the rim color ramp.|
|Gradient|Gradient|A|The `rimColorRamp`, but explicitly defined through a gradient. The left value is the position on the gradient from 0 to 1, and the right value is the color at that position.|
|localLightDirection|Vector4|A|The direction of the local light. Default is (1, 0, 0, 0).|
|resourceMap|File Path|V, A|The texture containing the body's resource map. Default is "Black".|
|resourceMapScale|Vector2|V, A|The scale of the resource map.|
|resourceMapOffset|Vector2|V, A|The offset of the resource map.|