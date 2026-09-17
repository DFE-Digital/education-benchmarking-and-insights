import { LineChartPayload } from ".";
import { LineChartDefinition } from "..";

function validatePayload(payload: LineChartPayload | undefined): string[] {
  if (!payload) {
    return ["Invalid payload"];
  }

  const validationErrors: string[] = [];
  if (Array.isArray(payload)) {
    const array = payload as LineChartDefinition[];
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
  } else if (!(payload as LineChartDefinition)?.data?.length) {
    validationErrors.push("Missing chart data");
  }

  return validationErrors;
}

export { validatePayload };
