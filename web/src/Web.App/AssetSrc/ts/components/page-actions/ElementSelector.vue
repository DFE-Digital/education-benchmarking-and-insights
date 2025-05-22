<script setup lang="ts">
import { onMounted, ref } from "vue";
import type { ElementSelectorProps } from ".";

const { elements } = defineProps<ElementSelectorProps>();
const selectedElementIndexes = ref<number[]>([]);

defineExpose({ selectedElementIndexes });

onMounted(() => {
  selectedElementIndexes.value = elements.map((_, i) => i);
});
</script>

<template>
  <div v-if="showValidationError" className="govuk-error-summary" data-module="govuk-error-summary">
    <div role="alert">
      <h2 className="govuk-error-summary__title">There is a problem</h2>
      <div className="govuk-error-summary__body">
        <ul className="govuk-list govuk-error-summary__list">
          <li>
            <a href="#elements-0">Select one or more items</a>
          </li>
        </ul>
      </div>
    </div>
  </div>
  <div class="govuk-form-group">
    <fieldset className="govuk-fieldset">
      <div className="govuk-checkboxes govuk-checkboxes--elements" data-module="govuk-checkboxes">
        <div v-for="(element, i) in elements" className="govuk-checkboxes__item" :key="i">
          <input
            v-model="selectedElementIndexes"
            class="govuk-checkboxes__input"
            type="checkbox"
            :id="`elements-${i}`"
            :name="`elements-${i}`"
            :value="i"
          />
          <label className="govuk-label govuk-checkboxes__label" :for="`elements-${i}`">
            {{ element.title || `Item ${i + 1}` }}
          </label>
        </div>
      </div>
    </fieldset>
  </div>
</template>
