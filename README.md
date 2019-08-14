This is the official wiki for the [Kopernicus plugin](https://github.com/Kopernicus/Kopernicus) for Kerbal Space Program. 
The current targeted version of Kopernicus is 1.7.3-1.

# Contributing
It is preferred that all contributions be done via a text editor and a Git client, as opposed to authoring the changes on GitHub.
This allows for consistent page formatting. If you wish to be able to test out changes before uploading, you must install Jekyll and its dependencies. Look at the "How to finally contribute" section below if you wish to contribute to a file or create a new file.

## External Programs
### Installing Jekyll
Head to [https://jekyllrb.com/docs/installation/](https://jekyllrb.com/docs/installation/) and scroll to the bottom. 
Click the link that corresponds to your operating system and follow the directions on that page.

### Installing Git
You may use whatever Git GUI/client you wish, however they all (to my knowledge) require Git. The latest version of Git can be downloaded from [https://git-scm.com/downloads](https://git-scm.com/downloads).

## How to finally contribute
### Authoring and Submitting
Once you have installed Git, fork the wiki repository, then clone your fork to your computer. Create a new branch when working on anything new. Once you have COMPLETELY FINISHED your changes, 
commit them to your repository and create a pull request to kopernicuswiki/wiki and target the `master` branch. Once your pull request has been approved by a maintainer,
feel free to merge it if it hasn't been already. Within a few moments, your content will have been added to the wiki!

### Page Format
As discussed in [issue #4](https://github.com/kopernicuswiki/kopernicuswiki.github.io/issues/4), the format for every page in the Wiki should be as follows:

Description of the item

**Example**
```
Your example of the item should go here
If comments are needed, they should be inline, // Like this!
Unless the comment is for a node. Then the comments should go

// Like this,
ForANode
{
  greatExample = true // This is obviously a great example!
}
```

|Property|Format|Description|
|--------|------|-----------|
|The name of the property|[The type of value](main/datatypes.md)|The description of the property.|
|(Example) order|Integer|The order the PQSMod should be applied in.| 

### Links
Intra-repository links are to be in the form `[text]({{ site.baseurl }}{% link <path in repo> %})`
