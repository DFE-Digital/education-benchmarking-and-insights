import { SchoolCensusCommon } from "../types";

export type SchoolCensusData = SchoolCensusCommon & {
  workforce: number;
};

export type AuxiliaryStaffData = SchoolCensusCommon & {
  auxiliaryStaff: number;
};

export type HeadcountData = SchoolCensusCommon & {
  workforceHeadcount: number;
};

export type NonClassroomSupportData = SchoolCensusCommon & {
  nonClassroomSupportStaff: number;
};

export type SeniorLeadershipData = SchoolCensusCommon & {
  seniorLeadership: number;
};

export type TeachingAssistantsData = SchoolCensusCommon & {
  teachingAssistant: number;
};

export type TotalTeachersData = SchoolCensusCommon & {
  teachers: number;
};

export type TotalTeachersQualifiedData = SchoolCensusCommon & {
  percentTeacherWithQualifiedStatus: number;
};
