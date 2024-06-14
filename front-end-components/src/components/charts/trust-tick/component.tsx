import { Text } from "recharts";
import { TrustTickProps } from "src/components/charts/trust-tick";

export function TrustTick(props: TrustTickProps) {
  const {
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    className,
    highlightedItemKey,
    linkToTrust,
    onClick,
    payload: { value },
    trustCompanyNumberResolver,
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    visibleTicksCount,
    ...rest
  } = props;
  const companyNumber =
    linkToTrust &&
    trustCompanyNumberResolver &&
    trustCompanyNumberResolver(value);
  if (!companyNumber) {
    return <Text>{value}</Text>;
  }

  function handleKeyPress(event: React.KeyboardEvent<SVGTextElement>) {
    if (onClick && companyNumber && event.key === "Enter") {
      onClick(companyNumber);
    }
  }

  return (
    <>
      {companyNumber && (
        <line
          x1={rest.x}
          y1={rest.y}
          y2={rest.y}
          width={rest.width}
          height={rest.height}
          className="trust-tick-focus"
        ></line>
      )}
      <Text
        fontWeight={companyNumber === highlightedItemKey ? "bold" : "normal"}
        cursor={companyNumber ? "pointer" : undefined}
        className={
          companyNumber
            ? "govuk-link govuk-link--no-visited-state trust-tick"
            : undefined
        }
        onClick={onClick ? () => onClick(companyNumber) : undefined}
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
