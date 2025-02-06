import React, { useState } from "react";
import { ShareContentByElementProps } from "./types";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";
import { ShareContent } from "../share-content";

export const ShareContentByElement: React.FC<ShareContentByElementProps> = ({
  disabled,
  elementSelector,
  title,
  showTitle,
  costCodes,
  ...props
}) => {
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();

  const handleImageCopied = () => {
    setImageCopied(true);
    setTimeout(() => {
      setImageCopied(false);
    }, 2000);
  };

  const downloadPng = useDownloadPngImage({
    elementSelector,
    onCopied: handleImageCopied,
    onLoading: setImageLoading,
    title,
    showTitle,
    costCodes,
  });

  return (
    <ShareContent
      copied={imageCopied}
      disabled={imageLoading || disabled}
      onCopyClick={async () => await downloadPng("copy")}
      onSaveClick={async () => await downloadPng("save")}
      title={title}
      {...props}
    />
  );
};
