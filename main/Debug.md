---
layout: default
title: Debug
---

The `Debug` subnode of the `Body` node describes the various debugging properties of the body.

**Example**
```
Debug
{
  exportMesh = true
  update = true
  showSOI = false
}
```

|Property|Format|Description|
|exportMesh|Boolean|Whether Kopernicus should save a .bin file containing the ScaledSpace mesh. Defaults to `true`.|
|update|Boolean|Whether Kopernicus should be forced to update the ScaledSpace mesh. Defaults to `false`.|
|showSOI|Boolean|Whether a wire frame should be displayed to show the body's SOI. Defaults to `false`.|
