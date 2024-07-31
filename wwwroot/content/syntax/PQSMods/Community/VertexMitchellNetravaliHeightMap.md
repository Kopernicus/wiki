**Author**: [Niako](https://github.com/pkmniako)
**Source**: [GitHub Repository](https://github.com/pkmniako/Kopernicus_VertexMitchellNetravaliHeightMap)
**Download**: [Latest release on GitHub](https://github.com/pkmniako/Kopernicus_VertexMitchellNetravaliHeightMap/releases)
**Wiki**: [GitHub Wiki](https://github.com/pkmniako/Kopernicus_VertexMitchellNetravaliHeightMap/wiki)
**Bundling allowed**: Explicitly allowed
**Internal mod name**: `PQSMod_VertexMitchellNetravaliHeightMap`

This is a community-contributed PQSMod that aims to solve a problem that exists with VertexHeightMap. Let's take Kerbin for example, a planet with a 600 km radius. This gives an equatorial circumference of approximately 3770 km. Even with 8K textures, every pixel will cover a width of approximately 0.46 km. Evidently Kerbals and their peculiar vehicles are much smaller than that, so for many vertices on the surface, they end up between adjacent pixels in the heightmap.

To solve this, KSP uses bilinear interpolation: it figures out where the vertex is between the four surrounding heightmap pixels and linearly interpolates along the x and y axes of the image. This works, but it results in an unsightly world of slanted squares. This is typically hidden by adding additional noise PQSMods.

On its face, the only way to get rid of this problem would be to increase the heightmap resolution until the squares become unnoticable. But if we settle for, for example, 0.25m sized squares (which would still be noticable), then for Kerbin alone this would require a heightmap with a width of 15 MILLION pixels, and a height of 7.54 million pixels. This results in an image containing 113.7 trillion (American English) or billion (British English) pixels. Using an L8-formatted DDS image, the expected filesize will therefore be 113.7 trillion bytes or 113 TB.

But then it's only fitting; one would be capturing the surface detail of an entire celestial body. Clearly this won't do.

Another solution, which is what Niako implemented in this PQSMod, uses better interpolating between the pixels. For example, what if we look at not just 2 pixels per axis (4 pixels total), but 4 pixels per axis (16 pixels total)? We may attempt to fit smoothened curves to the height data stored in the texture. The better this fit approximates the actual terrain contour, the closer the resulting interpolation will resemble the true intended terrain shape.

An example fit is a cubic bezier spline, in which case one would be using the bicubic interpolation. Mitchell-Netravali interpolation is the result of a scientific investigation into image filtering, specifically artifacts produced by reconstruction filters. Put simply, it is an abstraction of spline based interpolation, with the spline behavior controlled by two parameters, B and C. For more information about B and C, as well as a helpful though suggestive plot of the behavior of various combinations of B and C, see the [Wikipedia article](https://en.wikipedia.org/wiki/Mitchell%E2%80%93Netravali_filters) on Mitchell-Netravali filters.

Taken from this Wiki article, we shall now list a number of values for B and C that are used in practice. Notably, if B = 0, the equations reduce to that for a class of splines called Cardinal splines.

|B|C|Spline name|Common implementations|
|0|Any|Cardinal splines||
|0|0.5|Catmull-Rom Spline|Bicubic filter in GIMP|
|0|0.75|Unnamed Cardinal spline|Bicubic filter in Photoshop|
|1/3|1/3|Mitchell-Netravali|Mitchell filter in ImageMagick.|
|1|0|B-Spline|Bicubic filter in Paint.NET|

Splines are a complicated subject. For a detailed explanation on what Cardinal, Catmull-Rom, Bezier and B-Splines are, we refer to an excellent [video by educator Freya Holmér](https://youtu.be/jvPPXbo87ds). Notable time stamps are:
- 44:10 for an explanation on Cardinal splines and the Catmull-Rom spline (which is a specific Cardinal spline). Freya first discusses the linear spline as a stepping stone toward Cardinal splines so the concept makes more sense.
- 53:15 for B-Splines.

For completeness, this document also contains a brief explanation of these splines and the behavior you can expect from them, at the end.

The upshot is that parameters B and C essentially control the smoothness of the interpolation (possibly at the expense of actually passing through the height values recorded in the heightmap). Careless configuration may introduce unwanted artifacts, such as rippling effects around high contrast edges (IE what ought to be cliffs in the terrain) or blurring.

## Example {#Example}
You are allowed to bundle Mitchell-Netravali interpolation with your planet mods. In this case, define it as a substitute for VertexHeightMap:
```
PQS
{
    Mods
    {
        VertexMitchellNetravaliHeightMap
        {
            map = ...
            offset = ...
            deformity = ...
            scaleDeformityByRadius = False
            order = ...
            enabled = True
            B = 1
            C = 0
        }
    }
}
```
But if you want to leave it up to the user, there is a trick you can do. When KSP imports a code library from a mod, ModuleManager picks up on it. As such, you can define config nodes if and only if a specific mod is installed, or if a specific mod is not installed. Using this, you can define both VertexHeightMap and VertexMitchellNetravaliHeightMap and have ModuleManager select one depending on whether Mitchell-Netravali interpolation is installed.

```
PQS
{
    Mods
    {
        // Source: Dres (Planet Cyran by The White Guardian)
        VertexHeightMap:NEEDS[!VERTEXMITCHELLNETRAVALIHEIGHTMAP]
        {
            map = PlanetCyran/Kopernicus/PluginData/Dres_NewHeight.dds
            offset = 0
            deformity = 8000
            scaleDeformityByRadius = False
            order = 20
            enabled = True
        }
        VertexMitchellNetravaliHeightMap:NEEDS[VERTEXMITCHELLNETRAVALIHEIGHTMAP]
        {
            map = PlanetCyran/Kopernicus/PluginData/Dres_NewHeight.dds
            offset = 0
            deformity = 8000
            scaleDeformityByRadius = False
            order = 20
            enabled = True
            B = 1
            C = 0
        }
    }
}
```

## Properties {#Properties}
Most of the properties are identical to those of the built-in [VertexHeightMap](/Syntax/PQSMods/VertexHeightMap) PQSMod. The only difference is the parameters `B` and `C`. These are both decimal numbers that are expected to be in the [0, 1] range.

## Appendix: Splines {#Appendix_Splines}

To understand the Cardinal spline, it is necessary to first talk about two other kinds of splines: the Bezier spline (which you have probably used already every time you define a FloatCurve for a PQSMod or Atmosphere) and the Hermite spline.

### Bezier Splines {#Bezier_Splines}
Interpolating linearly between points to connect them into a segmented line is a simple task, but it exhibits the behavior of heightmaps that we are looking to avoid: flat lines that connect harshly to create a jagged line. If we want to model, for example, the pressure-versus-altitude profile of a planetary atmosphere then this won't do. We need smoothness!

What if, given two line segments and an interpolation parameter t, we interpolate linearly along both line segments, and then between the two interpolated points? So three interpolations in total? The result is a smoother curve called a quadratic Bézier curve.

However, its bigger brother, the cubic Bézier curve, is more popular, where we have four points and three line segments, along which we interpolate to get three points, then another interpolation to get two points, and then one last interpolation to get the final position along the curve. For more points, we can use a higher degree Bezier curve, but there are two problems:
1. The resulting curve will only pass through the first and last points and just lazily follow the points in-between;
2. Moving a single point will affect a large portion of the curve. Being able to move a point and only affect the interpolation between that point and its nearest neighbours is called 'local control', and right now we don't have it, but we want to. After all, without it, the slightest adjustment would require adjusting every other control point, too.

The solution is to connect Bézier curves together. This construction is called a Bézier spline. For the cubic Bézier spline, every control point has an in-tangent and an out-tangent. These are two 'ghost' points that assist in describing the trajectory of the curve between two adjacent control points. After all, for a cubic Bézier curve, we need four points to interpolate between. These four points, then, are control point A, the out-tangent of control point A, the in-tangent of control point B, and control point B itself.

### Hermite Splines {#Hermite_Splines}
Using a cubic Bézier spline, we can now place the control points where we want the Bézier curve to pass through, and use the tangents to control the shape of the curve between the control points. But this becomes tedious if we are using a curve to describe motion. We will have control of position versus time, but velocity will be implicit. What if we instead define the curve in terms of control points with an outgoing velocity vector? This is the idea behind Hermite splines. Instead of pairs of tangent vectors, we define the position and velocity, as a more kinematic description of the curve's motion.

### Cardinal Splines {#Cardinal_Splines}
But can we get even simpler? What if we go back to connecting two adjacent points with a straight line, and make a leap toward Hermite splines, by calculating velocity as the vector from point N to point N+2? By calculating 'velocity' implicitly, we should have a smooth curve passing through all control points that is solely defined by the positions of the control points.

But in practice the resulting curves are a little strange. They mostly look like applying a teeny bit of smoothing to the straight line segments, with small and tight corners. We can relax the curve by scaling down the 'velocities' we calculated with a scaling factor, `s`. Setting `s` too small results in strong curvature at the joints with flattening along the segments. If `s=0`, we're left with linear interpolation.

A special case is when `s = 0.5`. This Cardinal spline is known as the Catmull-Rom spline.

### Continuity {#Continuity}
To explain what a B-Spline is, we first need to discuss a property of splines called continuity. All of the splines we discussed above form a continuous, uninterrupted line. In other words, the change in position is continuous and there are no jumps or 'teleporting' going on.

This is called 'C0 continuity'. What, then, is C1 continuity? A spline achieves C1 continuity if there are no jumps in velocity either. A Hermite spline for example is always C0 and C1 continuous because it explicitly defines velocity at each point and interpolates the velocities of two adjacent control points.

A Bézier spline is typically C0 continuous, but it need not be C1 continuous. A Bézier spline is C1 continuous if and only if the in-tangent and out-tangent of each control point are mirrored. This means that a C1 continuous Bézier spline essentially only has one tangent, the in-tangent, with the out-tangent being defined implicitly by the in-tangent.

Or, in other words, enforcing C1 continuity on a Bézier spline turns it into something resembling a Hermite spline. This is no coincidence, a C1 continuous Bézier spline can be seamlessly converted into a Hermite spline and vice versa.

But what of C2 continuity, meaning that there are no jumps in acceleration? This would result in an even smoother curve, but this is more difficult to achieve. Attempting to make a Bézier spline for example C2 compliant, creates some problems. If we enforce the constriant that the acceleration at the end of one spline section must equal the acceleration at the start of the next spline section, then the constraint that we end up with is that the in-tangent of the second control point of a spline segment is controlled by the in-tangent and the position of the section's first control point, as well as the out-tangent of the control point before that.

An alert reader will notice something terrible occurring. We already gave up the ability to control both tangents to make the spline C1 continuous, but now all we're left with almost no control over the tangents. We can only control the in-tangent of the last control point, and the tangents for the first spline segment. All other tangents are defined implicitly in order to retain C2 continuity. The result is that we have given up local control.

So, what if we instead attempt to create an entirely new spline, focused on being C0, C1 and C2 continuous?

### B-Splines {#B_Splines}
We will skip the mathematics that leads to the construction of the B-Spline. The resulting spline is buttery smooth, but it does something that we haven't seen before: it isn't passing through the control points. We have given up that power in exchange for C2 continuity.

A B-Spline is always C0, C1 and C2 continuous. Using a B-Spline to interpolate your heightmap gives a very, very smooth surface, but the altitude values may not exactly match what is described in the heightmap.