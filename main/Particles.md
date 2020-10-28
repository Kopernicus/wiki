---
layout: default
title: Particles (Post 1.8)
subtitle: "You would not believe your eyes, if ten million fireflies..."
---

### WARNING - The Particles subnode has been removed in Kopernicus 1.8.1-1, but it has been re-added in versions after 1.8.1. Only use this node if your pack does not support 1.8.1.
#### This page describes the *Post-1.8 version*. The *Pre-1.8 version* is [here]({{ site.baseurl }}{% link main/Particles.md %}).

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

      minEmission = 1
      maxEmission = 4
      sizeMin = 20.0
      sizeMax = 45.0
      lifespanMin = 500
      lifespanMax = 1250

      speedScale = -0.0000001
      rate = -0.05
      collide = false
      randVelocity = 0.05, 0.05, 0.05
      force = 0,0 0
      
      shadowCast = true
      shadowCast = false

      LifetimeColors
      {
        startColor = 0.9,0.3,0.4,1
        endColor = 0.2,0.8,0.2,1
      }
    }
  }
}
```

**Migrating from Pre-1.8**
Many of the new properties are optional, but there are a few tweaks that need to be done to get existing scripts to work. Firstly, the `minEmission` and `maxEmission` properties need to be converted to Integers, rather than Floats. Next, if you have a `mesh` property, it needs to be renamed to `emitMesh`. Finally, the `shadowCast` and `shadowEffect` properties should be specified - they are `true`/`false` values describing whether the particles should cast and be affected by shadows, respectively. Post-1.8 particle nodes are mostly backwards-compatible with Pre-1.8 installs, requiring only a change of the `mesh`/`emitMesh` property - however, pack makers can include both properties in their configuration file, as Kopernicus does not parse properties that do not exist in that version.

|Property|Format|Description|
|--------|------|-----------|
|target|String|The name of the body that the particles move towards/away from.|
|texture|File Path|The particle texture. Keeping them on the small side is generally a good idea as there will be lots of particles in nearly every case.|
|shader|String|The shader to use for the particles.|
|shape|EmissionShape|The shape of the particles. Possible values are `Ellipsoid`, `Ellipse`, `Sphere`, `Ring`, `Cuboid`, `Plane`, `Line`, and `Point`.|
|shape1D|Float|The 1-dimensional size of the particles.|
|shape2D|Vector2|The 2-dimensional size of the particles.|
|shape3D|Vector3|The 3-dimensional size of the particles.|
|minEmission|Integer|The particle emission rate.|
|maxEmission|Integer|Does not seem to have an observable effect.|
|sizeMin|Float|The minimum size of each particle, presumably in unity-units.|
|sizeMax|Float|The maximum size of each particle.|
|lifespanMin|Float|The minimum time a particle will exist for, measured in seconds.|
|lifespanMax|Float|The maximum time a particle will exist for, measured in seconds.|
|speedScale|Float|The rate of particle movement. A negative rate means particles will move away from the target, while a positive one will make them move towards it. You MUST set them to low values with about the amount of zeroes in the example, be it positive or negative, if you wish to have your particles move at a reasonable rate.|
|rate|Float|Controls the rate at which particles change scale. Positive rate makes them grow, negative rate makes them shrink.|
|scale|Vector3|Determines the scale of the particle for each axis in the Vector3.|
|emitMesh|File Path|The emission mesh of the particles. Likely in an .mu format.|
|collide|Boolean|Determines whether the particles have a collision mesh (can collide with each other, and possibly the vessel).|
|bounce|Float|The amount of force applied to a particle after colliding.|
|damping|Float|The amount of speed a particle loses after colliding.|
|shadowCast|Boolean|Whether the particles should cast shadows.|
|shadowEffect|Boolean|Whether the particles should have shadows cast upon it (i.e., receive shadows).|
|autoSeed|Boolean|Whether to automatically generate a random seed. Overwrites the set `seed`.|
|seed|Integer|The random seed for the particle generation. Must be non-negative!|
|randVelocity|Vector3|Chances for a particle to spawn with a random velocity in the X, Y, and Z directions, respectively.|
|force|Vector3|UNKNOWN. Leading theory is that a force is applied to the particle at the time of its creation using this vector.|
|Colors|Node|A group of Colors, named `color1` to `color5`. Each particle will randomly choose from one of these five colors to use when it is created. There are five colors due to a restriction in Unity's legacy particle system.|
|LifetimeColors|Node|Also `lifetimeColors`. A pair of Colors named `startColor` and `endColor`. Similar in format to the `Color` node, this node describes the overall particle color over the particle's lifetime by forming a Gradient between the start and end color. "Pair" means two, so there are *only two colors.*|
