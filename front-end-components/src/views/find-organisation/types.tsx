export type TrustInputProps = {
  input: string;
  companyNumber: string;
};

export type FindOrganisationProps = {
  findMethod: string;
  schoolInput?: string;
  schoolError?: string;
  trustInput?: string;
  trustError?: string;
  urn?: string;
  companyNumber?: string;
};

export type SchoolInputProps = {
  input: string;
  urn: string;
};

export type SuggestResult<T> = {
  id: string;
  text: string;
  document: T;
};

export type SchoolDocument = {
  urn: string;
};

export type TrustDocument = {
  companyNumber: string;
};
