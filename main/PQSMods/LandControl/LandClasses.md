```
landClasses	//Not unlike HCM, land classes are assigned, allowing for color change, what scatters are created in which area, etc.
{
    Value
    {
        alterApparentHeight = 100
        alterRealHeight = 10	//These "alter" inputs allegedly adjust the terrain, but I've only witnessed true PQS adjustment in the creation of icecaps.
        color = 1,1,1,0		//What color you want the land to be
        coverageBlend = 1	//How much it blends with terrain under (and/or over?) it
        coverageFrequency = 12	
        coverageOctaves = 6	//Honestly not sure this actually does anything
        coveragePersistance = 0.5
        coverageSeed = 234124
        name = IceCaps	//Self explanatory
        latDelta = 0
        latitudeDouble = True	//Whether it should use latitudeDouble as well as normal latitude
        lonDelta = 1	//Not a clue what these Delta options do
        minimumRealHeight = 20
        noiseBlend = 0.25	//How much colored noise blends with standard Value color
        noiseColor = 0.552238822,0.519182861,0.480795324,0	//Color of colored noise added
        noiseFrequency = 24	//Frequency of the color noise
        noiseOctaves = 8	//Octaves of noise, duh
        noisePersistance = 0.5	//?
        noiseSeed = 5646345	//Seed for color noise
        delete = False
        altitudeRange	//Like HCM, determines the heights at which this land class affects color and/or creates scatters
        {
            endEnd = 2	//In all of these endEnd & endStart/startEnd & startStart inputs, the density/intensity of whatever it is you're doing will fade from the first input to the second.  They will not if you just make them the same number.
            endStart = 2
            startEnd = -0.5
            startStart = -0.5
        }
        latitudeRange	//Determines the start point for latitude range
        {
            endEnd = 0.0414999984204769
            endStart = 0.0399999991059303
            startEnd = -10
            startStart = -10
        }
        latitudeDoubleRange	//Determines the end point for latitude range
        {
            endEnd = 11
            endStart = 11
            startEnd = 0.96000000089407
            startStart = 0.958500001579523
        }
        longitudeRange	//Determines the longitude of the land class
        {
            endEnd = 10
            endStart = 10
            startEnd = -10
            startStart = -10
        }
        scatters	//List what scatters you want to appear in this land class, if any.
        {
            Value
            {
                density = -1	//Unknown?  At first I suspected it increased the density of scatter spawns up to 20.
                scatterName = BrownRock		//Must match the name set under the previous scatters node
                delete = False
            }
        }
    }
}
```