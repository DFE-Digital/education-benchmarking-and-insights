<script setup lang="ts">
import { onMounted, onUnmounted, ref } from "vue";
import type { ModalDialogProps } from ".";

const { overlayContentId } = defineProps<ModalDialogProps>();
const emit = defineEmits(["cancel", "close", "closed", "ok"]);
defineOptions({
  inheritAttrs: false,
});

const overlayClass = "modal-content-overlay";
const content = overlayContentId ? document.getElementById(overlayContentId) : null;
const overlayElements = [
  ...(document.getElementsByClassName("govuk-skip-link") as HTMLCollectionOf<HTMLElement>),
  ...document.getElementsByTagName("header"),
  ...document.getElementsByTagName("nav"),
  ...(content ? [content] : document.getElementsByTagName("main")),
  ...document.getElementsByTagName("footer"),
];

const closeOnEscapeKey = (e: KeyboardEvent) => (e.key === "Escape" ? emit("close") : null);

onMounted(() => {
  document.querySelector("body")?.classList.add(overlayClass);

  // Set page content to inert to indicate to screenreaders it is inactive
  overlayElements.forEach((el) => {
    el.inert = true;
    el.setAttribute("aria-hidden", "true");
  });

  document.body.addEventListener("keydown", closeOnEscapeKey);
});

onUnmounted(() => {
  document.querySelector("body")?.classList.remove(overlayClass);

  // Make page content active again when modal is closed
  overlayElements.forEach((el) => {
    el.inert = false;
    el.setAttribute("aria-hidden", "false");
  });

  document.body.removeEventListener("keydown", closeOnEscapeKey);
  emit("closed");
});

const okButton = ref<HTMLButtonElement>();
const focusOKButton = () => {
  okButton.value?.focus();
};

defineExpose({ focusOKButton });
</script>

<template>
  <div className="modal-overlay">
    <div
      aria-modal
      aria-labelledby="dialog-title"
      className="modal"
      role="alertdialog"
      tabIndex="0"
      :aria-busy="ariaBusy"
      :aria-describedby="ariaDescribedBy"
      :aria-live="ariaLive"
    >
      <div role="document">
        <h1 id="dialog-title" className="govuk-heading-m">
          {{ title }}
        </h1>
        <div className="govuk-body"><slot></slot></div>
        <div className="govuk-button-group">
          <button
            ref="okButton"
            v-if="ok"
            v-bind="$attrs"
            className="govuk-button govuk-button--ok"
            data-module="govuk-button"
            data-prevent-double-click="true"
            :aria-disabled="okDisabled"
            :disabled="okDisabled"
            @click="$emit('ok')"
          >
            {{ okLabel || "OK" }}
          </button>
          <button
            v-if="cancel"
            className="govuk-button govuk-button--secondary govuk-button--cancel"
            data-module="govuk-button"
            @click="$emit('cancel')"
          >
            {{ cancelLabel || "Cancel" }}
          </button>
        </div>
        <button
          aria-label="Close modal dialog"
          className="govuk-button govuk-button--secondary govuk-button--close"
          data-module="govuk-button"
          @click="$emit('close')"
        >
          ×
        </button>
      </div>
    </div>
  </div>
</template>
