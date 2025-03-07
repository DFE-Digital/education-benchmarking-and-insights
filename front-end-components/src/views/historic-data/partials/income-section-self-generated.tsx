import { HistoricChart } from "src/composed/historic-chart-composed";
import { IncomeHistoryRow } from "src/services";
import { Loading } from "src/components/loading";

export const IncomeSectionSelfGenerated: React.FC<{
  data: IncomeHistoryRow[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartTitle="Self-generated funding total"
            data={data}
            seriesConfig={{
              totalSelfGeneratedFunding: {
                label: "Total self-generated funding",
                visible: true,
              },
            }}
            valueField="totalSelfGeneratedFunding"
          >
            <h3 className="govuk-heading-s">Self-generated funding total</h3>
          </HistoricChart>

          <HistoricChart
            chartTitle="Income from facilities and services"
            data={data}
            seriesConfig={{
              incomeFacilitiesServices: {
                label: "Income from facilities and services",
                visible: true,
              },
            }}
            valueField="incomeFacilitiesServices"
          >
            <h3 className="govuk-heading-s">
              Income from facilities and services
            </h3>
          </HistoricChart>

          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about income from facilities and services
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  income from meals provided to external customers, including
                  other schools
                </li>
                <li>
                  income from assets such as the hire of premises, equipment or
                  other facilities
                </li>
                <li>
                  all other income the school receives from facilities and
                  services, like income for consultancy, training courses and
                  examination fees
                </li>
                <li>
                  any interest payments received from bank accounts held in the
                  school's name or used to fund school activities
                </li>
                <li>
                  income from the sale of school uniforms, materials, private
                  phone calls, photocopying, publications, books
                </li>
                <li>income from before and after school clubs</li>
                <li>
                  income from the re-sale of items to pupils, like musical
                  instruments, classroom resources, commission on photographs
                </li>
                <li>income from non-catering vending machines</li>
                <li>income from a pupil-focused special facility</li>
                <li>
                  rental of school premises including deductions from salaries
                  where staff live on site
                </li>
                <li>income from universities for student/teacher placements</li>
                <li>income from energy/feed in tariffs</li>
                <li>
                  income from SEN and alternative provision support services
                  commissioned by a local authority or another school, for
                  delivery under a service level agreement
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  payments received from other schools for which you have not
                  provided a service
                </li>
                <li>income from community-focused special facilities</li>
                <li>high-needs place funding</li>
                <li>high-needs top-up funding</li>
                <li>any balances carried forward from previous years</li>
              </ul>
            </div>
          </details>
          <HistoricChart
            chartTitle="Income from catering"
            data={data}
            seriesConfig={{
              incomeCatering: {
                label: "Income from catering",
                visible: true,
              },
            }}
            valueField="incomeCatering"
          >
            <h3 className="govuk-heading-s">Income from catering</h3>
          </HistoricChart>

          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about income from catering
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  income from catering, school milk, and catering vending
                  machines
                </li>
                <li>
                  any payments received from catering contractors, such as where
                  a contractor has previously overcharged the school
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>receipts for catering for external customers</li>
                <li>income from non-catering vending machines</li>
                <li>any balances carried forward from previous years</li>
              </ul>
            </div>
          </details>
          <HistoricChart
            chartTitle="Donations and/or voluntary funds"
            data={data}
            seriesConfig={{
              donationsVoluntaryFunds: {
                label: "Donations and/or voluntary funds",
                visible: true,
              },
            }}
            valueField="donationsVoluntaryFunds"
          >
            <h3 className="govuk-heading-s">
              Donations and/or voluntary funds
            </h3>
          </HistoricChart>

          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about donations and/or voluntary funds
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>
                This is income from private sources under the control of the
                governing body, including:
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>income provided from foundation, diocese or trust funds</li>
                <li>business sponsorship</li>
                <li>income from fundraising activities</li>
                <li>
                  contributions from parents (not directly requested by the
                  school) used to provide educational benefits
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  contributions or donations that are not used for the benefit
                  of students' learning or the school
                </li>
                <li>
                  balances available in trust funds or other private or
                  non-public accounts
                </li>
                <li>balances carried forward from previous years</li>
              </ul>
            </div>
          </details>
          <HistoricChart
            chartTitle="Receipts from supply teacher insurance claims"
            data={data}
            seriesConfig={{
              receiptsSupplyTeacherInsuranceClaims: {
                label: "Receipts from supply teacher insurance claims",
                visible: true,
              },
            }}
            valueField="receiptsSupplyTeacherInsuranceClaims"
          >
            <h3 className="govuk-heading-s">
              Receipts from supply teacher insurance claims
            </h3>
          </HistoricChart>

          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about receipts from supply teacher insurance claims
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  payments from staff absence insurance schemes to cover the
                  cost of supply teachers (including those offered by the local
                  authority)
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  insurance receipts for any other claim, for example absence of
                  non-teaching staff, or building, contents, and public
                  liability
                </li>
                <li>balances carried forward from previous years</li>
              </ul>
            </div>
          </details>
          <HistoricChart
            chartTitle="Investment income"
            data={data}
            seriesConfig={{
              investmentIncome: {
                label: "Investment income",
                visible: true,
              },
            }}
            valueField="investmentIncome"
          >
            <h3 className="govuk-heading-s">Investment income</h3>
          </HistoricChart>

          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about investment income
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>interest</li>
                <li>dividend income</li>
                <li>other investment income</li>
              </ul>
            </div>
          </details>

          <HistoricChart
            chartTitle="Other self-generated income"
            data={data}
            seriesConfig={{
              otherSelfGeneratedIncome: {
                label: "Other self-generated income",
                visible: true,
              },
            }}
            valueField="otherSelfGeneratedIncome"
          >
            <h3 className="govuk-heading-s">Other self-generated income</h3>
          </HistoricChart>

          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about other self-generated income
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>fundraising activity</li>
                <li>lettings</li>
                <li>non-governmental grants</li>
                <li>commercial sponsorship</li>
                <li>consultancy</li>
              </ul>
            </div>
          </details>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
