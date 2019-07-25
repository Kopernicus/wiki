---
layout: default
title: OnDemand
subtitle: Quite un-demanding, if you ask me.
---

The `OnDemand { }` subnode of the `ScaledVersion { }` node tells Kopernicus to use OnDemand loading for the textures specified inside the subnode. The fields set inside this subnode are exactly the same as their equivalents in the [`Material { }` subnode]({{ "main/ScaledVersion/Material.html" | relative_url }}) of the `ScaledVersion { }` node.

**Example**
```
ScaledVersion
{
	...
	OnDemand
	{
		texture = Fruits/PluginData/Banana_colormap.dds
		normals = Fruits/PluginData/Banana_normalmap.dds
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|texture|File Path|(Also `mainTex`) The texture containing the color map for the body.|
|normals|File Path|(Also `bumpMap`) The texture containing the normal map for the body.|