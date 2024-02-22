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
    const png = await getPng();

    if (png) {
      saveAs(png, fileName);
    }
  }, [getPng, fileName]);

  return { downloadPng, ref };
}
