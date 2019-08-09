---
layout: default
title: Emissive FX
subtitle: Ocean? More like Glow-cean!
---

The EmissiveFX portion of Kopernicus Expansion allows you to configure an emission, or glow, for the specified body.

**Example**
```
Body
{
  ScaledVersion
  {
    EmissiveOverlay
    {
      emissiveMap = Fruits/PluginData/Orange_glowmap.png
      color = RGBA(193,176,10,100)
      brightness = 1
      transparency = 0.1
    }
  }
  Ocean
  {
    Mods
    {
      EmissiveFX
      {
        color = 1.0,0.25,0,1
        brightness = 1
        transparency = 0.1
      }
    }
  }
}
```

*EmissiveOverlay*

|Property|Format|Description|
|--------|------|-----------|
|emissiveMap|File Path|The texture defining how emissive a spot on the planet is. The default is the ScaledVersion `mainTex`.|
|color|Color|The color of the emission. Default is white.|
|brightness|Single|The brightness of the emission. The default is 1.25.|
|transparency|Single|The visibility of the original texture through the glow. The default is 0.75.|

*Mods/EmissiveFX*

|Property|Format|Description|
|--------|------|-----------|
|color|Color|The color of the emission. Default is white.|
|brightness|Single|The brightness of the emission. Default is 1.4.|
|transparency|Single|The visibility of the original texture through the glow. Te default is 0.6.|
