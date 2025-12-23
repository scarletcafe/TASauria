import { defineConfig } from 'vitepress'
import { withMermaid } from 'vitepress-plugin-mermaid'

// https://vitepress.dev/reference/site-config
export default defineConfig(withMermaid({
  base: '/TASauria/',
  title: "TASauria Documentation",
  description: "User guide and API reference for TASauria, a plugin and library for remotely controlling the BizHawk emulator.",
  head: [
    ['link', { href: '/TASauria/apple-touch-icon.png', rel: 'apple-touch-icon', sizes: '180x180', }],
    ['link', { href: '/TASauria/favicon-96x96.png',rel: 'icon', type: 'image/png', sizes: '96x96', }],
    ['link', { href: '/TASauria/favicon.ico', rel: 'shortcut icon', }],
    ['link', { href: '/TASauria/favicon.svg', rel: 'icon', type: 'image/svg+xml', }],
    ['link', { href: '/TASauria/site.webmanifest', rel: 'manifest', }],
    ['link', { href: '/TASauria/web-app-manifest-192x192.png', rel: 'icon', type: 'image/png', sizes: '512x512', }],
    ['link', { href: '/TASauria/web-app-manifest-192x192.png',rel: 'icon', type: 'image/png', sizes: '192x192', }],

    ['meta', { name: 'apple-mobile-web-app-title', content: 'TASauria' }],
  ],
  locales: {
    en: {
      label: 'English',
      lang: 'en',
      link: '/en',
      themeConfig: {
        lastUpdated: true,
        editLink: {
          pattern: "https://github.com/scarletcafe/TASauria/edit/main/documentation/source/:path",
          text: "Edit this page on GitHub"
        },

        nav: [
          { text: 'Home', link: '/en/' },
          { text: 'Cookbook', link: '/en/cookbook/' }
        ],

        sidebar: [
          {
            text: 'Installation and setup',
            items: [
              { text: 'Getting started', link: '/en/getting-started' },
              { text: 'Building manually', link: '/en/building-manually' },
            ]
          },
          {
            text: 'Using the Python API',
            items: [
              { text: 'First steps', link: '/en/python-api/first-steps' },
              { text: 'Client API reference', link: '/en/python-api/client-api-reference' },
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
              { text: 'General conventions', link: '/en/http-ws-api/general' },
              { text: 'Client commands', link: '/en/http-ws-api/client-commands' },
              { text: 'Joypad commands', link: '/en/http-ws-api/joypad-commands' },
              { text: 'Memory commands', link: '/en/http-ws-api/memory-commands' },
              { text: 'Meta commands', link: '/en/http-ws-api/meta-commands' },
              { text: 'Movie commands', link: '/en/http-ws-api/movie-commands' },
              { text: 'Savestate commands', link: '/en/http-ws-api/savestate-commands' },
              { text: 'Test commands', link: '/en/http-ws-api/test-commands' },
            ]
          },
          {
            text: 'Additional reading',
            items: [
              { text: 'Lua equivalents', link: '/en/additional-reading/lua-equivalents' },
              { text: 'Performance', link: '/en/additional-reading/performance' },
              { text: 'Alternatives', link: '/en/additional-reading/alternatives' },
            ]
          }
        ],
      }
    },
    ja: {
      label: '日本語',
      lang: 'ja',
      link: '/ja',
      title: 'TASauriaのドキュメント',
      themeConfig: {
        lastUpdated: true,
        editLink: {
          pattern: "https://github.com/scarletcafe/TASauria/edit/main/documentation/source/:path",
          text: "このページをGitHubで編集"
        },

        nav: [
          { text: 'ホーム', link: '/ja/' },
          { text: 'Cookbook', link: '/ja/cookbook/' }
        ],

        sidebar: [
          {
            text: 'インストールと設定',
            items: [
              { text: 'はじめに', link: '/ja/getting-started' },
              { text: '手動でコンパイル', link: '/ja/building-manually' },
            ]
          },
          {
            text: 'Python APIの使い方',
            items: [
              { text: 'First steps', link: '/ja/python-api/first-steps' },
              { text: 'クライアントAPIリファレンス', link: '/ja/python-api/client-api-reference' },
            ]
          },
          {
            text: 'Cookbook',
            items: [

            ]
          },
          {
            text: 'HTTP・WSのAPIの使い方',
            items: [
              { text: '基本情報', link: '/ja/http-ws-api/general' },
              { text: 'クライアントコマンド', link: '/ja/http-ws-api/client-commands' },
              { text: '入力コマンド', link: '/ja/http-ws-api/joypad-commands' },
              { text: 'メモリーコマンド', link: '/ja/http-ws-api/memory-commands' },
              { text: 'メタコマンド', link: '/ja/http-ws-api/meta-commands' },
              { text: 'ムービーコマンド', link: '/ja/http-ws-api/movie-commands' },
              { text: 'セーブステートコマンド', link: '/ja/http-ws-api/savestate-commands' },
              { text: 'テスト用コマンド', link: '/ja/http-ws-api/test-commands' },
            ]
          },
          {
            text: 'Additional reading',
            items: [
              { text: 'LuaからPythonへの代替法', link: '/ja/additional-reading/lua-equivalents' },
              { text: 'パフォーマンス', link: '/ja/additional-reading/performance' },
              { text: '代替', link: '/ja/additional-reading/alternatives' },
            ]
          }
        ],
      }
    }
  },
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    logo: '/favicon.svg',

    search: {
      provider: 'local'
    },

    socialLinks: [
      { icon: 'github', link: 'https://github.com/scarletcafe/TASauria' },
    ]
  }
}))
