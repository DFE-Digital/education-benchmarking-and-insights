import { ElementAndAttributes } from "src/hooks/useDownloadImage";

export type ElementSelectorProps = {
  elements: ElementAndAttributes[];
  onChange: (selected: ElementAndAttributes[]) => void;
  selected: ElementAndAttributes[];
  showValidationError?: boolean;
};
