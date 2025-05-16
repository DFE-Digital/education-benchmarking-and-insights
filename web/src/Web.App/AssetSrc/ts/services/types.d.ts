import { Options } from "html-to-image/lib/types.js";

type DownloadMode = "save" | "copy";

interface ImageOptions extends Options {
  onCloned?: (node: HTMLElement) => void;
}

interface DownloadOptions extends Pick<ImageOptions, "filter"> {
  costCodes?: string[];
  fileName?: string;
}

interface DownloadPngImageOptions extends DownloadOptions {
  elementSelector: () => HTMLElement | undefined | null;
  mode: DownloadMode;
  onCopied?: (fileName: string) => void;
  onLoading?: (loading: boolean) => void;
  onSaved?: (fileName: string) => void;
  showTitle?: boolean;
  title?: string;
}

interface ElementAndAttributes {
  costCodes?: string[] | undefined;
  element: HTMLElement;
  title?: string;
}

interface DownloadPngImagesOptions extends DownloadOptions {
  elementsSelector: () => ElementAndAttributes[];
  onImagesLoading?: (loading: boolean) => void;
  onProgress?: (percentage: number) => void;
  showTitles?: boolean;
  signal?: AbortSignal;
}

type SaveImageToBrowserProps = {
  triggerElement: HTMLButtonElement;
} & Omit<DownloadPngImageOptions, "mode" | "onCopied" | "onSaved">;

type CopyImageToClipboardProps = {
  triggerElement: HTMLButtonElement;
} & Omit<DownloadPngImageOptions, "mode" | "onCopied" | "onSaved">;
