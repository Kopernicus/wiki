---
layout: default
title: Body
---

The `Body { }` node contains all the aspects of a body and describes the essential components for making a body. `Body { }` is a subnode of the `@Kopernicus` node.

**Subnodes**
- [Template { }]({{ site.baseurl }}{% link content/Template.md %})
- [Properties { }]({{ site.baseurl }}{% link content/Properties/Properties.md %})
- [Orbit { }]({{ site.baseurl }}{% link content/Orbit.md %})
- [ScaledVersion { }]({{ site.baseurl }}{% link content/ScaledVersion/ScaledVersion.md %})
- [Atmosphere { }]({{ site.baseurl }}{% link content/Atmosphere/Atmosphere.md %})
- [PQS { }]({{ site.baseurl }}{% link content/PQSMods/PQS.md %})
- [Ocean { }]({{ site.baseurl }}{% link content/Ocean.md %})
- [Rings { }]({{ site.baseurl }}{% link content/Rings.md %})
- [~~Particles { }~~]({{ site.baseurl }}{% link content/Particles.md %}) *(Removed in 1.8.1-1!)*
- [HazardousBody { }]({{ site.baseurl }}{% link content/HazardousBody.md %})
- [SpaceCenter { }]({{ site.baseurl }}{% link content/SpaceCenter.md %})
- [Debug { }]({{ site.baseurl }}{% link content/Debug.md %})
- PostSpawnOrbit { }

**Example**
```
@Kopernicus:AFTER[Kopernicus]
{
    Body
    {
        name = Pear
        cacheFile = Fruits/Cache/Pear.bin
        barycenter = false
        identifier = Fruits/Pear
        randomMainMenuBody = true
        contractWeight = 15

        Debug
        {
            ...
        }
        Template
        {
            ...
        }
        Properties
        {
            ...
        }
        Orbit
        {
            ...
        }
        ScaledVersion
        {
            ...
        }
        PQS
        {
            ...
        }
        Ocean
        {
            ...
        }
    }
```

|Property|Format|Description|
|--------|------|-----------|
|name|Text|The name of the body.|
|cacheFile|File Path|The path to the cache file for the body.|
|barycenter|Boolean|Whether the body should act as a barycenter. Also makes the body unselectable.|
|cbNameLater|Text|(Deprecated, use `Properties/displayName` to change the name instead.) Applies a name change after loading the body.|
|identifier|Text|The Unique Body Identifier (UBI) for the body. Used in the [Interstellar Consortium](https://forum.kerbalspaceprogram.com/index.php?/topic/177439-kopernicus-interstellar-consortium/) and follows the format `System/Body`.|
|self|List of String List|(Optional) The UBIs that the body implements. Each line in this node should have the key "implements" and have a String[] as a value.|
|finalizeOrbit|Boolean|Whether the orbit of the body should be finalized.|
|randomMainMenuBody|Boolean|Whether the body should have a chance at being displayed on the Main Menu.|
|contractWeight|Integer|How often contracts should be generated for a body. Default is 30.|
