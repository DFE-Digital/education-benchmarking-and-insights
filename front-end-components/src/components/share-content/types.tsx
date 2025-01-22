import { MouseEventHandler } from "react";

export type ShareContentProps = {
  disabled?: boolean;
  onSaveClick?: MouseEventHandler<HTMLButtonElement> | undefined;
  saveEventId?: string;
  title: string;
};
