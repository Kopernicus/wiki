---
layout: default
title: VertexSimplexHeight
---

The `VertexSimplexHeight { }` PQSMod effectively generates Perlin noise more efficiently than `VertexHeightNoise { }` and is less buggy too.
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
                VertexSimplexHeight
                {
                    deformity = Double
                    frequency = Double
                    octaves = Double
                    persistance = Double
                    seed = Int32
                    enabled = true
                    order = 2
                }
            }
        }
    }
}
```