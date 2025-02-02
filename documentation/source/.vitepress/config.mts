import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "TASauria Documentation",
  description: "User guide and API reference for TASauria, a plugin and library for remotely controlling the BizHawk emulator.",
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    search: {
      provider: 'local'
    },

    nav: [
      { text: 'Home', link: '/' },
      { text: 'Examples', link: '/markdown-examples' }
    ],

    sidebar: [
      {
        text: 'Installation and setup',
        items: [
          { text: 'Getting started', link: '/getting-started' },
          { text: 'Building manually', link: '/building-manually' }
        ]
      },
      {
        text: 'Cookbook',
        items: [

        ]
      },
      {
        text: 'Using the Python API',
        items: [

        ]
      },
      {
        text: 'Using the HTTP/WS API',
        items: [
          { text: 'General conventions', link: '/http-ws-api/general' },
          { text: 'Client commands', link: '/http-ws-api/client-commands' },
          { text: 'Memory commands', link: '/http-ws-api/memory-commands' },
          { text: 'Test commands', link: '/http-ws-api/test-commands' },
        ]
      }
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/vuejs/vitepress' }
    ]
  }
})
