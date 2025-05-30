import { MouseEventHandler } from "react";
import { DownloadPngImageOptions } from "src/hooks/useDownloadImage";

export type ShareContentProps = Pick<
  DownloadPngImageOptions<HTMLElement>,
  "showTitle" | "title"
> & {
  copied?: boolean;
  copiedLabel?: string;
  disabled?: boolean;
  onCopyClick?: MouseEventHandler<HTMLButtonElement> | undefined;
  onSaveClick?: MouseEventHandler<HTMLButtonElement> | undefined;
  copyEventId?: string;
  saveEventId?: string;
  showCopy?: boolean;
  showSave?: boolean;
  costCodes?: string[];
};
