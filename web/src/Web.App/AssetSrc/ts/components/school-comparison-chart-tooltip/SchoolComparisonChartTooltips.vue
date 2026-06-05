<script setup lang="ts">
import { SchoolComparisonChartTooltip } from "@/main";
import type { FocusSource, SchoolComparisonChartTooltipsProps, SchoolComparisonChartTooltipPropsData } from ".";
import { onMounted, onUnmounted, ref } from "vue";

const { data } = defineProps<SchoolComparisonChartTooltipsProps>();
const tooltipX = ref(0);
const tooltipY = ref(0);
const datum = ref<SchoolComparisonChartTooltipPropsData | null>(null);
const focusSource = ref<FocusSource>();
const keyAttribute = "data-key";

const eventListenerMouseEnter = (e: MouseEvent, d: SchoolComparisonChartTooltipPropsData) => {
  const tooltipOffset = 10;
  const x = e.clientX + tooltipOffset;
  const y = e.clientY + window.scrollY + tooltipOffset;

  datum.value = d;
  tooltipX.value = x;
  tooltipY.value = y;
  focusSource.value = "mouse";
};

const eventListenerElementFocus = (e: Event, d: SchoolComparisonChartTooltipPropsData) => {
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

const addMouseListeners = (el: SVGGeometryElement, d: SchoolComparisonChartTooltipPropsData) => {
  el.addEventListener("mouseenter", (e) => eventListenerMouseEnter(e, d));
  el.addEventListener("mouseleave", eventListenerExit);
};

const addFocusListeners = (el: Element, d: SchoolComparisonChartTooltipPropsData) => {
  el.addEventListener("focus", (e) => eventListenerElementFocus(e, d));
  el.addEventListener("blur", eventListenerExit);
};

const removeMouseListeners = (el: SVGGeometryElement, d: SchoolComparisonChartTooltipPropsData) => {
  el.removeEventListener("mouseenter", (e) => eventListenerMouseEnter(e, d));
  el.removeEventListener("mouseleave", eventListenerExit);
};

const removeFocusListeners = (el: Element, d: SchoolComparisonChartTooltipPropsData) => {
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
    <SchoolComparisonChartTooltip :datum="datum" :x="tooltipX" :y="tooltipY" :focus-source="focusSource" />
  </Teleport>
</template>
