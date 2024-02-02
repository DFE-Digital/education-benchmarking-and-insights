export type SchoolWorkforceProps = {
  schools: SchoolWorkforceData[];
};

export type SchoolWorkforceData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  schoolWorkforceHeadcount: number;
};

export type AuxiliaryStaffProps = {
  schools: AuxiliaryStaffData[];
};

export type AuxiliaryStaffData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  auxiliaryStaffFTE: number;
  schoolWorkforceHeadcount: number;
};

export type HeadcountProps = {
  schools: HeadcountData[];
};

export type HeadcountData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  schoolWorkforceHeadcount: number;
};

export type NonClassroomSupportProps = {
  schools: NonClassroomSupportData[];
};

export type NonClassroomSupportData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  nonClassroomSupportStaffFTE: number;
  schoolWorkforceHeadcount: number;
};

export type SeniorLeadershipProps = {
  schools: SeniorLeadershipData[];
};

export type SeniorLeadershipData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  seniorLeadershipFTE: number;
  schoolWorkforceHeadcount: number;
};

export type TeachingAssistantsProps = {
  schools: TeachingAssistantsData[];
};

export type TeachingAssistantsData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  teachingAssistantsFTE: number;
  schoolWorkforceHeadcount: number;
};

export type TotalTeachersProps = {
  schools: TotalTeachersData[];
};

export type TotalTeachersData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  totalNumberOfTeachersFTE: number;
  schoolWorkforceHeadcount: number;
};

export type TotalTeachersQualifiedProps = {
  schools: TotalTeachersQualifiedData[];
};

export type TotalTeachersQualifiedData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  schoolWorkforceFTE: number;
  teachersWithQTSFTE: number;
};
