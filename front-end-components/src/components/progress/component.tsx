import React from "react";
import { ProgressProps } from ".";
import classNames from "classnames";
import "./styles.scss";

// inspired by https://medium.com/@pppped/how-to-code-a-responsive-circular-percentage-chart-with-svg-and-css-3632f8cd7705
export const Progress: React.FC<ProgressProps> = ({
  className,
  percentage,
  ...props
}) => {
  return (
    <svg
      viewBox="0 0 40 40"
      className={classNames("progress-wrapper", className)}
      {...props}
    >
      <path
        className="progress-circle"
        d="M20 4.0845
      a 15.9155 15.9155 0 0 1 0 31.831
      a 15.9155 15.9155 0 0 1 0 -31.831"
        strokeDasharray={`${percentage}, 100`}
      />
    </svg>
  );
};
