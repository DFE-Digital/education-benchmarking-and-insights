import { MouseEventHandler } from "react";
import { DownloadPngImageOptions } from "src/hooks/useDownloadImage";

export type ShareContentProps = Pick<
  DownloadPngImageOptions<HTMLElement>,
  "showTitle" | "title"
> & {
  disabled?: boolean;
  onSaveClick?: MouseEventHandler<HTMLButtonElement> | undefined;
  saveEventId?: string;
};
