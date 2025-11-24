import { ChartMode } from "src/components";
import {
  useChartModeContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { ChartProgress } from "../chart-progress";
import "./styles.scss";
import { ChartOptionsProps } from "./types";
import { ChartPhases } from "../chart-phases";
import classNames from "classnames";

export const ChartOptions = ({
  className,
  phases,
  stacked,
  handlePhaseChange,
}: ChartOptionsProps) => {
  const { chartMode, setChartMode } = useChartModeContext();
  const { available, selected, setSelected } = useProgressIndicatorsContext();

  return (
    <div className={classNames("chart-options-flex", className)}>
      {phases && (
        <div>
          <ChartPhases
            phases={phases}
            handlePhaseChange={handlePhaseChange ? handlePhaseChange : () => {}}
          />
        </div>
      )}
      <div>
        <ChartMode
          chartMode={chartMode}
          handleChange={setChartMode}
          stacked={stacked}
        />
      </div>
      {available && available.length > 0 && (
        <div>
          <ChartProgress
            options={available}
            defaultSelected={selected}
            onChanged={setSelected}
            stacked={stacked}
          />
        </div>
      )}
    </div>
  );
};
