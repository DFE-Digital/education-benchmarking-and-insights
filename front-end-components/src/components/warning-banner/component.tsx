import { WarningBannerProps } from "src/components/warning-banner";

export const WarningBanner: React.FC<WarningBannerProps> = (props) => {
  const { isRendered, message } = props;

  return isRendered ? (
    <div className="govuk-inset-text govuk-body govuk-!-margin-0">
      {message}
    </div>
  ) : null;
};
