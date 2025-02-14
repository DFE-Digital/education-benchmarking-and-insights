import saveAs from "file-saver";
import JSZip from "jszip";
import { useCallback } from "react";
import { DownloadMode, ImageOptions, ImageService } from "src/services";

export type DownloadPngImageOptions<T> = {
  elementSelector: (ref?: T) => HTMLElement | undefined;
  fileName?: string;
  onCopied?: (fileName: string) => void;
  onLoading?: (loading: boolean) => void;
  onSaved?: (fileName: string) => void;
  ref?: React.RefObject<T>;
  showTitle?: boolean;
  costCodes?: string[];
  title?: string;
} & Pick<ImageOptions, "filter">;

export type ElementAndAttributes = {
  element: HTMLElement;
  title?: string;
  costCodes?: string[] | undefined;
};

export type DownloadPngImagesOptions = {
  fileName?: string;
  elementsSelector: () => ElementAndAttributes[];
  onImagesLoading?: (loading: boolean) => void;
  onProgress?: (percentage: number) => void;
  showTitles?: boolean;
  costCodes?: string[];
  signal?: AbortSignal;
} & Pick<ImageOptions, "filter">;

const imageTitleHeight = 50;
const type = "image/png";
const backgroundColor = "#fff";
const costCodesListHeight = 50;

export function useDownloadPngImage<T>({
  elementSelector,
  fileName: fileNameProp,
  filter,
  onCopied,
  onLoading,
  onSaved,
  showTitle,
  costCodes,
  ref,
  title,
}: DownloadPngImageOptions<T>) {
  const fileName = getFileName(title, fileNameProp);

  const downloadPng = useCallback(
    async (mode: DownloadMode) => {
      const element = elementSelector(ref?.current || undefined);
      if (!element) {
        return;
      }

      const getBlob = async () => {
        return await ImageService.toBlob(
          element,
          getImageOptions(element, type, title, showTitle, costCodes, filter)
        );
      };

      const download = async () => {
        const blob = await getBlob();
        if (blob) {
          if (window.saveAs) {
            window.saveAs(blob, fileName);
          } else {
            saveAs.saveAs(blob, fileName);
          }

          if (onSaved) {
            onSaved(fileName);
          }
        }
      };

      const copy = async () => {
        const blob = await getBlob();
        if (blob) {
          const data = [new ClipboardItem({ [type]: blob })];
          await navigator.clipboard.write(data);
          if (onCopied) {
            onCopied(fileName);
          }
        }
      };

      if (onLoading) {
        onLoading(true);

        // If loader event is subscribed, allow it to be triggered before the actual generate and download PNG process
        // due to the latter performing a synchronous XMLHttpRequest on the main thread which is documented as being
        // detrimental effects to the end user's experience (see `Issues` in browser DevTools for more information).
        setTimeout(async () => {
          try {
            if (mode === "copy") {
              await copy();
            } else {
              await download();
            }
          } catch (err) {
            console.error(`Unable to download image ${fileName}`, err);
          } finally {
            onLoading(false);
          }
        }, 100);
      } else {
        if (mode === "copy") {
          await copy();
        } else {
          await download();
        }
      }
    },
    [
      elementSelector,
      fileName,
      filter,
      onCopied,
      onLoading,
      onSaved,
      ref,
      showTitle,
      title,
      costCodes,
    ]
  );

  return downloadPng;
}

