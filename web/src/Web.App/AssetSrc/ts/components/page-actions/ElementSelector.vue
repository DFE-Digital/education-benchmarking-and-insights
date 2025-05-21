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

<!-- todo: validation -->
<template>
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
