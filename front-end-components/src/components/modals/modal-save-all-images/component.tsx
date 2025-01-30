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
  const [ariaStatus, setAriaStatus] = useState<number>();
  const [cancelSignal, setCancelSignal] = useState<AbortController>(
    new AbortController()
  );

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

  const handleProgress = (percentage?: number) => {
    setProgress(percentage);
  };

  const downloadPngs = useDownloadPngImages({
    elementsSelector,
    fileName,
    onImagesLoading: setImagesLoading,
    onProgress: handleProgress,
    showTitles,
    signal: cancelSignal.signal,
  });

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = useCallback(
    (abort: boolean) => {
      if (imagesLoading && abort) {
        cancelSignal.abort();
        setCancelSignal(new AbortController());
        // todo: confirm cancel operation
        // todo: confirm page leave
      }

      setProgress(undefined);
      setIsModalOpen(false);
      window.setTimeout(function () {
        button.current?.focus();
      }, 0);
    },
    [cancelSignal, imagesLoading]
  );

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

      setTimeout(() => {
        handleCloseModal(false);
      }, 2500);
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

  useEffect(() => {
    if (!progress || progress < 24) {
      setAriaStatus(0);
    } else if (progress < 49) {
      setAriaStatus(25);
    } else if (progress < 74) {
      setAriaStatus(50);
    } else if (progress < 99) {
      setAriaStatus(75);
    } else {
      setAriaStatus(100);
    }
  }, [progress, setAriaStatus]);

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
      <Modal
        cancel
        cancelLabel={progress === 100 ? "Close" : "Cancel"}
        ok
        okLabel="Start"
        okProps={{
          "data-custom-event-chart-name":
            saveEventId && modalTitle ? modalTitle : undefined,
          "data-custom-event-id": saveEventId,
          "data-prevent-double-click": "true",
          disabled: imagesLoading || progress === 100,
          "aria-disabled": imagesLoading || progress === 100,
        }}
        onClose={() => handleCloseModal(true)}
        onOK={handleDownloadStart}
        open={isModalOpen}
        title={modalTitle}
        aria-busy={!!progress}
        aria-describedby="save-progress"
        aria-live="polite"
        {...props}
      >
        <div className="govuk-body">
          {
            /* todo: copy review */
            progress
              ? progress === 100
                ? "File generation has completed."
                : "Please wait while the images are generated."
              : "Select 'Start' to begin the image generation process. This may take a few minutes to complete."
          }
        </div>

        {showProgress && progress && (
          <>
            <div
              className="govuk-visually-hidden"
              role="status"
              id="save-progress"
              children={
                progress === 100
                  ? "File generation has completed."
                  : `${ariaStatus}% complete`
              }
            />
            {progress !== 100 && (
              <Progress percentage={progress} width={100} height={100} />
            )}
          </>
        )}
      </Modal>
    </div>
  );
}
