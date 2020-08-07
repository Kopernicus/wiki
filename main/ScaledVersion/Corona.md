---
layout: default
title: Coronas
sidenote: what the hell squad
---

The `Coronas { }` node is a wrapper node for separate `Corona { }` subnodes that contain information for a corona.

**Subnodes**
* `Material { }` (parameters below)

**Example**
```
ScaledVersion
{
    Coronas
    {
        Corona
        {
            rotation = 0
            speed = -1
            updateInterval = 5
            scaleLimitX = 0
            scaleLimitY = 0
            scaleSpeed = 0
            
            Material
            {
                texture = TheStarclimberProgram/ConfigFiles/KepsolAndKilter/PluginData/KepsolCorona.png
                inverseFade = 1
            }
        }
    }
}
```

Through experimentation, it was realized that the only setting really worth changing is Material/texture, as not much improvement can be gotten out of wrangling with the other settings. Because of this, the description tables are hidden but can be viewable with the below buttons. With that in mind, feel free to use the above values apart from the texture path.  

<button data-toggle="collapse" data-target="#collapse-table">Show <code>Corona { }</code> Table</button>

{: #collapse-table .collapse}  
|Property|Format|Description|
|--------|------|-----------|
|scaleSpeed|Float|Speed at which the corona rescales|
|scaleLimitY|Float|The y-coordinate of the scale limit.|
|scaleLimitX|Float|The x-coordinate of the scale limit.|
|updateInterval|Float|The number of seconds before the corona updates.|
|speed|Integer|The speed at which ???|
|rotation|Float|The rotation of the texture around the star as viewed in-game.|
  
<button data-toggle="collapse" data-target="#collapse-mat-table">Show <code>Material { }</code> Table</button>

{: #collapse-mat-table .collapse}  
|Property|Format|Description|
|--------|------|-----------|
|texture|File Path|The texture containing the corona texture.|
|mainTexScale|Vector2|The scale of the corona texture.|
|mainTexOffset|Vector2|The offset of the corona texture.|
|invFade|Float|The soft particles factor. Default is 1.|
