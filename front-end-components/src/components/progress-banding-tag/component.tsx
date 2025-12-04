import { ProgressBanding, ProgressBandingTagProps } from "./types";

export const ProgressBandingTag: React.FC<ProgressBandingTagProps> = ({
  banding,
}: ProgressBandingTagProps) => {
  switch (banding) {
    case ProgressBanding.AboveAverage:
      return (
        <strong className="govuk-tag govuk-tag--light-blue govuk-!-font-size-14">
          Above average
        </strong>
      );
    case ProgressBanding.WellAboveAverage:
      return (
        <strong className="govuk-tag govuk-tag--turquoise govuk-!-font-size-14">
          Well above average
        </strong>
      );
  }

  return null;
};
