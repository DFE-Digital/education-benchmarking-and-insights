import React, { PropsWithChildren } from "react";
import "src/components/share-content/styles.css";
import { ShareContentProps } from "./types";

export const ShareContent: React.FC<PropsWithChildren<ShareContentProps>> = ({
  children,
  copied,
  disabled,
  hideCopy,
  onCopyClick,
  onSaveClick,
  copyEventId,
  saveEventId,
  title,
}) => {
  return (
    <div className="share-buttons">
      <button
        className="govuk-button govuk-button--secondary share-button share-button--save"
        data-module="govuk-button"
        data-prevent-double-click="true"
        onClick={onSaveClick}
        disabled={disabled}
        aria-disabled={disabled}
        data-custom-event-id={saveEventId}
        data-custom-event-chart-name={saveEventId && title}
      >
        {children ? (
          children
        ) : (
          <>
            Save <span className="govuk-visually-hidden">{title}</span> as image
          </>
        )}
      </button>
      {!hideCopy && (
        <button
          className="govuk-button govuk-button--secondary share-button share-button--copy"
          data-module="govuk-button"
          data-prevent-double-click="true"
          onClick={onCopyClick}
          disabled={disabled}
          aria-disabled={disabled}
          data-custom-event-id={copyEventId}
          data-custom-event-chart-name={copyEventId && title}
        >
          {copied ? (
            "Copied"
          ) : children ? (
            children
          ) : (
            <>
              Copy <span className="govuk-visually-hidden">{title}</span> image
            </>
          )}
        </button>
      )}
    </div>
  );
};
