---
layout: default
title: Data Types
---

The Data Types used by Kopernicus are used for every key/value pair, and some have their own unique values.

**Example**
```
name = String
iconColor = Color
cacheFile = File Path
radius = Double
maxZoom = Single
contractWeight = Integer
timewarpAltitudeLimits = Integer[]
cameraSmaRatioBounds = Single[]
pressureCurve = FloatCurve
```

|Data Type|Description|Examples|
|---------|-----------|--------|
|String|A collection of characters.|"Hello", "Kerbin", "Sarnus123"|
|Integer|A number that has no decimal part.|1, 100, -13, 69|
|Double|A number that *can* store decimals. Used for most decimal keys.|3.1415926, 2, -100.01|
|Single|Similar to a Double, but can store smaller decimals.|69.964201, -10, 0.37|
|Color|A color. It can be expressed in many ways, as shown below.<br>"0.5, 0.1, 0.3, 1.0" - A color in RGBA format, where each number is between 0 and 1.<br>"RGBA(128, 0, 255, 255)" - A color explicitly specified in RGBA format. Each parameter is a number between 0 and 255.<br>"RGB(100, 255, 0)" - A color in RGB format. Similar to RGBA, but the Alpha value is always 255.<br>"HSBA(137, 109, 203, 255)" - A color in HSBA format (Hue, Saturation, Brightness, Alpha). Each parameter is a value between 0 and 255.<br>"#7289da" - A color in hexadecimal format.<br>"XKCD.Cyan" - An XKCD color. More info [here](https://xkcd.com/color/rgb/).|Listed to the left.|
|File Path|A String containing a file path to a file. The path is relative to GameData, meaning the path starts after GameData.|"Fruit/Configs/Apple.cfg", "Mod/PluginData/SampleTex.png", "Fruit/Cache/Grapefruit.bin"|
|x[]|A comma-separated list with values of type x.|"Integer[] = 1, 2, -10, 24", "String[] = Hi, Bye, IOParser, Excel"|
|FloatCurve|A list of keys, each with 2 or 4 values. The first two values are the "time" and "value" values, and the next two are the derivatives of the curve (optional).|Error: Exceeds size limit.|
|NoiseType|The type of noise to use with Noise-generating PSQMods|Options are "Perlin", "Billow", or "RidgedMultiFractal"|
|Vector2/3|TODO|TODO|
|Quaternion|TODO|TODO|
