import { useEffect, useRef, useState } from "react";
import { ModalSaveImagesProps } from ".";
import {
  ModalSaveImagesButton,
  ModalSaveImagesButtonHandler,
} from "./modal-save-images-button";
import { ModalSaveImagesModal } from "./modal-save-images-modal";

export function ModalSaveImages({
  buttonLabel,
  waitForEventType,
  ...props
}: ModalSaveImagesProps) {
  const [modalOpen, setModalOpen] = useState(false);
  const [buttonDisabled, setButtonDisabled] = useState(!!waitForEventType);
  const button = useRef<ModalSaveImagesButtonHandler>(null);
  const [modeChanged, setModeChanged] = useState(false);

  useEffect(() => {
    if (waitForEventType) {
      document.addEventListener(
        waitForEventType,
        (e: CustomEventInit<boolean>) => {
          setButtonDisabled(e.detail !== true);
        },
        false
      );
    }
  }, [waitForEventType]);

  const handleOpenModal = async () => {
    setButtonDisabled(true);

    const radio = document.getElementById("mode-chart") as HTMLInputElement;
    if (radio && radio.checked === false) {
      setModeChanged(true);
      radio.click();
      await new Promise<void>((resolve) => {
        setTimeout(() => {
          resolve();
        }, 250);
      });
    }

    setModalOpen(true);
  };

  const handleCloseModal = () => {
    if (modeChanged) {
      const radio = document.getElementById("mode-table") as HTMLInputElement;
      if (radio) {
        radio.click();
      }
    }

    setModalOpen(false);
    setButtonDisabled(false);
    window.setTimeout(function () {
      button.current?.focus();
    }, 0);
  };

  return (
    <>
      <ModalSaveImagesButton
        buttonLabel={buttonLabel}
        disabled={buttonDisabled}
        onOpenModal={handleOpenModal}
        ref={button}
      />
      {modalOpen && (
        <ModalSaveImagesModal onCloseModal={handleCloseModal} {...props} />
      )}
    </>
  );
}
