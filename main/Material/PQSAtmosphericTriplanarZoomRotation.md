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
            factorBlendWidth = // Single CHECK WITH GAMESLINX EXAMPLE
            factorRotation = // Single
            saturation = // single
            contrast = single
            tintColor = Color
            specularColor = color
            albedoBrightness = single
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
            midTiling = single
        }
    }
}
```