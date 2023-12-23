import React from "react";
import TeachingSupportStaff from "./teaching-support-staff.tsx";
import {SchoolExpenditure} from "../../services/school-api.tsx";
import CateringStaffServices from "./catering-staff-services.tsx";
import AdministrativeSupplies from "./administrative-supplies.tsx";
import EducationalIct from "./educational-ict.tsx";
import EducationalSupplies from "./educational-supplies.tsx";
import NonEducationalSupportStaff from "./non-educational-support-staff.tsx";
import PremisesStaffServices from "./premises-staff-services.tsx";
import OtherCosts from "./other-costs.tsx";
import Utilities from "./utilities.tsx";

const ExpenditureAccordion: React.FC<AccordionData> = ({urn, schools}) => {

    return (
        <div className="govuk-grid-row">
            <div className="govuk-grid-column-full">
                <div className="govuk-accordion" data-module="govuk-accordion" id="accordion-default">
                    <TeachingSupportStaff urn={urn} schools={schools}/>
                    <NonEducationalSupportStaff urn={urn} schools={schools}/>
                    <EducationalSupplies urn={urn} schools={schools}/>
                    <EducationalIct urn={urn} schools={schools}/>
                    <PremisesStaffServices urn={urn} schools={schools}/>
                    <Utilities/>
                    <AdministrativeSupplies urn={urn} schools={schools}/>
                    <CateringStaffServices urn={urn} schools={schools}/>
                    <OtherCosts urn={urn} schools={schools}/>
                </div>
            </div>
        </div>
    )
};

export default ExpenditureAccordion

export type AccordionData = {
    urn: string
    schools: SchoolExpenditure[]
}