import React from "react";

const Utilities: React.FC = () => {
    return (
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
    )
};

export default Utilities