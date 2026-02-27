import { useContext } from "react";
import { SourceInfoContext } from "src/contexts";
import { SourceInfoProps } from "src/components/source-info";
import "./styles.scss";

export const SourceInfo: React.FC<SourceInfoProps> = ({
  recoupment,
  lineCodes,
}) => {
  const hasLineCodes = lineCodes && lineCodes.length > 0;
  const ctx = useContext(SourceInfoContext);

  if (!ctx || !hasLineCodes) {
    return null;
  }

  const { yearsLabel, glossaryUrl } = ctx;

  const lineLabel = `Line${lineCodes.length > 1 ? "s" : ""} ${lineCodes.join(", ")}`;

  return (
    <div className="govuk-!-margin-bottom-6 app-source-info">
      {recoupment ? (
        <>
          <p className="govuk-body-s govuk-!-margin-bottom-1">Sources:</p>
          <ul className="govuk-list govuk-list--bullet">
            <li className="govuk-body-s" data-test-id="line-code-source">
              {yearsLabel}, {lineLabel}
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
            </li>
          </ul>
        </>
      ) : (
        <p className="govuk-body-s" data-test-id="line-code-source">
          Source: {yearsLabel}, {lineLabel}
        </p>
      )}
    </div>
  );
};
