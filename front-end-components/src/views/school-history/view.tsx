import React, { useState, useLayoutEffect } from "react";
import { initAll } from "govuk-frontend";

export const SchoolHistory: React.FC<any> = () => {
    const [activeTab, setActiveTab] = useState<string>('spending');

    useLayoutEffect(() => {
        initAll();
    }, []);

    const handleTabClick = (event: React.MouseEvent<HTMLAnchorElement>, tabId: string) => {
        event.preventDefault(); 
        setActiveTab(tabId);
    };

    return (
        <div className="govuk-tabs" data-module="govuk-tabs">
            <ul className="govuk-tabs__list">
                {["spending", "income", "balance", "workforce"].map((tabId) => (
                    <li
                        key={tabId}
                        className={`govuk-tabs__list-item ${activeTab === tabId ? "govuk-tabs__list-item--selected" : ""}`}
                    >
                        <a
                            className="govuk-tabs__tab"
                            href={`#${tabId}`}
                            onClick={(e) => handleTabClick(e, tabId)}
                        >
                            {tabId.charAt(0).toUpperCase() + tabId.slice(1)}
                        </a>
                    </li>
                ))}
            </ul>
            {["spending", "income", "balance", "workforce"].map((tabId) => (
                <div
                    key={tabId}
                    className={`govuk-tabs__panel ${activeTab !== tabId ? "govuk-tabs__panel--hidden" : ""}`}
                    id={tabId}
                >
                    <h2 className="govuk-heading-l">{tabId.charAt(0).toUpperCase() + tabId.slice(1)}</h2>
                    {/*content */}
                    <p>Content for {tabId}</p>
                </div>
            ))}
        </div>
    );
};
