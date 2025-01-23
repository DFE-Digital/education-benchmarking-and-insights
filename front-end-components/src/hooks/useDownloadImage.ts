import saveAs from "file-saver";
import JSZip from "jszip";
import { useCallback } from "react";
import { ImageOptions, ImageService } from "src/services";

export type DownloadPngImageOptions<T> = {
  elementSelector: (ref?: T) => HTMLElement | undefined;
  fileName?: string;
  onImageLoading?: (loading: boolean) => void;
  ref?: React.RefObject<T>;
  showTitle?: boolean;
  title?: string;
} & Pick<ImageOptions, "filter">;

type ElementAndTitle = {
  element: HTMLElement;
  title?: string;
};

export type DownloadPngImagesOptions = {
  elementsSelector: () => ElementAndTitle[];
  onImagesLoading?: (loading: boolean) => void;
  showTitles?: boolean;
} & Pick<ImageOptions, "filter">;

const imageTitleHeight = 50;

export function useDownloadPngImage<T>({
  fileName: fileNameProp,
  elementSelector,
  filter,
  onImageLoading,
  showTitle,
  ref,
  title,
}: DownloadPngImageOptions<T>) {
  const fileName = getFileName(title, fileNameProp);

  const downloadPng = useCallback(async () => {
    const element = elementSelector(ref?.current || undefined);
    if (!element) {
      return;
    }

    const download = async () => {
      const blob = await ImageService.toBlob(
        element,
        getImageOptions(element, title, showTitle, filter)
      );

      if (blob) {
        if (window.saveAs) {
          window.saveAs(blob, fileName);
        } else {
          saveAs.saveAs(blob, fileName);
        }
      }
    };

    if (onImageLoading) {
      onImageLoading(true);

      // If loader event is subscribed, allow it to be triggered before the actual generate and download PNG process
      // due to the latter performing a synchronous XMLHttpRequest on the main thread which is documented as being
      // detrimental effects to the end user's experience (see `Issues` in browser DevTools for more information).
      setTimeout(async () => {
        try {
          await download();
        } catch (err) {
          console.error(`Unable to download image ${fileName}`, err);
        } finally {
          onImageLoading(false);
        }
      }, 100);
    } else {
      await download();
    }
  }, [
    elementSelector,
    fileName,
    filter,
    onImageLoading,
    ref,
    showTitle,
    title,
  ]);

  return downloadPng;
}

export function useDownloadPngImages({
  elementsSelector,
  filter,
  onImagesLoading,
  showTitles,
}: DownloadPngImagesOptions) {
  const fileName = "download.zip";

  const downloadPng = useCallback(async () => {
    const elements = elementsSelector();
    if (!elements?.length) {
      return;
    }

    const download = async () => {
      const zip = new JSZip();

      for (let i = 0; i < elements.length; i++) {
        const { element, title } = elements[i];

        const blob = await ImageService.toBlob(
          element,
          getImageOptions(element, title, showTitles, filter)
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
  }, [elementsSelector, filter, onImagesLoading, showTitles]);

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
      child.classList.add("govuk-heading-s");
      child.style.height = `${imageTitleHeight}px`;
      child.style.margin = `${imageTitleHeight / 2}px 0 -${imageTitleHeight / 2}px ${imageTitleHeight / 2}px`;
      child.style.padding = "0";
      node.insertBefore(child, node.firstChild);
    };
  }

  return {
    cacheBust: true,
    backgroundColor: "#fff",
    type: "image/png",
    filter,
    width: element.clientWidth,
    height,
    onCloned,
  };
};
