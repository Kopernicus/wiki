---
layout: default
title: VertexHeightNoiseVertHeightCurve2
---

The `VertexHeightNoiseVertHeightCurve2` PQSMod is one of several mods in the HeightNoise family. They all produce heightmap noise, which can make terrain considerably more interesting.
It is considered by some to be a much more customizable and far stabler alternative to `VertexHeightNoise { }`.

**Example**
```
PQS
{
  Mods
  {
    VertexHeightNoiseVertHeightCurve2
    {
      deformity = 200000
      ridgedMode = Perlin

      ridgedAddSeed = 1
      ridgedAddFrequency = 4
      ridgedAddLacunarity = 0.7
      ridgedAddOctaves = 4

      ridgedSubSeed = 1
      ridgedSubFrequency = 8
      ridgedSubLacunarity = 1.4
      ridgedSubOctaves = 15

      simplexCurve
      {
        key = 0 0 0.146 0.146
        key = 0.79 0.245 0.68 1.5
        key = 1 1 6.11 6.11
      }

      simplexHeightStart = 0
      simplexHeightEnd = 1000
      simplexSeed = 1
      simplexOctaves = 8
      simplexPersistence = 0.5
      simplexFrequency = 1
      
      enabled = true
      order = 2
    }
  }
}
```

NOTE: `___` is substituted for "Add" and "Sub." "Add" creates a "base layer" of noise, and "Sub" layers on top of "Add," subdividing it and increasing detail. Finally, "simplex" creates a third and final layer atop the base and subdivision layers.

|Property|Format|Description|
|--------|------|-----------|
|deformity|Single|The overall deformity of the noise.|
|ridgedMode|[NoiseTypes]({{ site.baseurl }}{% link main/datatypes.md %})|The ridge noise type for both `ridgedAdd` and `ridgedSub`.|
|ridged___Frequency|Single|The size of the each feature of the ridged noise. As frequency gets bigger, size gets smaller.|
|ridged___Lacunarity|Single|The size of the gaps that are in the noise.|
|ridged___Octaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|ridged___Seed|Integer|The random seed of the noise.|
|simplexCurve|FloatCurve|A curve that assigns a height multiplier to a width value. Roughly speaking, simplexCurve draws 1/2 of a cross-section of the feature you are trying to create.|
|simplexHeightStart|Double|The starting height of the simplex, or 0 on the `simplexCurve`.|
|simplexHeightEnd|Double|The ending height of the simplex, or 1 on the `simplexCurve`.|
|simplexFrequency|Double|Similar to `ridged___Frequency`.|
|simplexOctaves|Double|Similar to `ridged___Octaves`.|
|simplexPersistence|Double|The complexity of or amount of detail in the noise.|
|simplexSeed|Integer|Similar to `ridged___Seed`.|
