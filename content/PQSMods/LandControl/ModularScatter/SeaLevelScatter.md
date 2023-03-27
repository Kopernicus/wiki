---
layout: default
title: SeaLevelScatter
---

The `SeaLevelScatter` ModularScatter component make individual scatter objects spawn at a randomized altitude.

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
                    altitudeVariance = 50 100
                }
            }
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|altitudeVariance|Float pair|The min and max altitude offset from the ground. Note that the final value is added to the scatter `verticalOffset`.|
