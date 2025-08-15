<template>
  <div v-if="visible" class="school-chart-tooltip" :style="{ top: y + 'px', left: x + 'px' }">
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
          <td class="govuk-table__cell">{{ datum?.totalPupils?.toString() }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import type { SchoolChartTooltipPropsData } from ".";

const visible = ref(false);
const x = ref(0);
const y = ref(0);
const datum = ref<SchoolChartTooltipPropsData | null>(null);

function show(d: SchoolChartTooltipPropsData, px: number, py: number) {
  datum.value = d;
  x.value = px;
  y.value = py;
  visible.value = true;
}

function hide() {
  visible.value = false;
}

defineExpose({ show, hide });
</script>
