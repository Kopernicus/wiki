<!-- TITLE: Emissive FX -->
<!--Subtitle: Ocean? More like Glowcean!-->

```text
//Example provided by SnailsAttack
@Kopernicus:AFTER[Kopernicus]
{
  Body
  {
    name = glowboy
    //body things
    
    ScaledVersion
    {
      //scaledspace things
      
      EmissiveOverlay
      {
        emissiveMap = DauntlessPlanetPack/PluginData/TaurusMaps/Taurus_Color.png //a texture file describing amount of glow
        color = RGBA(255,108,0,1) //the color of the glow
        brightness = 1 //glow brightness
        transparency = 0.1 //how much of the original texture shows though the glow?
      }
    }
    
    Ocean
    {
      Mods
      {
        EmissiveFX
        {
          color = 1.0,0.25,0,1 //color of glow
          brightness = 1 //how bright is the glow?
          transparency = 0.1 // how much of the original texture shows though the glow?
        }
      }
    }
  }
}
```
