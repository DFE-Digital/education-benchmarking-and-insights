import { HistoricChart } from "src/composed/historic-chart-composed";
import { Income } from "src/services";
import { Loading } from "src/components/loading";

export const IncomeSectionGrantFunding: React.FC<{
  data: Income[];
}> = ({ data }) => {
  return (
    <>
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Grant funding total"
            data={data}
            seriesConfig={{
              totalGrantFunding: {
                label: "total grant funding",
                visible: true,
              },
            }}
            valueField="totalGrantFunding"
          >
            <h3 className="govuk-heading-s">Grant funding total</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Direct grants"
            data={data}
            seriesConfig={{
              directGrants: {
                label: "Direct grants",
                visible: true,
              },
            }}
            valueField="directGrants"
          >
            <h3 className="govuk-heading-s">Direct grants</h3>
          </HistoricChart>

          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about direct grants
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>Where there is funding, direct grants include:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>pre-16 funding</li>
                <li>post-16 funding</li>
                <li>
                  Department of Education (DfE)/Education Funding Agency (EFA)
                  revenue grants
                </li>
                <li>other DfE or EFA revenue grants</li>
                <li>
                  other income (local authority and other government grants)
                </li>
                <li>government source (non-grant)</li>
              </ul>
            </div>
          </details>
          <HistoricChart
            chartName="Pre-16 and post-16 funding"
            data={data}
            seriesConfig={{
              prePost16Funding: {
                label: "Pre-16 and post-16 funding",
                visible: true,
              },
            }}
            valueField="prePost16Funding"
          >
            <h3 className="govuk-heading-s">Pre-16 and post-16 funding</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Other DfE/EFA revenue grants"
            data={data}
            seriesConfig={{
              otherDfeGrants: {
                label: "Other DfE/EFA revenue grants",
                visible: true,
              },
            }}
            valueField="otherDfeGrants"
          >
            <h3 className="govuk-heading-s">Other DfE/EFA revenue grants </h3>
          </HistoricChart>

          <HistoricChart
            chartName="Other income (local authority and other government grants)"
            data={data}
            seriesConfig={{
              otherIncomeGrants: {
                label:
                  "Other income (local authority and other government grants)",
                visible: true,
              },
            }}
            valueField="otherIncomeGrants"
          >
            <h3 className="govuk-heading-s">
              Other income (local authority and other government grants)
            </h3>
          </HistoricChart>

          <HistoricChart
            chartName="Government source (non-grant)"
            data={data}
            seriesConfig={{
              governmentSource: {
                label: "Government source (non-grant)",
                visible: true,
              },
            }}
            valueField="governmentSource"
          >
            <h3 className="govuk-heading-s">Government source (non-grant)</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Community grants"
            data={data}
            seriesConfig={{
              communityGrants: {
                label: "Community grants",
                visible: true,
              },
            }}
            valueField="communityGrants"
          >
            <h3 className="govuk-heading-s">Community grants</h3>
          </HistoricChart>

          <HistoricChart
            chartName="Academies"
            data={data}
            seriesConfig={{
              academies: {
                label: "Academies",
                visible: true,
              },
            }}
            valueField="academies"
          >
            <h3 className="govuk-heading-s">Academies</h3>
          </HistoricChart>
        </>
      ) : (
        <Loading />
      )}
    </>
  );
};
