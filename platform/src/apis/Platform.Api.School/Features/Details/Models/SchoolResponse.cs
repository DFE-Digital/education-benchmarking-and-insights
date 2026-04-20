using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Platform.Api.School.Features.Details.Models;

[ExcludeFromCodeCoverage]
public record SchoolResponse
{
    /// <summary>
    /// The unique reference number for the school.
    /// </summary>
    public string? URN { get; set; }

    /// <summary>
    /// The name of the school.
    /// </summary>
    public string? SchoolName { get; set; }

    /// <summary>
    /// The finance type of the school (e.g., Academy, Maintained).
    /// </summary>
    public string? FinanceType { get; set; }

    /// <summary>
    /// The overall phase of education (e.g., Primary, Secondary).
    /// </summary>
    public string? OverallPhase { get; set; }

    /// <summary>
    /// The type of school (e.g., Community School, Academy Converter).
    /// </summary>
    public string? SchoolType { get; set; }

    /// <summary>
    /// Indicates whether the school has a sixth form.
    /// </summary>
    public bool HasSixthForm { get; set; }

    /// <summary>
    /// Indicates whether the school has a nursery.
    /// </summary>
    public bool HasNursery { get; set; }

    /// <summary>
    /// Indicates whether the school is a Private Finance Initiative (PFI) school.
    /// </summary>
    public bool IsPFISchool { get; set; }

    /// <summary>
    /// The date of the school's last Ofsted inspection.
    /// </summary>
    public DateTime? OfstedDate { get; set; }

    /// <summary>
    /// The description of the school's Ofsted rating.
    /// </summary>
    public string? OfstedDescription { get; set; }

    /// <summary>
    /// The telephone number of the school.
    /// </summary>
    public string? Telephone { get; set; }

    /// <summary>
    /// The website URL of the school.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// The contact email address of the school.
    /// </summary>
    public string? ContactEmail { get; set; }

    /// <summary>
    /// The name of the school's headteacher.
    /// </summary>
    public string? HeadteacherName { get; set; }

    /// <summary>
    /// The email address of the school's headteacher.
    /// </summary>
    public string? HeadteacherEmail { get; set; }

    /// <summary>
    /// The company number of the trust the school belongs to (if applicable).
    /// </summary>
    public string? TrustCompanyNumber { get; set; }

    /// <summary>
    /// The name of the trust the school belongs to (if applicable).
    /// </summary>
    public string? TrustName { get; set; }

    /// <summary>
    /// The URN of the lead school in the federation (if applicable).
    /// </summary>
    public string? FederationLeadURN { get; set; }

    /// <summary>
    /// The name of the lead school in the federation (if applicable).
    /// </summary>
    public string? FederationLeadName { get; set; }

    /// <summary>
    /// The local authority code for the school.
    /// </summary>
    public string? LACode { get; set; }

    /// <summary>
    /// The name of the local authority.
    /// </summary>
    public string? LAName { get; set; }

    /// <summary>
    /// The London weighting area of the school.
    /// </summary>
    public string? LondonWeighting { get; set; }

    /// <summary>
    /// The street address of the school.
    /// </summary>
    public string? AddressStreet { get; set; }

    /// <summary>
    /// The locality of the school's address.
    /// </summary>
    public string? AddressLocality { get; set; }

    /// <summary>
    /// Address line 3 of the school.
    /// </summary>
    public string? AddressLine3 { get; set; }

    /// <summary>
    /// The town of the school's address.
    /// </summary>
    public string? AddressTown { get; set; }

    /// <summary>
    /// The county of the school's address.
    /// </summary>
    public string? AddressCounty { get; set; }

    /// <summary>
    /// The postcode of the school's address.
    /// </summary>
    public string? AddressPostcode { get; set; }

    /// <summary>
    /// A collection of schools within the same federation.
    /// </summary>
    public IEnumerable<SchoolResponse>? FederationSchools { get; set; }

    /// <summary>
    /// The full formatted address of the school.
    /// </summary>
    public string Address => string.Join(", ", new List<string?>
    {
        AddressStreet,
        AddressLocality,
        AddressLine3,
        AddressTown,
        AddressCounty,
        AddressPostcode
    }.Where(x => !string.IsNullOrEmpty(x)));
}