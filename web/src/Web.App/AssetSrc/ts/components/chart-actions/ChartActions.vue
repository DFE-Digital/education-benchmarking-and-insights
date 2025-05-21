<script setup lang="ts">
import type { ChartActionsProps } from ".";
import { DownloadService } from "../../services";
const { elementId } = defineProps<ChartActionsProps>();
const elementSelector = () => document.getElementById(elementId);
</script>

<template>
  <div class="share-buttons">
    <button
      v-if="showSave"
      class="govuk-button govuk-button--secondary share-button share-button--save"
      data-module="govuk-button"
      data-prevent-double-click="true"
      :data-custom-event-chart-name="saveEventId && title"
      :data-custom-event-id="saveEventId"
      :disabled="disabled"
      :aria-disabled="disabled"
      @click="
        (ev) => {
          DownloadService.saveImageToBrowser({
            costCodes,
            elementSelector,
            showTitle,
            title,
            triggerElement: ev.target as HTMLButtonElement,
          });
        }
      "
    >
      Save <span v-if="title" class="govuk-visually-hidden">{{ title }}</span> as image
    </button>
    <button
      v-if="showCopy"
      class="govuk-button govuk-button--secondary share-button share-button--copy"
      data-module="govuk-button"
      data-prevent-double-click="true"
      :data-custom-event-chart-name="copyEventId && title"
      :data-custom-event-id="copyEventId"
      :disabled="disabled"
      :aria-disabled="disabled"
      @click="
        (ev) => {
          DownloadService.copyImageToClipboard({
            costCodes,
            elementSelector,
            showTitle,
            title,
            triggerElement: ev.target as HTMLButtonElement,
          });
        }
      "
    >
      Copy <span v-if="title" class="govuk-visually-hidden">{{ title }}</span> image
    </button>
  </div>
</template>
