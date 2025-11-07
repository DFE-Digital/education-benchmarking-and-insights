import { ChartMode } from "src/components";
import {
  useChartModeContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { ChartProgress } from "../chart-progress";
import { ProgressBanding } from "src/views";
import "./styles.scss";

export const ChartOptionsProgress = () => {
  const { chartMode, setChartMode } = useChartModeContext();
  const { available, selected, setSelected } = useProgressIndicatorsContext();

  const handleChecked = (progress: ProgressBanding) => {
    if (selected.includes(progress)) {
      setSelected(selected.filter((s) => s != progress));
    } else {
      setSelected([...selected, progress]);
    }
  };

  return (
    <div className="chart-options-flex">
      <div>
        <ChartMode chartMode={chartMode} handleChange={setChartMode} stacked />
      </div>
      <div>
        <ChartProgress
          options={available}
          selected={selected}
          onChecked={handleChecked}
          stacked
        />
      </div>
    </div>
  );
};
