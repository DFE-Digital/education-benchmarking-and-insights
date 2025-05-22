<script setup lang="ts">
import { ref, watchEffect } from "vue";
import type { ProgressIndicatorProps } from ".";

const { percentage } = defineProps<ProgressIndicatorProps>();

const ariaStatus = ref(0);

watchEffect(() => {
  if (!percentage || percentage < 24) {
    ariaStatus.value = 0;
  } else if (percentage < 49) {
    ariaStatus.value = 25;
  } else if (percentage < 74) {
    ariaStatus.value = 50;
  } else if (percentage < 99) {
    ariaStatus.value = 75;
  } else {
    ariaStatus.value = 100;
  }
});
</script>

<template>
  <div v-id="progressId" className="govuk-visually-hidden" role="status" :id="progressId">
    <template v-if="percentage === 100 && completeMessage">
      {{ completeMessage }}
    </template>
    <template v-if="percentage !== 100 || !completeMessage">
      {{ `${ariaStatus}% complete` }}
    </template>
  </div>
  <svg
    v-if="percentage < 100"
    viewBox="0 0 40 40"
    class="progress-wrapper"
    :width="size"
    :height="size"
  >
    <path
      class="progress-circle"
      d="M20 4.0845
      a 15.9155 15.9155 0 0 1 0 31.831
      a 15.9155 15.9155 0 0 1 0 -31.831"
      :strokeDasharray="`${percentage}, 100`"
    />
  </svg>
</template>
