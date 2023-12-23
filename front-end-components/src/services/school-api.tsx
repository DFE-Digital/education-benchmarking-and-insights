import {v4 as uuidv4} from "uuid";

export default class SchoolApi {

    static async getSchoolExpenditure(urn: string): Promise<ExpenditureResult> {
        return fetch(
            `/api/school/${urn}/expenditure`,
            {
                redirect: "manual",
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'X-Correlation-ID': uuidv4()
                }
            }
        )
            //.then(handleApiError) // TODO global api error handler passed in?
            .then(res => res.json())
            .then(res => {
                if (res.error) {
                    throw (res.error);
                }

                return res;
            });
    }
}

export type ExpenditureResult = {
    totalResults: bigint
    page: bigint
    pageSize: bigint
    pageCount: bigint
    results: SchoolExpenditure[]
}

export type SchoolExpenditure = {
    urn: string
    name: string
    totalExpenditure: number
    totalIncome: number
    numberOfPupils: bigint
    totalTeachingSupportStaffCosts: number
    teachingStaffCosts: number
    supplyTeachingStaffCosts: number
    educationalConsultancyCosts: number
    educationSupportStaffCosts: number
    agencySupplyTeachingStaffCosts: number
    netCateringCosts: number
    cateringStaffCosts: number
    cateringSuppliesCosts: number
    incomeCatering: number
}