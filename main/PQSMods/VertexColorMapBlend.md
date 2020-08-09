---
layout: default
title: VertexColorMapBlend
---

Like its alternative [VertexColorMap]({{ site.baseurl }}{% link main/PQSMods/VertexColorMap.md %}), the `VertexColorMapBlend` PQSMod adds color to a body using a color map. However, this PQSMod "blends" in the color map to the existing texture by blending the edges of the color segment (i.e., between transparent and colored sections on the map).

**Example**
```
PQS
{
    Mods
    {
        VertexColorMapBlend
        {
            map = BUILTIN/eve_coloraddition
            blend = 0.25
            order = 9999993
            enabled = True
            name = _LandClass
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|map|File Path|The path to the color map to use and blend.|
|blend|Float|The amount of blend to use.|
