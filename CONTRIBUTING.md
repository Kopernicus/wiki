# How To Contribute

There are a number of ways to contribute to the Wiki to make it accessible to many skill levels.

## Content only

All wiki content is written in markdown (.md) files, which individual page files load and embed to create the page you see. Markdown syntax is used for Discord post formatting, but a reference can be found at [https://www.markdownguide.org/](https://www.markdownguide.org/). The easiest way to contribute is to edit or create a markdown file and either [create a Pull Request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests) to the wiki or to give it to DeltaDizzy or R-T-B to add to the wiki. A pull request is preferred because it is simpler to review and easier to integrate.

Markdown files should be placed in wwwroot/content, inside a folder that corresponds with what the page is describing (for example, if you are adding a PQSMod it should probably go in wwwroot/content/Syntax/PQSMods).

## Content and Backend

If you also want to create the page file (assuming you are making a new page), copy [the example razor file](/ExamplePage.razor) and modify it. You will need to make the following changes:

1. Change the url after `@page` to the one you want the page to be at. `https://kopernicuswiki.org` will be put before the url you list.
2. In the `@code` block, change the value of `pageTitle` to the title you want displayed to page visitors.
3. In the `MarkdownPageRenderer` tag, change the value of `srcFile` to the file path of your markdown content file.

The path is relative to the wwwroot folder so "content/Syntax/Body.md" searches for wwwroot/content/Syntax/Body.md. A link to your new page should be added somewhere depending on how you want users to find it. Do not link to the file path, but use the url you defned at the top of the razor file. See wwwroot/content/index.md for examples.

RouteList.cs in the repository root will also need to be modified, but if you don't want to do it we can. Find the correct section (or create one if it is a new section), and insert a line like `"pageName" => "pageUrl",`, where `pageName` is the title of your page as listed in the razor code block and `pageUrl` is the defined url of the page.

If you need help or have any questions, feel free to ping @DeltaDizzy on the Kopernicus discord, in DMs, or on GitHub.