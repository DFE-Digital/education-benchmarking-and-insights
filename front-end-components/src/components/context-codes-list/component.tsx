import { ContextCodesListProps } from "src/components/context-codes-list";

export const ContextCodesList: React.FC<ContextCodesListProps> = ({
  category,
  codes,
  itemClassName,
  label,
}) =>
  codes &&
  codes.length > 0 && (
    <div className="app-cost-code-list">
      {label && <p className="govuk-body">{label}</p>}
      <ul
        className="govuk-list"
        id={
          category
            ? `${category.toLowerCase().replace(/\W/g, "-")}-tags`.replace(
                /-{2,}/g,
                "-"
              )
            : undefined
        }
      >
        {codes.map((code) => (
          <li key={code}>
            <strong
              className={
                itemClassName === undefined ? "govuk-tag" : itemClassName
              }
            >
              {code}
            </strong>
          </li>
        ))}
      </ul>
    </div>
  );
