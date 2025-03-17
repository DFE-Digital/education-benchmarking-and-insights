using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityHighNeedsNationalRankingsViewModel(string code, string? commentary, string? valueLabel, LocalAuthorityRanking? result, int count)
{
    public string Code => code;
    public string? Commentary => commentary;
    public string? ValueLabel => valueLabel;

    public LocalAuthorityRank[] Closest
    {
        get
        {
            if (result == null)
            {
                return [];
            }

            if (count >= result.Ranking.Length)
            {
                return result.Ranking;
            }

            var window = new Queue<LocalAuthorityRank>(result.Ranking.Take(count).ToArray());
            if (result.Ranking.All(r => r.Code != code))
            {
                return window.ToArray();
            }

            var start = Convert.ToInt32(Math.Round(count / 2m));
            for (var i = 0; i < result.Ranking.Length; i++)
            {
                var current = result.Ranking[i];
                if (current.Code == code)
                {
                    return window.ToArray();
                }

                if (i < start)
                {
                    continue;
                }

                var next = result.Ranking.ElementAtOrDefault(i + count - start);
                if (next == null)
                {
                    return window.ToArray();
                }

                window.Enqueue(next);
                window.Dequeue();
            }

            return window.ToArray();
        }
    }
}