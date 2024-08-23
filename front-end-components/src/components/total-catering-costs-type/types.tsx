import { TotalCateringCostsField } from "src/services";

export type TotalCateringCostsTypeProps = {
  field: TotalCateringCostsField;
  onChange: (field: TotalCateringCostsField) => void;
  prefix?: string;
};
