import React from "react";
import TeachingSupportStaff from "./teaching-support-staff";
import {SchoolExpenditure} from "../services/school-api";
import CateringStaffServices from "./catering-staff-services";
import AdministrativeSupplies from "./administrative-supplies";
import EducationalIct from "./educational-ict";
import EducationalSupplies from "./educational-supplies";
import NonEducationalSupportStaff from "./non-educational-support-staff";
import PremisesStaffServices from "./premises-staff-services";
import OtherCosts from "./other-costs";
import Utilities from "./utilities";

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