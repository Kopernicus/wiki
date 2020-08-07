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
|Color|A color. It can be expressed in many ways, each of which is shown in the table at the bottom of the page.|See Table|
|Filepath|A String containing a file path to a file. There are two main ways of specifiying paths, shown in the second table on this page.|See Table|
|x[]|A comma-separated list with values of type x.|"Integer[] = 1, 2, -10, 24", "String[] = Hi, Bye, IOParser, Excel"|
|NoiseType|The type of noise to use with Noise-generating PSQMods|Options are "Perlin", "Billow", or "RidgedMultiFractal"|
|NoiseQuality|The quality of the noise being generated|Options are "Low", "Standard", "Medium", or "High".|
|Vector2/3|A list of floating point numbers. A Vector2 holds two numbers, while a Vector3 holds 3.|Vector 2: 1,1 - Vector3: 1,0.2,1|
|FloatCurve|A list of keys, each with 2 or 4 values. The first two values are the "time" and "value" values, and the next two are the derivatives of the curve (optional).|See below for a simple example, but read [this forum thread on FloatCurves](https://web.archive.org/web/20170607054017/https://forum.kerbalspaceprogram.com/index.php?/topic/84201-info-ksp-floatcurves-and-you-the-magic-of-tangents/)|

```
ExampleFloatCurve
{
    key = 0 250
    key = 3.35 500 36.70368 -1.219512
    key = 15.65 485 -3.517569 -16.3017
    key = 52.3 100 -3.71531 -15.77211
    key = 53.64 0
}
```

|Path Format|Description|Example|
|-----------|-----------|-------|
|GameData Path|The file path within GameData. This method is mostly used for specifying assets from either the same mod or a different one from the config itself.|MPE/MPE_Textures/PluginData/Ervo_biomes.png|
|BUILTIN Path|The name of a texture located in the stock asset files|BUILTIN/Grass2 - A full list of BUILTIN textures can be found [here](https://github.com/GER-Space/Kerbal-Konstructs/wiki/Builtin-Textures-for-KSP-1.8), though they include things not used for planets.|

|Color Format|Description|Example|
|------------|-----------|-------|
|Fractional RGBA|A color in RGBA format, where each number is between 0 and 1.|0.5, 0.3, 0.1, 1.0|
|Explicit RGBA|A color explicitly specified in RGBA format. Each parameter is a number between 0 and 255.|RGBA(100,50,0,255)|
|RGB|A color in RGB format. Similar to RGBA, but the Alpha value is always 255.|RGB(100,50,0)|
|HSBA|A color in HSBA format (Hue, Saturation, Brightness, Alpha). Each parameter is a value between 0 and 255.|HSBA(109, 250, 37)|
|Hexadecimal|A color in hexadecimal format (letters can be upper or lowercase).|#7DA665|
|XKCD|An XKCD color. More info [here](https://xkcd.com/color/rgb/). |XKCD.Cyan|
