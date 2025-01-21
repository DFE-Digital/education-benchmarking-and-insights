import { AccordionSectionProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import {
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import { Section } from "..";

export function AccordionSection<
  TData extends SchoolChartData | TrustChartData,
>({ index, title, ...rest }: AccordionSectionProps<TData>) {
  const elementId = title.toLowerCase().replace(/\W/g, "-");
  const [hash] = useHash();

  return (
    <div
      className={classNames("govuk-accordion__section", {
        "govuk-accordion__section--expanded": hash === `#${elementId}`,
      })}
      id={elementId}
    >
      <div className="govuk-accordion__section-header">
        <h2 className="govuk-accordion__section-heading">
          <span
            className="govuk-accordion__section-button"
            id={`accordion-heading-${index}`}
          >
            {title}
          </span>
        </h2>
      </div>
      <div
        id={`accordion-content-${index}`}
        className="govuk-accordion__section-content"
        aria-labelledby={`accordion-heading-${index}`}
        role="region"
      >
        <Section {...rest} />
      </div>
    </div>
  );
}
