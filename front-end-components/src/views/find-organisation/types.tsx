export type TrustInputProps = {
  input: string;
  companyNumber: string;
  exclude?: string[];
};

export type FindOrganisationProps = {
  code?: string;
  companyNumber?: string;
  findMethod: string;
  laError?: string;
  laInput?: string;
  schoolError?: string;
  schoolInput?: string;
  trustError?: string;
  trustInput?: string;
  urn?: string;
};

export type SchoolInputProps = {
  input: string;
  urn: string;
  exclude?: string[];
  excludeMissingFinancialData?: boolean;
};

export type SuggestResult<T> = {
  id: string;
  text: string;
  document: T;
};

export type LaInputProps = {
  input: string;
  code: string;
  exclude?: string[];
};

/**
 * @example 
    {
      "text": "*Harrow* Way Community School",
      "document": {
        "urn": "116431",
        "schoolName": "Harrow Way Community School",
        "town": "Andover",
        "postcode": "SP10 3RH",
        "hasSixthForm": false
      }
    }
 */
export type SchoolDocument = {
  hasSixthForm: boolean;
  schoolName: string;
  postcode: string;
  town: string;
  urn: string;
};

/**
 * @example
    {
      "text": "*Advantage* Multi Academy Trust",
      "document": {
        "companyNumber": "10969334",
        "trustName": "Advantage Multi Academy Trust"
      }
    }
 */
export type TrustDocument = {
  companyNumber: string;
  trustName: string;
};

/**
 * @example {
      "text": "Redcar *and* Cleveland",
      "document": {
        "code": "807",
        "name": "Redcar and Cleveland"
      }
    }
 */
export type LaDocument = {
  code: string;
  name: string;
};
