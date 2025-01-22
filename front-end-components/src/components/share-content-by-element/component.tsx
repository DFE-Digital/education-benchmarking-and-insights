import React, { useState } from "react";
import { ShareContentByElementProps } from "./types";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";
import { ShareContent } from "../share-content";

export const ShareContentByElement: React.FC<ShareContentByElementProps> = ({
  disabled,
  elementSelector,
  title,
  showTitle,
  ...props
}) => {
  const [imageLoading, setImageLoading] = useState<boolean>();
  const downloadPng = useDownloadPngImage({
    elementSelector,
    onImageLoading: setImageLoading,
    title,
    showTitle,
  });

  return (
    <ShareContent
      disabled={imageLoading || disabled}
      onSaveClick={async () => await downloadPng()}
      title={title}
      {...props}
    />
  );
};
