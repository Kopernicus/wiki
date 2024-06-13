namespace kopernicus_wiki;

public class RouteList {
    public static string Get(string name) {
        return name switch {
            "Home" => "/",
            "Config Nodes" => "/Prerequisites/ConfigNodes",
            "Data Types" => "/Prerequisites/DataTypes",
            "Getting Started" => "/Guides/GettingStarted",
            // Body node
            "Body" => "/Syntax/Body",
            // Body subnodes
            "Atmosphere" =>              "/Syntax/Atmosphere",
            "AtmosphereFromGround" =>    "/Syntax/AtmosphereFromGround",
            "Debug" =>                   "/Syntax/Debug",
            "HazardousBody" =>           "/Syntax/HazardousBody",
            "Ocean" =>                   "/Syntax/Ocean",
            "PQS" =>                     "/Syntax/PQS",
            "Properties" =>              "/Syntax/Properties",
            "Rings" =>                   "/Syntax/Rings",
            "ScaledVersion" =>           "/Syntax/ScaledVersion",
            "SpaceCenter" =>             "/Syntax/SpaceCenter",
            "Template" =>                "/Syntax/Template",
            // PQSMods
            "PQSMods" =>                             "/Syntax/PQSMods",
            "LandControl" =>                         "/Syntax/PQSMods/LandControl/LandControl",
            "HeightColorMap" =>                      "/Syntax/PQSMods/HeightColorMap",
            "HeightColorMap2" =>                     "/Syntax/PQSMods/HeightColorMap2",
            "VertexColorMap" =>                      "/Syntax/PQSMods/HeightColorMap",
            "VertexColorMapBlend" =>                 "/Syntax/PQSMods/VertexColorMapBlend",
            "VertexHeightMap" =>                     "/Syntax/PQSMods/VertexHeightMap",
            "VertexHeightNoise" =>                   "/Syntax/PQSMods/VertexHeightNoise",
            "VertexHeightNoiseVertHeightCurve2" =>   "/Syntax/PQSMods/VertexHeightNoiseVertHeightCurve2",
            "VertexSimplexHeight" =>                 "/Syntax/PQSMods/VertexSimplexHeight",
            "VertexSimplexHeightAbsolute" =>         "/Syntax/PQSMods/VertexSimplexHeightAbsolute",
            "VertexSimplexNoiseColor" =>             "/Syntax/PQSMods/VertexSimplexNoiseColor",
            // LandControl subnodes
            "LandClasses" =>                         "/Syntax/PQSMods/LandControl/LandClasses",
            "ScatterMaterialType" =>                 "/Syntax/PQSMods/LandControl/ScatterMaterials",
            "Scatters" =>                            "/Syntax/PQSMods/LandControl/Scatters",
            "ModularScatter" =>                      "/Syntax/PQSMods/LandControl/ModularScatter",
            "HeatEmitter" =>                         "/Syntax/PQSMods/LandControl/ModularScatter/HeatEmitter",
            "LightEmitter" =>                        "/Syntax/PQSMods/LandControl/ModularScatter/LightEmitter",
            "ScatterColliders" =>                    "/Syntax/PQSMods/LandControl/ModularScatter/ScatterColliders",
            "SeaLevelScatters" =>                    "/Syntax/PQSMods/LandControl/ModularScatter/SeaLevelScatters",
            // Properties subnodes
            "Biomes" =>                              "/Syntax/Properties/Biomes",
            "ScienceValues" =>                       "/Syntax/Properties/ScienceValues",
            // ScaledVersion subnodes
            "Corona" =>                              "/Syntax/ScaledVersion/Corona",
            "Light" =>                               "/Syntax/ScaledVersion/Light",
            "Material" =>                            "/Syntax/ScaledVersion/Material",
            "OnDemand" =>                            "/Syntax/ScaledVersion/OnDemand",
            // PQS Materials
            "AtmosphericTriplanarZoomRotation" =>                "/Syntax/Material/AtmosphericTriplanarZoomRotation",
            "AtmosphericTriplanarZoomRotationTextureArray" =>    "/Syntax/Material/AtmosphericTriplanarZoomRotationTextureArray",
            // Kopernicus Expansion
            "Comet Tails"           => "/Syntax/Expansion/CometTails",
            "Emissive FX"           => "/Syntax/Expansion/EmissiveFX",
            "EVA Footprints"        => "/Syntax/Expansion/EVAFootprints",
            "Procedural Gas Giants" => "/Syntax/Expansion/ProceduralGasGiants",
            "Reentry Effects"       => "/Syntax/Expansion/ReentryEffects",
            "VertexHeightDeformity" => "/Syntax/Expansion/VertexHeightDeformity",
            "VertexHeightMap16"     => "/Syntax/Expansion/VertexHeightMap16",
            "Wormholes"             => "/Syntax/Expansion/Wormholes",
            _ => ""
        };
    } 
}