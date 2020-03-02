---
layout: default
title: ScatterMaterialType
---

The contents of the `Material {}` node can very greatly and depend on the value assigned to `materialType`

## Diffuse
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture2D, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
}
```

## BumpedDiffuse
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture2D, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	bumpMap = texture2d, default is "bump"
	bumpMapScale = vector2
	bumpMapOffset = vector2
}
```

## DiffuseDetail
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture2D, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	detail = texture2d, default is "gray"
	detailScale = vector2
	detailOffset = vector2
}
```

## DiffuseWrapped
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture2D, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	diff = Single, default is 2
}
```

## CutoutDiffuse
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture2D, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	cutoff = Single, default is 0.5
}
```

## AerialCutout
```
Material
{
	color = Color, default is 1,1,1,1
	mainTex = Texture2D, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	texCutoff = Single, default is 0.5
	fogColor = Color, default is 0,0,1,1
	heightFallOff = single, default is 1
	globalDensity = single, default is 1
	atmsophereDepth = 
}
```