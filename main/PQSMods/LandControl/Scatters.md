```
Scatters
{
    Value
    {
        materialType = DiffuseWrapped // Type of material to use - ScatterMaterialType { Diffuse, BumpedDiffuse, DiffuseDetail, DiffuseWrapped, CutoutDiffuse, AerialCutout, Standard}
        mesh = BUILTIN/boulder		//Filepath to mesh. Must be .obj!!! - maybe list BUILTINs
        castShadows = True	//Obvious - BOOLEAN
        densityFactor = 1 - DOUBLE
        material = BUILTIN/scatter_rock_laythe	//Avoid using this.  Delete it.  Can take the place of the Material subside below.  The two are not compatible together.
        maxCache = 512		//How many scatters this has loaded? Not sure. - INT32
        maxCacheDelta = 32	//No idea what delta means - INT32
        maxLevelOffset = 0 // INT32
        maxScale = 1.5	//Max size - SINGLE
        minScale = 0.25	//Minimun size - SINGLE
        maxScatter = 30	//Max # of scatters loaded?? - INT32
        maxSpeed = 1000		//Mostly unknown. It affects the frequency of scatter spawns. Posssibly a misspelling of maxSpread.
        recieveShadows = True		//If it receives shadows or not duh - BOOLEAN
        name = BrownRock	//Name - STRING
        seed = 345234534	//Seed for scatter distribution
        verticalOffset = 0		//How far it is offset, so it can be floating or offset underground
        delete = False
        Material
        {
            // COntents depend on materialType chosen above, see ScatterMaterialType page
        }
    }
}
```