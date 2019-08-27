---
layout: default
title: VertexHeightNoise
---

The `VertexHeightNoise` PQSMod is a mod that adds height noise to the terrain. This makes the terrain bumpier, though the "style" of bumps/features change with the noise type. 
The noise is also additive, meaning that instead of overwriting the terrain altitude, it simply adds or subtracts from it.

**Example**
```
PQS
{
  Mods
  {
    VertexHeightNoise
    {
      deformity = 1200
      frequency = 0.5
      octaves = 3
      persistence = 0.2
      seed = 134256
      noiseType = Perlin
      mode = High
      lacunarity = 0.7
      
      enabled = true
      order = 25
    }
  }
}
```

This may seem nice, but there are several issues with VertexHeightNoise. These include:

* Scarped terrain at the planet's poles
* A black line which appears around sea level
* Terrain disappears after going below sea level

It is recommended that you instead use one of the VertexHeightNoiseVertHeightCurve PQSMods instead.

* VertexHeightNoiseVertHeightCurve
* [VertexHeightNoiseVertHeightCurve2]({{ site.baseurl }}{% link PQSMods/VertexHeightNoiseVertHeightCurve2.md %})
* VertexHeightNoiseVertHeightCurve3

|Property|Format|Description|
|--------|------|-----------|
|deformity|Single|The deformity of the simplex terrain noise.|
|frequency|Single|The size of the each feature of the simplex terrain noise. As frequency gets bigger, size gets smaller.|
|octaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Single|The complexity of or amount of detail in the noise.|
|lacunarity|Single|The size of the gaps that are in the noise.|
|seed|Integer|The random seed of the noise.|
|noiseType|[NoiseType]({{ site.baseurl }}{% link main/datatypes.md %})|The type of the specified noise.|
|mode|[NoiseQuality]({{ site.baseurl }}{% link main/datatypes.md %})|The quality mode of the noise.|