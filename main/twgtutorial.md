---
layout: default
title: A beginner`s guide to Kopernicus - The basics
---
```
	Body
	{
		Welcome!
		PQS
		{
			Mods
			{
				VertexNiceTutorialMusic
				{
					music = idk
					doesThisModExist = nope
					isThisJokeGettingTooLong = true
				}
			}
		}
	}
}
```
All kidding aside, welcome to this tutorial on Kopernicus modding. So, let me teach you how to use perhaps one of the most creative and powerful mods there is for KSP: Kopernicus. While you can simply download packs, some of you may be willing to give it a try for yourself.

However, many of you probably have no idea what all those confusing lines and numbers do. I will explain that here. In the horrible joke above, I did already show how to start your config. I will repeat the important part and filter out the joke below. First off, you need to create a config file. Simply open notepad and click `save as`. Then save it as "(Yourplanetname).cfg". Remember to add the .cfg part to the name! Most of the time it will result in a config file. If not, download a pack like Outer Planets Mod or New Horizons, copy and paste a config and delete everything inside.

So, now you`ve got your config file! The next step is adding everything Kopernicus needs to create a planet.

Step 1: starting
```
@Kopernicus:AFTER[Kopernicus]
{
	Body
	{
		
	}
}
```
Now, what everthing does: the `@Kopernicus:AFTER[Kopernicus]` forces KSP to load your planet(s) after Kopernicus itself. Otherwise, it would load your pack with Kopernicus still inactive, and thus Kopernicus will not load your pack since it has already been loaded by KSP.

Now,  we need to add some basic information.

