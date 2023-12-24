import React from "react";
import TeachingSupportStaff from "./teaching-support-staff";
import {SchoolExpenditure} from "../../services/school-api";
import CateringStaffServices from "./catering-staff-services";
import AdministrativeSupplies from "./administrative-supplies";
import EducationalIct from "./educational-ict";
import EducationalSupplies from "./educational-supplies";
import NonEducationalSupportStaff from "./non-educational-support-staff";
import PremisesStaffServices from "./premises-staff-services";
import OtherCosts from "./other-costs";
import Utilities from "./utilities";
import {ChartMode} from "../../constants";

const ExpenditureAccordion: React.FC<ExpenditureAccordionProps> = ({urn, schools,mode}) => {

    return (
        <div className="govuk-grid-row">
            <div className="govuk-grid-column-full">
                <div className="govuk-accordion" data-module="govuk-accordion" id="accordion-default">
                    <TeachingSupportStaff urn={urn} schools={schools} mode={mode}/>
                    <NonEducationalSupportStaff urn={urn} schools={schools} mode={mode}/>
                    <EducationalSupplies urn={urn} schools={schools} mode={mode}/>
                    <EducationalIct urn={urn} schools={schools} mode={mode}/>
                    <PremisesStaffServices urn={urn} schools={schools} mode={mode}/>
                    <Utilities/>
                    <AdministrativeSupplies urn={urn} schools={schools} mode={mode}/>
                    <CateringStaffServices urn={urn} schools={schools} mode={mode}/>
                    <OtherCosts urn={urn} schools={schools} mode={mode}/>
                </div>
            </div>
        </div>
    )
};

export default ExpenditureAccordion

export type ExpenditureAccordionProps = {
    urn: string
    schools: SchoolExpenditure[]
    mode: ChartMode
}