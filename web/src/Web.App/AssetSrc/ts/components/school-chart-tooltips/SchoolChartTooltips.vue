<script setup lang="ts">
import { SchoolChartTooltip } from "@/main";
import type { SchoolChartTooltipsProps } from ".";
import type { SchoolChartTooltipPropsData } from "../school-chart-tooltip";
import { onMounted, ref } from "vue";

const { data } = defineProps<SchoolChartTooltipsProps>();
const tooltipX = ref(0);
const tooltipY = ref(0);
const datum = ref<SchoolChartTooltipPropsData | null>(null);

const attachMouseListeners = (el: SVGGeometryElement, d: SchoolChartTooltipPropsData) => {
  el.addEventListener("mouseenter", (e) => {
    const tooltipOffset = 10;
    const x = e.clientX + tooltipOffset;
    const y = e.clientY + window.scrollY + tooltipOffset;

    datum.value = d;
    tooltipX.value = x;
    tooltipY.value = y;
  });

  el.addEventListener("mouseleave", () => {
    datum.value = null;
  });
};

const attachFocusListeners = (el: Element, d: SchoolChartTooltipPropsData) => {
  el.addEventListener("focus", () => {
    const rect = el.getBoundingClientRect();
    const x = rect.right + 18;
    const y = rect.top + 6;

    datum.value = d;
    tooltipX.value = x;
    tooltipY.value = y;
  });

  el.addEventListener("blur", () => {
    datum.value = null;
  });
};

// todo: remove event listeners on unmount
onMounted(() => {
  const elements = document.querySelectorAll("[data-key]");

  elements.forEach((el) => {
    const key = el.getAttribute("data-key");
    const datum = data.find((d) => d.urn === key);
    if (!datum) {
      return;
    }

    switch (el.tagName.toLowerCase()) {
      case "rect":
        attachMouseListeners(el as SVGGeometryElement, datum);
        break;
      case "a":
        attachFocusListeners(el, datum);
        break;
    }
  });
});
</script>

<template>
  <Teleport to="body">
    <SchoolChartTooltip :datum="datum" :x="tooltipX" :y="tooltipY" />
  </Teleport>
</template>
