---
layout: default
title: Reentry Effects
subtitle: Customize your Reentry like never before!
---

The `ReentryFX { }` subnode is a part of Kopernicus Expansion and allows you to add and customize the body's reentry effects.

**Example**
```
// This is the default effect configuration, provided by SnailsAttack.
Body
{
	ReentryEffects
	{
		ReentryHeat
		{
			airspeedNoisePitch
			{
				min = 0.5
				max = 2
			}
			airspeedNoiseVolume
			{
				min = 0
				max = 1
			}
			edgeFade
			{
				min = 0
				max = 0.3
			}
			falloff1
			{
				min = 0.9
				max = 0.9
			}
			falloff2
			{
				min = 1
				max = 1
			}
			falloff3
			{
				min = 2
				max = 1
			}
			intensity
			{
				min = 0
				max = 0.11
			}
			length
			{
				min = 5
				max = 15
			}
			lightPower
			{
				min = -3
				max = 8
			}
			wobble
			{
				min = 1
				max = 1
			}	
			color
			{
				min = 1,0.294,0.114,0
				max = 1,0.294,0.114,1
			}
		}
		Condensation
		{
			airspeedNoisePitch
			{
				min = 0.3
				max = 1
			}
			airspeedNoiseVolume
			{
				min = 0
				max = 0.5
			}
			edgeFade
			{
				min = 0
				max = 0
			}
			falloff1
			{
				min = 0.9
				max = 0.9
			}
			falloff2
			{
				min = -0.6
				max = -0.6
			}
			falloff3
			{
				min = 0.5
				max = 0.5
			}
			intensity
			{
				min = 0
				max = 0.11
			}
			length
			{
				min = 2
				max = 3.5
			}
			lightPower
			{
				min = 0
				max = 0
			}
			wobble
			{
				min = 0
				max = -0.2
			}	
			color
			{
				min = 0.22,0.22,0.22,0
				max = 0.22,0.22,0.22,1
			}
		}
	}
}
```

NOTE: Anything that isn't a Node is a Min/Max Node that contains simply a `min` and a `max` key that details the minimum and maximum for that Min/Max Node. Also, items between Nodes belong to the Node above it.

|Property/Node|Format|Description|
|-------------|------|-----------|
|ReentryEffects|Node|Node for all aerodrag effects.|
|ReentryHeat|Node|Node for the reentry flame effects. The minimum values occur when the reentry flames are fading into or out of existence, and the maximum values occur when the reentry flames are in full effect. The "Fade-in" and "Fade-out" duration depend on your craft's speed and the atmospheric pressure.|
|airspeedNoisePitch|Single|"Woosh" sound effect pitch. 1 is probably the "normal" pitch, but the maximum is unknown.|
|airspeedNoiseVolume|Single|"Woosh" sound effect volume. 1 appears to be the max volume.|
|edgeFade|Single|How intense the flames are and how much they fade closer to the ends and edges of the plume. Values greater than 2(?) are invisible.|
|falloff1|Single|Determines some aspect of the intensity of the min-max transition fade as well as how much the reentry flames appear to billow. 0 makes the effects appear straight, values greater than 2 look bad. Negative values of ~1 sort of make the transitions overlap and the flames bunch up together, while negative values greater than 5 just become extremely buggy.|
|falloff2|Single|How "fuzzy" and wobbly the reentry effects appear perpendicular to your drag. The highest effective value is somewhere around 5. 0 makes the effects appear straight, negative values of ~1 make the flames project outwards behind the craft, and higher values make it shoot out of the craft like a laser beam.|
|falloff3|Single|Function undetermined. Disables the reentry flames if set to 0 or a negative number.|
|intensity|Single|How sporadically the reentry flames wobble close to the ends and edges of the plume.|
|length|Single|How large the reentry flames are. 1 is right up against the craft, 10 envelopes the whole ship.|
|lightPower|Single|The intensity of the light which is cast upon the wind-wards side of your  craft during reentry. 10 means it'll be very bright, 0 means there won't be any.|
|wobble|Single|How sporadically the reentry flames wobble on a large scale. 0 makes the effects stationary (sort of), 1 makes em vibrate as normal. Values higher than 1 tend not to look so good.|
|color|Color|The color of the reentry flames in RGBA. Darker colors will result in more flames which are more transparent, negative values are invisible.|
|Condensation|Node|The minimum values occur when the condensation plume is fading into or out of existence, and the maximum values occur when the condensation plume is in full effect. The "Fade-in" and "Fade-out" duration depend on your craft's speed and the atmospheric pressure.|
|airspeedNoisePitch|Single|"Woosh" sound effect pitch. 1 is probably the "normal" pitch, but the maximum is unknown.|
|airspeedNoiseVolume|Single|"Woosh" sound effect volume. 1 appears to be the max volume.|
|edgeFade|Single|How intense the plume is and how much it fades closer to its ends and edges. Values greater than 2(?) are invisible.|
|falloff1|Single|Determines some aspect of the intensity of the min-max transition fade as well as how much the condensation plume appears to billow. 0 makes the effects appear straight, values greater than 2 look bad. Negative values of ~1 sort of make the transitions overlap and the plumes bunch up together, while negative values greater than 5 just become extremely buggy.|
|falloff2|Single|How "fuzzy" and wobbly the condensation plume appears perpendicular to your drag. The highest effective value is somewhere around 5. 0 makes the effects appear straight, negative values of ~1 make the plume project outwards behind the craft, and higher values make it shoot out of the craft like a laser beam.|
|falloff3|Single|Function undetermined. Disables the reentry flames if set to 0 or a negative number.|
|intensity|Single|How sporadically the condensation plume wobbles close to the ends and edges of the plume.|
|length|Single|How large the condensation plume is.|
|lightPower|Single|The intensity of the light which is cast upon the wind-wards side of your  craft when the condensation plume is in effect. 10 means it'll be very bright, 0 means there won't be any.|
|wobble|Single|How sporadically the condensation plume wobbles on a large scale. 0 makes the effects stationary (sort of), 1 makes em vibrate as normal. Values higher than 1 tend not to look so good.|
|color|Color|The color of the condensation plume in RGBA. Darker colors will result in a more transparent plume, negative values are invisible.|
