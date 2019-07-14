---
layout: default
title: Biome
---

The `Biome { }` node is a subnode of `Biomes { }`, a wrapper node for all of the Biomes. Each biome contains its own name, color, and science multiplier.

**Example**
```
Biomes
{
  Biome
  {
    name = Lowlands
    value = 1.25
    color = 0.5, 0.2, 0.1, 1.0
  }
  Biome
  {
    name = Highlands
    value = 0.9
    color = 0.75, 0.6, 0.95, 1.0
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|name|String|The name of the biome.|
|displayName|String|The name to be displayed. Can be a localization tag.|
|value|Single|The science multiplier for the biome.|
|color|Color|The color of the biome on the biomeMap, specified at `Properties/biomeMap`.|
