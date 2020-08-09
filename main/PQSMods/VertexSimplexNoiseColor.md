---
layout: default
title: VertexSimplexNoiseColor
---

The `VertexSimplexNoiseColor` PQSMod generates RGB [Perlin noise]({{ site.baseurl }}{% link main/DataTypes.md %}) for use in terrain coloration.

**Example**
```
PQS
{
  Mods
  {
    VertexSimplexNoiseColor
    {
      blend = Float
      colorEnd = Color
      colorSTart = Color
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