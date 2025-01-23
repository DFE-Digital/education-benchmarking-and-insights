import { ShareContentByElementProps } from "../share-content-by-element";

type ElementAndTitle = {
  element: HTMLElement;
  title?: string;
};

export type ShareContentByElementsProps = Omit<
  ShareContentByElementProps,
  "elementSelector" | "title" | "showTitle"
> & {
  elementsSelector: () => ElementAndTitle[];
  fileName?: string;
  label: string;
  onClick: () => Promise<void>;
  showProgress?: boolean;
  showTitles?: boolean;
};
