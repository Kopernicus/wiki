---
layout: default
title: VertexHeightNoiseVertHeightCurve2
---

The `VertexHeightNoiseVertHeightCurve2 { }` PQSMod is one of several mods in the HeightNoise family. They all produce heightmap noise, which can make terrain considerably more interesting.
It is considered by some to be a much more customizeable and far stabler alternative to `VertexHeightNoise { }`.

**Example**

```
@Kopernicus
{
    Body
    {
        PQS
        {
            Mods
            {
                VertexHeightNoiseVertHeightCurve2
                {
                    deformity = 200000

                    // The ridgedAdd values create kind of a "base layer" of noise for the rest to build on 

                    ridgedAddSeed = 1
                    ridgedAddFrequency = 4 //4
                    ridgedAddLacunarity = 0.7 //0.6
                    ridgedAddOctaves = 4 //2

                    // ridgedSub layers on top of ridgedAdd, subdivinding it and increasing detail 

                    ridgedSubSeed = 1
                    ridgedSubFrequency = 8 //20
                    ridgedSubLacunarity = 1.4 //2
                    ridgedSubOctaves = 15 //6

                    // Roughly speaking, simplexCurve draws 1/2 of a cross-section of the feature you are trying to create.
                    simplexCurve
                    {
                       key = 0 0 0.146 0.146
                        key = 0.79 0.245 0.68 1.5
                        key = 1 1 6.11 6.11
                    }

                    // These values act as a third and final layer atop the base, subdivision, and cross-section layers.
                    simplexHeightStart = 0
                    simplexHeightEnd = 1000
                    simplexSeed = 1
                    simplexOctaves = 8 //1
                    simplexPersistence = 0.5 //1
                    simplexFrequency = 1
                    
                    enabled = true
                    order = 2
                }
            }
        }
    }
}
```