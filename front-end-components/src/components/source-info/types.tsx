export type SourceInfoProps = {
  additionalInfo?: SourceInfoMode;
  lineCodes?: string[];
};

export const SourceInfoModes = {
  Glossary: "glossary",
  Hospital: "hospital",
  Simple: "simple",
} as const;

export type SourceInfoMode =
  (typeof SourceInfoModes)[keyof typeof SourceInfoModes];
