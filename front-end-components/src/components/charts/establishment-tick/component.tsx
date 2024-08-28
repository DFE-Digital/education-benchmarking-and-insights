/* eslint-disable @typescript-eslint/no-unused-vars */
import { Text } from "recharts";
import { EstablishmentTickProps } from "src/components/charts/establishment-tick";
import { ChartLink } from "../chart-link";
import { createElement, useState } from "react";

export function EstablishmentTick(props: EstablishmentTickProps) {
  const {
    className,
    highlightedItemKey,
    linkToEstablishment,
    href,
    payload: { value },
    establishmentKeyResolver,
    tickFormatter,
    verticalAnchor,
    visibleTicksCount,
    tooltip,
    ...rest
  } = props;
  const [focused, setFocused] = useState<boolean>();
  const key =
    linkToEstablishment &&
    establishmentKeyResolver &&
    establishmentKeyResolver(value);
  if (!key) {
    return <Text>{value}</Text>;
  }

  const name = String(value);
  const truncateAt = (rest.width as number)
    ? Math.floor((rest.width as number) / 7)
    : 50;
  return (
    <>
      <line
        x1={rest.x}
        y1={rest.y}
        y2={rest.y}
        width={rest.width}
        height={rest.height}
        className="establishment-tick-focus"
      ></line>
      <text
        fontWeight={key === highlightedItemKey ? "bold" : "normal"}
        className="recharts-text establishment-tick"
        {...rest}
      >
        <ChartLink
          href={href(key)}
          className="govuk-link govuk-link--no-visited-state"
          aria-label={name}
          onFocus={() => setFocused(true)}
          onBlur={() => setFocused(false)}
        >
          {name
            .substring(0, truncateAt)
            .split(" ")
            .map((s, i) => (
              <tspan key={i}>{(i > 0 ? " " : "") + s}</tspan>
            ))}
          {name.length > truncateAt && "…"}
        </ChartLink>
      </text>
      {tooltip && focused && (
        <foreignObject x="428" y="30" width="400" height="200">
          <div className="recharts-tooltip-wrapper">
            {createElement(tooltip, { active: true })}
          </div>
        </foreignObject>
      )}
    </>
  );
}
