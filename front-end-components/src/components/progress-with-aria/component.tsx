import { useEffect, useState } from "react";
import { ProgressWithAriaProps } from "./types";
import { Progress } from "../progress";

export const ProgressWithAria: React.FC<ProgressWithAriaProps> = ({
  completeMessage,
  percentage,
  progressId,
  size,
}: ProgressWithAriaProps) => {
  const [ariaStatus, setAriaStatus] = useState<number>();

  useEffect(() => {
    if (!percentage || percentage < 24) {
      setAriaStatus(0);
    } else if (percentage < 49) {
      setAriaStatus(25);
    } else if (percentage < 74) {
      setAriaStatus(50);
    } else if (percentage < 99) {
      setAriaStatus(75);
    } else {
      setAriaStatus(100);
    }
  }, [percentage, setAriaStatus]);

  return (
    <>
      <div
        className="govuk-visually-hidden"
        role="status"
        id={progressId}
        children={
          percentage === 100 && completeMessage
            ? completeMessage
            : `${ariaStatus}% complete`
        }
      />
      {percentage < 100 && (
        <Progress percentage={percentage} width={size} height={size} />
      )}
    </>
  );
};
