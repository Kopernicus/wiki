<!--Title: Wormholes-->
<!--Subtitle: Interstellar Peekaboo with a chance of Heat Death-->

```text
@Kopernicus:AFTER[Kopernicus]
{
  Body
  {
    name = WormholeExample
		
    //body things
    
    Wormhole
    {
      partner = WormholeTarget //The name of the body the wormhole "connects" to
      influenceAltitude = 8000 //Altitude in meters, below this a ship is uncontrollable
      jumpMaxAltitude = 7700 //maximum periapsis height for jump to occur
      jumpMinAltitude = 10 //minimum periapsis height for jump to occur
      entryMessage = test //message displayed when entering the influenceAltitude
      exitMessage = yeet //message displayed when exiting the influenceAltitude
      heatRate = 0.1 //amount of heat applied per frame while jumping
      entryMsgDuration = 7 //number of seconds the entry message is on-screen (optional, default is 2)
      exitMsgDuration = 7 //number of seconds the exit message is on-screen (optional, default is 2)
    }
  }
}
```
