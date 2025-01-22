import { ShareContentProps } from "../share-content/types";

export type ShareContentByElementProps = Omit<
  ShareContentProps,
  "onSaveClick"
> & {
  elementSelector: () => HTMLElement | undefined;
};
