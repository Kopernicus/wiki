---
layout: default
title: VertexSimplexHeightAbsolute
---

The `VertexSimplexHeightAbsolute` PQSMod is a mod that adds height noise to the terrain. TBD

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
|deformity|Single|The deformity of the simplex terrain noise.|
|frequency|Single|The size of the each feature of the simplex terrain noise. As frequency gets bigger, size gets smaller.|
|octaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|persistence|Single|The complexity of or amount of detail in the noise.|
|seed|Integer|The random seed of the noise.|