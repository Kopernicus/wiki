---
layout: default
title: Coronas
subtitle: What's that bright light around that star?
---

The `Coronas { }` node is a wrapper node for separate `Value { }` subnodes that contain information for a corona.

**Example**
```
TODO
```

|Property|Format|Description|
|--------|------|-----------|
|scaleSpeed|Single|The speed of the scaling of the corona?|
|scaleLimitY|Single|The y-coordinate of the scale limit.|
|scaleLimitX|Single|The x-coordinate of the scale limit.|
|updateInterval|Single|The number of seconds before the corona updates.|
|speed|Integer|The speed at which ???|
|rotation|Single|The direction it turns in? -1 to 1?|
|Material|ParticleAddSmooth|A node that gives the corona's material. Described below.|

|Property|Format|Description|
|--------|------|-----------|
|texture|File Path|The texture containing the corona texture. Default is "White".|
|mainTexScale|Vector2|The scale of the corona texture.|
|mainTexOffset|Vector2|The offset of the corona textures.|
|invFade|Single|The soft particles factor. Default is 1.|