---
layout: default
title: AtmosphereFromGround
---

The `AtmosphereFromGround { }` subnode of the `Atmosphere { }` node describes the atmosphere's color when seen in LocalSpace.

**Example**
```
Atmosphere
{
	AtmosphereFromGround
	{
		DEBUG_alwaysUpdateAll = False
		doScale = True
		innerRadius = 7255463
		invWaveLength = 1.79365575, 2.34972358, 5.58000231, 0.5
		outerRadius = 7441501
		samples = 4
		transformScale = 1.02499998, 1.02499998, 1.02499998
		waveLength = 0.864102423, 0.807692051, 0.650640965, 0.5
		outerRadiusMult = 1.025
		innerRadiusMult = 0.9749999
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|DEBUG_alwaysUpdateAll|Boolean|Whether all parameters should get recalculated and reapplied every frame.|
|doScale|Boolean|Whether the atmosphere mesh should be scaled automatically.|
|innerRadius|Single|The lower bound of the atmosphere effect.|
|invWaveLength|Color|The inverse wavelength. Either this OR `waveLength` should be used. The inverse wavelength is equivalent to each of the color values of the `waveLength` squared twice (to the fourth power).|
|outerRadius|Single|The upper bound of the atmosphere effect.|
|samples|Single|?|
|transformScale|Vector3|The scale of the atmosphere mesh in all three directions. Automatically set if `doScale` is enabled.|
|waveLength|Color|The wavelength of the atmosphere. Either this OR `invWaveLength` should be used. The wavelength is equal to the fourth root of each of the color values of the `invWaveLength`.|
|innerRadiusMult|Single|A multiplier that automatically sets innerRadius based on the planet's radius. Replaces `innerRadius`.|
|outerRadiusMult|Single|A multiplier that automatically sets outerRadius based on the planet's radius. Replaces `outerRadius`.|