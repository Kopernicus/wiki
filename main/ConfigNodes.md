---
layout: default
title: Config Nodes
subtitle: The stuff used everywhere
---

# What is a ConfigNode?
ConfigNodes are a data storage / settings format that is used by KSP. They are most commonly used in `.cfg` files, but you can find them inside of `.craft` and `.sfs` files too.
Whenever you want to modify anything in KSP, you will have to deal with ConfigNodes.

A Config Node consists of three parts:

* header
* values
* subnodes

Only the header is required, the other parts are optional, which means that you can create a ConfigNode which is empty.

## Header

You declare a ConfigNode by writing down a name, followed by a pair of curly braces. So, if we would want to create a node called `Kopernicus`, you would write it like this:

```text
Kopernicus
{

}
```

It is **required** to pay attention to the indentation of the braces, since every opened brace will need to be closed again. Also, if your brace layout is incorrect, it might happen that a node becomes a subnode of another node, and the mod that is parsing them doesn't find the node.

## Values

If you want to add values to your ConfigNode, you need a key and a value. The key is most likely predefined by the mod you are trying to configure, the value is something you need to choose.

```text
Kopernicus
{
    name = My cool system
}
```

Make sure you use a fixed indentation level (4 spaces in this example), so your config stays clean and readable. Also make sure that your editor doesn't mix tabs and spaces, since tabs can have a dynamic width. A config that looks good to you might look totally horrible to a person that wants to read it.
The best way would be to use only tabs (so no spaces at all!), or to have your editor convert all tabs into spaces automatically.

## Subnodes

Since adding all values into the same ConfigNode could get messy at some point, mods can structure them into subnodes. This means, that every node can contain an unlimited amount of other nodes. The "main" node, i.e. the node in the `.cfg` file that isn't enclosed by another one, is then called the **root** or the **root-node**

```text
Kopernicus
{
    name = My cool system
    Body
    {
        name = Kerbin
        Properties
        {
            description = That blue ball around the Sun
        }
    }
}
```

In this example, the node `Kopernicus` is the root node, and `Body` and `Properties` are subnodes of their respective parent.

## Comments

To structure your config file even more you can and should make use of comments: KSP will ignore everything that is behind two forward slashes while parsing the file. You can use this to explain certain parts of the file and what they do, or leave reminders to yourself.

```text
Kopernicus
{
    // Naming the system
    name = My cool system
    
    // Creating my first body
    Body
    {
        // I have heard this name before...
        name = Kerbin
        
        // Access the properties of the body
        Properties
        {
            // Describing Kerbin
            description = That blue ball around the Sun
        }
    }
}
```

## Tips and Tricks

ConfigNodes don't care about whether a key already exists, they will happily accept two occurences of a key or a node name in the same parent node. However, how these values get handled depends on the mod you are modifying. Some mods will use the last one, others the first one.
Unless it is explicitly supported to have more than one value / node with the same name, you should always avoid them.

To make sure that you are correctly opening and closing your ConfigNodes, you can use this simple trick: Simply add the name of the node behind the closing brace as a comment:

```text
Kopernicus
{
    // Naming the system
    name = My cool system
		
    // Creating my first body
    Body
    {
        // I have heard this name before...
        name = Kerbin
        
        // Access the properties of the body
        Properties
        {
            // Describing Kerbin
            description = That blue ball around the Sun
        
        } // Properties
    
    } // Body

} // Kopernicus
```


That way you can always tell where you actually added the node.

# ModuleManager
One of the biggest problems of config nodes is, that they don't allow you to easily modify an already existing config. The only way to do this in stock KSP would be to overwrite the file, which would make it very hard to:
* Revert the change
* Mix multiple mods that attempt to change the file

A mod that fixes this problem for us is ModuleManager: It allows you to write your configs as patches that are later parsed and applied to the configurations in memory, i.e. while the game is loaded. The files on your harddrive don't get modified. Since ModuleManager has lots of advanced features that you probably will never need, this tutorial will only cover the most basic ones:
* Editing
* Copying
* Deleting
* Conditions

## Editing
Editing is probably the most important functionality that ModuleManager provides. It works by prefixing a node or a value with `@`. This indicates, that MM should search for a node (or value) with the specified name, and then apply the contained changes to it. If you are editing a node, their contents are merged (i.e. values get added), but when you edit a value, it's content is overwritten.
```
@Kopernicus
{
    name = My name!
}
```
This config would edit the first node called `Kopernicus` that it could find, and then add the value `name = My name!` to it. That means, if you would apply it to this node:
```
Kopernicus
{
    name = Banana
}
```
the result would be:
```
Kopernicus
{
    name = Banana
    name = My name!
}
```
You see that it has added a second instance of the `name` value.

Now, if you wanted to edit the name value that already exists, you would prefix the name with `@`. Pay attention that you keep the `@` prefix on the parent node! Without that, MM would not realize that the whole config is supposed to be a patch, and just search for something to edit in the node itself.
```
@Kopernicus
{
    @name = My name!
}
```

However, the `@` prefix only works if the name that is specified exists as a value or node. If the `name` value wouldn't exist in the original node that is being edited, MM would just ignore it. If you need to place a value somewhere, but you don't know whether it already exists or not, you can use the `%` prefix, which means "edit-or-create".

## Copying
When you have understood editing, copying is fairly easy. Using the `+` prefix, you can create a copy of a node (and only a node!) in the same place where MM found the original one. The body of the node is then parsed by MM as if you would edit the node, so you can change properties like names or ID's.
```
Kopernicus
{
    Body
    {
        name = Banana
        works = False
    }
}
@Kopernicus
{
    +Body
    {
        @name = Nabana
    }
}
```
This config would produce this result:
```
Kopernicus
{
    Body
    {
        name = Banana
        works = False
    }
    Body
    {
        name = Nabana
        works = False
    }
}
```

## Deleting
Sometimes you have to get rid of a node or a value entirely. You can do this, by using the `!` prefix. You still need to define a correct node / value though! This means that this: `!Banana` would be invalid. You have to use `!Banana { }` to delete a node and `!banana = DEL` to delete a value. Note that the value of `!banana` doesn't matter, but it has to be there.

```
Kopernicus
{
    Body
    {
        name = Banana
    }
}
@Kopernicus
{
    !Body { }
}
```
would result in
```
Kopernicus
{
}
```

## Conditions
All configs that were previously shown would apply themselves to the first node they can find that matches their name. This is undesired in most cases, since you want to be able to finetune the config of a specific body or part etc. ModuleManager can, instead of selecting the first matching one, select configs based on their values. To do this, you have to add `:HAS[#name[value]]` to the name of the node. `name` is the key of the value you want to select, and `value` it's value. If the value doesn't matter to you, you can use a wildcard, that will match everything: `:HAS[#name[*]]`. You can also negate the selection, `:HAS[~name[value]]` will select a config that doesn't have a property with the specified name.

**TODO: :NEEDS, :FOR, :AFTER**

