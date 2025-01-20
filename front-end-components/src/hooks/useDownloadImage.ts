import saveAs from "file-saver";
import { useCallback } from "react";
import { toBlob } from "html-to-image";
import { Options } from "html-to-image/lib/types";

type DownloadPngImageOptions<T> = {
  ref?: React.RefObject<T>;
  fileName: string;
  onImageLoading?: (loading: boolean) => void;
  elementSelector: (ref: T) => HTMLElement | undefined;
} & Pick<Options, "filter">;

export function useDownloadPngImage<T>({
  ref,
  fileName,
  onImageLoading,
  elementSelector,
  filter,
}: DownloadPngImageOptions<T>) {
  const downloadPng = useCallback(async () => {
    if (!ref?.current) {
      return;
    }

    const element = elementSelector(ref.current);
    if (!element) {
      return;
    }

    const download = async () => {
      const blob = await toBlob(element, {
        cacheBust: true,
        backgroundColor: "#fff",
        type: "image/png",
        filter,
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
  }, [ref, fileName, onImageLoading, elementSelector, filter]);

  return downloadPng;
}
