import { ErrorBannerProps } from "src/components/error-banner";

const hasText = (msg?: string | null) => !!msg?.trim();

export const ErrorBanner: React.FC<ErrorBannerProps> = (props) => {
  const { isRendered, message } = props;

  return isRendered && hasText(message) ? (
    <div className="govuk-warning-text">
      <span className="govuk-warning-text__icon" aria-hidden="true">
        !
      </span>
      <strong className="govuk-warning-text__text">
        <span className="govuk-visually-hidden">Warning</span>
        {message}
      </strong>
    </div>
  ) : null;
};
