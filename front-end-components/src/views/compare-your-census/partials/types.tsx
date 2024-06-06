export type SchoolCensusData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  workforceFTE: number;
};

export type AuxiliaryStaffData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  auxiliaryStaffFTE: number;
};

export type HeadcountData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  workforceHeadcount: number;
};

export type NonClassroomSupportData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  nonClassroomSupportStaffFTE: number;
};

export type SeniorLeadershipData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  seniorLeadershipFTE: number;
};

export type TeachingAssistantsData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  teachingAssistantFTE: number;
};

export type TotalTeachersData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  teachersFTE: number;
};

export type TotalTeachersQualifiedData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  percentTeacherWithQualifiedStatus: number;
};
