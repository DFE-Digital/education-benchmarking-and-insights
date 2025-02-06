import { CostCodesListProps } from "src/components/cost-codes-list";
import { useCostCodeMapContext } from "src/contexts/hooks";

export const CostCodesList: React.FC<CostCodesListProps> = (props) => {
  const { category } = props;

  const { categoryCostCodes } = useCostCodeMapContext(category);

  return (
    categoryCostCodes && (
      <ul className="app-cost-code-list">
        {categoryCostCodes.map((costCode) => (
          <li key={costCode}>
            <strong className="govuk-tag">{costCode}</strong>
          </li>
        ))}
      </ul>
    )
  );
};
