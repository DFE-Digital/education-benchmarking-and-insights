export type SchoolWorkforceData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  workforceFte: number;
};

export type AuxiliaryStaffData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  auxiliaryStaffFte: number;
};

export type HeadcountData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  workforceHeadcount: number;
};

export type NonClassroomSupportData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  nonClassroomSupportStaffFte: number;
};

export type SeniorLeadershipData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  seniorLeadershipFte: number;
};

export type TeachingAssistantsData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  teachingAssistantsFte: number;
};

export type TotalTeachersData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  teachersFte: number;
};

export type TotalTeachersQualifiedData = {
  urn: string;
  name: string;
  schoolType: string;
  localAuthority: string;
  numberOfPupils: bigint;
  teachersQualified: number;
};
