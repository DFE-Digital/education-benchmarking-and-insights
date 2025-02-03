import { ElementAndTitle } from "src/hooks/useDownloadImage";

export type ElementSelectorProps = {
  elements: ElementAndTitle[];
  onChange: (selected: ElementAndTitle[]) => void;
  selected: ElementAndTitle[];
  showValidationError?: boolean;
};
