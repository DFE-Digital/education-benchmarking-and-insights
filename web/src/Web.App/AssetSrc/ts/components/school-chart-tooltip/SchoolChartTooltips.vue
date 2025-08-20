<script setup lang="ts">
import { SchoolChartTooltip } from "@/main";
import type { FocusSource, SchoolChartTooltipsProps, SchoolChartTooltipPropsData } from ".";
import { onMounted, onUnmounted, ref } from "vue";

const { data } = defineProps<SchoolChartTooltipsProps>();
const tooltipX = ref(0);
const tooltipY = ref(0);
const datum = ref<SchoolChartTooltipPropsData | null>(null);
const focusSource = ref<FocusSource>();
const keyAttribute = "data-key";

const eventListenerMouseEnter = (e: MouseEvent, d: SchoolChartTooltipPropsData) => {
  const tooltipOffset = 10;
  const x = e.clientX + tooltipOffset;
  const y = e.clientY + window.scrollY + tooltipOffset;

  datum.value = d;
  tooltipX.value = x;
  tooltipY.value = y;
  focusSource.value = "mouse";
};

const eventListenerElementFocus = (e: Event, d: SchoolChartTooltipPropsData) => {
  const rect = (e.target as Element).getBoundingClientRect();
  const x = rect.right + 18;
  const y = rect.top + window.scrollY + 6;

  datum.value = d;
  tooltipX.value = x;
  tooltipY.value = y;
  focusSource.value = "keyboard";
};

const eventListenerExit = () => {
  datum.value = null;
};

const addMouseListeners = (el: SVGGeometryElement, d: SchoolChartTooltipPropsData) => {
  el.addEventListener("mouseenter", (e) => eventListenerMouseEnter(e, d));
  el.addEventListener("mouseleave", eventListenerExit);
};

const addFocusListeners = (el: Element, d: SchoolChartTooltipPropsData) => {
  el.addEventListener("focus", (e) => eventListenerElementFocus(e, d));
  el.addEventListener("blur", eventListenerExit);
};

const removeMouseListeners = (el: SVGGeometryElement, d: SchoolChartTooltipPropsData) => {
  el.removeEventListener("mouseenter", (e) => eventListenerMouseEnter(e, d));
  el.removeEventListener("mouseleave", eventListenerExit);
};

const removeFocusListeners = (el: Element, d: SchoolChartTooltipPropsData) => {
  el.removeEventListener("focus", (e) => eventListenerElementFocus(e, d));
  el.removeEventListener("blur", eventListenerExit);
};

onMounted(() => {
  const elements = document.querySelectorAll(`[${keyAttribute}]`);

  elements.forEach((el) => {
    const key = el.getAttribute(keyAttribute);
    const datum = data.find((d) => d.urn === key);
    if (!datum) {
      return;
    }

    switch (el.tagName.toLowerCase()) {
      case "rect":
        addMouseListeners(el as SVGGeometryElement, datum);
        break;
      case "a":
        addFocusListeners(el, datum);
        break;
    }
  });
});

onUnmounted(() => {
  const elements = document.querySelectorAll(`[${keyAttribute}]`);

  elements.forEach((el) => {
    const key = el.getAttribute(keyAttribute);
    const datum = data.find((d) => d.urn === key);
    if (!datum) {
      return;
    }

    switch (el.tagName.toLowerCase()) {
      case "rect":
        removeMouseListeners(el as SVGGeometryElement, datum);
        break;
      case "a":
        removeFocusListeners(el, datum);
        break;
    }
  });
});

defineOptions({
  name: "SchoolChartTooltips",
});
</script>

<template>
  <Teleport to="body">
    <SchoolChartTooltip :datum="datum" :x="tooltipX" :y="tooltipY" :focus-source="focusSource" />
  </Teleport>
</template>
