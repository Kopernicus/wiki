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
	diff = Float, default is 2
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
	cutoff = Float, default is 0.5
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
	texCutoff = Float, default is 0.5
	fogColor = Color, default is 0,0,1,1
	heightFallOff = single, default is 1
	globalDensity = single, default is 1
	atmosphereDepth = Float, default is 1
}
```

## Standard
```
Material
{
	color = Color, default is 1,1,1,1
    mainTex = Texture2D, default is white
	mainTexScale = vector2
	mainTexOffset = vector2
	cutoff = Float, default is 0.5, Alpha Cutoff
    glossiness = Float, default is 0.5, smoothness
    glossMapScale = Float, default is 1
    smoothnessTextureChannel = TextureChannel, default is 0
    metallic = Float, default is 0
    metallicGlossMapScale = Vector2
    metallicGlossMapOffset = Vector2
    specularHighlights = Boolean, default is true
    glossyReflections = Boolean, default is true
    bumpScale = Float, default is 1
    bumpMap = texture2d, default is "bump"
	bumpMapScale = vector2
	bumpMapOffset = vector2
    parallax = Float, default is 0.02, height scale
    parallaxMap = texture2d, default is black
    parallaxMapOffset = Vector2
    occlusionStrength = Float, default is 1
    occlusionMap = Texture2D, default is white
    occlusionMapScale = Vector2
    occlusionMapOffset = Vector2
    emissionColor = Color, default is 0,0,0,1
    emissionMap = Texture2D, default is white
    emissionMapScale = Vector2
    emissionMapOffset = Vector2
    detailMask = Texture2D, default is white
    detailMaskOffset = Vector2
    detailMaskScale = Vector2
    detailAlbedoMap = Texture2D, default is grey
    detailAlbedoMapOffset = Vector2
    detailAlbedoMapScale = Vector2
    detailNormalMap = Texture2D, default is bump
    detailNormalMapScale = Vector2
    detailNormalMapOffset = Vector2
    UVSec = UvSet, default is 0
    mode = BlendMode, default is 0
    srcBlend = Float, default is 1
    dstBlend = Float, default is 0
    ZWrite = Float, default is 0
}
```
