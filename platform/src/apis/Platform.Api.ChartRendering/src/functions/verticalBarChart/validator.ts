import { VerticalBarChartPayload } from ".";
import { ChartDefinition } from "..";

function validatePayload(
  payload: VerticalBarChartPayload | undefined,
): string[] {
  if (!payload) {
    return ["Invalid payload"];
  }

  const validationErrors: string[] = [];
  if (Array.isArray(payload)) {
    const array = payload as ChartDefinition[];
    if (array.length === 0) {
      validationErrors.push("Missing chart definitions");
    } else {
      array.forEach((chart, index) => {
        if (!chart.id) {
          validationErrors.push(`Missing id for chart at index ${index}`);
        } else if (!chart.data.length) {
          validationErrors.push(`Missing chart data for ${chart.id}`);
        }
      });
    }
  } else if (!(payload as ChartDefinition)?.data?.length) {
    validationErrors.push("Missing chart data");
  }

  return validationErrors;
}

export { validatePayload };
