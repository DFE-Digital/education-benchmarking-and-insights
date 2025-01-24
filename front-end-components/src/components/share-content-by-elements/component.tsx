import React, { useEffect, useState } from "react";
import { ShareContentByElementsProps } from "./types";
import { useDownloadPngImages } from "src/hooks/useDownloadImage";
import { ShareContent } from "../share-content";
import { Progress } from "../progress";

export const ShareContentByElements: React.FC<ShareContentByElementsProps> = ({
  disabled,
  elementsSelector,
  fileName,
  showProgress,
  showTitles,
  label,
  onClick,
  ...props
}) => {
  const [imagesLoading, setImagesLoading] = useState<boolean>();
  const [progress, setProgress] = useState<number>();

  const downloadPngs = useDownloadPngImages({
    elementsSelector,
    fileName,
    onImagesLoading: setImagesLoading,
    onProgress: setProgress,
    showTitles,
  });

  useEffect(() => {
    if (!imagesLoading) {
      setProgress(undefined);
    }
  }, [imagesLoading]);

  return (
    <ShareContent
      disabled={imagesLoading || disabled}
      onSaveClick={async () => {
        if (onClick) {
          await onClick();
        }

        await downloadPngs();
      }}
      {...props}
      title={label}
    >
      <>
        {showProgress && progress && (
          <Progress
            className="progress-left"
            percentage={progress}
            width={18}
            height={18}
          />
        )}
        {label}
      </>
    </ShareContent>
  );
};
