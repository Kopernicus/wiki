---
layout: default
title: SeaLevelScatter
---

The `SeaLevelScatter` ModularScatter component allows scatters to be spawnable at (or relative?) to sea level.

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
                SeaLevelScatter
                {
                    altitudeVariance = 10
                }
            }
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|altitudeVariance|Single|Either this determines how far the scatter can spawn from sea level (marking the vertical range) or the set altitude above sea level.|
