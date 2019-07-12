The `ScaledVersion { }` node in a configuration file for Kopernicus is a less-detailed model of your planet that appears in the map view and from large distances.


### Subnodes
***
* [Material { }](https://github.com/Kopernicus/Kopernicus/wiki/Material) = Updates to textures and atmosphere rims.
* [Light { }](https://github.com/BryceSchroeder/Kopernicus/wiki/Light) = Used for making stars.
* [Coronas { }](https://github.com/Kopernicus/Kopernicus/wiki/Coronas) = Used for making stars.

## Explanation:
|Property|Format|Description|
|--------|------|-----------|
|**type**|string|Either _Vacuum_ or _Atmospheric_, depending on whether the body has an atmosphere or not
|**fadeStart**|float|Altitude, in meters, at which the transition to ScaledSpace starts
|**fadeEnd**|float|Altitude, in meters, at which the transition to ScaledSpace ends
|**sphericalModel**|boolean|_True_ or _False_?Does?
|**deferMesh**|boolean|_True_ or _False_?Does?
