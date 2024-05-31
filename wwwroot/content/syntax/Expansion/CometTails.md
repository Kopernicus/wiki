The `CometTails { }` subnode is a part of Kopernicus Expansion and allows you to add Dust and Ion trails to bodies.

## Example

This is the default configuration, except for the `type`.
```
Body
{
  CometTails
  {
    CometTail
    { 
      type = Ion
      color = 1,1,1,1
      rimPower = 1.41
      distortion = 0.143
      alphaDistortion = 0.262
      zDistortion = 0.12
      frequency = 0.1547
      lacunarity = 0.1518
      gain = 0.734
      radius = 2000
      length = 16000
      opacityCurve
      {
        key = 0 0.6
        key = 5E9 0.45
        key = 1.25E10 0.1
        key = 2E10 0.0075
        key = 3E10 0
      }
      brightnessCurve
      {
        key = 0 1
        key = 5E9 0.4
        key = 1.25E10 0.09
        key = 2E10 0.0075
        key = 3E10 0
      }
    }
  }
}
```

|Property|Format|Description|
|type|CometTailType|The type of comet tail. The possible values are `Ion` and `Dust`.|
|color|Color|The color of the tail. Default is white. NOTE: Alpha value does not matter.|
|rimPower|Decimal|How close the tail rim stays to the edge of the body. The higher the number, the closer it is. Default is 1.41.|
|distortion|Decimal|The distortion of the tail. Default is 0.143.|
|alphaDistortion|Decimal|The transparency distortion of the tail. Default is 0.262.|
|zDistortion|Decimal|? Default is 0.12|
|frequency|Decimal|The frequency of the comet tail's noise. Default is 1.547|
|lacunarity|Decimal|The [lacunarity](https://en.wikipedia.org/wiki/Lacunarity) of the comet tail's noise. Default is 1.518.|
|gain|Decimal|? Default is 0.734.|
|radius|Decimal|The radius of the teardrop that makes up the comet tail. Default is 2000.|
|length|Decimal|The length of the comet tail. Default is 16000.|
|opacityCurve|FloatCurve|A curve that associates a distance value with an opacity value from 0 to 1?|
|brightnessCurve|FloatCurve|A curve that associates a distance value with a brightness value.|
