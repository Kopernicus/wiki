If you add a  `Template` node to the configuration, Kopernicus will import the planetary data and adapt it using the rest of your configuration file. There are various parameters to describe if specific data should be ignored, such as oceans or atmosphere. This node is completely optional, but if you miss an important value, then Kopernicus may throw an exception if you do not specify a template body.

## Example {#Example}
```
Template
{
  name = Eve
  removeAtmosphere = true
  removeAllPQSMods = true
  removeOcean = true
}
```

|Property|Format|Description|
|--------|------|-----------|
|name|Text|The name of your template body. *Only the names of stock-bodies are valid.*|
|removePQS|Boolean|Whether Kopernicus should remove the surface of the template body. See the [PQS subnode]( /Syntax/PQSMods/PQS) for more details.|
|removeAtmosphere|Boolean|Whether to remove the atmosphere from the template body. See the [Atmosphere subnode]( /Syntax/Atmosphere/Atmosphere) for more details.|
|removeBiomes|Boolean|Whether to remove the biomes of the template body. See the [Biome subnode]( /Syntax/Properties/Biome) for more details.|
|removeOcean|Boolean|Whether to remove the ocean of the template body. See the [Ocean subnode]( /Syntax/Ocean) for more details.|
|removePQSMods|Text List|A selection of terrain modifications that Kopernicus should remove. Possible values are listed on the [PQSMods page]( /Syntax/PQSMods).|
|removeAllPQSMods|Boolean|Whether to remove every terrain modification from the template body and make it a perfect sphere.|
|removeProgressTree|Boolean|Whether to remove contracts and milestones from the template body.|
|removeCoronas|Boolean|Whether to remove the coronas from the template body (really only applicable for the Sun).|
