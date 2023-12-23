import React from "react";
import HorizontalBarChart, {BarData} from "../horizontal-bar-chart/horizontal-bar-chart";

const AccordionChartContent: React.FC<AccordionChartContentData> = ({heading, chosenSchoolName, data}) => {
    return (
        <div>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <h3 className="govuk-heading-s">{heading}</h3>
                </div>
                <div className="govuk-grid-column-one-third">
                    <p className="govuk-body">[Save as image]</p>
                </div>
            </div>
            {data.labels.length > 0 &&
                <div className="govuk-grid-row">
                    <div className="govuk-grid-column-full">
                        <HorizontalBarChart data={data} chosenSchool={chosenSchoolName} xLabel='per pupil'/>
                    </div>
                </div>
            }
        </div>
    )
};

export default AccordionChartContent

export type AccordionChartContentData = {
    heading: string
    chosenSchoolName: string
    data: BarData
}
