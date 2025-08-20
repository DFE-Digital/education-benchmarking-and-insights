<script setup lang="ts">
import { ref } from "vue";
import type { ChartActionsProps } from ".";
import { DownloadService } from "../../services";

const { costCodes, elementId, showTitle, title } = defineProps<ChartActionsProps>();
const saving = ref(false);
const copying = ref(false);
const copied = ref(false);

const elementSelector = () => document.getElementById(elementId);

const saveImage = () => {
  saving.value = true;

  DownloadService.downloadPngImage({
    costCodes,
    elementSelector,
    mode: "save",
    showTitle,
    title,
  })
    .then(
      () => {
        console.debug("Image saved successfully");
      },
      (err: Error) => {
        console.warn("Unable to save image", err);
      }
    )
    .finally(() => {
      saving.value = false;
    });
};

const copyImage = () => {
  copying.value = true;

  DownloadService.downloadPngImage({
    costCodes,
    elementSelector,
    mode: "copy",
    showTitle,
    title,
  })
    .then(
      () => {
        console.debug("Image copied successfully");
        copied.value = true;
        setTimeout(() => {
          copied.value = false;
        }, 2000);
      },
      (err: Error) => {
        console.warn("Unable to copy image", err);
      }
    )
    .finally(() => {
      copying.value = false;
    });
};

defineOptions({
  name: "ChartActions",
});
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
      :disabled="saving"
      :aria-disabled="saving"
      @click="saveImage()"
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
      :disabled="copying"
      :aria-disabled="copying"
      @click="copyImage()"
    >
      <template v-if="!copied">
        Copy <span v-if="title" class="govuk-visually-hidden">{{ title }}</span> image
      </template>
      <template v-if="copied"> Copied</template>
    </button>
  </div>
</template>
