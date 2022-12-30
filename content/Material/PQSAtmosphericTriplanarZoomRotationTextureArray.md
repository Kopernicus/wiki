---
layout: default
title: AtmosphericTriplanarZoomRotationTextureArray
---

A PQS Material utilizing 1.9's "ultra" shader.

**Example**
```
PQS
{
    ...
    materialType = AtmosphericTriplanarZoomRotationTextureArray
    Material
    {
        colorLerpModifier = 1
        atlasTiling = 100000
        AtlasTex
        {
            ...
        }
        NormalTex
        {
            ...
        }
        
        factor = 10
        factorBlendWidth = 0.1
        factorRotation = 30
        saturation = 1
        contrast = 1
        tintColor = 1,1,1,0 // Color UNSaturation (Alpha = factor)
        specularColor = 0.2,0.2,0.2,0.2
        albedoBrightness = 2
        steepPower = 1
        steepTexStart = 20000
        steepTexEnd = 30000
        steepTex = GameData/Fruits/PluginData/blueberry_steeptex.dds
        steepTexScale = 1,1
        steepTexOffset = 0,0
        steepBumpMap = GameData/Fruits/PluginData/blueberry_steepbump.dds
        steepBumpMapScale = 1,1
        steepBumpMapOffset = 0,0
        steepNearTiling = 1
        steepTiling = 1
        
        globalDensity = 1 // AP Global Density
        fogColorRamp = GameData/Fruits/PluginData/blueberry_fogramp.dds
        fogColorRampScale = 1,1
        fogColorRampOfset = 0,0
        planetOpacity = 1
        oceanFogDistance = 1000
    }
    ...
}
```
