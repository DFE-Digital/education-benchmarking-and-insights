import { ContextCodesListProps } from "src/components/context-codes-list";

export const ContextCodesList: React.FC<ContextCodesListProps> = ({
  category,
  codes,
}) =>
  codes && (
    <ul
      className="app-cost-code-list"
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
          <strong className="govuk-tag">{code}</strong>
        </li>
      ))}
    </ul>
  );
