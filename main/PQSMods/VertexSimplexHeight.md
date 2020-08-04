---
layout: default
title: VertexSimplexHeight
---

The `VertexSimplexHeight` PQSMod generates monochrome [Perlin noise]({{ site.baseurl }}{% link main/datatypes.md %}) for use in terrain deformation. It is more efficient and less buggy than [`VertexHeightNoise`]({{ site.baseurl }}{% link main/PQSMods/VertexHeightNoise.md %}).

**Example**
```
PQS
{
    Mods
    {
        VertexSimplexHeight
        {
            deformity = 10000
            frequency = 0.6
            octaves = 5
            persistence = 0.7
            seed = 78309

            enabled = true
            order = 2
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|deformity|Float|The deformity of the perlin noise.|
|frequency|Float|The size of the each feature of the perlin noise. As frequency gets bigger, size gets smaller.|
|octaves|Float|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Float|The complexity of or amount of detail in the noise.|
|seed|Integer|The random seed of the noise.|
