using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewModels.Components;

public class GivenALocalAuthoritySchoolFilterAccordionSectionViewModel
{
    private enum TestEnum
    {
        A = 1,
        B = 2,
        C = 3
    }

    [Fact]
    public void ShouldSetBaseProperties()
    {
        const string accordionId = nameof(accordionId);
        const int sectionIndex = 1;
        const string formPrefix = nameof(formPrefix);
        const string heading = nameof(heading);
        const string formFieldName = nameof(formFieldName);
        TestEnum[] allFilters = [TestEnum.A, TestEnum.B, TestEnum.C];
        TestEnum[] selectedFilters = [TestEnum.A, TestEnum.B];
        string LabelSelector(TestEnum e) => $"Label {e}";
        string ValueSelector(TestEnum e) => $"Value {(int)e}";

        LocalAuthoritySchoolFilterAccordionSectionViewModelBase model = new LocalAuthoritySchoolFilterAccordionSectionViewModel<TestEnum>(accordionId, sectionIndex, formPrefix, heading, formFieldName, allFilters, selectedFilters, LabelSelector, ValueSelector);

        Assert.Equal(accordionId, model.AccordionId);
        Assert.Equal(sectionIndex, model.SectionIndex);
        Assert.Equal(heading, model.Heading);
        Assert.Equal($"{formPrefix}{formFieldName}", model.FormFieldName);
        Assert.Equal([TestEnum.A, TestEnum.B, TestEnum.C], model.AllFilters);
        Assert.Equal([TestEnum.A, TestEnum.B], model.SelectedFilters);
        Assert.Equal("Label A", model.LabelSelector(TestEnum.A));
        Assert.Equal("Value 2", model.ValueSelector(TestEnum.B));
    }
}