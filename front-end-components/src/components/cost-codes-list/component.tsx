import { CostCodesListProps } from "src/components/cost-codes-list";
import { ContextCodesList } from "src/components/context-codes-list";
import { useCostCodesContext } from "src/contexts/hooks";

export const CostCodesList: React.FC<CostCodesListProps> = ({ category }) => {
  const { categoryCostCodes, itemClassName, label, tags } =
    useCostCodesContext(category);

  return (
    categoryCostCodes && (
      <ContextCodesList
        category={category}
        codes={categoryCostCodes.concat(
          categoryCostCodes.length > 0 && tags ? tags : []
        )}
        itemClassName={itemClassName}
        label={label}
      />
    )
  );
};
