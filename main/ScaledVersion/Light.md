---
layout: default
title: Light
---

The `Light { }` subnode of the `ScaledVersion { }` node describes the light quality of the star itself. 

## Explanation:
***
|Property|Format|Description|
|--------|------|-----------|
|sunFlare|File Path|The path to an asset bundle containing a Unity LensFlare object that should be applied to the star.|
|sunlightColor|Color|The color of the LocalSpace starlight. Influences vessels and PQS terrain.|
|sunlightIntensity|Single|The intensity of the LocalSpace starlight. Usage not recommended, because of a lacking distance limit. Use IntensityCurve instead.|
|sunlightShadowStrength|Single|The strength of the shadows caused by LocalSpace starlight.|
|scaledSunlightColor|Color|The color of the ScaledSpace starlight. Influences the ScaledSpace representation of the bodies.|
|scaledSunlightIntensity|Single|The intensity of the ScaledSpace starlight. Usage not recommended, because of a lacking distance limit. Use ScaledIntensityCurve instead.|
|IVASunColor|Color|The color of the starlight in IVA view.|
|IVASunIntensity|Single|The intensity of the IVA starlight. Usage not recommended, because of a lacking distance limit. Use IVAIntensityCurve instead.|
|ambientLightColor|Color|The color of ambient lighting when orbiting near the star.|
|sunLensFlareColor|Color|The color of the star's LensFlare effect. Gets multiplied with the color of the base texture (yellow-ish for stock flare).|
|givesOffLight|Boolean|Whether the star should emit light and have a LensFlare effect, or whether it's an object like a black hole.|
|sunAU|Double|Distance in meters. ?Something to do with setting AU from parent star to home world? What's it for? we know it does not affect star luminosity calcuation.|
|luminosity|Double|Misnomer. Insolation in watts per square meter at Kerbin's orbit. Calculate for other stars as the (starluminosity)^0.5 * 1360, where starLuminosity = 1 is the starLuminosity of the stock sun (roughly 1/100th the luminosity of the real world sun). You can use it to calcuate the starluminocity by 4 * Math.PI * kerbinAU * kerbinAU * luminosity. |
|insolation|Double| ?value modifying _luminosity_ for power reaching surface?|
|radiationFactor|Double|?Description here?|
|brightnessCurve|FloatCurve|Associates a distance value with a multiplier for the brightness of the LensFlare effect. The distances are measured in 1/`sunAU` value. See the example above for more info.|
|IntensityCurve|FloatCurve|Associates a distance value (in meters) with a value that describes the intensity of the LocalSpace starlight at that point.|
|ScaledIntensityCurve|FloatCurve|Associates a distance value (in meters / 6000) with a value that describes the intensity of the ScaledSpace starlight at that point.|
|IVAIntensityCurve|FloatCurve|Associates a distance value (in meters) with a value that describes the intensity of the IVA starlight at that point.|


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
