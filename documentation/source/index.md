---
layout: doc
aside: false

---

<style>
.root-language-button {
    border-color: var(--vp-button-brand-border);
    border-radius: 20px;

    background-color: var(--vp-button-brand-bg);
    padding: 0 20px;

    color: var(--vp-button-brand-text) !important;
    line-height: 38px;
    font-size: 14px;
    font-weight: 600;
    text-align: center !important;

    text-decoration: inherit !important;
    transition: color 0.25s, border-color 0.25s, background-color 0.25s !important;
}

.root-language-button:hover {
    border-color: var(--vp-button-brand-hover-border);
    background-color: var(--vp-button-brand-hover-bg);
    color: var(--vp-button-brand-hover-text) !important;
}

.root-language-button:active {
    border-color: var(--vp-button-brand-active-border);
    background-color: var(--vp-button-brand-active-bg);
    color: var(--vp-button-brand-active-text) !important;
}
</style>

<div style="display: flex; flex-direction: column; align-items: center; gap: 1em;">
    <img src="/favicon.svg" width="256em" />
    <h1>TASauria</h1>
    <h3>Please select a language</h3>
    <a class="root-language-button" href="./en/">English</a>
    <a class="root-language-button" href="./ja/">日本語</a>
</div>
