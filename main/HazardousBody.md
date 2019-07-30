---
layout: default
title: HazardousBody
---

The `HazardousBody` subnode of the `Body` node indicates that the body is hazardous, and thus, the body will gradually apply heat to the vessel as the vessel nears the body. It replaces `HazardousOceans { }` as the `HazardousBody { }` has more fine control and detail.

**Example**
```
TODO
```

|Property|Format|Description|
|heat|Double|The average heat on the body.|
|interval|Single|How much time passes between applying the heat to a vessel.|
|AltitudeCurve|FloatCurve|A multiplier of the average heat that gets applied at a certain altitude.|
|LatitudeCurve|FloatCurve|A multiplier of the average heat that gets applied at a certain latitude.|
|LongitudeCurve|FloatCurve|A multiplier of the average heat that gets applied at a certain longitude.|
|HeatMap|File Path|A greyscale map for fine control of the heat on the body. Black = 0, White = 1.|
