---
layout: default
title: HeatEmitter
---
The `HeatEmitter` ModularScatter component allows for scatters and their respective body to emit heat depending on the biome of and distance to the scatter.

```md
LandControl
{
    ...
    Scatters
    {
        Value
        {
            ...
            Components
            {
                HeatEmitter
                {
                    temperature = 300
                    sumTemp = false
                    biomeName = Highlands
                    
                    AltitudeCurve
                    {
                        key = 0 0
                        key = 60000 1.1
                        key = 65000 1.3
                        key = 80000 1.4
                        key = 130000 0.8
                    }
                    DistanceCurve
                    {
                        key = 0 2
                        key = 100 1.8
                        key = 1000 1.5
                        key = 2000 1.1
                        key = 4000 0.7
                        key = 8000 0.2
                        key = 16000 0
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
|temperature|Double|The ambient temperature, corresponds to `ambientTemp` in HazardousBody.|
|sumTemp|Boolean|Whether `temperature` should be added to the current heat rather than set the current heat.|
|biomeName|String|The name of the biome for this scatter to have its HeatEmitter enabled for.|
|AltitudeCurve|FloatCurve|(Optional) Float curve that associates an altitude above the body's core with a temperature multiplier.|
|LatitudeCurve|FloatCurve|(Optional) Float curve that associates a latitude with a temperature multiplier.|
|LongitudeCurve|FloatCurve|(Optional) Float curve that associates a longitude with a temperature multiplier.|
|DistanceCurve|FloatCurve|(Optional) Float curve that associates a distance from the scatter with a temperature multiplier.|
|HeatMap|File Path|(Optional) The path to a greyscale map that gives finer control of the temperature on the body. Black = 0, White = 1.|
