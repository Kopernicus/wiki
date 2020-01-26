---
layout: default
title: LandControl
---

LandControl is a PQSMod that controls regional, longitudinal, and latitudinal aspects. It is mainly used to add scatters, which are 3D meshed objects that are generated on the surface. Scatters are mainly controlled through the [`ModularScatter`]({{ site.baseurl }}{% link ModularScatter/ModularScatter.md %}) class.

**Example**
```
PQS
{
	Mods
	{
		LandControl
		{
			altitudeBlend = 0.1
			altitudeFrequency = 12
			altitudeOctaves = 2
			altitudePersistance = 0.6
			altitudeSeed = 12249
			createColors = True
			createScatter = True
			heightMap = BUILTIN/Home2 (G) for Aster
			latitudeBlend = 0.1
			latitudeFrequency = 5
			latitudeOctaves = 5
			latitudePersistance = 0.6
			latitudeSeed = 47373
			longitudeBlend = 0.6
			longitudeFrequency = 4
			longitudeOctaves = 4
			longitudePersistance = 0.6
			longitudeSeed = 768453
			useHeightMap = False
			vHeightMax = 40000
			order = 999
			enabled = True
			name = _LandClassOcean
			scatters
			{
				Value
				{
					materialType = DiffuseWrapped
					material = BUILTIN/scatter_rock_kerbin
					mesh = BUILTIN/boulder
					castShadows = True
					densityFactor = 0.25
					maxCache = 512
					maxCacheDelta = 64
					maxLevelOffset = 0
					maxScale = 1.5
					maxScatter = 10
					maxSpeed = 200
					minScale = 0.15
					recieveShadows = True
					name = boulder
					seed = 123887
					verticalOffset = -0.25
					instancing = False
					Material
					{
						mainTex = BUILTIN/rock00
						color = 1,1,1,0.621999979
						diff = 0.2
					}
				}
			}
			landClasses
			{
				Value
				{
					alterApparentHeight = 0
					alterRealHeight = 0
					color = 0.180000007,0.100000001,0.0700000003,1
					coverageBlend = 1
					coverageFrequency = 2
					coverageOctaves = 4
					coveragePersistance = 0.5
					coverageSeed = 121214
					name = BaseSun
					latDelta = 1
					latitudeDouble = True
					lonDelta = 0
					minimumRealHeight = 0
					noiseBlend = 0.5
					noiseColor = 0.280000001,0.100000001,0.0700000003,1
					noiseFrequency = 8
					noiseOctaves = 4
					noisePersistance = 0.5
					noiseSeed = 453737
					altitudeRange
					{
						endEnd = 1
						endStart = 1
						startEnd = -1
						startStart = -1
					}
					latitudeDoubleRange
					{
						endEnd = 1
						endStart = 1
						startEnd = 0
						startStart = 0
					}
					latitudeRange
					{
						endEnd = 1
						endStart = 1
						startEnd = 0
						startStart = 0
					}
					longitudeRange
					{
						endEnd = 0.5
						endStart = 0.5
						startEnd = 0
						startStart = 0
					}
					scatters
					{
						Value
						{
							density = 1
							scatterName = boulder
							delete = False
						}
					}
				}
			}
		}
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|altitudeBlend|Single|The blend between the different altitudes.|
|altitudeFrequency|Single|The frequency of the noise applied to the altitudes.|
|altitudeOctaves|Integer|The octaves of the noise applied to the altitudes.|
