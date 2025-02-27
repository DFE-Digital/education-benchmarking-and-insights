import { LocalAuthoritySection251 } from "src/services";
import { HistoricChartSection251Section } from "../types";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/historic-data-high-needs/partials/section-251-section.tsx";
export * from "src/views/historic-data-high-needs/partials/send-2-section.tsx";

export const section251Sections: HistoricChartSection251Section<LocalAuthoritySection251>[] =
  [
    {
      heading: "High needs amount per head 2-18 population",
      charts: [
        {
          name: "Total place funding for special schools and AP/PRUs",
          field: "highNeedsAmountTotalPlaceFunding",
        },
      ],
    },
  ];
