import React from "react";
import "src/components/share-content/styles.css";
import { ShareContentProps } from "./types";

export const ShareContent: React.FC<ShareContentProps> = ({
  disabled,
  onSaveClick,
  saveEventId,
  title,
}) => {
  return (
    <div>
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        data-prevent-double-click="true"
        onClick={onSaveClick}
        disabled={disabled}
        aria-disabled={disabled}
        data-custom-event-id={saveEventId}
        data-custom-event-chart-name={saveEventId && title}
      >
        Save <span className="govuk-visually-hidden">{title}</span> as image
      </button>
    </div>
  );
};