Step 2: basic info
```
@Kopernicus:AFTER[Kopernicus]
{
	Body
	{
		name = [Your Planet`s Name]
		cacheFile = [Optional]
	}
}
```
The planet's name speaks for itself, it is the name that will be seen in-game. The `cacheFile` is optional: Kopernicus generates a cache file by default, but with the cacheFile line you can specify a filepath and force Kopernicus to generate the cachefile(s) in a specific location. For instance, you can use the following filepath: "MyFirstPack/CacheFiles/MyPlanet.bin". As you can see, you should not add `GameData/`, Kopernicus automatically searches in the GameData folder. Also, make sure that, at the end, you add the name of your planet plus `.bin`. Never ever forget to add `.bin`!

Step 3: Template

It might be a good idea to add the following lines:
```
Template
{
    name = Kerbin
    removeAllPQSMods = true
    removeOcean = true
}
```
These lines go right after `name` in the `Body { }` node. Your entire config now looks like this:
```
@Kopernicus:AFTER[Kopernicus]
{
	Body
	{
		name = [Your Planet`s Name]
		cacheFile = [Optional]
		Template
		{
			name = Laythe
			removeAllPQSMods = true
			removeOcean
		}
	}
}
```

All the other nodes that will be talked about in this tutorial go inside the `Body { }` node. Don't worry if you get lost, the completed config will be posted at the end.

In this case, I chose Laythe as a template. If you choose a template, Kopernicus clones a stock planet and renames it. Next, you can either add:

- `removeAllPQSMods = true` to remove all PQS mods and turn the templated planet into a flat textureless sphere

- `removePQSMods = PQSMod1,PQSMod2,etc.` to remove specific PQSmods from the templated planet that your planet does not need.

As you can see I also added `removeOcean = true`. I think that line is pretty self-explanatory.

Step 4: Properties

Now we will set the physical properties of your planet.
```
Properties //Physical properties
{
    description = First planet you ever made! You can be proud of yourself.
    radius = 7000000 //Distance from the planet's core to it's surface. How 'big' is the planet?
    geeASL = 0.67 //Surface gravity in Gs
    rotationPeriod = 36000 // how long a day is on your planet, measured in seconds
    rotates = true //Optional, defaults to true
    tidallyLocked = false // optional, defaults to false
    initialRotation = 0 // optional, defaults to 0
    isHomeWorld = false //optional, defaults to false
    timewarpAltitudeLimits = 0 30000 30000 60000 300000 300000 400000 700000 // the altitudes the various timewarp levels become available at
    ScienceValues //Scientific expiriments value multiplier
    {
        landedDataValue = 2 //For expiriments taken on the surface
        splashedDataValue = 2 //For expiriments taken while splashed down
        flyingLowDataValue = 11 //For expiriments taken while flying in the lower atmosphere
        flyingHighDataValue = 8 //For expiriments taken while flying in the upper atmosphere
        inSpaceLowDataValue = 7 //For expiriments taken in space, close to your planet
        inSpaceHighDataValue = 6 //For expiriments taken in space, far away from your planet
        recoveryValue = 7 //Science multiplier for expiriment data taken from recovered vessels
        flyingAltitudeThreshold = 12000 // transition altitude between flying low/flying high
        spaceAltitudeThreshold = 140000 // transition altitude between in space low/in space high
    }
}
```
As you can see I`ve added some notes to the config. You don't have to include those in your config. Anyhow, the `description` defines the info displayed when clicking the info-tab in the map view in-game. `tidallyLocked` determines if a planet`s surface does not move relative to the parent object: it`s rotation period is identical to it`s orbital period. `initialRotation` determines how a planet is rotated on start. `isHomeWorld` is for debugging purposes.

Then there`s `timewarpAltitudeLimits`. This entry determines what timewarp speed is unlocked at what altitude. For instance, in the example code the time warp speeds 5x and 10x are unlocked at 30000m above sea level.

Step 5: Orbit Properties

It`s already starting to look like something, isn`t it? Now, we must specify your planet`s orbit.
```
Orbit //Orbit properties
{
    referenceBody = Sun
    color = 1,1,1,1
    inclination = 0.5 //Orbit inclination relative to referenceBody`s equator
    eccentricity = 0.02 //Orbit eccentricity, how elliptical is the orbit?
    semiMajorAxis = 9000000000 //Average distance to reference body
    longitudeOfAscendingNode = 0 //Position of ascending node relative to the surface of the reference body
    argumentOfPeriapsis = 0
    meanAnomalyAtEpoch = 0
    epoch = 0 //Position of your planet when it is first loaded. Not nessecary to give a number unless your planet shares it`s orbit, can be used to create laplace resonances
}
```
Again, I`ve left some notes in place. The `referenceBody` defines what celestial body your planet orbits. You can use stock celestial bodies like Sun, Moho, and Dres but you can also use your own planets or planets added by another mod.

The `color` entry defines the color of your planet`s orbit line in the map view. For instance, Jool`s orbit is green, Eve`s orbit is purple, Kerbin`s orbit is blue, Duna`s orbit is red, and the color specified in the example would result in a white orbit line. The `1,1,1,1` determine how much red, green and blue is present, and the last one determines the `lightness`. It should be on a scale of 0-1 or you could use `RGBA( R(0-255), G(0-255), B(0-255), A(0-255))`

Step 6: ScaledSpace

If you`d load up your planet right now, it wouldn`t work yet. But if it would work, in the map view it would look identical to the templated planet. To combat this we need to update the ScaledSpace with the following lines:
```
ScaledVersion // version of your planet that exists in scaledspace
{
    type = Atmospheric
    fadeStart = 0
    fadeEnd = 0
    Material
    {
        texture = (filepath)/YourPlanet_color.dds //Texture map
        normals = (filepath)/YourPlanet_normal.dds //Normal map
        shininess = 0
        specular = 0.0,0.0,0.0,1.0
        rimPower = 3 //Atmosphere rim power
        rimBlend = 0.2 //Atmosphere rim blend
        Gradient //Atmosphere rim color defenitions
        {
            0.0 = 0.06,0.06,0.06,1
            0.5 = 0.05,0.05,0.05,1
            1.0 = 0.0196,0.0196,0.0196,1
        }
    }
}
```
Now, the following entries are optional: `type`, `fadeStart`, `fadeEnd`, `shininess`, `specular`, `rimPower`, `rimBlend`, and `Gradient`. Nevertheless I will explain what they do.

`type`, `fadeStart` and `fadeEnd` can be used to make things look just a bit better. `type` examples are `Atmospheric` and `Vacuum`.

`shininess` and `specular` can add a little touch to a planet`s scaledspace such as an icy glow.

`rimPower`, `rimBlend` and `Gradient` create a colored atmospheric rim around your planet. For instance, Eve has this purple glow around it. Furthermore: under `Gradient`, you see `0.0`, `0.5` and `1.0`. For `1.0` you must copy exactly what I wrote, and for 0.0 and 0.5, just add the colors you want. 0.0 and 0.5 define the atmosphere rim color on opposite sides of the planet, which color defines which side I do not know for sure. It just needs some trial and error.

Now, the most important entries: `texture` and `normals`. `texture` needs a filepath that leads to the texture file you made for your planet. It will glue this texture over the templated planet. To make sure that your planet neither looks like a recolored Eve for example nor a flat ball, the `normals` entry needs a normal map in the `DXT5_nm` format. The normal map will make your planet look 3D in scaledspace rather than a perfectly smooth orb. If you are uncertain how to export your normal map as `DXT5_nm`, I will do a tutorial on that too. It can be done with Photoshop, but I`ve managed to do it with GIMP, which is absolutely free! Futhermore, you do not have to save it as DXT5_nm for your normal map to work, it`s just that normal maps that are not saved as DXT5_nm create an annoying lighting issue in ScaledSpace. It`s not gamebreaking, it just looks ugly.

Step 7: Atmospheres (optional step)

If you want to create a planet that has an atmosphere, then do not skip this step. Otherwise, go on.

If you`re still here, add the following lines:
```
Atmosphere
{
    ambientColor = 0.24, 0.25, 0.25, 1
    lightColor = 0.65, 0.57, 0.475, 0.5
    enabled = true
    oxygen = true
    altitude = 77000.0
    pressureCurve
    {
        key = 0		121.59		-1.32825662337662E-02	-1.32825662337662E-02
        key = 3850	70.45212	1.08101766233766E-02	-1.08101766233766E-02
        key = 7700	38.35164	-6.61608311688312E-03	-6.61608311688312E-03
        key = 11550	19.50828	-3.70578701298701E-03	-3.70578701298701E-03

        key = 15400	9.81708		-1.89074805194805E-03	-1.89074805194805E-03
        key = 19250	4.94952		-9.4665974025974E-04	-9.4665974025974E-04
        key = 23100	2.5278		-4.7371948051948E-04	-4.7371948051948E-04
        key = 26950	1.30188		-2.38877922077922E-04	-2.38877922077922E-04
        key = 30800	0.68844		-1.20685714285714E-04	-1.20685714285714E-04
        key = 34650	0.3726		-6.2212987012987E-05	-6.2212987012987E-05
        key = 38500	0.2094		-3.29298701298701E-05	-3.29298701298701E-05

        key = 42350	0.11904		-1.80935064935065E-05	-1.80935064935065E-05
        key = 46200	0.07008		-1.02857142857143E-05	-1.02857142857143E-05
        key = 50050	0.03984		-6.21818181818182E-06	-6.21818181818182E-06
        key = 53900	0.0222		-3.63116883116883E-06	-3.63116883116883E-06
        key = 57750	0.01188		-2.07272727272727E-06	-2.07272727272727E-06
        key = 61600	0.00624		-1.13766233766234E-06	-1.13766233766234E-06
        key = 65450	0.00312		-6.07792207792208E-07	-6.07792207792208E-07
        key = 69300	0.00156		-3.42857142857143E-07	 -3.42857142857143E-07
        key = 73150	0.00048		-2.02597402597403E-07	-2.02597402597403E-07
        key = 77000	0		-1.24675324675325E-07	-1.24675324675325E-07
    }
    pressureCurveIsNormalized = false
    temparatureSeaLevel = 288.15
    temparatureCurve
    {
        key = 0		288.15		-0.008333333766		-0.008333333766
        key = 9240	212.4633208	-0.001180336104		-0.001176697662
        key = 16170	212.4633208	0.001176697662		0.001176697662
        key = 23870	266.5252345	0.0006431355844		0.0006431355844
        key = 43120	266.5252345	-0.0008869198701	-0.0008869198701
        key = 61600	183.9579481	-0.001180336104		-0.001180336104
        key = 69300	183.9579481	0.0006152915584		0.0006152915584
        key = 77000	226.2245352	0.0009020832468		0.0009020832468
        key = 115500	0		-0.0005839079221	-0.0005839079221
    }
    temparatureSunMultCurve
    {
        key = 0			1	0			0
        key = 5923.076923	0.5	-0.00007792207792	-0.0001333050649
        key = 6526.656231	0	0			0
        key = 11883.45738	0	0			0
        key = 28045.35461	0.2	0			0
        key = 42527.78708	0.2	0			0
        key = 54071.53228	0	0			0
        key = 77000		0.4	0			0
    }
}
```
Looks quite confusing, doesn`t it? No worries, I will explain everything.

Let`s begin with `ambientColor` and `lightColor`. `ambientColor` provides a slight tint on the spacecraft. For instance, take a good look at your spacecraft when you`re on Eve, and you will see that your craft is tinted slightly purple. That is ambientColor at work. `lightColor` defines what color the atmosphere is. lightColor is a bit glitchy and needs some trial and error to work. Furthermore, the red and blue are swapped in the lightColor entry, so lightColor needs it`s color in the following format: Blue, Green, Red, Alpha.

`enabled` is pretty self-explanatory, and `oxygen` determines if the atmosphere of your planet has oxygen. It must be either `true` or `false`. An atmosphere that contains oxygen allows the use of air-breathing engines, like on Kerbin and Laythe.

`altitude` determines the maximum altitude of your atmosphere. Let`s take Kerbin for example. On Kerbin, the `altitude` is 70000m. Laythe = 50000m, Eve is roughly 90000m, etc.

Then `pressureCurve`. That one confuses me too. It determines the atmospheric pressure at certain altitudes. There is a calculator that can calculate the keys for you, but you will have to ask @KillAshley for a link.

The calculator can also calculate the `temparatureCurve` and `temparatureSunMultCurve` for you. As for the other entries: `temparatureSeaLevel` determines the temparature at sea level in Kelvin, and `pressureCurveIsNormalized` should be set to false.

Step 8: confusing stuff

Now we`re getting to the confusing part: PQS mods. Start off by creating the following lines:

		PQS
		{
			Mods
			{
Now we will add the mods needed for your basic planet one by one. Let`s start with VertexHeightMap:

				VertexHeightMap
				{
					map = (Filepath)/YourPlanet_height.dds
					offset = -500
					deformity = 3000.0
					scaleDeformityByRadius = false
					order = 20
					enabled = true
				}
VertexHeightMap needs a heightmap to function. It is possible to create planets that do not need heightmaps, but that is more advanced stuff, and this is about the basics, so we`ll stick with heightmaps for now. `offset` is basically how elevated the terrain is relative to your planet`s sea level. This can be used to fine-tune the sealevel to make sure the coasts are all correct. `deformity` basically asks `how high do you want the tallest mountains to be?` and asks an answer in meters. In the answer I chose 3000.0m.

Set `scaleDeformityByRadius` to false, and set `enabled` to true. Then there`s `order`. PQSMods have to be loaded in a specific order. The lower the number, the earlier it is loaded. You can use this to specify which mod must be loaded in what order. Set this one to 20.

Alright, next mod: VertexColorMap. This one is optional. Only use this one if your planet is colored diffrently than it is in ScaledSpace.

				VertexColorMap
				{
					map = (filepath)/MyPlanet_color.dds
					order = 20
					enabled = true
				}
Probably the simples PQSMod out there. It applies the color map we used for ScaledSpace updating earlier to your planet`s surface.

Next mods are a bit more complex. Feel free to copy them if you`d like.

				VertexHeightNoiseVertHeightCurve2
				{
					deformity = 100
					ridgedAddSeed = 123456
					ridgedAddFrequency = 12
					ridgedAddLacunarity = 2
					ridgedAddOctaves = 4
					ridgedSubSeed = 654321
					ridgedSubFrequency = 12
					ridgedSubLacunarity = 2
					ridgedSubOctaves = 4
					simplexCurve
					{
						key = 0 0 0.1466263 0.1466263
						key = 0.7922793 0.2448772 0.6761706 1.497418
						key = 1 1 6.106985 6.106985
					}
					simplexHeightStart = 0
					simplexHeightEnd = 6500
					simplexSeed = 123456
					simplexOctaves = 4
					simplexPersistence = 0.6
					simplexFrequency = 12
					enabled = true
					order = 200
				}
				HeightColorMap
				{
					blend = 1
					order = 500
					enabled = true
					LandClasses
					{
						Class
						{
							name = Bottom
							altitudeStart = 0
							altitudeEnd = 0.7
							color = 0.1,0.1,0.1,1.0
							lerpToNext = true
						}
						Class
						{
							name = Base
							altitudeStart = 0.7
							altitudeEnd = 0.75
							color = 0.7,0.55,0.2,1.0
							lerpToNext = true
						}
						Class
						{
							name = Low
							altitudeStart = 0.75
							altitudeEnd = 0.85
							color = 0.7,0.6,0.4,1.0
							lerpToNext = true
						}
						Class
						{
							name = Grad
							altitudeStart = 0.85
							altitudeEnd = 0.95
							color = 1.0,0.9,0.7,1.0
							lerpToNext = true
						}
						Class
						{
							name = High
							altitudeStart = 0.95
							altitudeEnd = 2
							color = 0.95,0.95,0.9,1.0
							lerpToNext = false
						}
					}
				}
			}
		}
They add the basic landclasses and some basic height-values-and-stuffTM

Now, you`re done! Make sure everything is closed off correctly, it should look like this:

			}
		}
	}
}
All the way until the last `}` has no more tabs or spaces in front of it.

But, you can still add an ocean. In that case, do not close off everything. We will continue where the last PQSMod example code ended.

Step 9: Oceans

First off, the example code.

		Ocean
		{
			maxQuadLengthsPerFrame = 0.03
			minLevel = 2
			maxLevel = 12
			minDetailDistance = 8
			oceanColor = 0.15,0.25,0.35,1
			Material
			{
				colorFromSpace = 0.15,0.25,0.35,1
				color = 0.15,0.25,0.35,1
			}
			FallbackMaterial
			{
				colorFromSpace = 0.15,0.25,0.35,1
				color = 0.15,0.25,0.35,1
			}
			Mods
			{
				AerialPerspectiveMaterial
				{
					globalDensity = -0.00001
					heightFalloff = 6.75
					atmosphereDepth = 150000
					DEBUG_SetEveryFrame = true
					cameraAlt = 0
					cameraAtmosAlt = 0
					heightDensAtViewer = 0
					enabled = true
					order = 200
				}
				OceanFX
				{
					Watermain
					{
						waterTex-0 = BUILTIN/sea-water1
						waterTex-1 = BUILTIN/sea-water2
						waterTex-2 = BUILTIN/sea-water3
						waterTex-3 = BUILTIN/sea-water4
						waterTex-4 = BUILTIN/sea-water5
						waterTex-5 = BUILTIN/sea-water6
						waterTex-6 = BUILTIN/sea-water7
						waterTex-7 = BUILTIN/sea-water8
					}
					framesPerSecond = 1
					spaceAltitude = 150000
					blendA = 0
					blendB = 0
					texBlend = 0
					angle = 0
					specColor = 0.000,0.000,0.000,0.000
					oceanOpacity = 0
					spaceSurfaceBlend = 0
					enabled = true
					order = 200
				}
			}
			Fog
			{
				fogColorEnd = 0.15,0.25,0.35,1
				fogColorStart = 0.15,0.25,0.35,1
				skyColorOpacityBase = 0.7
			}
		}
	}
Long, confusing code, eh? Let me explain: leave `maxQuadLengtsPerFrame`, `minLevel`, `maxLevel` and `minDetailDistance` as is unless you know what you`re doing. With `oceanColor`, you can set your ocean`s color. Just input the wanted color, then copy that code and replace the color codes under `Material` and `FallbackMaterial`. Just punch in the same numbers.

Then, the `Mods`. I don`t recommend changing anything under `AerialPerspectiveMaterial` unless you know what you`re doing. But now it gets more interesting. Do you see `Watermain`, under `OceanFX`? There, we need to specify the ocean`s texture. You can use the BUILTIN textures specified in the example to get the same ocean textures as Eve, Kerbin and Laythe, or you could make your own ocean textures and create filepaths to those. Again, I don`t recommend touching the rest of the values unless you know what you`re doing.

Then there`s the `Fog` mod. This mod adds underwater fog. Just punch in the same colors as `oceanColor`.

But, there is one more, simple thing you can do to make your pack over 1000 times better!

How, you might ask. The answer is simple: Kopernicus has an incredible feature called OnDemandLoading: it will only load the textures of planets nearby, so if you would be orbiting Duna for instance, it would only load the textures of Duna and Ike, and won`t load the others, such as Moho, Dres, and Jool.

This saves a lot of memory! But, in order to make it work with your pack, there are two things that you must do:

- All textures must be stored in a folder called `PluginData`. There can be multiple PluginData folders, but as long as all textures are saved in a folder called PluginData, you`re good.

- All texture formats must be specified. This step is easy: at the end of every `texture-filepath`, add what format it is: .dds, .png, .jpg, etc.

And done! You are now using OnDemandLoading, which means that everyone using your pack will experience exploration with memory optimized.

And done! Again, make sure everything is closed off properly. I will soon write another tutorial on how to export your maps correctly through GIMP as well as more advanced Kopernicus stuff: procedural planets (heightmapless planets) and biomes. I hope this guide helped. If so, please let me know.