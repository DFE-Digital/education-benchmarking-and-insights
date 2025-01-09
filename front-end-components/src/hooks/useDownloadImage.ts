import saveAs from "file-saver";
import { useCallback } from "react";
import { applyStyle } from "html-to-image/lib/apply-style";
import { cloneNode } from "html-to-image/lib/clone-node";
import { embedImages } from "html-to-image/lib/embed-images";
import { embedWebFonts } from "html-to-image/lib/embed-webfonts";
import {
  getImageSize,
  getPixelRatio,
  createImage,
  canvasToBlob,
  nodeToDataURL,
  checkCanvasDimensions,
} from "html-to-image/lib/util";
import { Options } from "html-to-image/lib/types";

type DownloadPngImageOptions<T> = {
  ref?: React.RefObject<T>;
  fileName: string;
  onImageLoading?: (loading: boolean) => void;
  elementSelector: (ref: T) => HTMLElement | undefined;
  title?: string;
} & Pick<Options, "filter">;

const imageTitleHeight = 50;

export function useDownloadPngImage<T>({
  ref,
  fileName,
  onImageLoading,
  elementSelector,
  filter,
  title,
}: DownloadPngImageOptions<T>) {
  const downloadPng = useCallback(async () => {
    if (!ref?.current) {
      return;
    }

    const element = elementSelector(ref.current);
    if (!element) {
      return;
    }

    const width = element.clientWidth;
    const height = element.clientHeight + (title ? imageTitleHeight : 0);
    let onCloned: (node: HTMLElement) => void | undefined;
    if (title) {
      onCloned = (node) => {
        const child = document.createElement("h1");
        child.innerText = title;
        child.style.height = `${imageTitleHeight}px`;
        child.style.margin = `${imageTitleHeight / 2}px 0 -${imageTitleHeight / 2}px ${imageTitleHeight / 2}px`;
        child.style.padding = "0";
        node.insertBefore(child, node.firstChild);
      };
    }

    const download = async () => {
      const blob = await toBlob(element, {
        cacheBust: true,
        backgroundColor: "#fff",
        type: "image/png",
        filter,
        width,
        height,
        onCloned,
      });

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
  }, [ref, fileName, onImageLoading, elementSelector, filter, title]);

  return downloadPng;
}

type HtmlToImageOptions = Options & {
  onCloned?: (node: HTMLElement) => void;
};

// below merged in from https://github.com/bubkoo/html-to-image/blob/master/src/index.ts
// with the addition of the onCloned() callback as added to the extended `Options` object
async function toSvg<T extends HTMLElement>(
  node: T,
  options: HtmlToImageOptions = {}
): Promise<string> {
  const { width, height } = getImageSize(node, options);
  const clonedNode = (await cloneNode(node, options, true)) as HTMLElement;
  if (options.onCloned) {
    options.onCloned(clonedNode);
  }

  await embedWebFonts(clonedNode, options);
  await embedImages(clonedNode, options);
  applyStyle(clonedNode, options);
  const datauri = await nodeToDataURL(clonedNode, width, height);
  return datauri;
}

async function toCanvas<T extends HTMLElement>(
  node: T,
  options: HtmlToImageOptions = {}
): Promise<HTMLCanvasElement> {
  const { width, height } = getImageSize(node, options);
  const svg = await toSvg(node, options);
  const img = await createImage(svg);

  const canvas = document.createElement("canvas");
  const context = canvas.getContext("2d")!;
  const ratio = options.pixelRatio || getPixelRatio();
  const canvasWidth = options.canvasWidth || width;
  const canvasHeight = options.canvasHeight || height;

  canvas.width = canvasWidth * ratio;
  canvas.height = canvasHeight * ratio;

  if (!options.skipAutoScale) {
    checkCanvasDimensions(canvas);
  }
  canvas.style.width = `${canvasWidth}`;
  canvas.style.height = `${canvasHeight}`;

  if (options.backgroundColor) {
    context.fillStyle = options.backgroundColor;
    context.fillRect(0, 0, canvas.width, canvas.height);
  }

  context.drawImage(img, 0, 0, canvas.width, canvas.height);

  return canvas;
}

async function toBlob<T extends HTMLElement>(
  node: T,
  options: HtmlToImageOptions = {}
): Promise<Blob | null> {
  const canvas = await toCanvas(node, options);
  const blob = await canvasToBlob(canvas);
  return blob;
}