export function useDownloadPngImages({
  elementsSelector,
  fileName: fileNameProp,
  filter,
  onImagesLoading,
  onProgress,
  showTitles,
  signal,
}: DownloadPngImagesOptions) {
  const fileName = fileNameProp ?? "download.zip";

  const downloadPng = useCallback(async () => {
    const elements = elementsSelector();
    if (!elements?.length) {
      return;
    }

    const download = async () => {
      const zip = new JSZip();

      for (let i = 0; i < elements.length; i++) {
        const { element, title, costCodes } = elements[i];
        if (onProgress) {
          onProgress(((i + 1) / elements.length) * 100);
        }

        const blob = await new Promise<Blob | null>((resolve, reject) => {
          signal?.addEventListener("abort", () => {
            reject("Download aborted at user request");
          });
          ImageService.toBlob(
            element,
            getImageOptions(element, type, title, showTitles, costCodes, filter)
          ).then(resolve);
        });

        if (blob) {
          zip.file(
            getFileName(title ? title : `${element.tagName}-${i}`),
            blob
          );
        }
      }

      const blob = await zip.generateAsync({ type: "blob" });
      if (blob) {
        if (window.saveAs) {
          window.saveAs(blob, fileName);
        } else {
          saveAs.saveAs(blob, fileName);
        }
      }
    };

    if (onImagesLoading) {
      onImagesLoading(true);

      // If loader event is subscribed, allow it to be triggered before the actual generate and download PNG process
      // due to the latter performing a synchronous XMLHttpRequest on the main thread which is documented as being
      // detrimental effects to the end user's experience (see `Issues` in browser DevTools for more information).
      setTimeout(async () => {
        try {
          await download();
        } catch (err) {
          const message = "Unable to download images";
          if (signal?.aborted) {
            console.warn(message, err);
          } else {
            console.error(message, err);
          }
        } finally {
          onImagesLoading(false);
        }
      }, 100);
    } else {
      await download();
    }
  }, [
    elementsSelector,
    fileName,
    filter,
    onImagesLoading,
    onProgress,
    showTitles,
    signal,
  ]);

  return downloadPng;
}

const getFileName = (title?: string, fileName?: string) => {
  return title
    ? `${title
        .toLowerCase()
        .replace(/\W/g, " ")
        .replace(/\s{2,}/g, " ")
        .trim()
        .replace(/\s/g, "-")}.png`
    : fileName
      ? fileName
      : "download.png";
};

const getImageOptions = (
  element: HTMLElement,
  type: string,
  title?: string,
  showTitle?: boolean,
  costCodes?: string[] | undefined,
  filter?: (domNode: HTMLElement) => boolean
): ImageOptions => {
  let height = element.clientHeight;

  // use customised onCloned() callback to add additional elements to the DOM once already
  // cloned for the purpose of generating the image (so as to not affect the original DOM,
  // but still be able to apply styles and fonts as resolved from class names).

  if (title && showTitle) {
    height += imageTitleHeight;
  }
  if (costCodes) {
    height += costCodesListHeight;
  }

  const onCloned = (node: HTMLElement): void => {
    if (costCodes) {
      node.insertBefore(createCostCodeList(costCodes), node.firstChild);
    }
    if (title && showTitle) {
      node.insertBefore(createTitleElement(title), node.firstChild);
    }
  };

  return {
    cacheBust: true,
    backgroundColor,
    type,
    filter,
    width: element.clientWidth,
    height,
    onCloned,
  };
};

function createCostCodeList(costCodes: string[]): HTMLElement {
  const costCodeList = document.createElement("ul") as HTMLElement;
  costCodeList.style.display = "flex";
  costCodeList.style.flexWrap = "wrap";
  costCodeList.style.gap = "10px";
  costCodeList.style.listStyle = "none";
  costCodeList.style.padding = "0px";

  costCodes.forEach((costCode) => {
    const li = document.createElement("li");
    li.innerText = costCode;
    li.style.fontFamily = '"GDS Transport", arial, sans-serif';
    li.style.fontSize = "1rem";
    li.style.display = "inline-block";
    li.style.color = "rgb(12, 45, 74)";
    li.style.backgroundColor = "rgb(187, 212, 234)";
    li.style.overflowWrap = "break-word";
    li.style.padding = "2px 8px 3px 8px";

    costCodeList.appendChild(li);
  });

  return costCodeList;
}

function createTitleElement(title: string): HTMLElement {
  const child = document.createElement("h2");
  child.innerText = title;
  child.style.fontFamily = '"GDS Transport", arial, sans-serif';
  child.style.fontSize = "1.25rem";
  child.style.fontWeight = "700";
  child.style.height = `${imageTitleHeight}px`;
  child.style.height = `${imageTitleHeight}px`;
  child.style.margin = `${imageTitleHeight / 4}px 0 -${imageTitleHeight / 4}px 0`;
  child.style.padding = "0";

  return child;
}
