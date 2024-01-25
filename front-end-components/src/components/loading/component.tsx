import React from "react";
import "src/components/loading/styles.css";
export const Loading: React.FC = () => {
  return (
    <div className="loading">
      <div className="spinner"></div>
    </div>
  );
};
