<!--Subtitle: The basic building blocks of every world-->
The `Properties { }` node in a configuration file for Kopernicus describes the planet itself. Parameters like description, mass, gravity and biomes are specified here. 



<table width= 500 border=1>
<tr><td><b>Variable</b></td><td><b>Type</b></td><td><b>Description</b></td></tr>
<tr><td>description</td><td>string</td><td>Here goes the description for the info box of the body that you can access in map view.<sup>*</sup></td></tr>
<tr><td>useTheInName</td><td>TRUE/FALSE</td><td>If the body name should be prefixed with "the" in some situations, such as "the sun."</td></tr>
<tr><td>radius</td><td>integer</td><td>The radius (half of the body's diameter) of the body in meters.</td></tr>
<tr><td>mass</td><td>float</td><td>The mass of the body in kilograms. You can use scientific notation here, like `1.234567+e20`</td></tr>
<tr><td>[gravParameter](https://en.wikipedia.org/wiki/Standard_gravitational_parameter)</td><td>float</td><td>Standard gravitational parameter, calcualted as the [gravitation constant](https://en.wikipedia.org/wiki/Gravitational_constant)(G) times the mass(M) of the body: G*M. This is the parameter that is used in the actual simulation. The gravitational acceleration in any point would be calculated as gravParameter/r<sup>2</sup> where r is a distance from that point to the body center.</td></tr>
<tr><td>geeASL</td><td>float</td><td>The Gravitational parameter At Sea Level in Gs. For Earth/Kerbin this would simply be 1. If the reference body is Kerbin, which has an acceleration of gravity of 9.8m/s<sup>2</sup>, geeASL= gravParameter/9.8</td></tr>
<tr><td>rotates</td><td>TRUE/FALSE</td><td>Statement that determines if the body rotates or not. In reality no celestial body doesn't rotate, but in KSP the Sun, aka Kerbol, doesn't rotate.</td></tr>
<tr><td>[rotationPeriod](https://en.wikipedia.org/wiki/Rotation_period)</td><td>integer</td><td>The period in seconds that the body needs to rotate around its axis one time.</td></tr>
<tr><td>initialRotation</td><td>integer</td><td>The rotation in degrees (0-359) that the body starts at (clockwise)</td></tr>
<tr><td>inverseRotThresholdAltitude</td><td>integer</td><td>?Altitude where the Game switches the reference frame in meters?</td></tr>
<tr><td>[albedo](https://en.wikipedia.org/wiki/Albedo)</td><td>float</td><td>?How reflective is the body. scale from 0 to 1?</td></tr>
<tr><td>[emissivity](https://en.wikipedia.org/wiki/Emissivity)</td><td>float</td><td>?scale from 0 to 1?</td></tr>
<tr><td>coreTemperatureOffset</td><td>?</td><td>?</td></tr>
<tr><td>[tidallyLocked](https://en.wikipedia.org/wiki/Tidal_locking)</td><td>TRUE/FALSE</td><td>Statement that determines if the body is tidally locked to its parent. This means that it takes as long to rotate arounds its own axis as it does to make a full orbit around its parent. In real-life and KSP most (large) moons are tidally locked.</td></tr>
<tr><td>isHomeWorld</td><td>TRUE/FALSE</td><td>Statement that determines if this is the body that houses KSC. For stability's sake It's recommended to keep this at false for any bodies you add.</td></tr>
<tr><td>timewarpAltitudeLimits</td><td>array of integers</td><td>Determines at which altitude above sealevel certain timewarp altitudes become available. 0 30000 30000 60000 100000 300000 600000 800000 means that 1x timewarp is available at 0 meters, 5x timewarp at 30000 meters all the way up to the max timewarp starting at 800000 meters.</td></tr>
<tr><td>sphereOfInfluence</td><td>float</td><td>In meters. The sphere of influence of the body. This is generally calculated as described [here](http://wiki.kerbalspaceprogram.com/wiki/Sphere_of_influence). In case you need it to be unrealistically big or small you can change it here.</td></tr>
<tr><td>[hillSphere](https://en.wikipedia.org/wiki/Hill_sphere)</td><td>integer</td><td>In meters. Similar to Sphere of Influence. ?How does Kopernicus/KSP use it?</td></tr>
<tr><td>solarRotationPeriod</td><td>?</td><td>?</td></tr>
<tr><td>navballSwitchRadiusMult</td><td>integer</td><td>?In meters?</td></tr>
<tr><td>navballSwitchRadiusMultLow</td><td>integer</td><td>?In meters?</td></tr>
<tr><td>selectable</td><td>?TRUE/FALSE?</td><td>?If the body should be unselectable?</td></tr>
<tr><td>RDVisibility</td><td>?</td><td>?</td></tr>
<tr><td>maxZoom</td><td>?integer?</td><td>?Max Zoom limit for TrackingStation and MapView. Set the number of meters that can fit in the full height of the screen?</td></tr>
<tr><td>biomeMap</td><td>file path</td><td>The path to the biome map texture, without the GameData in front of it and with an extension. See the Biome subnode for more information</td></tr>
</table>

<sup>*</sup>As of 2018-0531 you can insert line breaks in descriptions using the syntax '\\\nn'


### Subnodes
***
* Biomes { }
* ScienceValues {}

## Example:

        Properties
        {
            description = Some nice description
            radius = 600000
            mass = 5.29E+22
            gravParameter = 9.81
            geeASL = 1.0
            rotates = true
            rotationPeriod = 21600
            initialRotation = 0
            tidallyLocked = false
            isHomeWorld =   true
            timewarpAltitudeLimits = 0 30000 30000 60000 100000 300000 600000 800000
            sphereOfInfluence = 84159286
        }
