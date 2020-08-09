---
layout: default
title: HeightColorMap2
---

The `HeightColorMap2` PQSMod is a mod that colors the terrain based on altitude using user-defined landclasses and has slightly more configuration options than [HeightColorMap]({{ site.baseurl }}{% link main/PQSMods/HeightColorMap.md %}).

**Subnodes**
* `LandClasses { }` (defined below)

**Example**
```
PQS
{
    Mods
    {
        HeightColorMap2
        {
            enabled = true
            order = 24
            
            blend = 0.1
            minHeight = -1000
            maxHeight = 50000
            
            LandClasses
            {
                LandClass
                {
                    name = _Ocean
                    color = 0.1,0.2,0.8,1
                    altitudeStart = 0.0
                    altitudeEnd = 0.125
                    lerpToNext = true
                }
                LandClass
                {
                    name = _Plains
                    color = 0.7,0.6,0.1,1
                    altitudeStart = 0.125
                    altitudeEnd = 0.375
                    lerpToNext = true
                }
                LandClass
                {
                    name = _MountainSide
                    color = 0.6,0.9,0.05,1
                    altitudeStart = 0.375
                    altitudeEnd = 0.75
                    lerpToNext = true
                }
                LandClass
                {
                    name = _RoundingTheSummit
                    color = 0.7,0.85,0.2,1
                    altitudeStart = 0.75
                    altitudeEnd = 0.9
                    lerpToNext = true
                }
                LandClass
                {
                    name = _IceCap
                    color = 0.9,0.9,0.9,1
                    altitudeStart = 0.9
                    altitudeEnd = 1.0
                    lerpToNext = false
                }
            }
        }
    }
}
```

|Property|Format|Description|
|--------|------|-----------|
|blend|Float|The blend between the LandClasses.|
|minHeight|Float|The minimum height, or `0.0` altitude, of a LandClass.|
|maxHeight|Float|The maxmium height, or `1.0` altitude, of a LandClass.|

## LandClasses
The `LandClasses { }` wraps several `LandClass { }` nodes that describe an individual region's color as defined by altitudes.

|Property|Format|Description|
|--------|------|-----------|
|name|String|The name of the LandClass.|
|color|Color|The color to be applied to the LandClass.|
|altitudeStart|Float|The starting altitude of the LandClass. NOTE: Altitude is measured in fractions of defined height: `altitude = (height - minHeight) / (maxHeight - minHeight)`.|
|altitudeEnd|Float|The ending altitude of the LandClass. Follows same measurement unit as `altitudeStart`.|
|lerpToNext|Boolean|Whether to blend into the next LandClass. Highly recommended to set to true on all but the last LandClass.|
