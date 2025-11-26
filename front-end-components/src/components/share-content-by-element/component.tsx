import React, { useState } from "react";
import { ShareContentByElementProps } from "./types";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";
import { ShareContent } from "../share-content";
import { useCostCodesContext } from "src/contexts";

export const ShareContentByElement: React.FC<ShareContentByElementProps> = ({
  disabled,
  elementSelector,
  title,
  showTitle,
  costCodes,
  resolveCostCodes,
  ...props
}) => {
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();
  const { categoryCostCodes, label } = useCostCodesContext(title ?? "");

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
    costCodes: resolveCostCodes ? categoryCostCodes : costCodes,
    costCodesLabel: label,
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
