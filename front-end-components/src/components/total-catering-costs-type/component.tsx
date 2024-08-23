import React from "react";
import { TotalCateringCostsTypeProps } from "src/components/total-catering-costs-type";
import { TotalCateringCostsField } from "src/services";

export const TotalCateringCostsType: React.FC<TotalCateringCostsTypeProps> = (
  props
) => {
  const { field, onChange, prefix } = props;

  return (
    <div className="govuk-form-group">
      <fieldset className="govuk-fieldset">
        <legend className="govuk-fieldset__legend govuk-!-margin-bottom-1">
          <h2 className="govuk-fieldset__heading">Display income cost as</h2>
        </legend>
        <div
          className="govuk-radios govuk-radios--small govuk-radios--inline"
          data-module="govuk-radios"
        >
          <div className="govuk-radios__item">
            <input
              className="govuk-radios__input"
              id={prefix ? `${prefix}-type-gross` : "type-gross"}
              name={prefix ? `${prefix}ChangedCostsType` : "changedCostsType"}
              type="radio"
              value="totalGrossCateringCosts"
              onChange={(e) =>
                onChange(e.target.value as TotalCateringCostsField)
              }
              checked={field == "totalGrossCateringCosts"}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor={prefix ? `${prefix}-type-gross` : "type-gross"}
            >
              Gross
            </label>
          </div>
          <div className="govuk-radios__item">
            <input
              className="govuk-radios__input"
              id={prefix ? `${prefix}-type-net` : "type-net"}
              name={prefix ? `${prefix}ChangedCostsType` : "changedCostsType"}
              type="radio"
              value="totalNetCateringCosts"
              onChange={(e) =>
                onChange(e.target.value as TotalCateringCostsField)
              }
              checked={field == "totalNetCateringCosts"}
            />
            <label
              className="govuk-label govuk-radios__label"
              htmlFor={prefix ? `${prefix}-type-net` : "type-net"}
            >
              Net
            </label>
          </div>
        </div>
      </fieldset>
    </div>
  );
};
