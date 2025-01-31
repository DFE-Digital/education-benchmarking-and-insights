import { ModalSaveImagesButtonProps } from "./modal-save-images-button";
import { ModalSaveImagesModalProps } from "./modal-save-images-modal";

export type ModalSaveImagesProps = Pick<
  ModalSaveImagesButtonProps,
  "buttonLabel"
> &
  Omit<ModalSaveImagesModalProps, "onCloseModal"> & {};
