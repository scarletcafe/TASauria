import { defineConfig } from 'vitepress'
import { withMermaid } from 'vitepress-plugin-mermaid'

// https://vitepress.dev/reference/site-config
export default defineConfig(withMermaid({
  title: "TASauria Documentation",
  description: "User guide and API reference for TASauria, a plugin and library for remotely controlling the BizHawk emulator.",
  head: [
    ['link', { rel: 'icon', type: 'image/png', href: '/favicon-96x96.png', sizes: '96x96' }],
    ['link', { rel: 'icon', type: 'image/svg+xml', href: '/favicon.svg' }],
    ['link', { rel: 'shortcut icon', href: '/favicon.ico' }],
    ['link', { rel: 'apple-touch-icon', href: '/apple-touch-icon.png', sizes: '180x180' }],
    ['meta', { name: 'apple-mobile-web-app-title', content: 'TASauria' }],
    ['link', { rel: 'manifest', href: '/site.webmanifest' }]
  ],
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    logo: '/favicon.svg',

    search: {
      provider: 'local'
    },

    nav: [
      { text: 'Home', link: '/' },
      { text: 'Cookbook', link: '/cookbook/' }
    ],

    sidebar: [
      {
        text: 'Installation and setup',
        items: [
          { text: 'Getting started', link: '/getting-started' },
          { text: 'Building manually', link: '/building-manually' },
        ]
      },
      {
        text: 'Using the Python API',
        items: [
          { text: 'First steps', link: '/python-api/first-steps' },
          { text: 'Client API reference', link: '/python-api/client-api-reference' },
        ]
      },
      {
        text: 'Cookbook',
        items: [

        ]
      },
      {
        text: 'Using the HTTP/WS API',
        items: [
          { text: 'General conventions', link: '/http-ws-api/general' },
          { text: 'Client commands', link: '/http-ws-api/client-commands' },
          { text: 'Joypad commands', link: '/http-ws-api/joypad-commands' },
          { text: 'Memory commands', link: '/http-ws-api/memory-commands' },
          { text: 'Meta commands', link: '/http-ws-api/meta-commands' },
          { text: 'Movie commands', link: '/http-ws-api/movie-commands' },
          { text: 'Savestate commands', link: '/http-ws-api/savestate-commands' },
          { text: 'Test commands', link: '/http-ws-api/test-commands' },
        ]
      },
      {
        text: 'Additional reading',
        items: [
          { text: 'Performance', link: '/additional-reading/performance' },
          { text: 'Alternatives', link: '/additional-reading/alternatives' },
        ]
      }
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/scarletcafe/TASauria' },
    ]
  }
}))
