---
layout: default
title: Orbit
subtitle: On Tycho and Prague
---

The `Orbit { }` node is a subnode of `Body { }` and contains the data needed to produce the body's correct orbital parameters.

**Example**
```
Orbit
{
  referenceBody = Sun
  color = 0.7,0.7,0.7,1
  mode = 1
  inclination = 1
  eccentricity = 0
  semiMajorAxis = 2620000000000
  longitudeOfAscendingNode = 40
  argumentOfPeriapsis = 10
  meanAnomalyAtEpoch = 0
  epoch = 30000000000000
}
```

|Property|Format|Description|
|--------|------|-----------|
|referenceBody|String|The `name` of the object the body orbits.|   
|color|Color|The color of the orbit line. See [the datatypes page]({{ site.baseurl }}{% link main/datatypes.md %}) for more info on colors.| 
|inclination|Double|The tilt of the orbit in degrees. 0 = normal, 90 = polar, 180 = retrograde, etc...|
|eccentricity|Double|The difference between your body's apoapsis and periapsis. It is a value between 0 and 1, where 0 is a perfect circle, and 1 is a straight line. 0.5 would give an oval shape.|
|period|Double|The custom orbital period in seconds. This can be used to set extreme orbital periods.|
|[semiMajorAxis](https://en.wikipedia.org/wiki/Semi-major_and_semi-minor_axes)|Double|The average altitude of the body above its `referenceBody`'s center.|
|longitudeOfAscendingNode|Double|The longitude at where the body crosses the `referenceBody`'s equator. It relies on `inclination`.|
|argumentOfPeriapsis|Double|The longitude of the `referenceBody` where the body's periapsis is.|
|[meanAnomalyAtEpoch](https://en.wikipedia.org/wiki/Mean_anomaly)|Double|The position of the body along the orbit, in radians, at the specified epoch.|
|meanAnomalyAtEpochD|Double|Similar to `meanAnomalyAtEpoch`, but is in degrees instead of radians. Useful for more precise measurement.|
|epoch|Double|The epoch at which `meanAnomalyAtEpoch` is described.|
|iconColor|[Color]({{ site.baseurl }}{% link main/datatypes.md %})|(Also nodeColor) The color of the orbit icon/node.|
|iconTexture|File Path|The path to the custom icon texture.|
|mode|OrbitDrawMode|Orbit Draw Mode. Possible values are `OFF`, `REDRAW_ONLY`, `REDRAW_AND_FOLLOW`, and `REDRAW_AND_RECALCULATE`. Default is `REDRAW_AND_RECALCULATE`.|
|icon|OrbitDrawIcons|Orbit Icon Mode. Possible values are `NONE`, `OBJ`, `OBJ_PE_AP`, and `ALL`. Default is `ALL`.|
|cameraSmaRatioBounds|Single[]|Orbit rendering bounds. Takes two values, the lower bound and the upper bound.|

NOTE: Hyperedit uses these same values. Simply move your planet around with it and copy the values out of its "complex" tab into their respective fields in the config. beware of SOI's changing size when they reload to adjust for the new position.
