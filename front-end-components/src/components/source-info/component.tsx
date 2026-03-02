import { useContext } from "react";
import { SourceInfoContext } from "src/contexts";
import {
  SourceInfoMode,
  SourceInfoModes,
  SourceInfoProps,
} from "src/components/source-info";
import "./styles.scss";

export const SourceInfo: React.FC<SourceInfoProps> = ({
  additionalInfo,
  lineCodes,
}) => {
  const hasLineCodes = lineCodes && lineCodes.length > 0;
  const ctx = useContext(SourceInfoContext);

  if (!ctx || !hasLineCodes) {
    return null;
  }

  const { yearsLabel, glossaryUrl } = ctx;

  const lineLabel = `Line${lineCodes.length > 1 ? "s" : ""} ${lineCodes.join(", ")}`;

  const modeToContent: Record<SourceInfoMode, () => JSX.Element> = {
    [SourceInfoModes.Hospital]: () => (
      <>
        <p className="govuk-body-s govuk-!-margin-bottom-1">Sources:</p>
        <ul className="govuk-list govuk-list--bullet">
          <li className="govuk-body-s" data-test-id="line-code-source">
            Section 251 {yearsLabel}, {lineLabel}
          </li>
          <li className="govuk-body-s">
            Dedicated Schools Grant {yearsLabel}: total hospital education
            deductions
          </li>
        </ul>
      </>
    ),

    [SourceInfoModes.Glossary]: () => (
      <>
        <p className="govuk-body-s govuk-!-margin-bottom-1">Sources:</p>
        <ul className="govuk-list govuk-list--bullet">
          <li className="govuk-body-s" data-test-id="line-code-source">
            Section 251 {yearsLabel}, {lineLabel}
          </li>
          <li className="govuk-body-s">
            Place funding for academies. You can read about how we calculate
            this in the{" "}
            <a
              href={glossaryUrl}
              className="govuk-link govuk-link--no-visited-state"
            >
              glossary
            </a>
            .
          </li>
        </ul>
      </>
    ),

    [SourceInfoModes.Simple]: () => (
      <p className="govuk-body-s" data-test-id="line-code-source">
        Source: Section 251 {yearsLabel}, {lineLabel}
      </p>
    ),
  };

  const mode = additionalInfo ?? SourceInfoModes.Simple;
  const content = modeToContent[mode]();

  return (
    <div className="govuk-!-margin-bottom-6 app-source-info">{content}</div>
  );
};
