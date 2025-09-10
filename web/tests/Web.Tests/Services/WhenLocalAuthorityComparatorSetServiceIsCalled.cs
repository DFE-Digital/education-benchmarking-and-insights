using System.Text;
using Microsoft.AspNetCore.Http;
using Moq;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Services;
using Web.Tests.Stubs;
using Xunit;

namespace Web.Tests.Services;

public class WhenLocalAuthorityComparatorSetServiceIsCalled
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
    private readonly LocalAuthorityComparatorSetService _service;
    private readonly SessionStub _session = new();

    public WhenLocalAuthorityComparatorSetServiceIsCalled()
    {
        var context = new DefaultHttpContext
        {
            Session = _session
        };

        _httpContextAccessor.Setup(a => a.HttpContext).Returns(context);
        _service = new LocalAuthorityComparatorSetService(_httpContextAccessor.Object);
    }

    [Fact]
    public void ShouldReadComparatorSetFromSessionIfPresent()
    {
        const string code = nameof(code);
        var key = SessionKeys.LocalAuthorityComparatorSetUserDefined(code);
        var set = new UserDefinedLocalAuthorityComparatorSet
        {
            Set = ["123", "456", "789"]
        };
        _session.Set(key, GetBytes(set));

        var result = _service.ReadUserDefinedComparatorSetFromSession(code);

        Assert.NotNull(result);
        Assert.Equivalent(set, result);
    }

    [Fact]
    public void ShouldSetAndReturnNewComparatorSetFromSessionIfNotPresent()
    {
        const string code = nameof(code);

        var result = _service.ReadUserDefinedComparatorSetFromSession(code);

        Assert.NotNull(result);
        Assert.Equivalent(new UserDefinedLocalAuthorityComparatorSet(), result);
        Assert.Single(_session.Keys);
        Assert.Equal(SessionKeys.LocalAuthorityComparatorSetUserDefined(code), _session.Keys.Single());
    }

    [Fact]
    public void ShouldSetComparatorSetInSession()
    {
        const string code = nameof(code);
        var set = new UserDefinedLocalAuthorityComparatorSet
        {
            Set = ["123", "456", "789"]
        };

        var result = _service.SetUserDefinedComparatorSetInSession(code, set);

        Assert.NotNull(result);
        Assert.Equivalent(set, result);
        Assert.Single(_session.Keys);
        Assert.Equal(SessionKeys.LocalAuthorityComparatorSetUserDefined(code), _session.Keys.Single());
    }

    [Fact]
    public void ShouldClearComparatorSetFromSession()
    {
        const string code = nameof(code);
        var key1 = SessionKeys.LocalAuthorityComparatorSetUserDefined(code);
        var key2 = SessionKeys.LocalAuthorityComparatorSetUserDefined(code + code);
        var set = new UserDefinedLocalAuthorityComparatorSet
        {
            Set = ["123", "456", "789"]
        };
        _session.Set(key1, GetBytes(set));
        _session.Set(key2, GetBytes(set));

        _service.ClearUserDefinedComparatorSetFromSession(code);

        Assert.Single(_session.Keys);
        Assert.Equal(key2, _session.Keys.Single());
    }

    private static byte[] GetBytes(UserDefinedLocalAuthorityComparatorSet value) => Encoding.UTF8.GetBytes(value.ToJson());
}