---
layout: default
title: AtmosphericTriplanarZoomRotation
---
https://discordapp.com/channels/609759014384828446/609759014384828449/678797826255487008
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
            saturation = single, modfies the ground texture saturation (scale of 0-1)
            contrast = single, modfies the ground texture brightness (scale of 0-1)
            tintColor = Color, sets the ground texture's color
            specularColor = color, sets the shine color of the ground texture CONFIRM THIS
            steepPower = single, stregth of cliff textures on steep slopes
            steepTexStart = Single, distance from camera that cliff textures start to fade out at. this should be closer to the camera than steepTexEnd
            steepTexEnd = single, distance from camera that cliff textures finish fading out at. this should be farther from the camera than steepTexStart.
            steepTex = texture2d, the texture to use
            steepTexScale = vector2, does nothing?? CONFIRM
            steepTexOffset = vector2 ????
            steepBumpMap = texture2d bump map for the steep texture
            steepBumpMapScale = vector2 same as above
            steepBumpMapOffset = vector2 same as above
            steepNearTiling = single, how many times to tile the texture (when near?)
            steepTiling = single // far tiling (see above)
            lowTex = texture3d // for all these just see above
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