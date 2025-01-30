import { ModalCommonProps } from "../modal";

export type ModalSaveAllImagesProps = ModalCommonProps & {
  buttonLabel: string;
  elementClassName: string;
  elementTitleAttr?: string;
  fileName?: string;
  modalTitle: string;
  saveEventId?: string;
  showProgress?: boolean;
  showTitles?: boolean;
};
