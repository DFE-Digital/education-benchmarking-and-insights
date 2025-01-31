import { forwardRef, useImperativeHandle, useRef } from "react";
import {
  ModalSaveImagesButtonHandler,
  ModalSaveImagesButtonProps,
} from "./types";

export const ModalSaveImagesButton = forwardRef<
  ModalSaveImagesButtonHandler,
  ModalSaveImagesButtonProps
>(({ buttonLabel, disabled, onOpenModal }, ref) => {
  const button = useRef<HTMLButtonElement>(null);

  useImperativeHandle(ref, () => ({
    async focus() {
      button.current?.focus();
    },
  }));

  return (
    <button
      aria-disabled={disabled}
      className="govuk-button govuk-button--secondary"
      data-module="govuk-button"
      disabled={disabled}
      onClick={onOpenModal}
      ref={button}
    >
      {buttonLabel}
    </button>
  );
});
