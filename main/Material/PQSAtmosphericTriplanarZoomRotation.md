---
layout: default
title: AtmosphericTriplanarZoomRotation
---
https://discordapp.com/channels/609759014384828446/609759014384828449/678797826255487008 // fog examples in gregs server
```
Body
{
    PQS
    {
        materialType = AtmosphericTriplanarZoomRotation
        Material
        {
            factor = // Integer, how many zoom levels there are (see video), fewer results in more discrete levels, 1 will softlock
            factorBlendWidth = // Float, smoothing applied to each transition between factors. goes as low as 0.05 (recommended) and 1. 1 would be a solid (instant) transition.
            rotationFactor = // Float, angle whch the texture rotates between factors, affects shadows
            albedoBrightness = Float // terrain brightness
            saturation = Float, modfies the ground texture saturation (scale of 0-1)
            contrast = Float, modfies the ground texture brightness (scale of 0-1)
            tintColor = Color, sets the ground texture's Color
            specularColor = Color, sets the shine Color of the ground texture CONFIRM THIS
            steepPower = Float, stregth of cliff textures on steep slopes
            steepTexStart = Float, distance from camera that cliff textures start to fade out at. this should be closer to the camera than steepTexEnd
            steepTexEnd = Float, distance from camera that cliff textures finish fading out at. this should be farther from the camera than steepTexStart.
            steepTex = Texture, the texture to use
            steepTexScale = X,Y, does nothing?? CONFIRM
            steepTexOffset = X,Y ????
            steepBumpMap = Texture bump map for the steep texture
            steepBumpMapScale = X,Y same as above
            steepBumpMapOffset = X,Y same as above
            steepNearTiling = Float, how many times to tile the texture (when near?)
            steepTiling = Float // far tiling (see above)
            lowTex = texture3d // for all these just see above
            lowTexScale = X,Y
            lowTexOffset = X,Y
            lowTiling = Float
            midTex = texture3d
            midTexScale = X,Y
            midTexOffset = X,Y
            midTiling = Float // must be a lot bigger than it used to be
            midBumpMap = Texture
            midBumpMapScale = X,Y
            midBumpMapOffset = X,Y
            midBumpTiling = Float // only the mid texture takes a bump map so it is recommended to isable low and high tiling. that way the terrain is entirely the mid texture (and thus has a bumpmap)
            highTex = Texture
            highTexScale = X,Y
            highTexOffset = X,Y
            highTiling = Float
            lowStart = Float // transition start, 0
            lowEnd = Float // transition end, 0.3
            highStart = Float // transition start, 0.8
            highEnd = Float // transition end, 1
            globalDensity = float // the negative reciprocal of how "quickly" the fog effect "builds up". The smaller this is, the quicker
            fogColorRamp = Texture, default is white, defines the colors that the fog cycles through as it goes throughout the day.
            fogColorRampScale = X,Y
            fogColorRampOffset = X,Y
            planetOpacity = Float, default is 1
            oceanFogDistance = Float, default is 1000
        }
    }
}
```