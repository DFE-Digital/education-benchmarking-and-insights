import { CostCodesListProps } from "src/components/cost-codes-list";
import { ContextCodesList } from "src/components/context-codes-list";
import { useCostCodeMapContext } from "src/contexts/hooks";

export const CostCodesList: React.FC<CostCodesListProps> = (props) => {
  const { category } = props;

  const { categoryCostCodes } = useCostCodeMapContext(category);

  return categoryCostCodes && <ContextCodesList codes={categoryCostCodes} />;
};
