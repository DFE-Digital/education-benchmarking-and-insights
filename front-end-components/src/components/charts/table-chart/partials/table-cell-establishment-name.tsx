import classNames from "classnames";
import { PartYearDataWarning } from "../../part-year-data-warning";
import { SchoolChartData, TrustChartData, LaChartData } from "../types";
import {
  AnchorProps,
  SelectedAnchorProps,
  TableCellEstablishmentNameProps,
} from "./types";
import { useContext } from "react";
import { SelectedEstablishmentContext } from "src/contexts";

export function TableCellEstablishmentName<
  TData extends SchoolChartData | TrustChartData | LaChartData,
>({
  localAuthority,
  row,
  trust,
  ...props
}: TableCellEstablishmentNameProps<TData>) {
  const { periodCoveredByReturn, schoolName, urn } = row as SchoolChartData;
  const { companyNumber, trustName } = row as TrustChartData;
  const { laCode, laName } = row as LaChartData;

  return (
    <td
      className={classNames("govuk-table__cell", {
        "table-cell-warning":
          periodCoveredByReturn !== undefined && periodCoveredByReturn < 12,
        "govuk-!-width-one-third": !!localAuthority,
      })}
    >
      <SelectedAnchor
        identifier={localAuthority ? laCode : trust ? companyNumber : urn}
        label={localAuthority ? laName : trust ? trustName : schoolName}
        localAuthority={localAuthority}
        trust={trust}
        {...props}
      />
      {periodCoveredByReturn !== undefined && periodCoveredByReturn < 12 && (
        <PartYearDataWarning periodCoveredByReturn={periodCoveredByReturn} />
      )}
    </td>
  );
}

const SelectedAnchor = ({
  identifier,
  linkToEstablishment,
  localAuthority,
  trust,
  ...props
}: SelectedAnchorProps) => {
  const selectedEstablishment = useContext(SelectedEstablishmentContext);

  const href = localAuthority
    ? `/local-authority/${identifier}`
    : trust
      ? `/trust/${identifier}`
      : `/school/${identifier}`;

  return (
    <>
      {selectedEstablishment == identifier ? (
        <strong>
          {linkToEstablishment ? (
            <Anchor href={href} {...props} />
          ) : (
            props.label
          )}
        </strong>
      ) : linkToEstablishment ? (
        <Anchor href={href} {...props} />
      ) : (
        props.label
      )}
    </>
  );
};

const Anchor = ({ href, label, preventFocus }: AnchorProps) => {
  return (
    <a
      className="govuk-link govuk-link--no-visited-state"
      href={href}
      tabIndex={preventFocus ? -1 : undefined}
    >
      {label}
    </a>
  );
};
