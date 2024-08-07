﻿using Web.App.Domain;
using Web.App.Infrastructure.Apis;
namespace Web.App.ViewModels;

public class TrustComparatorsSubmittedViewModel(Trust trust, PutComparatorSetUserDefinedRequest request, bool isEdit)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public Guid Identifier => request.Identifier;
    public bool IsEdit => isEdit;
}