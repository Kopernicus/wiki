---
layout: default
title: Particles (Pre 1.8)
subtitle: "You would not believe your eyes, if ten million fireflies..."
---

### WARNING - The Particles subnode has been removed in Kopernicus 1.8.1-1, but it has been re-added in versions after 1.8.1. Only use this node if your pack does not support 1.8.1.
#### This page describes the *Pre-1.8 version*. The *Post-1.8 version* is [here]({{ site.baseurl }}{% link main/Particles.md %}).

The `Particles { }` wrapper node is used to add particles to bodies. You can define several different "species" of particles by using multiple `Value { }` subnodes. NOTE:  Do not use this with a rapidly rotating body.  Doing so will yield unintended results that will be easily made apparent after a quick timewarp.

**Example**
```
// Credit to Zaffre for rough descriptions of parameters.
Body
{
  Particles
  {
    Value
    {
      target = Sun
      texture = Fruits/PluginData/Tomato_particle.dds

      minEmission = 1.0
      maxEmission = 4.0
      sizeMin = 20.0
      sizeMax = 45.0
      lifespanMin = 500
      lifespanMax = 1250

      speedScale = -0.0000001
      rate = -0.05
      collide = false
      randVelocity = 0.05, 0.05, 0.05
      force = 0,0 0

      Colors
      {
        color1 = 0.9,0.3,0.4,1
        color2 = 0.8,0.6,0.2,1
        color3 = 1,0.7,0.9,1
        color4 = 0.7,0.7,0.7,1
        color5 = 0.6,0.2,0.8,1
      }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|target|String|The name of the body that the particles move towards/away from.|
|texture|File Path|The particle texture. Keeping them on the small side is generally a good idea as there will be lots of particles in nearly every case.|
|minEmission|Single|The particle emission rate.|
|maxEmission|Single|Does not seem to have an observable effect.|
|sizeMin|Single|The minimum size of each particle, presumably in unity-units.|
|sizeMax|Single|The maximum size of each particle.|
|lifespanMin|Single|The minimum time a particle will exist for, measured in seconds.|
|lifespanMax|Single|The maximum time a particle will exist for, measured in seconds.|
|speedScale|Single|The rate of particle movement. A negative rate means particles will move away from the target, while a positive one will make them move towards it. You MUST set them to low values with about the amount of zeroes in the example, be it positive or negative, if you wish to have your particles move at a reasonable rate.|
|rate|Single|Controls the rate at which particles change scale. Positive rate makes them grow, negative rate makes them shrink.|
|scale|Vector3|Determines the scale of the particle for each axis in the Vector3.|
|collide|Boolean|Determines whether the particles have a collision mesh (can collide with each other, and possibly the vessel).|
|mesh|File Path|The collision mesh of the particles, if `collide` is true. Likely in an .mu format.|
|randVelocity|Vector3|Chances for a particle to spawn with a random velocity in the X, Y, and Z directions, respectively.|
|force|Vector3|UNKNOWN. Leading theory is that a force is applied to the particle at the time of its creation using this vector.|
|Colors|Node|A group of Colors, named `color1` to `color5`. Each particle will randomly choose from one of these five colors to use when it is created. There are five colors due to a restriction in Unity's legacy particle system.|
