import { AriaAttributes, PropsWithChildren } from "react";

export type ModalProps = PropsWithChildren &
  ModalCommonProps &
  AriaAttributes & {
    cancel?: boolean;
    cancelLabel?: string;
    ok?: boolean;
    okLabel?: string;
    okProps?: React.ButtonHTMLAttributes<HTMLButtonElement> &
      Record<string, string | boolean | undefined>;
    onCancel?: () => void;
    onClose: () => void;
    onOK: () => void;
    open?: boolean;
    title: string;
  };

export type ModalCommonProps = {
  overlayContentId?: string;
  portalId: string;
};

export type ModalHandler = {
  focusOKButton: () => void;
};
