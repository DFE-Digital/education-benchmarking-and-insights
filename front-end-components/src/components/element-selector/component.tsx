import { ElementAndAttributes } from "src/hooks/useDownloadImage";
import { ElementSelectorProps } from "./types";
import "./styles.scss";
import classNames from "classnames";

export function ElementSelector({
  elements,
  onChange,
  selected,
  showValidationError,
}: ElementSelectorProps) {
  const handleChange = (element: ElementAndAttributes, checked: boolean) => {
    const selectedElements = [...selected.filter((e) => e !== element)];
    if (checked) {
      selectedElements.push(element);
    }

    onChange(selectedElements);
  };

  return (
    <>
      {showValidationError && (
        <div className="govuk-error-summary" data-module="govuk-error-summary">
          <div role="alert">
            <h2 className="govuk-error-summary__title">There is a problem</h2>
            <div className="govuk-error-summary__body">
              <ul className="govuk-list govuk-error-summary__list">
                <li>
                  <a href="#elements-0">Select one or more items</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      )}
      <div
        className={classNames("govuk-form-group", {
          "govuk-form-group--error": showValidationError,
        })}
      >
        <fieldset className="govuk-fieldset">
          <div
            className="govuk-checkboxes govuk-checkboxes--elements"
            data-module="govuk-checkboxes"
          >
            {elements.map((element, i) => {
              const id = `elements-${i}`;

              return (
                <div key={i} className="govuk-checkboxes__item">
                  <input
                    checked={selected.includes(element)}
                    className="govuk-checkboxes__input"
                    id={id}
                    name={id}
                    onChange={(e) => handleChange(element, e.target.checked)}
                    type="checkbox"
                    value={id}
                  />
                  <label
                    className="govuk-label govuk-checkboxes__label"
                    htmlFor={id}
                  >
                    {element.title || `Item ${i + 1}`}
                  </label>
                </div>
              );
            })}
          </div>
        </fieldset>
      </div>
    </>
  );
}
