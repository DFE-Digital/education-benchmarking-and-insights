import { useCallback, useEffect, useRef, useState } from "react";
import { ModalSaveAllImagesProps } from ".";
import { Modal } from "../modal";
import { useDownloadPngImages } from "src/hooks/useDownloadImage";
import { Progress } from "src/components/progress";

export function ModalSaveAllImages({
  buttonLabel,
  elementClassName,
  elementTitleAttr,
  fileName,
  modalTitle,
  saveEventId,
  showProgress,
  showTitles,
  ...props
}: ModalSaveAllImagesProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const button = useRef<HTMLButtonElement>(null);
  const [modeChanged, setModeChanged] = useState(false);
  const [imagesLoading, setImagesLoading] = useState<boolean>();
  const [progress, setProgress] = useState<number>();

  const elementsSelector = () => {
    const results = [];
    const elements = document.getElementsByClassName(elementClassName);

    for (let i = 0; i < elements.length; i++) {
      const element = elements[i] as HTMLElement;
      const title = elementTitleAttr
        ? element.getAttribute(elementTitleAttr) || undefined
        : undefined;
      results.push({ element, title });
    }

    return results;
  };

  const downloadPngs = useDownloadPngImages({
    elementsSelector,
    fileName,
    onImagesLoading: setImagesLoading,
    onProgress: setProgress,
    showTitles,
  });

  useEffect(() => {
    if (!imagesLoading) {
      setProgress(undefined);
    }
  }, [imagesLoading]);

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = useCallback(() => {
    if (imagesLoading) {
      // todo: cancel operation if still in progress
      // todo: confirm cancel
      // todo: confirm page leave
    }

    setIsModalOpen(false);
    window.setTimeout(function () {
      button.current?.focus();
    }, 0);
  }, [imagesLoading]);

  const handleDownloadStart = async () => {
    await prepareContent();
    await downloadPngs();
  };

  useEffect(() => {
    if (progress === 100) {
      if (modeChanged) {
        const radio = document.getElementById("mode-table") as HTMLInputElement;
        if (radio) {
          radio.click();
        }
      }

      handleCloseModal();
    }
  }, [handleCloseModal, modeChanged, progress]);

  const prepareContent = async () => {
    const radio = document.getElementById("mode-chart") as HTMLInputElement;
    if (radio && radio.checked === false) {
      setModeChanged(true);
      radio.click();
      await new Promise<void>((resolve) => {
        setTimeout(() => {
          resolve();
        }, 1000);
      });
    }
  };

  return (
    <div>
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        onClick={handleOpenModal}
        ref={button}
      >
        {buttonLabel}
      </button>
      {isModalOpen && (
        <Modal
          cancel
          ok
          okProps={{
            "data-custom-event-chart-name":
              saveEventId && modalTitle ? modalTitle : undefined,
            "data-custom-event-id": saveEventId,
            "data-prevent-double-click": "true",
            disabled: imagesLoading,
            "aria-disabled": imagesLoading,
          }}
          onClose={handleCloseModal}
          onOK={handleDownloadStart}
          title={modalTitle}
          {...props}
        >
          {
            // todo: user feedback
          }
          {showProgress && progress && (
            <Progress percentage={progress} width={64} height={64} />
          )}
        </Modal>
      )}
    </div>
  );
}
