<script setup lang="ts">
import { ref, watch } from "vue";
import type { SchoolComparisonChartTooltipProps } from ".";

const { datum, x, y, focusSource } = defineProps<SchoolComparisonChartTooltipProps>();
const top = ref(0);
const left = ref(0);
const offsetX = ref(0);
const tooltip = ref<HTMLDivElement>();
const gutter = 30;
const maxWidth = 300;

watch([() => x, () => y], ([newX, newY]) => {
  left.value = newX;
  top.value = newY;
});

watch(tooltip, () => {
  offsetX.value = 0;

  // reposition tooltip if it overflows the right edge of the window from a mouse focus trigger
  const bounds = tooltip.value?.getBoundingClientRect();
  if (
    focusSource === "mouse" &&
    bounds &&
    bounds.x + bounds.width + gutter > window.outerWidth &&
    window.outerWidth > bounds.width
  ) {
    offsetX.value = -1 * (maxWidth - (window.outerWidth - bounds.x)) - gutter;
  }
});
</script>

<template>
  <div
    v-if="!!datum"
    class="school-chart-tooltip"
    :style="{
      top: top + 'px',
      left: left + 'px',
      transform: offsetX !== 0 ? 'translate(' + offsetX + 'px,0px)' : undefined,
      width: offsetX !== 0 ? maxWidth + 'px' : undefined,
    }"
    ref="tooltip"
  >
    <div v-if="!(datum?.periodCoveredByReturn === 12)" class="school-tags">
      <strong class="govuk-tag govuk-tag--red">
        Only has {{ datum?.periodCoveredByReturn }}
        {{ datum?.periodCoveredByReturn === 1 ? "month" : "months" }} of data
      </strong>
    </div>
    <table class="govuk-table govuk-table--small-text-until-tablet tooltip-table">
      <caption class="govuk-table__caption govuk-table__caption--s">
        {{
          datum?.schoolName
        }}
      </caption>
      <thead class="govuk-table__head govuk-visually-hidden">
      <tr class="govuk-table__row">
        <th scope="col" class="govuk-table__header">Item</th>
        <th scope="col" class="govuk-table__header">Value</th>
      </tr>
      </thead>
      <tbody class="govuk-table__body">
      <tr class="govuk-table__row">
        <th scope="row" class="govuk-table__header">Local authority</th>
        <td class="govuk-table__cell">{{ datum?.laName }}</td>
      </tr>
      <tr class="govuk-table__row">
        <th scope="row" class="govuk-table__header">School type</th>
        <td class="govuk-table__cell">{{ datum?.schoolType }}</td>
      </tr>
      <tr class="govuk-table__row">
        <th scope="row" class="govuk-table__header">Number of pupils</th>
        <td class="govuk-table__cell">
          {{ datum?.totalPupils?.toString() }}
        </td>
      </tr>
      <tr v-if="(datum?.shouldShowTag)" class="govuk-table__row">
        <th scope="row" class="govuk-table__header">Progress 8 banding</th>
        <td class="govuk-table__cell">
          <strong
            :class="[
              'govuk-tag',
              `govuk-tag--${datum?.progressBandingColour}`,
              'govuk-!-font-size-16'
            ]">
            {{ datum?.progressBanding }}
          </strong>
        </td>
      </tr>
      </tbody>
    </table>
  </div>
</template>
