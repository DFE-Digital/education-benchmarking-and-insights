import React, { PropsWithChildren } from "react";
import { ShareContentProps } from "./types";
import classNames from "classnames";

export const ShareContent: React.FC<PropsWithChildren<ShareContentProps>> = ({
  children,
  copied,
  copiedLabel,
  disabled,
  onCopyClick,
  onSaveClick,
  copyEventId,
  saveEventId,
  showCopy,
  showSave,
  title,
}) => {
  const classes = "govuk-button govuk-button--secondary share-button";
  const props = {
    "data-module": "govuk-button",
    "data-prevent-double-click": "true",
    disabled: disabled,
    "aria-disabled": disabled,
  };

  return (
    <div className="share-buttons">
      {showSave && (
        <button
          className={classNames(classes, "share-button--save")}
          onClick={onSaveClick}
          data-custom-event-chart-name={saveEventId && title}
          data-custom-event-id={saveEventId}
          {...props}
        >
          {children ? (
            children
          ) : (
            <>
              Save <span className="govuk-visually-hidden">{title}</span> as
              image
            </>
          )}
        </button>
      )}
      {showCopy && (
        <button
          className={classNames(classes, "share-button--copy")}
          onClick={onCopyClick}
          data-custom-event-chart-name={copyEventId && title}
          data-custom-event-id={copyEventId}
          {...props}
        >
          {copied ? (
            (copiedLabel ?? "Copied")
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
