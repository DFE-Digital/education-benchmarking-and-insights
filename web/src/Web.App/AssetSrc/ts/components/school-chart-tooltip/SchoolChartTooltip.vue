<script setup lang="ts">
import type { SchoolChartTooltipProps } from ".";

const { datum, x, y } = defineProps<SchoolChartTooltipProps>();
</script>

<template>
  <div v-if="!!datum" class="school-chart-tooltip" :style="{ top: y + 'px', left: x + 'px' }">
    <div v-if="!(datum?.periodCoveredByReturn === 12)" className="tooltip-part-year-warning">
      <div class="govuk-warning-text govuk-!-margin-0">
        <span class="govuk-warning-text__icon" aria-hidden="true">!</span>
        <strong class="govuk-warning-text__text">
          <span class="govuk-visually-hidden">Warning</span>
          This school only has {{ datum?.periodCoveredByReturn }}
          {{ datum?.periodCoveredByReturn === 1 ? "month" : "months" }} of data available.
        </strong>
      </div>
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
          <td class="govuk-table__cell">{{ datum?.totalPupils?.toString() }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
