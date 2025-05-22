<script setup lang="ts">
import { onMounted, onUnmounted, ref, useTemplateRef, watchEffect } from "vue";
import type { PageActionsProps } from ".";
import { ModalDialog } from "@/main";
import ElementSelector from "./ElementSelector.vue";
import { DownloadService } from "@/services";
import type { ElementAndAttributes } from "@/services/types";

const { all, costCodesAttr, elementClassName, elementTitleAttr, fileName, showTitles } =
  defineProps<PageActionsProps>();

const open = ref(false);
const button = ref<HTMLButtonElement>();
const progress = ref<number | undefined>(undefined);
const modalTemplate = useTemplateRef("modal");
const elementSelectorTemplate = useTemplateRef("elementSelector");
const cancelMode = ref(false);
const imagesLoading = ref(false);
const selectedElements = ref<ElementAndAttributes[]>([]);
const allElements = ref<ElementAndAttributes[]>([]);
const showValidationError = ref(false);

const progressId = "save-progress";
let abortController = new AbortController();
let autoCloseTimeout: NodeJS.Timeout | undefined;

watchEffect(() => {
  if (cancelMode.value) {
    modalTemplate.value?.focusOKButton();
  }
});

watchEffect(() => {
  if (progress.value === 100) {
    autoCloseTimeout = setTimeout(() => {
      close(false);
    }, 10000);
  }
});

const startDownload = () => {
  DownloadService.downloadPngImages({
    elementsSelector: () => (all ? allElements.value : selectedElements.value),
    fileName,
    onImagesLoading: (loading) => (imagesLoading.value = loading),
    onProgress: (percentage) => (progress.value = percentage),
    showTitles,
    signal: abortController.signal,
  }).then(
    () => {
      console.debug("Images saved successfully");
    },
    (err: Error) => {
      console.warn("Unable to save images", err);
    }
  );
};

const validated = () => {
  const valid =
    !!elementSelectorTemplate.value &&
    elementSelectorTemplate.value.selectedElementIndexes.length > 0;
  showValidationError.value = !valid;
  return valid;
};

const abort = () => {
  abortController.abort();
  abortController = new AbortController();
};

const close = (shouldAbort: boolean) => {
  if (imagesLoading.value && shouldAbort) {
    abort();
  }

  progress.value = undefined;
  cancelMode.value = false;
  open.value = false;
  showValidationError.value = false;
};

const ok = () => {
  if (cancelMode.value) {
    close(true);
    return;
  }

  selectedElements.value =
    elementSelectorTemplate.value?.selectedElementIndexes.map((i) => allElements.value[i]) ?? [];

  if (validated()) {
    startDownload();
  }
};

const cancel = () => {
  if (!imagesLoading.value) {
    close(true);
    return;
  }

  if (cancelMode.value) {
    cancelMode.value = false;
    return;
  }

  cancelMode.value = true;
};

onMounted(() => {
  const results = [];

  const elements = document.getElementsByClassName(elementClassName);
  for (let i = 0; i < elements.length; i++) {
    const element = elements[i] as HTMLElement;
    const title = elementTitleAttr
      ? element.getAttribute(elementTitleAttr) || undefined
      : undefined;
    const rawCostCodes = costCodesAttr ? element.getAttribute(costCodesAttr) : undefined;
    const costCodes = rawCostCodes ? (JSON.parse(rawCostCodes) as string[]) : undefined;
    results.push({ element, title, costCodes });
  }

  allElements.value = results;
  selectedElements.value = results;
});

onUnmounted(() => {
  if (autoCloseTimeout) {
    clearTimeout(autoCloseTimeout);
  }
});
</script>

<template>
  <button
    class="govuk-button govuk-button--secondary"
    data-module="govuk-button"
    ref="button"
    :disabled="open"
    :aria-disabled="open"
    @click="open = true"
  >
    {{ buttonLabel ?? "Save all" }}
  </button>
  <Teleport to="body">
    <ModalDialog
      v-if="open"
      ref="modal"
      :aria-busy="!!progress"
      :aria-described-by="!!progress ? progressId : undefined"
      :aria-live="!!progress ? 'polite' : undefined"
      :cancel="true"
      :cancel-label="cancelMode ? 'Back' : progress === 100 ? 'Close' : 'Cancel'"
      :data-custom-event-chart-name="saveEventId && modalTitle ? modalTitle : undefined"
      :data-custom-event-id="saveEventId"
      :ok="true"
      :ok-label="'Start'"
      :ok-disabled="!cancelMode && (imagesLoading || progress === 100)"
      :title="modalTitle"
      @cancel="cancel()"
      @close="close(true)"
      @closed="button?.focus()"
      @ok="ok()"
    >
      <div v-if="cancelMode" class="govuk-body">
        Are you sure you want to cancel saving your images?
      </div>
      <template v-if="!cancelMode">
        <div className="govuk-body">
          <span v-if="progress === 100">
            Your file has been saved and downloaded successfully.
          </span>
          <span v-if="!!progress && progress !== 100">
            Please wait while your image{{
              !all && selectedElements.length === 1 ? " is" : "s are"
            }}
            saved.
          </span>
          <span v-if="!progress">
            <template v-if="!all">Select the charts you want to download.</template>
            This may take a few minutes to complete.
          </span>
        </div>
        <ElementSelector
          v-if="!all && !progress"
          ref="elementSelector"
          :elements="allElements"
          :showValidationError="showValidationError"
        />
        <ProgressWithAria
          v-if="showProgress && !!progress"
          complete-message="Your file has been saved and downloaded successfully."
        />
      </template>
    </ModalDialog>
  </Teleport>
</template>
