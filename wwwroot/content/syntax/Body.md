The `Body { }` node contains all the aspects of a body and describes the essential components for making a body. `Body { }` is a subnode of the `@Kopernicus` node.

 ## Subnodes {#subnodes}
- [Template { }](/Syntax/Template)
- [Properties { }](/Syntax/Properties)
- [Orbit { }](/Syntax/Orbit)
- [ScaledVersion { }](/Syntax/ScaledVersion)
- [Atmosphere { }]({/Syntax/Atmosphere)
- [PQS { }](/Syntax/PQS)
- [Ocean { }](/Syntax/Ocean)
- [Rings { }](/Syntax/Rings)
- [HazardousBody { }](/Syntax/HazardousBody)
- [SpaceCenter { }](/Syntax/SpaceCenter)
- [Debug { }](/Syntax/Debug)
- PostSpawnOrbit { }

## Example {#example}
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
|identifier|Text|The Unique Body Identifier (UBI) for the body. Used in the [Interstellar Consortium](https://forum.kerbalspaceprogram.com/index.php?/topic/177439-kopernicus-interstellar-consortium/) and follows the format `System/Body`.|
|implements|Text|(Optional) The UBIs that the body implements. Any number of these can be used. Each line should have the key "implements" and have a UBI as the value.|
|finalizeOrbit|Boolean|Whether the orbit of the body should be finalized.|
|randomMainMenuBody|Boolean|Whether the body should have a chance at being displayed on the Main Menu.|
|contractWeight|Integer|How often contracts should be generated for a body. Default is 30.|

## Note on cbNameLater {#cbnamelater}
A common usecase for cbNameLater was for mods that replaced Kerbin with another planet, to name it to the correct (non-Kerbin) name after loading if Properties/displayName didn't work. A new, better way to do this has been added in recent versions:
```
@Kopernicus_config
{
    %HomeWorldName = CorrectName
}
```