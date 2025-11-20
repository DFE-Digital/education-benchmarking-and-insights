export type SchoolCensusData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  workforce: number;
};

export type AuxiliaryStaffData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  auxiliaryStaff: number;
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
  nonClassroomSupportStaff: number;
};

export type SeniorLeadershipData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  seniorLeadership: number;
};

export type TeachingAssistantsData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  teachingAssistant: number;
};

export type TotalTeachersData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  teachers: number;
};

export type TotalTeachersQualifiedData = {
  urn: string;
  totalPupils: bigint;
  schoolName: string;
  schoolType: string;
  laName: string;
  percentTeacherWithQualifiedStatus: number;
};
