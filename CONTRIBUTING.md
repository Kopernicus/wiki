# How To Contribute

There are a number of ways to contribute to the Wiki to make it accessible to many skill levels.

## Content only

All wiki content is written in markdown (.md) files. Markdown syntax is used for Discord post formatting, but a reference can be found at [https://www.markdownguide.org/](https://www.markdownguide.org/). The easiest way to contribute is to edit or create a markdown file and either [create a Pull Request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests) to the wiki or to give it to DeltaDizzy or R-T-B to add to the wiki. A pull request is preferred because it is simpler to review and easier to integrate.

Markdown files should be placed in `docs`, inside a folder that corresponds with what the page is describing (for example, if you are adding a PQSMod it should probably go in `docs/Syntax/PQSMods`).

VitePress supports additional markdown features beyond standard markdown, such as custom containers, code group tabs, and more. See the [VitePress Markdown Extensions](https://vitepress.dev/guide/markdown) documentation for details.

## Content and Site Structure

Since VitePress generates pages directly from markdown files, there is no need to create a separate page file. Simply creating the markdown file in the correct location is enough for VitePress to generate a page for it. The URL of the page is determined by its file path relative to the `docs` directory (for example, `docs/Syntax/Body.md` becomes `/Syntax/Body`).

If you want your new page to appear in the sidebar, you will need to add an entry in the sidebar configuration in `docs/.vitepress/config.mts`. Find the correct section (or create one if it is a new section) and add an item with the page's title and link.

## Running the Site Locally

To preview your changes locally:

1. Make sure you have [Node.js](https://nodejs.org/) installed.
2. Run `npm install` to install dependencies.
3. Run `npm run dev` to start the development server.

The site will be available at `http://localhost:5173` (or another port if that one is in use).

## Questions

If you need help or have any questions, feel free to ping @DeltaDizzy on the Kopernicus discord, in DMs, or on GitHub.
