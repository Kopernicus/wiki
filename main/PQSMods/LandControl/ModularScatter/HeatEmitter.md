---
layout: default
title: HeatEmitter
---

Hello!

```md
PQS
{
    Mods
    {
        LandControl
        {
            ...
            scatters
            {
                Value
                {
                    ...
                    Components
                    {
                        temperature = // Double
                        sumTemp = // Double
                        biomeName = // String
                    }
                }
            }
        }
    }
}
```
Many parameters function identically to corresponding parameters in [HazardousBody]({{site.baseurl}}{% link main/HazardousBody.md %}).

|Property|Format|Description|
|--------|------|-----------|
|temperature|Double|The ambient temperature, corresponds to `ambientTemp`|
|sumTemp|Boolean|Whether `temperature` should be added.|
|biomeName|String|The name of the biome.|
|AltitudeCurve|FloatCurve|Multiplier curve that changes `ambientTemp` with altitude.|
||||