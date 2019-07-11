<!--Subtitle: On Tycho and Prague-->
##Example:

        Orbit
        {
            referenceBody = Sun
            color = 0.7,0.7,0.7,1
            mode = 1
            inclination = 1
            eccentricity = 0
            semiMajorAxis = 2620000000000
            longitudeOfAscendingNode = 40
            argumentOfPeriapsis = 10
            meanAnomalyAtEpoch = 0
            epoch = 30000000000000
        }
  

The Orbit node goes in the Body node.   
* referenceBody is what the object orbits. put in its name exactly as it is spelled.   
* color is the color of the orbit. it is a RGBA value, meaning the 1st number is how much red, 2nd is how much green, and 3rd is how much blue. the fourth value is how opaque the orbit appears, and it is 1 for every stock planet. each number is a value between 0 and 1 and reflects between 0 and 100%. searching RGB color picker in google gives several sites to allow you to visualise this.  
* inclination is how tilted the orbit is in degrees. 0 = normal, 90 = polar, 180 = retrograde ect...
* eccentricity is the difference between your planets apoapsis and periapsis. it is a value between 0 and 1 where 0 is a perfect circle and 1 is a straight line. 0.5 would give an oval shape.   
* semiMajorAxis is the average altitude of the planet above its referencebody's center.   
* longitudeOfAscendingNode is where the planet crosses the equator. it is useless without inclination.  
* argumentOfPeriapsis changes what longitude the periapsis is over.  
* meanAnomalyAtEpoch and epoch both say where the planet is on day 0.  

NOTE: Hyperedit uses these same values. Simply move your planet around with it and copy the values out of its "complex" tab into their respective feilds in the config. beware of SOI's changing size when they reload to adjust for the new position.
