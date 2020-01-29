---
layout: default
title: HeatEmitter
---

Hello!

```
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