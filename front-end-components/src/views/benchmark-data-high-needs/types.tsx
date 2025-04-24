import { ReactNode } from "react";
import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import {
  LocalAuthoritySection251,
  LocalAuthorityEducationHealthCarePlan,
} from "src/services";

export type BenchmarkDataHighNeedsProps = {
  code: string;
  editLink?: string;
  fetchTimeout?: number;
  set: string[];
};

export type BenchmarkDataHighNeedsViewProps = BenchmarkDataHighNeedsProps & {};

export type BenchmarkDataHighNeedsAccordionProps = Omit<
  BenchmarkDataHighNeedsProps,
  "code"
>;

export type BenchmarkChartSection251Section<
  TData extends LocalAuthoritySection251,
> = {
  heading: string;
  charts: BenchmarkDataHighNeedsSection251Chart<TData>[];
};

export type BenchmarkDataHighNeedsSection251Chart<
  TData extends LocalAuthoritySection251,
> = {
  name: string;
  field: ResolvedStatProps<TData>["valueField"];
  details?: {
    label: string;
    content: ReactNode;
  };
  lineCodes?: string[];
};

export type BenchmarkChartSend2Section<
  TData extends LocalAuthorityEducationHealthCarePlan,
> = {
  heading?: string;
  charts: BenchmarkDataHighNeedsSend2Chart<TData>[];
};

export type BenchmarkDataHighNeedsSend2Chart<
  TData extends LocalAuthorityEducationHealthCarePlan,
> = {
  name: string;
  field: ResolvedStatProps<TData>["valueField"];
  details?: {
    label: string;
    content: ReactNode;
  };
};

export type Section251SectionProps<TData> = {
  data?: TData[];
};

export type Send2SectionProps<TData> = {
  data?: TData[];
};
