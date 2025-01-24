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
  title?: string;
} & Pick<ImageOptions, "filter">;

type ElementAndTitle = {
  element: HTMLElement;
  title?: string;
};

export type DownloadPngImagesOptions = {
  fileName?: string;
  elementsSelector: () => ElementAndTitle[];
  onImagesLoading?: (loading: boolean) => void;
  onProgress?: (percentage: number) => void;
  showTitles?: boolean;
} & Pick<ImageOptions, "filter">;

const imageTitleHeight = 50;
const type = "image/png";
const backgroundColor = "#fff";

export function useDownloadPngImage<T>({
  elementSelector,
  fileName: fileNameProp,
  filter,
  onCopied,
  onLoading,
  onSaved,
  showTitle,
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
          getImageOptions(element, type, title, showTitle, filter)
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
        const { element, title } = elements[i];
        if (onProgress) {
          onProgress(((i + 1) / elements.length) * 100);
        }

        const blob = await ImageService.toBlob(
          element,
          getImageOptions(element, type, title, showTitles, filter)
        );

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
          console.error(`Unable to download images`, err);
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
  filter?: (domNode: HTMLElement) => boolean
): ImageOptions => {
  let height = element.clientHeight;

  // use customised onCloned() callback to add additional elements to the DOM once already
  // cloned for the purpose of generating the image (so as to not affect the original DOM,
  // but still be able to apply styles and fonts as resolved from class names).
  let onCloned: ((node: HTMLElement) => void) | undefined;
  if (title && showTitle) {
    height += imageTitleHeight;
    onCloned = (node) => {
      const child = document.createElement("h2");
      child.innerText = title;
      child.style.fontFamily = '"GDS Transport", arial, sans-serif';
      child.style.fontSize = "1.1875rem";
      child.style.fontWeight = "700";
      child.style.height = `${imageTitleHeight}px`;
      child.style.height = `${imageTitleHeight}px`;
      child.style.margin = `${imageTitleHeight / 2}px 0 -${imageTitleHeight / 2}px ${imageTitleHeight / 4}px`;
      child.style.padding = "0";
      node.insertBefore(child, node.firstChild);
    };
  }

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
