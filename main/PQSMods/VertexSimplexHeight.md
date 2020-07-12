---
layout: default
title: VertexSimplexHeight
---

The `VertexSimplexHeight` PQSMod generates monochrome [Perlin noise]({{ site.baseurl }}{% link main/datatypes.md %}) for use in terrain deformation.

**Example**
```
PQS
{
  Mods
  {
    VertexSimplexHeight
    {
      deformity = Float
      frequency = Float
      octaves = Float
      persistence = Float
      seed = 340978

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