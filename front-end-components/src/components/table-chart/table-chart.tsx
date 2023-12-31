import React, {useContext} from "react";
import {TableChartProps} from "../../types";
import {ChartDimensionContext, SelectedSchoolContext} from "../../contexts";

const TableChart: React.FC<TableChartProps> = (props) => {
    const {heading, tableHeadings, chartDimensions,elementId, data} = props;
    const dimension = useContext(ChartDimensionContext);
    const selectedSchool = useContext(SelectedSchoolContext);

    return (
        <div className="govuk-grid-row">
            <div className="govuk-grid-column-full">
                {heading}
                {chartDimensions && chartDimensions.dimensions.length > 0 &&
                    <div className="govuk-form-group">
                        <label className="govuk-label" htmlFor={`${elementId}-dimension`}>
                            View table as
                        </label>
                        <select className="govuk-select" id={`${elementId}-dimension`} name="dimension"
                                onChange={chartDimensions.handleChange} defaultValue={dimension}>
                            {chartDimensions.dimensions.map((dimension, idx) => {
                                return <option key={idx} value={dimension}>{dimension}</option>;
                            })}
                        </select>
                    </div>
                }
                <table className="govuk-table">
                    <thead className="govuk-table__head">
                    <tr className="govuk-table__row">
                        {tableHeadings.map((heading) => {
                            return <th scope="col" className="govuk-table__header">{heading}</th>;
                        })}
                    </tr>
                    </thead>
                    <tbody className="govuk-table__body">
                    {data && data.map((row) => {
                        return <tr className="govuk-table__row">
                            <td className="govuk-table__cell">
                                {
                                    selectedSchool.urn == row.urn ?
                                        <strong>
                                            <a className="govuk-link govuk-link--no-visited-state"
                                               href={`/school/${row.urn}`}>
                                                {row.school}
                                            </a>
                                        </strong> :
                                        <a className="govuk-link govuk-link--no-visited-state"
                                           href={`/school/${row.urn}`}>
                                            {row.school}
                                        </a>
                                }

                            </td>
                            {row.additionalData && row.additionalData.map((data) => {
                                return <td className="govuk-table__cell">{String(data)}</td>
                            })}
                            <td className="govuk-table__cell">{row.value.toFixed(2)}</td>
                        </tr>;
                    })}
                    </tbody>
                </table>
            </div>
        </div>
    )
};

export default TableChart;