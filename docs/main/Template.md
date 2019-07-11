<!--Subtitle: Filling in the gaps you leave-->
If you add a  `Template { }` node to the configuration, Kopernicus will import the planetary data and adapt it using the rest of your configuration file. There are various parameters to describe, if specific data should be ignored, such as oceans or atmosphere. This node is completely optional.

<table width= 500 border=1>
<tr><td><b>Variable</b></td><td><b>Type</b></td><td><b>Description</b></td></tr>
<tr><td>name</td><td>string</td><td>The name of your template-body. <i>Only the names of stock-bodies are valid.</i></td></tr>
<tr><td>removePQS</td><td>TRUE/FALSE</td><td>If this is set to true, Kopernicus will remove the surface of the template body listed in 'name'. See PQS subnode for more details.</td></tr>
<tr><td>removeAtmosphere</td><td>TRUE/FALSE</td><td>Removes the atmosphere from the template body listed in 'name'. See Atmosphere subnode for more details.</td></tr>
<tr><td>removeOcean</td><td>TRUE/FALSE</td><td>Removes the ocean of the template body listed in 'name'. See Ocean subnode for more details.</td></tr>
<tr><td>removePQSMods</td><td>string list</td><td>Selection of terrain-modifications that Kopernicus should remove. (Comma-seperated list. Details to come)</td></tr>
<tr><td>removeAllPQSMods</td><td>TRUE/FALSE</td><td>Removes every terrain-modification from the template body listed in 'name' and make it a perfect sphere.</td></tr>
</table>
