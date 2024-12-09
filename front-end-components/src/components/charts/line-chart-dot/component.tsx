import classNames from "classnames";
import { useState, useEffect } from "react";
import { ChartLink } from "../chart-link";
import { LineChartDotProps } from "./types";

export function LineChartDot({
  cx,
  cy,
  r,
  onActiveIndexChanged,
  index,
  payload,
  keyField,
}: LineChartDotProps) {
  const [focused, setFocused] = useState<boolean>(false);
  useEffect(() => {
    if (focused) {
      onActiveIndexChanged(index); // index
    } else {
      onActiveIndexChanged(undefined);
    }
  }, [focused, onActiveIndexChanged, index]);
  if (cx === +cx! && cy === +cy! && r === +r!) {
    return (
      <>
        <ChartLink
          href="#"
          className="govuk-link govuk-link--no-visited-state"
          aria-label={
            payload && keyField
              ? ((payload as never)[keyField] as string)
              : undefined
          }
          onFocus={() => setFocused(true)}
          onBlur={() => setFocused(false)}
          role="link"
        >
          <circle
            className={classNames("recharts-dot", "recharts-line-dot", {
              "recharts-line-dot-focus": focused,
            })}
            cx={cx}
            cy={cy}
            r={r}
          ></circle>
        </ChartLink>
      </>
    );
  }

  return null;
}
