import {
  ResponsiveContainer,
  ComposedChart,
  CartesianGrid,
  XAxis,
  YAxis,
  Tooltip,
  Legend,
  Bar,
  Line,
  Text,
} from "recharts";
import { HigherNeedsPlansData } from "./types";

export function HigherNeedsPlansComposed() {
  const data: HigherNeedsPlansData[] = [
    {
      name: "Authority A",
      count: 120,
      income: 18000,
      expenditure: 12000,
    },
    {
      name: "Authority B",
      count: 180,
      income: 26000,
      expenditure: 19000,
    },
    {
      name: "Authority C",
      count: 80,
      income: 12000,
      expenditure: 8000,
    },
    {
      name: "Authority D",
      count: 150,
      income: 22000,
      expenditure: 13000,
    },
    {
      name: "Authority E",
      count: 200,
      income: 30000,
      expenditure: 20000,
    },
  ];

  return (
    <div
      className="govuk-body govuk-body-s"
      style={{ width: 1024, height: 768 }}
    >
      <ResponsiveContainer width="100%" height="100%">
        <ComposedChart
          className="high-needs-plans-composed"
          data={data}
          barGap={10}
          barCategoryGap={40}
          margin={{ top: 0, right: 50, bottom: 50, left: 50 }}
        >
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis
            dataKey="name"
            label={{
              value: "Local Authorities",
              position: "bottom",
              offset: 15,
            }}
          />
          <YAxis
            yAxisId="left"
            label={
              <Text x={0} y={0} dx={30} dy={390} offset={0} angle={-90}>
                Cost (£)
              </Text>
            }
          />
          <YAxis
            yAxisId="right"
            orientation="right"
            label={
              <Text
                x={0}
                y={0}
                dx={975}
                dy={340}
                offset={0}
                angle={-270}
                fill="#4169E1"
              >
                Count
              </Text>
            }
            domain={["dataMin - 20", "dataMax + 20"]}
            stroke="#4169E1"
            tick={{ color: "#4169E1" }}
          />
          <Tooltip
            formatter={(value, _, props) => [
              value,
              props.dataKey === "income"
                ? "Income (£)"
                : props.dataKey === "expenditure"
                  ? "Expenditure (£)"
                  : "Cost (£)",
            ]}
            labelStyle={{ fontWeight: "bold", marginBottom: 5 }}
          />
          <Legend
            verticalAlign="top"
            layout="vertical"
            wrapperStyle={{
              top: 62,
              left: 150,
              backgroundColor: "#fff",
              border: "1px solid #AEAEAE",
              borderRadius: 5,
              padding: 10,
            }}
            payload={[
              { value: "Income (£)", type: "rect", color: "#6cad89" },
              { value: "Expenditure (£)", type: "rect", color: "#c96464" },
            ]}
          />
          <Bar yAxisId="left" dataKey="income" fill="#6cad89" />
          <Bar yAxisId="left" dataKey="expenditure" fill="#c96464" />
          <Line
            yAxisId="right"
            dataKey="count"
            stroke="#4169E1"
            strokeWidth={5}
            legendType="none"
          />
        </ComposedChart>
      </ResponsiveContainer>
    </div>
  );
}
