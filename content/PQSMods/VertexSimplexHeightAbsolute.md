---
layout: default
title: VertexSimplexHeightAbsolute
---

The `VertexSimplexHeightAbsolute` PQSMod conforms the terrain to a set height using simplex noise.

**Example**
```
PQS
{
  Mods
  {
    VertexSimplexHeightAbsolute
    {
      deformity = 1200
      frequency = 0.5
      octaves = 3
      persistence = 0.2
      seed = 134256
      
      enabled = true
      order = 25
    }
  }
}
```


|Property|Format|Description|
|--------|------|-----------|
|deformity|Float|The deformity of the simplex terrain noise.|
|frequency|Float|The size of the each feature of the simplex terrain noise. As frequency gets bigger, size gets smaller.|
|octaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Float|The complexity of or amount of detail in the noise.|
|seed|Integer|The random seed of the noise.|
