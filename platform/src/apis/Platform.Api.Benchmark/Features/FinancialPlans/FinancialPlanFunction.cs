using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Json;
using Platform.Sql;

namespace Platform.Api.Benchmark.Features.FinancialPlans;

public class FinancialPlanFunction(IDatabaseFactory dbFactory)
{
    [Function(nameof(FinancialPlanFunction))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = Routes.FinancialPlan)] HttpRequestData req,
        string urn,
        int year,
        CancellationToken cancellationToken = default)
    {
        var plan = await req.ReadAsJsonAsync<FinancialPlanDetails>(cancellationToken);
        if (plan == null) return req.CreateResponse(HttpStatusCode.BadRequest);

        using var conn = await dbFactory.GetConnection();

        // Ensure referential integrity by validating against the School table
        const string schoolSql = "SELECT URN FROM School WHERE URN = @URN";
        var schoolUrn = await conn.QueryFirstOrDefaultAsync<string>(schoolSql, new { URN = urn });
        if (schoolUrn == null) return req.CreateResponse(HttpStatusCode.NotFound);

        // Select ONLY necessary columns to check for existing record and manage versioning
        const string selectSql = "SELECT URN, Year, Version FROM FinancialPlan WHERE URN = @URN AND Year = @Year";
        var existing = await conn.QueryFirstOrDefaultAsync<dynamic>(selectSql, new { URN = urn, Year = year });

        // Logic for deriving key efficiency metrics (Teacher Contact Ratio, Average Class Size, In-Year Balance)
        // These values would be calculated based on the complex 'plan' inputs provided
        decimal? teacherContactRatio = null; 
        string? contactRatioRating = null;
        decimal? inYearBalance = null;
        string? inYearBalancePercentIncomeRating = null;
        decimal? averageClassSize = null;
        string? averageClassSizeRating = null;

        if (existing == null)
        {
            const string insertSql = @"
                INSERT INTO FinancialPlan (
                    URN, Year, Input, Created, CreatedBy, UpdatedAt, UpdatedBy, IsComplete, Version,
                    TeacherContactRatio, ContactRatioRating, InYearBalance, InYearBalancePercentIncomeRating, AverageClassSize, AverageClassSizeRating
                ) VALUES (
                    @URN, @Year, @Input, @Created, @CreatedBy, @UpdatedAt, @UpdatedBy, @IsComplete, @Version,
                    @TeacherContactRatio, @ContactRatioRating, @InYearBalance, @InYearBalancePercentIncomeRating, @AverageClassSize, @AverageClassSizeRating
                )";

            await conn.ExecuteAsync(insertSql, new
            {
                URN = urn,
                Year = year,
                Input = plan.ToJson(),
                Created = DateTimeOffset.UtcNow,
                CreatedBy = plan.UpdatedBy,
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = plan.UpdatedBy,
                IsComplete = plan.IsComplete,
                Version = 1,
                TeacherContactRatio = teacherContactRatio,
                ContactRatioRating = contactRatioRating,
                InYearBalance = inYearBalance,
                InYearBalancePercentIncomeRating = inYearBalancePercentIncomeRating,
                AverageClassSize = averageClassSize,
                AverageClassSizeRating = averageClassSizeRating
            });

            return req.CreateResponse(HttpStatusCode.Created);
        }

        const string updateSql = @"
            UPDATE FinancialPlan SET 
                Input = @Input, 
                UpdatedAt = @UpdatedAt, 
                UpdatedBy = @UpdatedBy, 
                IsComplete = @IsComplete, 
                Version = Version + 1,
                TeacherContactRatio = @TeacherContactRatio,
                ContactRatioRating = @ContactRatioRating,
                InYearBalance = @InYearBalance,
                InYearBalancePercentIncomeRating = @InYearBalancePercentIncomeRating,
                AverageClassSize = @AverageClassSize,
                AverageClassSizeRating = @AverageClassSizeRating
            WHERE URN = @URN AND Year = @Year";

        await conn.ExecuteAsync(updateSql, new
        {
            URN = urn,
            Year = year,
            Input = plan.ToJson(),
            UpdatedAt = DateTimeOffset.UtcNow,
            UpdatedBy = plan.UpdatedBy,
            IsComplete = plan.IsComplete,
            TeacherContactRatio = teacherContactRatio,
            ContactRatioRating = contactRatioRating,
            InYearBalance = inYearBalance,
            InYearBalancePercentIncomeRating = inYearBalancePercentIncomeRating,
            AverageClassSize = averageClassSize,
            AverageClassSizeRating = averageClassSizeRating
        });

        return req.CreateResponse(HttpStatusCode.NoContent);
    }
}
