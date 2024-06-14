/* eslint-disable @typescript-eslint/no-unused-vars */
import { Text } from "recharts";
import { EstablishmentTickProps } from "src/components/charts/establishment-tick";

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
    ...rest
  } = props;
  const urn =
    linkToEstablishment &&
    establishmentKeyResolver &&
    establishmentKeyResolver(value);
  if (!urn) {
    return <Text>{value}</Text>;
  }

  const name = String(value);
  const truncateAt = (rest.width as number)
    ? Math.floor((rest.width as number) / 7)
    : 50;
  return (
    <>
      {urn && (
        <line
          x1={rest.x}
          y1={rest.y}
          y2={rest.y}
          width={rest.width}
          height={rest.height}
          className="establishment-tick-focus"
        ></line>
      )}
      <text
        fontWeight={urn === highlightedItemKey ? "bold" : "normal"}
        className="recharts-text establishment-tick"
        {...rest}
      >
        <a
          href={href(urn)}
          className="govuk-link govuk-link--no-visited-state"
          aria-label={name}
        >
          {name
            .substring(0, truncateAt)
            .split(" ")
            .map((s, i) => (
              <tspan key={i}>{(i > 0 ? " " : "") + s}</tspan>
            ))}
          {name.length > truncateAt && "…"}
        </a>
      </text>
    </>
  );
}
