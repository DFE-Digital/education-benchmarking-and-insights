import { PropsWithChildren } from "react";

export type ModalProps = PropsWithChildren & {
  cancel?: boolean;
  cancelLabel?: string;
  ok?: boolean;
  okLabel?: string;
  onClose: () => void;
  onOK: () => void;
  overlayContentId?: string;
  portalId: string;
  title: string;
};
