---
layout: default
title: VertexHeightDeformity
---

```
PQS
{
  Mods
  {
    VertexHeightDeformity
    {
      deformity = 0.01
      scaleDeformityByRadius = true
      enabled = true
      order = 21
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|deformity|Double|The amount of deformation that the terrain should undergo. Each point's height on the sphere is multiplied by this value.|
|scaleDeformityByRadius|Boolean|Whether to multiply the deformity by the body's radius.|