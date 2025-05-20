import classNames from "classnames";
import { DownloadService } from "../../services/index";

// todo: migrate to Vue.js component (#261747)
export function ChartActions({
  copyEventId,
  costCodes,
  elementId,
  placeholderElement,
  saveEventId,
  showCopy,
  showSave,
  showTitle,
  title,
}: ChartActionsProps): void {
  const classes = "govuk-button govuk-button--secondary share-button";

  if (placeholderElement) {
    const buttonsElement = document.createElement("div");
    placeholderElement.parentNode?.insertBefore(buttonsElement, placeholderElement);
    buttonsElement.className = "share-buttons";
    const elementSelector = () => document.getElementById(elementId);

    if (showSave) {
      const saveButton = document.createElement("button");
      buttonsElement.appendChild(saveButton);

      saveButton.className = classNames(classes, "share-button--save");
      saveButton.setAttribute("data-module", "govuk-button");
      saveButton.setAttribute("data-prevent-double-click", "true");
      saveButton.setAttribute("data-custom-event-chart-name", (saveEventId && title) ?? "");
      saveButton.setAttribute("data-custom-event-id", saveEventId ?? "");
      saveButton.addEventListener("click", (ev: MouseEvent) => {
        DownloadService.saveImageToBrowser({
          costCodes,
          elementSelector,
          showTitle,
          title,
          triggerElement: ev.target as HTMLButtonElement,
        });
      });
      saveButton.innerHTML = title
        ? `Save <span class="govuk-visually-hidden">${title}</span> as image`
        : "Save as image";
    }

    if (showCopy) {
      const copyButton = document.createElement("button");
      buttonsElement.appendChild(copyButton);

      copyButton.className = classNames(classes, "share-button--copy");
      copyButton.setAttribute("data-module", "govuk-button");
      copyButton.setAttribute("data-prevent-double-click", "true");
      copyButton.setAttribute("data-custom-event-chart-name", (copyEventId && title) ?? "");
      copyButton.setAttribute("data-custom-event-id", copyEventId ?? "");
      copyButton.addEventListener("click", (ev: MouseEvent) => {
        DownloadService.copyImageToClipboard({
          costCodes,
          elementSelector,
          showTitle,
          title,
          triggerElement: ev.target as HTMLButtonElement,
        });
      });
      copyButton.innerHTML = title
        ? `Copy <span class="govuk-visually-hidden">${title}</span> image`
        : "Copy image";
    }

    placeholderElement.remove();
  }
}
