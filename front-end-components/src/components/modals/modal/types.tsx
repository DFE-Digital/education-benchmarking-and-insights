import { PropsWithChildren } from "react";

export type ModalProps = PropsWithChildren &
  ModalCommonProps & {
    cancel?: boolean;
    cancelLabel?: string;
    ok?: boolean;
    okLabel?: string;
    okProps?: React.ButtonHTMLAttributes<HTMLButtonElement> &
      Record<string, string | boolean | undefined>;
    onClose: () => void;
    onOK: () => void;
    title: string;
  };

export type ModalCommonProps = {
  overlayContentId?: string;
  portalId: string;
};
