---
layout: default
title: Wormholes
subtitle: Interstellar Peekaboo with a chance of heat death
---

The `Wormhole { }` node of the Kopernicus Expansion provides the ability to transform bodies into wormholes to other bodies.

**Example**
```
Body
{
	Wormhole
	{
		partner = WormholeTarget
		influenceAltitude = 8000
		jumpMaxAltitude = 7700
		jumpMinAltitude = 10
		entryMessage = Entering the wormhole.
		exitMessage = Bye, wormhole!
		heatRate = 0.1
		entryMsgDuration = 7
		exitMsgDuration = 7
	}
}
```

|Property|Format|Description|
|--------|------|-----------|
|partner|String|The name of the destination body.|
|influenceAltitude|Double|The altitude in meters at which the wormhole will start to affect the ship. Below this height, the ship is uncontrollable.|
|jumpMaxAltitude|Double|The maximum periapsis height for jump to occur.|
|jumpMinAltitude|Double|The minimum periapsis height for jump to occur.|
|heatRate|Double|The amount of heat, in Kelvins, applied to the ship per frame while jumping.|
|entryMessage|String|The message displayed when entering the influenceAltitude.|
|exitMessage|String|The message displayed when exiting the influenceAltitude.|
|entryMsgDuration|Single|The number of seconds that the entry message is on-screen. This value is optional, and the default is 2.|
|exitMsgDuration|Single|The number of seconds that the exit message is on-screen. This value is optional, and the default is 2.|