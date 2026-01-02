using Xunit;

namespace Web.E2ETests
{
    [CollectionDefinition(nameof(RequiresSigninCollection))] public class RequiresSigninCollection {  }
}

namespace Web.E2ETests.Features.LocalAuthority
{
    [Collection(nameof(RequiresSigninCollection))] public partial class LocalAuthorityAccessToSchoolCurriculumAndFinancialPlanningForItsSchools_Feature {  }
}

namespace Web.E2ETests.Features.School
{
    [Collection(nameof(RequiresSigninCollection))] public partial class SchoolAccessToParentsTrustForecastFeature {  }
    [Collection(nameof(RequiresSigninCollection))] public partial class SchoolCreateComparatorSetFeature {  }
    [Collection(nameof(RequiresSigninCollection))] public partial class SchoolCreateCustomDataFeature {  }
    [Collection(nameof(RequiresSigninCollection))] public partial class SchoolCurriculumAndFinancialPlanningFeature {  }
    [Collection(nameof(RequiresSigninCollection))] public partial class SchoolHomepageFeature {  }
}

namespace Web.E2ETests.Features.Trust
{
    [Collection(nameof(RequiresSigninCollection))] public partial class TrustBenchmarkITSpendingFeature {  }
    [Collection(nameof(RequiresSigninCollection))] public partial class ViewTrustComparatorSetFeature {  }
    [Collection(nameof(RequiresSigninCollection))] public partial class TrustCreateComparatorSetFeature {  }
    [Collection(nameof(RequiresSigninCollection))] public partial class TrustCurriculumAndFinancialPlanningFeature {  }
}


