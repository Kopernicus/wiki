---
layout: default
title: Atmosphere
---

The `Atmosphere { }` subnode of the `Body { }` node describes the body's atmospheric attributes, such as oxygen, pressure, and temperature. It also contains the `AtmosphereFromGround { }` subnode (AFG), which details the atmosphere's colors.

**Subnodes**
* [AtmosphereFromGround { }]({{ site.baseurl }}{% link main/Atmosphere/AtmosphereFromGround.md %})

**Example**
```
Body
{
	...
	Atmosphere
	{
		enabled = true
		oxygen = false
		ambientColor = 0.2,0.3,0.8,1
		altitude = 35000
		staticPressureASL = 73.3086375

		AtmosphereFromGround
		{
			...
		}

		pressureCurve
		{
			key =	0	73.3086375	-9.40053885714286E-03	-9.40053885714286E-03
			key =	1750	56.8576945	-9.06132071428572E-03	-9.06132071428572E-03
			key =	3500	41.594015	-7.66193321571429E-03	-7.66193321571429E-03
			key =	5250	30.04092583	-5.75651121285714E-03	-5.75651121285714E-03
			key =	7000	21.44622817	-4.16994392857143E-03	-4.16994392857143E-03
			key =	8750	15.44612208	-2.90616120814286E-03	-2.90616100142857E-03
			key =	10500	11.27466225	-2.14383385714286E-03	-2.14383385714286E-03
			key =	12250	7.942703583	-1.57375037842857E-03	-1.57375037842857E-03
			key =	14000	5.766536167	-1.03374362157143E-03	-1.03374362157143E-03
			key =	15750	4.324600667	-7.27255171714286E-04	-7.27255171714286E-04
			key =	17500	3.221142583	-5.39731E-04	-5.39731E-04
			key =	19250	2.435542167	-4.01197907285714E-04	-4.01197907285714E-04
			key =	21000	1.816949667	-3.32120814571429E-04	-3.32120814571429E-04
			key =	22750	1.273118833	-2.57703878428571E-04	-2.57703878428571E-04
			key =	24500	0.9149863333	-1.74466857142857E-04	-1.74466857142857E-04
			key =	26250	0.6624848333	-1.36190255014286E-04	-1.36190255014286E-04
			key =	28000	0.4383204167	-1.16655755014286E-04	-1.16655755014286E-04
			key =	29750	0.2541896667	-9.19878571428571E-05	-9.19878571428571E-05
			key =	31500	0.1163629167	-6.40814285714286E-05	-6.40814285714286E-05
			key =	33250	0.02990466667	-3.32465407285714E-05	-3.32465407285714E-05
			key =	35000	0	-1.70883816414286E-05	-1.70883816414286E-05
		}
		pressureCurveIsNormalized = false
		temperatureSeaLevel = 266.33
		temperatureCurve
		{
			key =	0	266.33	-0.01833333429	-0.01833333429
			key =	4200	196.3746529	-0.002596739429	-0.002588734857
			key =	7350	196.3746529	0.002588734857	0.002588734857
			key =	10850	246.342758	0.001414898286	0.001414898286
			key =	19600	246.342758	-0.001951223714	-0.001951223714
			key =	28000	170.0278339	-0.002596739429	-0.002596739429
			key =	31500	170.0278339	0.001353641429	0.001353641429
			key =	35000	209.0938069	0.001984583143	0.001984583143
			key =	52500	0	-0.001284597429	-0.001284597429
		}
		temperatureSunMultCurve
		{
			key =	0	1	0	0
			key =	2692.307692	0.5	-0.0001714285714	-0.0002932711429
			key =	2966.661923	0	0	0
			key =	5401.571537	0	0	0
			key =	12747.88846	0.2	0	0
			key =	19330.81231	0.2	0	0
			key =	24577.96922	0	0	0
			key =	35000	0.4	0	0
		}
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|enabled|Boolean|Whether the body has an atmosphere.|
|oxygen|Boolean|Whether the atmosphere contains oxygen. Used for whether jet engines should work in the atmosphere.|
|ambientColor|Color|All objects inside of the atmosphere will slightly shine in this color.|
|lightColor|Color|(Deprecated) Sets the AFG `waveLength` to this value. If AFG is not included, it automatically creates a new AFG.|
|staticDensityASL|Double|Atmospheric density at sea level. Used to calculate the parameters of the atmosphere if no curves are used.|
|staticPressureASL|Double|The static pressure at sea level in kPa. Used to calculate the parameters of the atmosphere if no curves are used. It is displayed in KSP under the body information.|
|temperatureSeaLevel|Double|The static temperature in Kelvin at sea level. Used to calculate the parameters of the atmosphere if no curves are used.|
|adiabaticIndex|Double|?|
|atmosphereDepth|Double|(Also `altitude`,`maxAltitude`) The atmosphere cutoff altitude/height of the atmosphere.|
|gasMassLapseRate|Double|?|
|atmosphereMolarMass|Double|?|
|temperatureLapseRate|Double|?|
|pressureCurveIsNormalized|Boolean|Whether the pressure curve height values should use absolute (0 - atmosphereDepth) or relative (0 - 1) values.|
|pressureCurve|FloatCurve|Assigns a pressure value (in kPa) to a height value inside of the atmosphere.|
|temperatureCurveIsNormalized|Boolean|Whether the temperature curve should use absolute (0 - atmosphereDepth) or relative (0 - 1) values.|
|temperatureCurve|FloatCurve|Assigns a temperature value (in Kelvin) to a height value inside of the atmosphere.|
|temperatureSunMultCurve|FloatCurve|?|
|temperatureLatitudeBiasCurve|FloatCurve|?|
|temperatureLatitudeSunMultCurve|FloatCurve|?|
|temperatureAxialSunBiasCurve|FloatCurve|?|
|temperatureEccentricityBiasCurve|FloatCurve|?|
