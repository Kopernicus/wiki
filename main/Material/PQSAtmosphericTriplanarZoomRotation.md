---
layout: default
title: AtmosphericTriplanarZoomRotation
---

```
Body
{
    PQS
    {
        materialType = AtmosphericTriplanarZoomRotation
        Material
        {
            factor = // Int32, how many zoom levels there are (see video), fewer results in more discrete levels, 1 will softlock
            factorBlendWidth = // Single, smoothing applied to each transition between factors. goes as low as 0.05 (recommended) and 1. 1 would be a solid (instant) transition.
            rotationFactor = // Single, angle whch the texture rotates between factors, affects shadows
            albedoBrightness = single // terrain brightness
            saturation = // single
            contrast = single
            tintColor = Color
            specularColor = color
            steepPower = single
            steepTexStart = Single
            steepTexEnd = single
            steepTex = texture2d
            steepTexScale = vector2
            steepTexOffset = vector2
            steepBumpMap = texture2d
            steepBumpMapScale = vector2
            steepBumpMapOffset = vector2
            steepNearTiling = single
            steepTiling = single // far tiling
            lowTex = texture3d
            lowTexScale = vector2
            lowTexOffset = vector2
            lowTiling = single
            midTex = texture3d
            midTexScale = vector2
            midTexOffset = vector2
            midTiling = single // must be a lot bigger than it used to be
            midBumpMap = Texture2D
            midBumpMapScale = vector2
            midBumpMapOffset = vector2
            midBumpTiling = single // only the mid texture takes a bump map so it is recommended to isable low and high tiling. that way the terrain is entirely the mid texture (and thus has a bumpmap)
            highTex = texture3d
            highTexScale = vector2
            highTexOffset = vector2
            highTiling = single
            lowStart = single // transition start, 0
            lowEnd = single // transition end, 0.3
            highStart = single // transition start, 0.8
            highEnd = single // transition end, 1
            globalDensity = single
            fogColorRamp = texture2d, default is white
            fogColorRampScale = vector2
            fogColorRampOffset = vector2
            planetOpacity = single, default is 1
            oceanFogDistance = single, default is 1000
        }
    }
}
```