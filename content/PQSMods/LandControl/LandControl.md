---
layout: default
title: LandControl
---

The `LandControl` PQSMod allows defining regions known as LandClasses, within which you can customize several features of the particular region.

**Subnodes**
* [Scatters { }]({{ site.baseurl }}{% link content/PQSMods/LandControl/Scatters.md %}) = Defines used scatters
* [LandClasses { }]({{ site.baseurl }}{% link content/PQSMods/LandControl/LandClasses.md %}) = Specifies land regions and their customizations

**Example**
```md
PQS
{
  Mods
  {
    LandControl
    {
      createColors = True
      createScatter = True
      heightMap = BUILTIN/oceanmoon_height
      useHeightMap = False
      vHeightMax = 6000
      
      altitudeBlend = 0.01
      altitudeFrequency = 2
      altitudeOctaves = 2
      altitudePersistance = 0.5
      altitudeSeed = 53453

      latitudeBlend = 0.05
      latitudeFrequency = 12
      latitudeOctaves = 6
      latitudePersistance = 0.5
      latitudeSeed = 53456345

      longitudeBlend = 0.05
      longitudeFrequency = 12
      longitudeOctaves = 4
      longitudePersistance = 0.5
      longitudeSeed = 98888

      order = 100
      enabled = True
      name = LCExample

      Scatters
      {
        ...
      }
      LandClasses
      {
        ...
      }
    }
  }
}
```

|Property|Format|Description|
|--------|------|-----------|
|createColors|Boolean|Whether to use/affect colors.|
|createScatters|Boolean|Whether to create scatters.|
|heightMap|File Path|Use currently unknown - could be using it as a mask?|
|useHeightMap|Boolean|Whether to use the height map for...?|
|vHeightMax|Decimal|The max height for the height map?|
|altitudeBlend|Decimal|The blend amount with adjacent terrain.|
|altitudeFrequency|Decimal|The size of the each feature of the terrain noise. As frequency gets bigger, size gets smaller.|
|altitudeOctaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|altitudePersistance|Decimal|The complexity of or amount of detail in the noise.|
|altitudeSeed|Integer|The random seed of the noise.|
|latitudeBlend|Decimal|The blend amount with adjacent terrain.|
|latitudeFrequency|Decimal|The size of the each feature of the terrain noise. As frequency gets bigger, size gets smaller.|
|latitudeOctaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|latitudePersistance|Decimal|The complexity of or amount of detail in the noise.|
|latitudeSeed|Integer|The random seed of the noise.|
|longitudeBlend|Decimal|The blend amount with adjacent terrain.|
|longitudeFrequency|Decimal|The size of the each feature of the terrain noise. As frequency gets bigger, size gets smaller.|
|longitudeOctaves|Integer|The amount of blanketing over the noise. Higher octaves mean rougher noise.|
|longitudePersistance|Decimal|The complexity of or amount of detail in the noise.|
|longitudeSeed|Integer|The random seed of the noise.|
