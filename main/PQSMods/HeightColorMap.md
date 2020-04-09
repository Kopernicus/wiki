---
layout: default
title: HeightColorMap
---

The `HeightColorMap` PQSMod is a mod that colors the tarrain based on altitude using user-defined landclasses.

**Example**
```
PQS
{
  Mods
  {
    HeightColorMap
    {
        blend = Float
        LandClasses
        {
            LandClass
            {
                name = String
                delete = Boolean // default is false
                color = Color
                altitudeStart = Float // Fractional altitude start | altitude = (vertexHeight - vertexMinHeightOfPQS) / vertexHeightDeltaOfPQS
                altitudeEnd = Float
                lerpToNext = Boolean // should we blend into the next landclass? set to true on all but the last
            }
        }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|map|File Path|The texture containing the color map for the body.|
