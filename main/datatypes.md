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
radius = Float
contractWeight = Integer
timewarpAltitudeLimits = Integer[]
cameraSmaRatioBounds = Single[]
pressureCurve = FloatCurve
```

|Data Type|Description|Examples|
|---------|-----------|--------|
|String|A collection of characters.|"Hello", "Kerbin", "Sarnus123"|
|Integer|A number that has no decimal part.|1, 100, -13, 69|
|Float|A floating-point number.  It can store decimal values as well as integers. Used for most decimal keys.|3.1415926, 2, -100.01|
|Color|A color. It can be expressed in many ways, each of which is shown in the table at the bottom of the page.
|File Path|A String containing a file path to a file. The path is relative to GameData, meaning the path starts after GameData.|"Fruit/Configs/Apple.cfg", "Mod/PluginData/SampleTex.png", "Fruit/Cache/Grapefruit.bin"|
|x[]|A comma-separated list with values of type x.|"Integer[] = 1, 2, -10, 24", "String[] = Hi, Bye, IOParser, Excel"|
|FloatCurve|A list of keys, each with 2 or 4 values. The first two values are the "time" and "value" values, and the next two are the derivatives of the curve (optional).|Error: Exceeds size limit.|
|NoiseType|The type of noise to use with Noise-generating PSQMods|Options are "Perlin", "Billow", or "RidgedMultiFractal"|
|NoiseQuality|The quality of the noise being generated|Options are "Low", "Standard", "Medium", or "High".|
|Vector2/3|TODO|TODO|
|Quaternion|TODO|TODO|

|Color Format|Description|Example|
|------------|-----------|-------|
|Fractional RGBA|A color in RGBA format, where each number is between 0 and 1.|0.5, 0.3, 0.1, 1.0|
|Explicit RGBA|A color explicitly specified in RGBA format. Each parameter is a number between 0 and 255.|RGBA(100,50,0,255)|
|RGB|A color in RGB format. Similar to RGBA, but the Alpha value is always 255.|RGB(100,50,0)|
|HSBA|A color in HSBA format (Hue, Saturation, Brightness, Alpha). Each parameter is a value between 0 and 255.|HSBA(109, 250, 37)|
|Hexadecimal|A color in hexadecimal format (letters can be upper or lowercase).|#7DA665|
|XKCD|An XKCD color. More info [here](https://xkcd.com/color/rgb/). |XKCD.Cyan|