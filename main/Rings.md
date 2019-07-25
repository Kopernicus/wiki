---
layout: default
title: Rings
subtitle: Enchanting Italians for 300 years
---

Each `Body { }` may have a node called `Rings { }`, a wrapper node containing one or more `Ring { }` nodes, each of which defines a planetary ring. These may be flat discs as with Saturn, loops of ribbon as with the Ringworld, or voluminous cylinders.

Distance units for rings are milliradii, thousandths of the radius of the parent body. So 1000 is 1 radius, 2000 is 2 radii, etc.

**Example**
```
Rings
{
	Ring
	{
		innerRadius = 2000
		outerRadius = 4000
		
		InnerRadiusMultiplier
		{
			key = 0 1 0 0
		}
		OuterRadiusMultiplier
		{
			key = 0 1 0 0
		}
		
		thickness = 100
		steps = 120

		texture = Fruits/PluginData/Grapefruit_rings.dds
		// Number of times to tile the texture around the ring
		// Texture coordinates depend on this!
		// If 0, then a thin strip from (0,0) to (1,1) is applied to the whole ring.
		// Otherwise the following rectangles are tiled top-to-bottom:
		//   |      |            |            |
		//   | side |   inner    |   outer    |
		//   |      |            |            |
		//   (0.0,0)-(0.2,1): Top/bottom edges
		//   (0.2,0)-(0.6,1): Inner edge
		//   (0.6,0)-(1.0,1): Outer edge
		tiles = 10
		color = 1,1,1,1
		unlit = false
		useNewShader = false
		penumbraMultiplier = 200

		angle = 30
		lockRotation = true
		longitudeOfAscendingNode = 30
		rotationPeriod = 600
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|innerRadius|Single|The distance from center of parent to inner edge of ring in milliradii.|
|outerRadius|Single|The distance from center of parent to outer edge of ring in milliradii.|
|InnerRadiusMultiplier|FloatCurve|A curve that defines a multiplier for the inner radius using an angle. The first value is an angle in degrees, while the second is the multiplier. Allows for the deformation of rings.|
|OuterRadiusMultiplier|FloatCurve|Similar to `InnerRadiusMultiplier`, but for the outer radius rather than the inner radius.|
|thickness|Single|The distance between top and bottom faces of ring in milliradii.|
|angle|Single|The axis angle in degrees (inclination) of the ring.|
|longitudeOfAscendingNode|Single|Angle in degrees between the absolute reference direction and the ascending node. Works just like the corresponding property on celestial bodies. Only effective if `lockRotation` is true.|
|texture|File Path|The path to the ring texture.|
|color|Color|A tint applied to the ring.|
|lockRotation|Boolean|Whether to lock the rotation of the ring. If false, the ring's LAN rotates with the parent body (unnatural if the `angle` is not 0).|
|rotationPeriod|Single|The number of seconds for the ring to complete one rotation. If zero, it will default to the parent body's `rotationPeriod`. Only noticeable if `tiles` is not 0.|
|unlit|Boolean|Whether to apply an Unlit/Transparent shader instead of a Transparent/Diffuse shader.|
|useNewShader|Boolean|Whether to use the new custom ring shader that includes a planet shadow instead of the built-in Unity shaders.|
|penumbraMultiplier|Single|A penumbra multiplier to the NewShader. Makes planet shadow softer (values larger than one) or less soft (smaller than one). Softness still depends on distance from sun, distance from planet and radius of sun and planet.|
|steps|Integer|The amount of vertices around the ring.|
|tiles|Integer|Number of times the texture should be tiled around the cylinder. If zero, use the old behavior of sampling a thin diagonal strip from (0,0) to (1,1). Look at the example above for more info.|
|innerShadeTexture|File Path|The path to the texture whose opaque pixels cast shadows on the ring's inner surface.|
|innerShadeTiles|Integer|The `innerShadeTexture` repeats this many times over the inner surface.|
|innerShadeRotationPeriod|Single|The number of seconds the `innerShadeTexture` takes to complete one rotation.|
