export interface ModalDialogProps {
  ariaBusy?: boolean;
  ariaDescribedBy?: string;
  ariaLive?: "polite" | "off" | "assertive";
  cancel?: boolean;
  cancelLabel?: string;
  ok?: boolean;
  okDisabled?: boolean;
  okLabel?: string;
  overlayContentId?: string;
  title: string;
}
