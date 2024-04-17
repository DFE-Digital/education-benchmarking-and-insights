import { WarningBannerProps } from "src/components/warning-banner";

export const WarningBanner: React.FC<WarningBannerProps> = (props) => {
  const { icon, visuallyHiddenText, message } = props;

  return (
    <div className="govuk-warning-text">
      <span className="govuk-warning-text__icon" aria-hidden="true">
        {icon}
      </span>
      <strong className="govuk-warning-text__text">
        <span className="govuk-visually-hidden">{visuallyHiddenText}</span>
        {message}
      </strong>
    </div>
  );
};
