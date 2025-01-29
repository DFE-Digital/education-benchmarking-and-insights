import React from "react";
import "./styles.scss";

export const Loading: React.FC = () => {
  return (
    <div className="loading">
      <div className="spinner"></div>
    </div>
  );
};
