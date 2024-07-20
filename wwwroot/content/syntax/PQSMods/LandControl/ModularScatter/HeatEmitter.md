The `HeatEmitter` ModularScatter component allows for scatters and their respective body to emit heat depending on the biome of and distance to the scatter.

```
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
                    DistanceCurve
                    {
                        key = 0 2
                        key = 100 1.8
                        key = 1000 0
                    }
                }
            }
        }
    }
}
```
Many parameters function identically to corresponding parameters in [HazardousBody](/Syntax/HazardousBody).

|Property|Format|Description|
|--------|------|-----------|
|temperature|Decimal|The ambient temperature, corresponds to `ambientTemp` in HazardousBody.|
|DistanceCurve|FloatCurve|Float curve that associates a distance from the scatter with a `temperature` multiplier.|
