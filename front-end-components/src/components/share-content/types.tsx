import { MouseEventHandler } from "react";
import { DownloadPngImageOptions } from "src/hooks/useDownloadImage";

export type ShareContentProps = Pick<
  DownloadPngImageOptions<HTMLElement>,
  "showTitle" | "title"
> & {
  copied?: boolean;
  disabled?: boolean;
  hideCopy?: boolean;
  onCopyClick?: MouseEventHandler<HTMLButtonElement> | undefined;
  onSaveClick?: MouseEventHandler<HTMLButtonElement> | undefined;
  copyEventId?: string;
  saveEventId?: string;
};
