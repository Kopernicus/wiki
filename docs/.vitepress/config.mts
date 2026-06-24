import { defineConfig } from 'vitepress'
import kspCfgGrammar from './ksp-cfg.tmLanguage.json'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "Kopernicus Wiki",
  description: "Kopernicus Wiki",

  head: [
    ['link', { rel: 'icon', href: '/icon.png' }],
  ],

  markdown: {
    // Register our custom shiki grammar for KSP config files.
    languages: [kspCfgGrammar as any],
  },

  // https://vitepress.dev/reference/default-theme-config
  themeConfig: {
    // This controls what shows up in the navbar
    nav: [
      { text: 'Home', link: '/' },
    ],

    // This controls what shows up on the sidebar
    sidebar: [
      {
        text: 'Prerequisites',
        items: [
          { text: 'What are ConfigNodes?', link: '/Prerequisites/ConfigNodes' },
          { text: 'A Beginner\'s Guide to Kopernicus', link: 'https://forum.kerbalspaceprogram.com/index.php?/topic/129540-a-beginners-guide-to-kopernicus-the-basics/' },
          { text: 'Data Types', link: '/Prerequisites/DataTypes' }
        ]
      },
      {
        text: 'Syntax',
        items: [
          { text: 'Body', link: '/Syntax/Body' },
          { text: 'Template', link: '/Syntax/Template' },
          { text: 'Properties', link: '/Syntax/Properties/' },
          { text: 'Orbit', link: '/Syntax/Orbit' },
          { text: 'ScaledVersion', link: '/Syntax/ScaledVersion/' },
          { text: 'Ocean', link: '/Syntax/Ocean' },
          { text: 'PQS', link: '/Syntax/PQS' },
          { text: 'PQSMods', link: '/Syntax/PQSMods/' },
          { text: 'Rings', link: '/Syntax/Rings' },
        ]
      },
      {
        text: 'API Documentation',
        items: [
          { text: 'PQSMod', link: '/API/PQSMod' }
        ]
      },
      {
        text: "Release Notes",
        link: "/RelNotes"
      }
    ],

    search: {
      provider: 'local',
    },

    logo: '/icon.png',
    socialLinks: [
      { icon: 'github', link: 'https://github.com/Kopernicus/wiki' }
    ]
  }
})
