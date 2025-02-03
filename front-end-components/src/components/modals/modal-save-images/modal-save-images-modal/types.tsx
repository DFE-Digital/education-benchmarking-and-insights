import { ModalCommonProps } from "../../modal";

export type ModalSaveImagesModalProps = ModalCommonProps & {
  all?: boolean;
  elementClassName: string;
  elementTitleAttr?: string;
  fileName?: string;
  modalTitle: string;
  onCloseModal: () => void;
  saveEventId?: string;
  showProgress?: boolean;
  showTitles?: boolean;
  waitForEventType?: string;
};
