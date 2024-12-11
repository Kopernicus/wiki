The `Light { }` subnode of the `ScaledVersion { }` node describes the light quality of the star itself.

## Example {#Example}

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
    key = -0.01573471 0.217353 1.706627 1.706627 // 1/0 - At furthest or unreal distance.
    key = 5.084181 3.997075 -0.001802375 -0.001802375 // 1/5 AU
    key = 38.56295 1.82142 0.0001713 0.0001713 // 1/38 AU - At an extremely close distance.
  }
  IntensityCurve
  {
    key = 0 0.9 0 0
    key = 1 0.9 0 0
  }
  ScaledIntensityCurve
  {
	  key = 0 0.9 0 0
	  key = 1 0.9 0 0
  }
  IVAIntensityCurve
  {
	  key = 0 0.8099999 0 0
	  key = 1 0.8099999 0 0
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|sunFlare|File Path|The path to an asset bundle containing a Unity LensFlare object that should be applied to the star.|
|sunlightColor|Color|The color of the LocalSpace starlight. Influences vessels and PQS terrain.|
|sunlightIntensity|Decimal|The intensity of the LocalSpace starlight. Usage not recommended, because of a lacking distance limit. Use IntensityCurve instead.|
|sunlightShadowStrength|Decimal|The strength of the shadows caused by LocalSpace starlight.|
|scaledSunlightColor|Color|The color of the ScaledSpace starlight. Influences the ScaledSpace representation of the bodies.|
|scaledSunlightIntensity|Decimal|The intensity of the ScaledSpace starlight. Usage not recommended, because of a lacking distance limit. Use ScaledIntensityCurve instead.|
|IVASunColor|Color|The color of the starlight in IVA view.|
|IVASunIntensity|Decimal|The intensity of the IVA starlight. Usage not recommended, because of a lacking distance limit. Use IVAIntensityCurve instead.|
|ambientLightColor|Color|The color of ambient lighting when orbiting near the star.|
|sunLensFlareColor|Color|The color of the star's LensFlare effect. Gets multiplied with the color of the base texture (yellow-ish for stock flare).|
|givesOffLight|Boolean|Whether the star should emit light and have a LensFlare effect, or whether it's an object like a black hole.|
|sunAU|Decimal|Distance in meters. This is the AU value used by `brightnessCurve`.|
|luminosity|Decimal|Misnomer. Insolation in watts per square meter at Kerbin's orbit. Calculate for other stars as the (starLuminosity)^0.5 * 1360, where starLuminosity = 1 is the starLuminosity of the stock sun (roughly 1/100th the luminosity of the real world sun). You can use it to calculate the starLuminosity by 4 * Math.PI * kerbinAU * kerbinAU * luminosity. |
|insolation|Decimal| ?value modifying _luminosity_ for power reaching surface?|
|radiationFactor|Decimal|Unknown, testing desired|
|brightnessCurve|FloatCurve|Associates a distance value with a multiplier for the brightness (which is actually just the size) of the sunflare effect. The distances are specified as 1/x AU, with 0 being the farthest and x being measured in units of `sunAU`. For example, a value of 2 means that key applies at 1/2 AU. See the example above for more information.|
|IntensityCurve|FloatCurve|Associates a distance value (in meters) with a value that describes the intensity of the LocalSpace starlight at that point.|
|ScaledIntensityCurve|FloatCurve|Associates a distance value (in meters / 6000) with a value that describes the intensity of the ScaledSpace starlight at that point.|
|IVAIntensityCurve|FloatCurve|Associates a distance value (in meters) with a value that describes the intensity of the IVA starlight at that point.|
