---
layout: default
title: VertexSimplexHeight
---

The `VertexSimplexHeight` PQSMod effectively generates [Perlin noise]({{ site.baseurl }}{% link main/datatypes.md %}) more efficiently than [`VertexHeightNoise { }`]({{ site.baseurl }}{% link PQSMods/VertexHeightNoise.md %}) and is less buggy too.

**Example**
```
PQS
{
  Mods
  {
    VertexSimplexHeight
    {
      deformity = Double
      frequency = Double
      octaves = Double
      persistence = Double
      seed = 340978

      enabled = true
      order = 2
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|deformity|Double|The deformity of the perlin noise.|
|frequency|Double|The size of the each feature of the perlin noise. As frequency gets bigger, size gets smaller.|
|octaves|Double|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Double|The complexity of or amount of detail in the noise.|
|seed|Integer|The random seed of the noise.|