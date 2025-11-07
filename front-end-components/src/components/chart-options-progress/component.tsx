import { ChartMode } from "src/components";
import {
  useChartModeContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { ChartProgress } from "../chart-progress";
import "./styles.scss";

export const ChartOptionsProgress = () => {
  const { chartMode, setChartMode } = useChartModeContext();
  const { available, selected, setSelected } = useProgressIndicatorsContext();

  return (
    <div className="chart-options-flex">
      <div>
        <ChartMode chartMode={chartMode} handleChange={setChartMode} stacked />
      </div>
      <div>
        <ChartProgress
          options={available}
          defaultSelected={selected}
          onChanged={setSelected}
          stacked
        />
      </div>
    </div>
  );
};
