import { Text } from "recharts";
import { YTickProps } from "src/components/charts/school-tick";

export function SchoolTick(props: YTickProps) {
  const {
    highlightedItemKey,
    linkToSchool,
    onClick,
    payload: { value },
    schoolUrnResolver,
  } = props;
  const urn = linkToSchool && schoolUrnResolver && schoolUrnResolver(value);
  if (!urn) {
    return <Text>{value}</Text>;
  }

  {
    /* TODO: replace with custom version of https://github.com/recharts/recharts/blob/master/src/component/Text.tsx
    to avoid CSS hacks to hide multiple <tspan>s */
  }
  return (
    <Text
      {...props}
      fontWeight={urn === highlightedItemKey ? "bold" : "normal"}
      cursor={urn ? "pointer" : undefined}
      className={
        urn ? "govuk-link govuk-link--no-visited-state school-tick" : undefined
      }
      onClick={onClick ? () => onClick(urn) : undefined}
      lineHeight={0}
    >
      {value}
    </Text>
  );
}
