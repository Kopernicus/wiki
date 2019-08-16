---
layout: default
title: HazardousBody
---

The `HazardousBody { }` subnode of the `Body { }` node indicates that the body is hazardous, and thus, the body will gradually apply heat to the vessel as the vessel nears the body. It replaces `HazardousOceans { }` as the `HazardousBody { }` has more fine control and detail.

**Example**
```
Body
{
  HazardousBody
  {
    Instance
    {
      heat = 0.01
      interval = 0.5
      HeatMap = Fruits/PluginData/Orange_heatmap.dds

      AltitudeCurve
      {
        key = 0 1
        key = 20000 0.9
        key = 40000 0.75
        key = 75000 0.5
        key = 100000 0.35
        key = 150000 0.1
        key = 200000 0
      }
      LatitudeCurve
      {
        key = -75 0.1
        key = -30 0.7
        key = 0 1
        key = 30 0.7
        key = 75 0.1
      }
      LongitudeCurve
      {
        key = -160 1
        key = -125 0.2
        key = -75 0.8
        key = -35 0.3
        key = 0 1
        key = 35 0.5
        key = 75 0.9
        key = 125 0.1
        key = 165 0.6
      }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|heat|Double|The average heat on the body.|
|interval|Single|How much time passes between applying the heat to a vessel.|
|AltitudeCurve|FloatCurve|A multiplier of the average heat that gets applied at a certain altitude.|
|LatitudeCurve|FloatCurve|A multiplier of the average heat that gets applied at a certain latitude.|
|LongitudeCurve|FloatCurve|A multiplier of the average heat that gets applied at a certain longitude.|
|HeatMap|File Path|A greyscale map for fine control of the heat on the body. Black = 0, White = 1.|
