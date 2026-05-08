import { VerticalBarChartPayload } from ".";
import { VerticalBarChartDefinition } from "..";

function validatePayload(
  payload: VerticalBarChartPayload | undefined
): string[] {
  if (!payload) {
    return ["Invalid payload"];
  }

  const validationErrors: string[] = [];
  if (Array.isArray(payload)) {
    const array = payload as VerticalBarChartDefinition[];
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
  } else if (!(payload as VerticalBarChartDefinition)?.data?.length) {
    validationErrors.push("Missing chart data");
  }

  return validationErrors;
}

export { validatePayload };
