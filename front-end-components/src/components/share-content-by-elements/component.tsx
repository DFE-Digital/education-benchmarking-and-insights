import React, { useState } from "react";
import { ShareContentByElementsProps } from "./types";
import { useDownloadPngImages } from "src/hooks/useDownloadImage";
import { ShareContent } from "../share-content";

export const ShareContentByElements: React.FC<ShareContentByElementsProps> = ({
  disabled,
  elementsSelector,
  showTitles,
  label,
  ...props
}) => {
  const [imagesLoading, setImagesLoading] = useState<boolean>();

  const downloadPngs = useDownloadPngImages({
    elementsSelector,
    onImagesLoading: setImagesLoading,
    showTitles,
  });

  return (
    <ShareContent
      disabled={imagesLoading || disabled}
      onSaveClick={async () => await downloadPngs()}
      {...props}
    >
      {label}
    </ShareContent>
  );
};
