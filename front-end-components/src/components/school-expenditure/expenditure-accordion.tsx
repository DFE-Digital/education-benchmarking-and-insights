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

const ExpenditureAccordion: React.FC<ExpenditureAccordionProps> = ({schools}) => {

    return (
        <div className="govuk-grid-row">
            <div className="govuk-grid-column-full">
                <div className="govuk-accordion" data-module="govuk-accordion" id="accordion-default">
                    <TeachingSupportStaff schools={schools}/>
                    <NonEducationalSupportStaff schools={schools}/>
                    <EducationalSupplies schools={schools}/>
                    <EducationalIct schools={schools}/>
                    <PremisesStaffServices schools={schools}/>
                    <Utilities/>
                    <AdministrativeSupplies schools={schools}/>
                    <CateringStaffServices schools={schools}/>
                    <OtherCosts schools={schools}/>
                </div>
            </div>
        </div>
    )
};

export default ExpenditureAccordion

export type ExpenditureAccordionProps = {
    schools: SchoolExpenditure[]
}