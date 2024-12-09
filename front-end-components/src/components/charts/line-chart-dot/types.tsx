import { FunctionComponent } from "react";
import { DotProps } from "recharts";
import {
  ValueType,
  NameType,
  Payload,
} from "recharts/types/component/DefaultTooltipContent";
import { ContentType, TooltipProps } from "recharts/types/component/Tooltip";

export interface LineChartDotProps extends DotProps {
  tooltip?: FunctionComponent<TooltipProps<ValueType, NameType>>;
  tooltipContent?: ContentType<ValueType, NameType>;
  payload?: Payload<ValueType, NameType>;
  dataKey?: string;
  onActiveIndexChanged: (index: number | undefined) => void;
  index?: number;
  keyField?: string;
}
