---
layout: default
title: Particles
---

The `Particle` node is a subnode of the `Particles` wrapper node, contained inside the `Body` node. TODO

**Example**
```
Particles
{
  Particle
  {
    target = Sun
    texture = KopernicusExamples/Particles Example/particle.png
    minEmission = 100
    maxEmission = 100
    lifespanMin = 4
    lifespanMax = 6
    sizeMin = 4
    sizeMax = 7
    speedScale = 0
    rate = -0.5
    randVelocity = 10.0, 10.0, 10.0
    Colors
    {
      color1 = 1.0, 0.0, 0.0, 1.0
      color2 = 0.0, 1.0, 0.0, 1.0
      color3 = 0.0, 0.0, 1.0, 1.0
      color4 = 1.0, 1.0, 0.0, 1.0
      color5 = 0.0, 1.0, 1.0, 1.0
    }
  }
}
From Kopernicus/KopernicusExamples
```

|Property|Format|Description|
|target|String|The name of the body that is the target of the particles|
|minEmission|Single|TODO|
|maxEmission|Single|TODO|
|lifespanMin|Single|Minimum lifespan of particles|
|lifespanMax|Single|Maximum lifespan of particles|
|sizeMin|Single|Minimum size of particles|
|sizeMax|Single|Maximum size of particles|
|speedScale|Single|TODO|
|rate|Single|Grow rate of particles in TODO units|
|randVelocity|Vector3|TODO|
|texture|File Path|The texture of the particles|
|scale|Vector3|The scale of the particles|
|mesh|File Path|The mesh of the particles|
|collide|Boolean|Whether the particles should have a collision box (i.e., collide with stuff)|
|force|Vector3|TODO|
|Colors|Color[]|The colors of the particles?|
