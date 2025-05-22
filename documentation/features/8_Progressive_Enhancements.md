# Developer Feature Documentation: Progressive Enhancements

As discussed in [ADR 0009](../architecture/decisions/0009-js-library.md), the move to server side rendered (SSR) charts will help support the minority of users who are unable to run JavaScript in their web browser. For those with JavaScript, components within the Web application rendered via Razor views may be enhanced for a value-added user experience.

## Vue.js

[Vue.js](https://vuejs.org/) has been implemented within the `Web` project within the monorepo. By default it ships with [Vite](https://vite.dev/) to manage the build process. It also comes with a dev server to help with rapid application development. To run the dev server, from within the `web/src/Web.App` folder run:

1. `npm i`
2. `npm run dev`

To build the Vue components (as well as the other compiled assets) run:

1. `npm run build`

which will populate `wwwroot` with the desired outputs. The `Web` application may then be (re)started in order to pull in the latest resources. This stage is performed as part of the build pipelines.

### Component basics

The [Vue documentation](https://vuejs.org/guide/essentials/template-syntax.html) is the best starting point for learning about the syntax and capabilities of the framework. The [Vue Single-File Component](https://vuejs.org/api/sfc-spec.html) convention should be used here, with each `.vue` file containing a `<template>`, `<script>` and optionally a `<style>` block. `<template>` defines what to render and `<script>` additional configuration and meta data related to that component. e.g.:

```vue
<!-- components/HelloWorld.Vue -->
<script setup lang="ts">
const { msg } = defineProps<{ msg: string; }>();

onMounted(() => {
    console.log("message", msg);
});
</script>

<template>
    <div>{{ msg }}</div>
</template>
```

`defineProps()` exposes the typed collection of properties to the `<template>`, as well as any consuming templates. It may also be destructured for use elsewhere within the `setup` block.

`onMounted()` is one of the common lifecycle functions that may be implemented if required, and in the example above writes the `msg` value to the console.

Basic state management is achieved via `ref`s. Values may be mutated by setting the `value` property and explicit effects performed using `watchEffect()`. `ref`s or other variables within the component's context may also be bound to elements or other templates using `v-bind:prop`, or more commonly just `:prop`. e.g.:

```vue
<script setup lang="ts">
import MyButton from "./MyButton.vue"
const count = ref(0);

watchEffect(() => {
    console.log(count.value));
}
</script>

<template>
    <MyButton @click="count.value++" :count="count" class="my-class">
        click(s) of the button
    </MyButton>
</template>
```

```vue
<!--./MyButton.vue -->
<script setup lang="ts">
defineProps<{ count: number; }>();
</script>

<template>
    <button>
        {{ count }}
        <slot></slot>
    </button>
</template>
```

In the example above, `:count` binds the `ref` `count` to the `MyButton` component props. The `@click` registers a click handler, which in this case increments the value of `count`. The child content may be rendered within the child component using `<slot>`s and the `class` is passed down as additional attributes that will render to the first element under the `<template>` in the child component (by default).

### Unit tests

Vite components may be unit tested using [Vitest](https://vitest.dev/) by creating `.spec.ts` files in `__tests__` folders alongside the component to be tested. e.g.:

```ts
// components/__tests__/HelloWorld.spec.ts
import { describe, it, expect } from "vitest";
import { mount } from "@vue/test-utils";
import HelloWorld from "../HelloWorld.vue"; // as above

describe("HelloWorld", () => {
    it("renders properly", () => {
        const wrapper = mount(HelloWorld, { props: { msg: "Hello Vitest" } });
        expect(wrapper.text()).toContain("Hello Vitest");
    });
});
```

### Other scripts

1. `npm run format` to format all included files based on the prettier/eslint configuration
2. `npm run test:unit` to run the unit tests

### IDE configuration

The following extensions are recommended for working with Vue in Visual Studio Code:

1. [Vue - Official](https://marketplace.visualstudio.com/items?itemName=Vue.volar)
2. [Vitest](https://marketplace.visualstudio.com/items?itemName=vitest.explorer)
3. [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint)
4. [EditorConfig for VS Code](https://marketplace.visualstudio.com/items?itemName=EditorConfig.EditorConfig)
5. [Prettier - Code formatter](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)

## Mounting Vue.js components

The `AssetSrc/ts/main.ts` entry point exposes all of the components that should be bundled, as well as the [createApp](https://vuejs.org/api/application.html#createapp) function. The project has been configured to build its output to `dist/vite`, which is copied to `wwwroot/js` when running `gulp`. This then allows the bundle to be imported directly into a Razor view, ideally as a partial within `/web/src/Web.App/Views/Shared/Enhancements`. e.g.:

```html
<script type="module" add-nonce="true">
    import {createApp, HelloWorld} from "@Html.FileVersionedPath("/js/main.js")";

    const element = document.getElementById("#placeholder");

    const helloWorld = createApp(HelloWorld, {
        msg: "@(Model.Message)"
    });

    helloWorld.mount(element);
</script>
```

This partial should in turn be called from a `scripts` section within the parent view. e.g.:

```html
<div id="placeholder"></div>

<!-- ... -->

@section scripts
{
    @await Html.PartialAsync("Enhancements/_HelloWorld", new HelloWorldViewModel
    {
        Message = "Hello"
    })
}
```

## Styles

Component styling specific to progressive enhancements should be either added to `enhancements.scss` or [maintained inline](https://vuejs.org/api/sfc-css-features.html) alongside each component itself. When migrating components from the `front-end` React project the source `css` files may be safely removed as both `main.scss` and `enhancements.scss` are built and included in `wwwroot` for consumption in the primary `_Layout.cshtml` view.

## Debugging

When running the Vue dev server, the [Vue DevTools](https://devtools.vuejs.org/) are enabled by default. These may be used to mutate state to validate reactiveness, for example. The standard browser dev tools may also be used in conjunction with this. For a production build, source maps have been configured making it easy to step through `.vue` or other utlitity `.ts` files.

<!-- Leave the rest of this page blank -->
\newpage
