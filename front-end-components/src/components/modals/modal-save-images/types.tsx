import { ModalCommonProps } from "../modal";

export type ModalSaveImagesProps = ModalCommonProps & {
  all?: boolean;
  buttonLabel: string;
  elementClassName: string;
  elementTitleAttr?: string;
  fileName?: string;
  modalTitle: string;
  saveEventId?: string;
  showProgress?: boolean;
  showTitles?: boolean;
};
