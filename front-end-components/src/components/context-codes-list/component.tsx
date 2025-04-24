import { ContextCodesListProps } from "src/components/context-codes-list";

export const ContextCodesList: React.FC<ContextCodesListProps> = (props) => {
  const { codes } = props;

  return (
    codes && (
      <ul className="app-cost-code-list">
        {codes.map((code) => (
          <li key={code}>
            <strong className="govuk-tag">{code}</strong>
          </li>
        ))}
      </ul>
    )
  );
};
