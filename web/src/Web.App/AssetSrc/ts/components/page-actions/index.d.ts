export interface PageActionsProps {
  all?: boolean;
  buttonLabel?: string;
  costCodesAttr?: string;
  elementClassName: string;
  elementTitleAttr?: string;
  fileName?: string;
  modalTitle: string;
  overlayContentId?: string;
  saveEventId?: string;
  showProgress?: boolean;
  showTitles?: boolean;
}

export interface ElementSelectorProps {
  elements: ElementAndAttributes[];
  showValidationError?: boolean;
}
