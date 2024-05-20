---
layout: default
title: HeightColorMap
---

The `HeightColorMap` PQSMod is a mod that colors the tarrain based on altitude using user-defined landclasses.

**Example**
```
PQS
{
  Mods
  {
    HeightColorMap
    {
        blend = Float
        LandClasses
        {
            LandClass
            {
                name = String
                delete = Boolean // default is false
                color = Color
                altitudeStart = Float // Fractional altitude start | altitude = (vertexHeight - vertexMinHeightOfPQS) / vertexHeightDeltaOfPQS
                altitudeEnd = Float
                lerpToNext = Boolean // should we blend into the next landclass? set to true on all but the last
            }
        }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|blend|Decimal|The blend between the LandClasses.|

## LandClasses
The `LandClasses { }` wraps several `LandClass { }` nodes that describe an individual region's color as defined by altitudes.

|Property|Format|Description|
|--------|------|-----------|
|name|Text|The name of the LandClass.|
|color|Color|The color to be applied to the LandClass.|
|altitudeStart|Decimal|The starting altitude of the LandClass. NOTE: Altitude is measured in fractions of valid PQS height: `altitude = (vertexHeight - vertexMinHeightOfPQS) / vertexHeightDeltaOfPQS`.|
|altitudeEnd|Decimal|The ending altitude of the LandClass. Follows same measurement unit as `altitudeStart`.|
|lerpToNext|Boolean|Whether to blend into the next LandClass. Highly recommended to set to true on all but the last LandClass.|
