import { SVGProps } from "react";

export type ProgressProps = Pick<
  SVGProps<SVGSVGElement>,
  "width" | "height" | "className"
> & {
  percentage: number;
};
