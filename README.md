This is the official wiki for the [Kopernicus](https://github.com/Kopernicus/Kopernicus) addon for Kerbal Space Program. 
The current targeted version of Kopernicus is 1.7.3-1.

# Contributing
It is preferred that all contributions be done via a text editor and a Git client, as opposed to authoring the changes on GitHub.
This allows for consistent page formatting. If you wish to be able to test out changes before uploading, you must install Jekyll and its dependencies.

## External Programs

### Installing Git
You may use whatever Git GUI/client you wish, although if you do not already have one we recommend GitKraken or Git itself. The latest version of Git can be downloaded from [https://git-scm.com/downloads](https://git-scm.com/downloads) and GitKraken can be found at [https://www.gitkraken.com/download](https://www.gitkraken.com/download)

### Installing Jekyll (optional)
Head to [https://jekyllrb.com/docs/installation/](https://jekyllrb.com/docs/installation/) and scroll to the bottom. 
Click the link that corresponds to your operating system and follow the directions on that page.

## Authoring Changes
Once you have installed all external programs, fork the wiki repository before cloning your fork to your computer. Create a new branch when making any changes whatsoever. Be sure that your changes follow the stylistic guidelines discussed below. 

### Page Format
The format for every page should be as follows:

\---
layout: default
title: pageTitle
\---

Description of the feature/node being discussed, plus an explanation of what it does.

**Example**
```
// Your example of the item should go here.
// Comments should be inline like these unless the comment is for a node. 
// If so, then the comments should go on the line above the node.

// ForANode information
ForANode
{
  greatExample = true // This is obviously a great example!
}

// There must always be one space between the comment delimiter (the //) and the beginning of the comment text.
// If on the same line as a key/value, there must also be one space between the end of the key/value and the delimiter.
```

|Property|Format|Description|
|--------|------|-----------|
|The name of the property|[type of the property](main/datatypes.md)|The description of the property.|
|(Example) order|Integer|The order the PQSMod should be applied in.| 

**Links**

Intra-repository links are to be in the form `[text]({{ site.baseurl }}{% link <absolute path in repo> %})`
