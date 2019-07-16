---
layout: default
title: Light
---

The `Light { }` subnode of the `ScaledVersion { }` node describes the light quality of the star itself. 

**Subnodes**
* [brightnessCurve { }](https://github.com/BryceSchroeder/Kopernicus/wiki/brightnessCurve)

## Explanation:
***
|Property|Format|Description|
|--------|------|-----------|
|sunFlare|File Path|?path to a file?|
|sunlightColor|4-tuple|RGBA values in 0-1 scale. Light cast in-game|
|sunlightIntensity|float| ?Description here?|
|sunlightShadowStrength|float|?Description here?|
|scaledSunlightColor|4-tuple|RGBA values in 0-1 scale. Applied to _ScaledSpace_ mesh of celestials|
|scaledSunlightIntensity|float| ?Description here?|
|IVASunColor|4-tuple|RGBA values in 0-1 scale. _scaledSunlightColor_ inside cockpits|
|IVASunIntensity|float| ?Description here?|
|ambientLightColor|4-tuple|RGBA values in 0-1 scale.|
|sunLensFlareColor|4-tuple|RGBA values in 0-1 scale.|
|givesOffLight|boolean|_True_ or _False_. Determines whether the body emits light, or whether it's an object like a black hole.|
|sunAU|float| Distance in meters. ?Something to do with setting AU from parent star to home world? What's it for? we know it does not affect star luminocity calcuation.
|luminosity|float| ?W/m2 reaching top of atmosphere of home world (1AU away from primary) You can use it to calcuate the starluminocity by 4 * Math.PI * kerbinAU * kerbinAU * luminosity
|insolation|float| ?value modifying _luminosity_ for power reaching surface?
|radiationFactor|float|?Description here?


**Example**
```
            Light
            {
                sunlightColor = 1.0,0.384,0.345,1.0
                sunlightIntensity = 0.9
                sunlightShadowStrength = 0.75
                scaledSunlightColor = 1.0,0.384,0.345,1.0
                scaledSunlightIntensity = 0.9
                IVASunColor = 1.0,0.384,0.345,1.0
                IVASunIntensity = 0.9
                ambientLightColor = 0.6,0.06,0.06,1.0
                sunLensFlareColor = 1.0,0.352,0.301,1.0
                givesOffLight = true
                sunAU = 13599840256
                luminosity = 1360
                insolation = 0.15
                brightnessCurve
                {
                    key = -0.01573471 0.217353 1.706627 1.706627
                    key = 5.084181 3.997075 -0.001802375 -0.001802375
                    key = 38.56295 1.82142 0.0001713 0.0001713
                }
            }
```
