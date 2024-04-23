import { WarningBannerProps } from "src/components/warning-banner";

export const WarningBanner: React.FC<WarningBannerProps> = (props) => {
  const { isRendered, message } = props;

  return isRendered ? <div className="govuk-inset-text">{message}</div> : null;
};
