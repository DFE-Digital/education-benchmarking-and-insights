import {v4 as uuidv4} from "uuid";
import {
    Expenditure, SchoolApiResult, Workforce,
} from "src/services";

export class SchoolApi {

    static async getSchoolExpenditure(urn: string): Promise<SchoolApiResult<Expenditure>> {
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

    static async getWorkforceBenchmarkData(urn: string): Promise<SchoolApiResult<Workforce>> {
        return fetch(`/api/school/${urn}/workforce`, 
        {
          redirect: 'manual',
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'X-Correlation-ID': uuidv4(),
          },
        })
          .then((res) => res.json())
          .then((res) => {
            if (res.error) {
              throw res.error;
            }
    
            return res;
          });
      }

}



