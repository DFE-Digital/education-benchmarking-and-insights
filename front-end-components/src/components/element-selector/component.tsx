import { ElementAndTitle } from "src/hooks/useDownloadImage";
import { ElementSelectorProps } from "./types";
import "./styles.scss";

export function ElementSelector({
  elements,
  onChange,
  selected,
}: ElementSelectorProps) {
  const handleChange = (element: ElementAndTitle, checked: boolean) => {
    const selectedElements = [...selected.filter((e) => e !== element)];
    if (checked) {
      selectedElements.push(element);
    }

    onChange(selectedElements);
  };

  return (
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
            <label className="govuk-label govuk-checkboxes__label" htmlFor={id}>
              {element.title || `Item ${i + 1}`}
            </label>
          </div>
        );
      })}
    </div>
  );
}
