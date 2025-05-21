<script setup lang="ts">
import { ref } from "vue";
import type { PageActionsProps } from ".";
import { ModalDialog } from "@/main";
defineProps<PageActionsProps>();
const open = ref(false);
</script>

<template>
  <button
    class="govuk-button govuk-button--secondary"
    data-module="govuk-button"
    @click="open = true"
  >
    {{ buttonLabel ?? "Save all" }}
  </button>
  <Teleport to="body">
    <ModalDialog
      v-if="open"
      :cancel="true"
      :ok="true"
      :okLabel="'Start'"
      :title="modalTitle"
      @close="open = false"
      >Modal body slot</ModalDialog
    >
  </Teleport>
</template>
