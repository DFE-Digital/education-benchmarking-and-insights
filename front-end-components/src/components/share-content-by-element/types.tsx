import { ShareContentProps } from "../share-content/types";

export type ShareContentByElementProps = Omit<
  ShareContentProps,
  "onSaveClick" | "onCopyClick"
> & {
  elementSelector: () => HTMLElement | undefined;
};
