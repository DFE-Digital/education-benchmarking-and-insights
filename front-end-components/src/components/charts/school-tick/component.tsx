import { Text } from "recharts";
import { SchoolTickProps } from "src/components/charts/school-tick";

export function SchoolTick(props: SchoolTickProps) {
  const {
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    className,
    highlightedItemKey,
    linkToSchool,
    onClick,
    payload: { value },
    schoolUrnResolver,
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    visibleTicksCount,
    ...rest
  } = props;
  const urn = linkToSchool && schoolUrnResolver && schoolUrnResolver(value);
  if (!urn) {
    return <Text>{value}</Text>;
  }

  function handleKeyPress(event: React.KeyboardEvent<SVGTextElement>) {
    if (onClick && urn && event.key === "Enter") {
      onClick(urn);
    }
  }

  return (
    <>
      {urn && (
        <line
          x1={rest.x}
          y1={rest.y}
          y2={rest.y}
          width={rest.width}
          height={rest.height}
          className="school-tick-focus"
        ></line>
      )}
      <Text
        fontWeight={urn === highlightedItemKey ? "bold" : "normal"}
        cursor={urn ? "pointer" : undefined}
        className={
          urn
            ? "govuk-link govuk-link--no-visited-state school-tick"
            : undefined
        }
        onClick={onClick ? () => onClick(urn) : undefined}
        onKeyDown={handleKeyPress}
        lineHeight={0}
        tabIndex={onClick ? 0 : undefined}
        {...rest}
      >
        {value}
      </Text>
    </>
  );
}
