import React from "react";
import TeachingSupportStaff from "./teaching-support-staff";
import {SchoolExpenditure} from "../services/school-api";
import CateringStaffServices from "./catering-staff-services";
import AdministrativeSupplies from "./administrative-supplies";
import EducationalIct from "./educational-ict";
import EducationalSupplies from "./educational-supplies";
import NonEducationalSupportStaff from "./non-educational-support-staff";

const ExpenditureAccordion: React.FC<AccordionData> = ({urn, schools}) => {

    return (
        <div className="govuk-grid-row">
            <div className="govuk-grid-column-full">
                <div className="govuk-accordion" data-module="govuk-accordion" id="accordion-default">
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-teaching-support-staff">
                            Teaching and teaching support staff
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-teaching-support-staff" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-teaching-support-staff">
                            <TeachingSupportStaff urn={urn} schools={schools}/>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-non-educational-support-staff">
                            Non-educational support staff
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-non-educational-support-staff"
                             className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-non-educational-support-staff">
                            <NonEducationalSupportStaff urn={urn} schools={schools}/>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-educational-supplies">
                            Educational supplies
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-educational-supplies" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-educational-supplies">
                            <EducationalSupplies urn={urn} schools={schools}/>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-educational-ict">
                            Educational ICT
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-educational-ict" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-educational-ict">
                            <EducationalIct urn={urn} schools={schools}/>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-premises-staff-services">
                            Premises staff and services
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-premises-staff-services" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-premises-staff-services">
                            <p className="govuk-body">This is the content for How people read.</p>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-utilities">
                            Utilities
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-utilities" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-utilities">
                            <p className="govuk-body">This is the content for How people read.</p>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-administrative-supplies">
                            Administrative supplies
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-administrative-supplies" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-administrative-supplies">
                            <AdministrativeSupplies urn={urn} schools={schools}/>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button"
                              id="accordion-heading-catering-staff-services">
                            Catering staff and services
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-catering-staff-services" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-catering-staff-services">
                            <CateringStaffServices urn={urn} schools={schools}/>
                        </div>
                    </div>
                    <div className="govuk-accordion__section">
                        <div className="govuk-accordion__section-header">
                            <h2 className="govuk-accordion__section-heading">
                        <span className="govuk-accordion__section-button" id="accordion-heading-other-cost">
                            Other costs
                        </span>
                            </h2>
                        </div>
                        <div id="accordion-content-other-cost" className="govuk-accordion__section-content"
                             aria-labelledby="accordion-heading-other-cost">
                            <p className="govuk-body">This is the content for How people read.</p>
                        </div>
                    </div>
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