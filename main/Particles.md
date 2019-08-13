---
layout: Default
title: Particles
subtitle: "You would not believe your eyes, If ten million fireflies..."
---

# Example
```
// This example was assembled primarily through the efforts of Zaffre.
@Kopernicus
{
  Body
  {
    Particles
    {
      Value
      {
        target = CelestialBody // The body the particles move towards/away from.
        texture = Texture2D // The particle texture. Keeping them on the small side is generally a good idea as there will be lots of particles in nearly every case.
        
        minEmission = Single
        maxEmission = Single // Does not seem to have an observable effect.
        sizeMin = Single // The minimum size of each particle, presumably in unity-units.
        sizeMax = Single // The maximum size of each particle.
        lifespanMin = Single // The minimum time a particle will exist for, measured in seconds.
        lifespanMax = Single // The maxiumum time a particle will exist for, measured in seconds.
        
        speedScale = Single // The rate of particle movement. A negative rate means particles will move away from the target, while a postivie one will make them move towards it.
        rate = Single // Controls the rate at which particles change scale. Positive rate makes them grow, Negative rate makes them shrink.
        collide = Boolean // Deterines whether the particles can collide with each other.
        randVelocity = Vector3 // Chances for a particle to spawn with a random velocity in the X, Y, and Z directions, respectively.
        force = Vector3 // UNKNOWN. Leading theory is that a force is applied to the particle at the time of its creation using this vector.
        
        Colors // Each particle will randomly choose fro one of these five colors to use when it is created. There are five colors due to a restriction in Unity's legacy particle system.
        {
          color1 = Color
          color2 = Color
          color3 = Color
          color4 = Color
          color5 = Color
        }
      }
    }
  }
}
```