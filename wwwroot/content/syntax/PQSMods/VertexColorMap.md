**Internal mod name:** `PQSMod_VertexColorMap`

The `VertexColorMap` PQSMod is a mod that applies a color map over the terrain.

## Example {#Example}
```
PQS
{
  Mods
  {
    VertexColorMap
    {
      map = Fruits/PluginData/Blueberry_colormap.dds
      order = 20
      enabled = true
    }
  }
}
```

## Properties {#Properties}
|Property|Format|Description|
|--------|------|-----------|
|map|File Path|The texture containing the color map for the body.|
