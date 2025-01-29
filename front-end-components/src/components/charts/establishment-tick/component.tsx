/* eslint-disable @typescript-eslint/no-unused-vars */
import { Text } from "recharts";
import { EstablishmentTickProps } from "src/components/charts/establishment-tick";
import { ChartLink } from "../chart-link";
import {
  createElement,
  useEffect,
  useLayoutEffect,
  useMemo,
  useRef,
  useState,
} from "react";
import "./styles.scss";

export function EstablishmentTick(props: EstablishmentTickProps) {
  const {
    className,
    establishmentKeyResolver,
    highlightedItemKey,
    href,
    linkToEstablishment,
    onFocused,
    payload: { value },
    specialItemFlags,
    tickFormatter,
    tooltip,
    verticalAnchor,
    visibleTicksCount,
    ...rest
  } = props;
  const textRef = useRef<SVGTextElement>(null);
  const [focused, setFocused] = useState<boolean>(false);
  const key = useMemo(() => {
    return (
      linkToEstablishment &&
      establishmentKeyResolver &&
      establishmentKeyResolver(value)
    );
  }, [establishmentKeyResolver, linkToEstablishment, value]);

  const partYear = useMemo(() => {
    return (
      key && specialItemFlags && specialItemFlags(key).includes("partYear")
    );
  }, [key, specialItemFlags]);

  const [textBoundingBox, setTextBoundingBox] = useState<{
    x?: number;
    y?: number;
  }>();
  useLayoutEffect(() => {
    const bbox = textRef.current?.getBBox();
    setTextBoundingBox({ x: bbox?.x, y: bbox?.y });
  }, []);

  useEffect(() => {
    if (key && onFocused) {
      onFocused(key, focused);
    }
  }, [key, onFocused, focused]);

  if (!key) {
    return <Text>{value}</Text>;
  }

  const name = String(value);
  const truncateAt = (rest.width as number)
    ? Math.floor((rest.width as number) / 7)
    : 45;
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
      {partYear && (
        <Exclamation x={textBoundingBox?.x} y={textBoundingBox?.y} />
      )}
      <text
        fontWeight={key === highlightedItemKey ? "bold" : "normal"}
        className="recharts-text establishment-tick"
        ref={textRef}
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

const Exclamation = ({
  x,
  y,
}: {
  x?: string | number;
  y?: string | number;
}) => {
  const parsedX = parseInt((x ?? 0).toString());
  const parsedY = parseInt((y ?? 0).toString());

  return (
    <g>
      <circle cx={parsedX - 16} cy={parsedY + 12} r={8} fill="#fff"></circle>
      <path
        transform={`translate(${parsedX - 30},${parsedY}),scale(0.75,0.75)`}
        fill="#000"
        d="M18,6A12,12,0,1,0,30,18,12,12,0,0,0,18,6Zm-1.49,6a1.49,1.49,0,0,1,3,0v6.89a1.49,1.49,0,1,1-3,0ZM18,25.5a1.72,1.72,0,1,1,1.72-1.72A1.72,1.72,0,0,1,18,25.5Z"
      ></path>
    </g>
  );
};
