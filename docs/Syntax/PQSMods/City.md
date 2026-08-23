# City
**Internal mod name:** `PQSMod_City`

City is the PQS mod used by Squad to implement easter eggs.
It is used to place statics on desired locations and mostly used to for easter eggs.
It is relatively easy to use, though can be annoying to work with at times.
Here's how it works:

---
### Example

```C#
PQS
{
    Mods
    {
        City
        {
            debugOrientated = False
            frameDelta = 1
            randomizeOnSphere = False
            reorientToSphere = True
            reorientFinalAngle = 0
            reorientInitialUp = 0,1,0
            repositionRadial = -0.9850651392,0.06465095745,0.15786005564
            repositionRadiusOffset = 3
            repositionToSphere = True
            repositionToSphereSurface = True
            repositionToSphereSurfaceAddHeight = True
            commnetStation = False
            isKSC = False
            order = 10023
            enabled = True
            name = Oscar
            LOD
            {
                Value
                {
                    visibleRange = 350000
                    model = CosmicSerenity/_Systems/Yunxiao/Meshes/Dinos/Saurundus/Oscar
                    scale = 3,3,3
                    delete = False
                }
            }
        }
        
    }
}
```
---
### Properties
Section will only cover the properties you'll actually use.

| Property | Format | Description |
| - | - | -
| randomizeOnSphere | Boolean | Self explanatory, randomizes the position of your object. |
| reorientToSphere | Boolean | Will reorient the object to the body's curve and not to the "Space referential". |
| reorientFinalAngle | Decimal | Defines the final orientation of the object (in Degrees). |
| repositionRadial | Decimal | Exact coordinates of the object in XYZ coordinates (See section below on how to convert Longitude and Latitude into these coordinates). |
| repositionRadiusOffset | Decimal | This parameter repositions vertically the object in reference to its anchor point (Usually the surface) in meters. 
| repositionToSphere | Boolean | Decides if the object uses the repositionRadial coordinates. |
| repositionToSphereSurface | Boolean | Dictates if the object's anchor point is set to the ground of your body. True -> Snaps on the surface, False -> Snaps to planet's center. (Preferably on `True`) |
| repositionToSphereSurfaceAddHeight | Boolean | Dictates if the object uses the repositionRadiusOffset value given. |
| commnetStation | Boolean | Tells the game if this object should act like a commnet ground dish on Kerbin (Direct connection to the KSC).|
| isKSC | Boolean | If the object should be considered as the KSC. |
| enabled | Boolean | If the PQS City should be enabled or not. |
| name | Text | Name of your PQS City, should be unique to each City. |
| visibleRange | Decimal | Visible range of the PQS City in meters. |
| model | File Path | File path of your model. Has to be in .mu format and has to be setup exactly like a part (texture referenced within the model and in the same folder, more on this later.)
| scale | Decimal | Size of the model, relative to its original size, only variable that can be edited in game via Kittopia.|
| delete | Boolean | If the City should be deleted or not. |

---
### repositionRadial
Here's a quick guide on how to position your PQS City where you want.
1. In-game, go to the exact location and copy the Latitude and Longitude values of your current position.

2. Run this python script in a Python interpreter (much easier and faster)
```Py
import math

PI = 3.14159265359

lat = float(input('Insert latitude: '))
long = float(input('Insert longitude: '))

# To rad
lat = lat*(2*PI/360)
long = long*(2*PI/360)

x = math.cos(lat)*math.cos(long)
y = math.sin(lat)
z = math.cos(lat)*math.sin(long)

print(f'X: {x}\nY: {y}\nZ: {z}')

print(f'repositionRadial = {x},{y},{z}')
```

3. Copy and paste the repositionRadial line given from the script.

4. That's it!

---

### Getting your model ready
I won't dive into much details so here's the basics.
This will need the blender .mu addon available here : https://github.com/taniwha/io_object_mu

Installation instructions here : https://forum.kerbalspaceprogram.com/topic/40056-12-17-blender-283-mu-importexport-addon/

So once your model is ready, here's how you'll set it up for KSP.
In the material tab scroll down until you see `Mu shader` and click on the `Shaders` dropdown

![1](https://i.imgur.com/qzDfAc9.png)

Load a preset - here I selected `KSP Diffuse` - and click the `Textures` Dropdown
Then, under `Name` you will write down the EXACT name of the texture the object will be using, no extensions.
![2](https://i.imgur.com/bVSoI58.png)

Export your .mu and the setup in your mod's folder should look like this:
![3](https://i.imgur.com/6Pkp52O.png)
They all NEED to be in the same folder (The one referenced in `model`).

Now if you've done everything correctly your PQS City should show up in-game.

---
### Animating PQS City
As of August 2026, not many people know how to achieve this, luckily, I wrote down a detailed guide on how to achieve this. You can find it here:
https://docs.google.com/document/d/1XJYBQ0QS0B8n3C9la1fozyeng8eGI96GhYxlkzd54Tg/edit?usp=sharing

![4](https://i.imgur.com/g0cFvxK.gif)
