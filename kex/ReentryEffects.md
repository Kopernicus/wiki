<!-- TITLE: Reentry Effects -->
<!-- SUBTITLE: Customize your Reentry like never before! -->
# Syntax/Layout
This is the default effect configuration, provided by SnailsAttack.

```text
@Kopernicus:AFTER[Kopernicus]
{
	@Body[Kerbin]
	{
    ReentryEffects // Node for all aerodrag effects. Min and max are for the lowest and highest speeds possible within each speed "category". 
    {
      ReentryHeat // Node for the reentry flame effects. Note that the max speed category only occurs at higher altitudes+speeds.
      {	
       	airspeedNoisePitch // Woosh sound effect pitch.
        {
          	min = 0.5 // 1 is probably the normal value, but the maximum possble value is unknown.
          	max = 2
        }
        airspeedNoiseVolume // Woosh sound effect volume.
        {
        	min = 0 // 1 appears to be the max volume.
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
        length // How large the reentry flames are.
        {
        	min = 5 // 1 is right up against the craft, 10 envelopes the whole ship
        	max = 15
        }
      	lightPower
      	{
    			min = -3
      		max = 8
      	}
        wobble // How sporadically the reentry flames wobble.
    		{
      		min = 1 // 0 makes the effects stationary (sort of), 1 makes em vibrate as normal. Values higher than 1 tend not to look so good.
    			max = 1
        }	
    		color // The color of the reentry flames in RGBA.
    		{
    			min = 1,0.294,0.114,0
          max = 1,0.294,0.114,1
      	}
      }
      Condensation
      {
       	airspeedNoisePitch // Woosh sound effect pitch.
        {
        		min = 0.3
        		max = 1
      	}
      	airspeedNoiseVolume // Woosh sound effect volume.
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
    		length // How large the condensation plume is.
    		{
      			min = 2
      			max = 3.5
    		}
    		lightPower
     		{
    			min = 0
    			max = 0
    		}
    		wobble // How sporadically the condensation plume wobbles.
    		{
      		min = 0
       		max = -0.2
    		}	
    		color // The color of the condensation plume in RGBA.
    		{
      		min = 0.22,0.22,0.22,0
      		max = 0.22,0.22,0.22,1
    		}
		  }
    }
  }
}
```
