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
import type { ImageOptions } from "./types";

// below merged in from https://github.com/bubkoo/html-to-image/blob/master/src/index.ts
// with the addition of the onCloned() callback as added to the extended `Options` object
export class ImageService {
  static async toSvg<T extends HTMLElement>(node: T, options: ImageOptions = {}): Promise<string> {
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

  static async toCanvas<T extends HTMLElement>(
    node: T,
    options: ImageOptions = {}
  ): Promise<HTMLCanvasElement> {
    const { width, height } = getImageSize(node, options);
    const svg = await ImageService.toSvg(node, options);
    const img = await createImage(svg);

    const canvas = document.createElement("canvas");
    const context = canvas.getContext("2d")!;
    const ratio = options.pixelRatio ?? getPixelRatio();
    const canvasWidth = options.canvasWidth ?? width;
    const canvasHeight = options.canvasHeight ?? height;

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

  static async toBlob<T extends HTMLElement>(
    node: T,
    options: ImageOptions = {}
  ): Promise<Blob | null> {
    const canvas = await ImageService.toCanvas(node, options);
    const blob = await canvasToBlob(canvas);
    return blob;
  }
}
