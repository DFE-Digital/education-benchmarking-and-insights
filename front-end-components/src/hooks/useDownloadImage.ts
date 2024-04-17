import saveAs from "file-saver";
import { useEffect, useCallback } from "react";
import { useCurrentPng } from "recharts-to-png";

export function useDownloadPngImage({
  fileName,
  onImageLoading,
}: {
  fileName: string;
  onImageLoading?: (loading: boolean) => void;
}) {
  const [getPng, { ref, isLoading }] = useCurrentPng({
    backgroundColor: "#fff",
  });

  useEffect(() => {
    onImageLoading && onImageLoading(isLoading);
  }, [isLoading, onImageLoading]);

  const downloadPng = useCallback(async () => {
    const download = async () => {
      const png = await getPng();
      if (png) {
        saveAs(png, fileName);
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
        } finally {
          onImageLoading(false);
        }
      }, 100);
    } else {
      await download();
    }
  }, [getPng, fileName, onImageLoading]);

  return { downloadPng, ref };
}
