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
		waveLength = 0.864102423, 0.807692051, 0.650640965, 0.5
		samples = 4
		innerRadiusMult = 0.975
		outerRadiusMult = 1.025
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|DEBUG_alwaysUpdateAll|Boolean|Whether all parameters should get recalculated and reapplied every frame.|
|doScale|Boolean|Whether the atmosphere mesh should be scaled automatically.|
|innerRadius|Float|The lower bound of the atmosphere effect in 1/6000ths of a meter.|
|outerRadius|Float|The upper bound of the atmosphere effect in 1/6000ths of a meter.|
|invWaveLength|BandOffset|The inverse wavelength. Either this OR `waveLength` should be used. The inverse wavelength is equivalent to each of the color values of the `waveLength` as x: `1/(x^4)` with an alpha of 0.5.|
|waveLength|BandOffset|The wavelength of the atmosphere. Either this OR `invWaveLength` should be used. The wavelength is equal to each of the color values of the `invWaveLength` as x: `sqrt(sqrt(1/x))` with an alpha of 0.5.|
|samples|Single|?|
|transformScale|Vector3|The scale of the atmosphere mesh in all three directions. Automatically set if `doScale` is enabled. If this is set, then `doScale` is set to false automatically.|
|innerRadiusMult|Float|A multiplier that automatically sets innerRadius based on the planet's radius. Replaces `innerRadius`.|
|outerRadiusMult|Float|A multiplier that automatically sets outerRadius based on the planet's radius. Replaces `outerRadius`.|

BandOffset is an input type unique to AtmosphereFromGround and is not strictly a color. Instead, it is the amount a series of specifically-colored bands are offset up and down. The way in which these bands overlap determine the atmosphere color. Note that in `waveLength`, a value of 1 means the band is all the way down.
