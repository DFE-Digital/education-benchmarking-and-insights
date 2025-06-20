// ReSharper disable RawStringCanBeSimplified
namespace Web.App.ViewModels;

public class HighNeedsGlossaryViewModel : GlossaryViewModel
{
    public GlossaryItem[] HighNeedsGlossary =>
    [
        new()
        {
            Term = "Balance carried forward",
            Meaning = """
                      Balance carried forward is a cumulative number that is 
                      decreased each year by any shortfall amount and 
                      increased each year by any excess amount.   

                      The s251 outturn statement will show the overall deficit 
                      or surplus the local authority is carrying compared to the 
                      funding they receive for the provision of education.
                      """
        },
        new()
        {
            Term = "Budget",
            Meaning = """
                      A financial plan outlining how allocated funds will be 
                      spent over the upcoming period. In this service this refers 
                      to the s251 budget statement. 

                      The budget calculations are carried out inclusive of the 
                      academy allocation. 
                      """
        },
        new()
        {
            Term = "DSG allocation",
            Meaning = """
                      Dedicated Schools Grant. Money allocated by 
                      Department for Education to local authorities for specific 
                      education related services. 

                      This is divided into the blocks it should be spent in and 
                      includes: 

                      * schools block (calculated based on national funding formula)
                      * high needs block
                      * early years block
                      * central school services block 
                      """
        },
        new()
        {
            Term = "In year balance",
            Meaning = """
                      The financial balance of a school or a local authority 
                      within a specific year, considering both income and 
                      expenditure.  

                      The in-year balance determines whether the school or 
                      authority has a surplus (spending less than income) or a 
                      deficit (spending more than income) for that particular 
                      year.
                      """
        },
        new()
        {
            Term = "National Funding",
            Meaning = """
                      Formula The methodology used by the DfE to determine the per 
                      pupil amount allocated to the schools block for each local 
                      authority.  
                      """
        },
        new()
        {
            Term = "Outturn",
            Meaning = """
                      The outturn is the actual amount spent on high needs by 
                      local authorities in each financial year.  This is taken from 
                      the s251 outturn statement.

                      **Use of outturn in this service** 

                      In this service the academy recoupment amount is added 
                      to the high needs spend reported by local authorities in 
                      their s251 outturn statement. This combined figure can 
                      then be compared against the high needs block amount 
                      from the DSG for the year.
                      """
        },
        new()
        {
            Term = "Place funding",
            Meaning = """
                      Place funding is allocated as an annual amount of core 
                      high needs funding for schools and colleges.  

                      Place funding is allocated at a standard rate for a number 
                      of places, reflecting the number of high needs 
                      placements which commissioning local authorities are 
                      expected to require in the period.
                      """
        },
        new()
        {
            Term = "S251",
            Meaning = """
                      The regulatory requirement to prepare and submit 
                      annual, education and children and young people s 
                      services, budget and outturn statements for each 
                      financial year.
                      """
        },
        new()
        {
            Term = "Top-up funding",
            Meaning = """
                      The additional financial support allocated by local 
                      authorities to schools for pupils with Education, Health, 
                      and Care Plans (EHCPs). This makes up the difference 
                      between the cost of meeting the needs in the EHCP and 
                      the place funding element of high needs for each pupil.
                      """
        }
    ];
}

public class GlossaryViewModel
{
    public GlossaryItem[] GeneralGlossary =>
    [
        new()
        {
            Term = "AAR",
            Meaning = """
                      Academy Accounts Return
                      """
        },
        new()
        {
            Term = "AP",
            Meaning = """
                      Alternative provision. This relates to academies.
                      """
        },
        new()
        {
            Term = "BFR",
            Meaning = """
                      Budget forecast return. Data collected for academic and 
                      financial year analysis by Department for Education.  This 
                      applies to the academy sector only.
                      """
        },
        new()
        {
            Term = "CFP",
            Meaning = """
                      Curriculum and Financial Planning. This is a management 
                      process that has been tailored for FBIT from the Integrated 
                      Curriculum and Financial Planning (ICFP) process. This 
                      helps schools plan the best curriculum for their pupils with 
                      the funding they have available.
                      """
        },
        new()
        {
            Term = "CFR",
            Meaning = """
                      Consistent Financial Reporting. The consistent financial 
                      reporting (CFR) framework provides a standard template 
                      for schools to collect information about their income and 
                      expenditure by financial years. Maintained schools then 
                      provide this information to their local authorities in a 
                      financial statement each year
                      """
        },
        new()
        {
            Term = "FBIS",
            Meaning = """
                      A summary showing highlights of your schools spending
                      compared with a number of similar schools. This replaced
                      Benchmarking Report Cards (BRC).
                      """
        },
        new()
        {
            Term = "FSM",
            Meaning = """
                      Free school meals
                      """
        },
        new()
        {
            Term = "High executive pay",
            Meaning = """
                      Financial compensation for senior executive staff in 
                      academy trusts.
                      """
        },
        new()
        {
            Term = "IDACI",
            Meaning = """
                      Income deprivation affecting children index
                      """
        },
        new()
        {
            Term = "KS2",
            Meaning = """
                      Key stage 2
                      """
        },
        new()
        {
            Term = "KS4",
            Meaning = """
                      Key stage 4
                      """
        },
        new()
        {
            Term = "LA",
            Meaning = """
                      Local authority
                      """
        },
        new()
        {
            Term = "National funding formula",
            Meaning = """
                      The way government decides how much core funding 
                      should be allocated for mainstream state funded schools.
                      """
        },
        new()
        {
            Term = "NMSS",
            Meaning = """
                      Non maintained special school (or independent special 
                      school)
                      """
        },
        new()
        {
            Term = "Ofsted rating",
            Meaning = """
                      A grade given to schools and other educational 
                      establishments by the Office for Standards in Education, 
                      Children's Services and Skills.
                      """

        },
        new()
        {
            Term = "ONS",
            Meaning = """
                      Office for National Statistics
                      """
        },
        new()
        {
            Term = "Post-school",
            Meaning = """
                      Further education (FE) colleges, sixth form colleges, 
                      independent colleges, special post-16 institutions and 
                      other post-16 providers that do not provide for pupils of 
                      compulsory school age, including 16-19 maintained 
                      schools and academies.
                      """
        },
        new()
        {
            Term = "PRU",
            Meaning = """
                      Pupil referral unit. These are local authority maintained.
                      """
        },
        new()
        {
            Term = "RAG",
            Meaning = """
                      Red, amber and green (RAG) priority ratings are shown 
                      to give an indication of the spending compared to similar 
                      schools. 
                              
                      The rating is not an indication of performance. It is used to
                      display if spending is significantly more or less than similar 
                      schools. This does not consider any individual spending 
                      strategies which might apply. 
                              
                      The ratings are intended for schools and trusts to identify 
                      potential areas to help them make informed spending 
                      decisions. 
                      """
        },
        new()
        {
            Term = "SEN",
            Meaning = """
                      Special educational needs
                      """
        },
        new()
        {
            Term = "SEN2",
            Meaning = """
                      A statutory annual data collection of special educational 
                      needs.
                      """
        },
        new()
        {
            Term = "VCU",
            Meaning = """
                      Vulnerable children's unit
                      """
        }
    ];
}

public class GlossaryItem
{
    public string? Term { get; init; }
    public string? Meaning { get; init; }
}