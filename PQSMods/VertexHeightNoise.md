---
layout: default
title: VertexHeightNoise
subtitle: TBD
---

The `VertexHeightNoise` PQSMod is a mod that adds heightnoise to the terrain. This makes the terrain bumpier, though the "style" of bumps/features change with the noise type. 
The noise is also additive, meaning that instead of overrwriting the terrain altitude, it simply adds or subtracts from it.

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
                VertexHeightNoise
                {
                    deformity = Single
                    frequency = Single
                    octaves = Single
                    persistance = Single
                    seed = Int32
                    noiseType = KopernicusNoiseType
                    mode = KopernicusNoiseQuality
                    lacunarity = Single
                    name = String // Optional unless you have more than one VertexHeightNoise PQSMod
                    order = Int32
                }
            }
        }
    }
}
```

This may seem nice, but there are several issues with VertexHeightNoise. These include:

* Scarped terrain at the planet's poles
* A black line which appears around sea level
* Terrain disappears after going below sea level

It is recommended that you instead use one of the VertexHeightNoiseVertHeightCurve PQSMods instead.

* {{ site.baseurl }}{% link PQSMods/VHNVHC.md %}
* {{ site.baseurl }}{% link PQSMods/VHNVHC2.md %}
* {{ site.baseurl }}{% link PQSMods/VHNVHC3.md %}