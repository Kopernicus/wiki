---
layout: default
title: Procedural Gas Giants
subtitle: Low Effort Storms, circa 2015
---

**Example**
```
ScaledVersion
{
  ProceduralGasGiant
  {
    rampTexture = Fruits/PluginData/Grapefruit_procgasgiantramp.dds
    stormMap = Fruits/PluginData/Grapefruit_stormmap.dds
    seed = 35293
    cloudSpeed = 50
    hasStorms = true
    distortion = 3.5
    frequency = 8.75
    lacunarity = 2.5
    gain = 0.5
    stormFrequency = 2.5
    stormDistortion = 8.35
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|rampTexture|File Path|The texture containing the color ramp gradient.|
|rampTextureGradient|Gradient|Similar to the `ScaledVersion/Material { }` Gradient, this gradient creates a ramp that sets the color ramp gradient.|
|generateRampFromScaledTexture|File Path|A texture that should be scaled in order to produce a rampTexture.|
|stormMap|File Path|The texture containing the storm map for the gas giant.|
|seed|Integer|The random seed for Perlin noise. Defaults to 0.|
|animate|Boolean|Whether to animate the gas giant. Animation only speeds up until 5000x timewarp speed. Defaults to `true`.|
|cloudSpeed|Single|The speed at which the clouds should move. Defaults to 20.|
|hasStorms|Boolean|Whether the gas giant should have storms.|
|distortion|Single|The distortion amount for the gas giant bands?|
|frequency|Single|The number of bands that should be present on the gas giant?|
|lacunarity|Single|?|
|gain|Single|?|
|stormFrequency|Single|The frequency of the storms that should occur on the gas giant.|
|stormDistortion|Single|The distortion of the storms.|