import React, { useState } from "react";
import { CompareYourWorkforceViewProps } from "src/views";
import { ChartMode, ChartModeChart } from "src/components";
import { SelectedSchoolContext, ChartModeContext } from "src/contexts";
import {
  AuxiliaryStaff,
  Headcount,
  NonClassroomSupport,
  SchoolWorkforce,
  SeniorLeadership,
  TeachingAssistants,
  TotalTeachers,
  TotalTeachersQualified,
} from "src/views/compare-your-workforce/partials";

export const CompareYourWorkforce: React.FC<CompareYourWorkforceViewProps> = (
  props
) => {
  const { type, id } = props;
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);

  const toggleChartMode = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDisplayMode(e.target.value);
  };

  return (
    <SelectedSchoolContext.Provider value={{ urn: id, name: "" }}>
      <div className="view-as-toggle">
        <ChartMode displayMode={displayMode} handleChange={toggleChartMode} />
      </div>
      <ChartModeContext.Provider value={displayMode}>
        <SchoolWorkforce id={id} type={type} />
        <TotalTeachers id={id} type={type} />
        <TotalTeachersQualified id={id} type={type} />
        <SeniorLeadership id={id} type={type} />
        <TeachingAssistants id={id} type={type} />
        <NonClassroomSupport id={id} type={type} />
        <AuxiliaryStaff id={id} type={type} />
        <Headcount id={id} type={type} />
      </ChartModeContext.Provider>
    </SelectedSchoolContext.Provider>
  );
};
