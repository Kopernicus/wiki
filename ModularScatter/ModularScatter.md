---
layout: default
title: Modular Scatter
---

Modular Scatter is a PQS LandControl add-on that allows scatters to be placed modularly or by biome. Most of LandControl `scatters` are made of Modular Scatter objects.

**Example**
```
PQS
{
    Mods
    {
        LandControl
        {
            Components
            {
                HeatEmitter
                {
                    ...
                }
                LightEmitter
                {
                    ...
                }
                ScatterColliders
                {
                    ...
                }
                SeaLevelScatter
                {
                    ...
                }
            }
            ...
        }
    }
}
```

**Components**
+ [HeatEmitter { }]({{ site.baseurl }}{% link ModularScatter/HeatEmitter.md %})
+ [LightEmitter { }]({{ site.baseurl }}{% link ModularScatter/LightEmitter.md %})
+ [ScatterColliders { }]({{ site.baseurl }}{% link ModularScatter/ScatterColliders.md %})
+ [SeaLevelScatter { }]({{ site.baseurl }}{% link ModularScatter/SeaLevelScatter.md %})
