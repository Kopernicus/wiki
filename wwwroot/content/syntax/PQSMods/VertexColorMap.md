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

|Property|Format|Description|
|--------|------|-----------|
|map|File Path|The texture containing the color map for the body.|
