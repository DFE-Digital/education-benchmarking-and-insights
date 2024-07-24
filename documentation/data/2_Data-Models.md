# Data Models

## Entity-Relationship Diagrams

### Raw

![Raw Data Model](./images/fbit_raw_data_model.png)

### Preprocessed

![Preprocessed Data Model](./images/fbit_preprocessed_data_model.png)
### SQL

![SQL Data Model](./images/fbit_sql_data_model.png)


## Element Transformative Flow

This section looks at each item listed in the SQL data model, and traces them back through their variable names within the pre-processing pipeline, to their source in the raw data file. In cases where the value is computed, or has undergone some logical process, we highlight the computation in the `notes on transformation` column.

It is also key to note that within the service, metrics such as "cost per pupil" are computed. However this has been ommited from this section to enable a more suscinct translation of the data schema. In cases where a "cost per pupil" or "cost per m2" unit is presented, this is simply the given metric divided by the `TotalPupils`, or the `GrossInternalFloorArea`, all of which can be found from the information below.

### Financial

|  raw file name |  raw column name |  pre-processing column name | sql table column name |  notes on transformation |
|------------------|----------------|-----------------------------|-----------------------|--------------------------|
|maintained_schools_master_list| Total Expenditure  E01 to E32  |     Total Expenditure    |TotalExpenditure  |     |  
|N/A - computed     |   |   |TotalTeachingSupportStaffCosts |Computed as the sum of Teaching and Teaching support, staff_Teaching staff, Teaching and Teaching support staff_Supply teaching staff, Teaching and Teaching support staff_Educational consultancy, Teaching and Teaching support staff_Education support staff, Teaching and Teaching support staff_Agency supply teaching staff|  
|maintained_schools_master_list| E01  Teaching Staff |    Teaching and Teaching support staff_Teaching staff    |TeachingStaffCosts  ||  
|maintained_schools_master_list| E02  Supply teaching staff  |     Teaching and Teaching support staff_Supply teaching staff    |SupplyTeachingStaffCosts  ||  
|maintained_schools_master_list|E03 Education support staff  |Teaching and Teaching support staff_Educational consultancy    |EducationSupportStaffCosts  ||  
|maintained_schools_master_list| E26 Agency supply teaching staff  |     Teaching and Teaching support staff_Agency supply teaching staff    |AgencySupplyTeachingStaffCosts  ||  
|maintained_schools_master_list| E05 Administrative and clerical staff  |     Non-educational support staff and services_Administrative and clerical staff    |AdministrativeClericalStaffCosts  ||
|maintained_schools_master_list| E07  Cost of other staff  |     Non-educational support staff and services_Other staff    |OtherStaffCosts  ||  
|N/A - computed|   |   |TotalNonEducationalSupportStaffCosts |Computed as the sum of Non-educational support staff and services_Administrative and clerical staff, Non-educational support staff and services_Other staff, Non-educational support staff and services_Professional services (non-curriculum)     |  
|maintained_schools_master_list| E28a  Bought in professional services - other (except PFI)  |     Non-educational support staff and services_Professional services (non-curriculum)    |ProfessionalServicesNonCurriculumCosts  ||  
|N/A - computed|   |   |TotalEducationalSuppliesCosts | Computed as the sum of Educational supplies_Examination fees, Educational supplies_Learning resources (not ICT equipment)   |  
|maintained_schools_master_list| E21  Exam fees  |     Educational supplies_Examination fees    |ExaminationFeesCosts  ||  
|maintained_schools_master_list| E19  Learning resources (not ICT equipment)  |     Educational supplies_Learning resources (not ICT equipment)    |LearningResourcesNonIctCosts  ||  
|maintained_schools_master_list| E20  ICT learning resources  |     Educational ICT_ICT learning resources    |LearningResourcesIctCosts  ||  
|N/A - computed|   |   |TotalPremisesStaffServiceCosts    |Computed as the sum of Premises staff and services_Cleaning and caretaking, Premises staff and services_Maintenance of premises, Premises staff and services_Other occupation costs, Premises staff and services_Premises staff |  
|maintained_schools_master_list| E14  Cleaning and caretaking  |     Premises staff and services_Cleaning and caretaking    |CleaningCaretakingCosts  ||  
|maintained_schools_master_list| E12  Building maintenance and improvement  |     Premises staff and services_Maintenance of premises    |MaintenancePremisesCosts  ||  
|maintained_schools_master_list| E18  Other occupation costs  |     Premises staff and services_Other occupation costs    |OtherOccupationCosts  ||  
|maintained_schools_master_list| E04  Premises staff  |     Premises staff and services_Premises staff    |PremisesStaffCosts  ||  
|N/A - computed|   |   |TotalUtilitiesCosts  |Computed as the sum of Utilities_Energy, Utilities_Water and sewerage|  
|maintained_schools_master_list| E16  Energy  |     Utilities_Energy    |EnergyCosts  ||  
|maintained_schools_master_list| E15  Water and sewerage  |     Utilities_Water and sewerage    |WaterSewerageCosts  ||  
|maintained_schools_master_list| E22 Administrative supplies  |     Administrative supplies_Administrative supplies (non educational)    |AdministrativeSuppliesNonEducationalCosts  ||  
|N/A - computed|   |   |TotalGrossCateringCosts |    Computed as the sum of Catering staff and supplies_Catering staff, Catering staff and supplies_Catering supplies     |  
|maintained_schools_master_list| E06 Catering staff  |     Catering staff and supplies_Catering staff    |CateringStaffCosts  ||  
|maintained_schools_master_list| E25  Catering supplies  |     Catering staff and supplies_Catering supplies    |CateringSuppliesCosts  ||  
|N/A - computed|   |   |TotalOtherCosts  |Computed as the sum of,Other costs_Other insurance premiums,Other costs_Direct revenue financing,Other costs_Grounds maintenance,Other costs_Indirect employee expense,Other costs_Interest charges for loan and bank,Other costs_PFI charges,Other costs_Rent and rates,Other costs_Special facilities,Other costs_Staff development and training,Other costs_Staff-related insurance,Other costs_Supply teacher insurance
|maintained_schools_master_list| E23  Other insurance premiums  |     Other costs_Other insurance premiums    |OtherInsurancePremiumsCosts  ||  
|maintained_schools_master_list| E30 Direct revenue financing (revenue contributions to capital)  |     Other costs_Direct revenue financing    |DirectRevenueFinancingCosts  ||  
|maintained_schools_master_list| E13  Grounds maintenance and improvement  |     Other costs_Grounds maintenance    |GroundsMaintenanceCosts  ||  
|maintained_schools_master_list| E08  Indirect employee expenses  |     Other costs_Indirect employee expenses    |IndirectEmployeeExpenses  ||  
|maintained_schools_master_list| E29  Loan interest  |     Other costs_Interest charges for loan and bank    |InterestChargesLoanBank  ||  
|maintained_schools_master_list| E28b Bought in professional services - other (PFI)  |     Other costs_PFI charges    |PrivateFinanceInitiativeCharges  ||  
|maintained_schools_master_list| E17  Rates  |     Other costs_Rent and rates    |RentRatesCosts  ||  
|maintained_schools_master_list| E24  Special facilities  |     Other costs_Special facilities    |SpecialFacilitiesCosts  ||  
|maintained_schools_master_list| E09  Development and training  |     Other costs_Staff development and training    |StaffDevelopmentTrainingCosts  ||  
|maintained_schools_master_list| E11  Staff related insurance  |     Other costs_Staff-related insurance    |StaffRelatedInsuranceCosts  ||  
|maintained_schools_master_list| E10  Supply teacher insurance  |     Other costs_Supply teacher insurance    |SupplyTeacherInsurableCosts  ||  
|maintained_schools_master_list| E31  Community focused school staff  |     Other costs_School staff    |CommunityFocusedSchoolStaff  ||  
|maintained_schools_master_list| E32 Community focused school costs  |     Other costs_School costs    |CommunityFocusedSchoolCosts  ||  
|N/A - computed|   |   |TotalTeachingSupportStaffCosts  |Computed as the sum of Teaching and Teaching support staff_Teaching staff, Teaching and Teaching support staff_Supply teaching staff, Teaching and Teaching support staff_Educational consultancy, Teaching and Teaching support staff_Education support staff, Teaching and Teaching support staff_Agency supply teaching staff  ||   
|maintained_schools_master_list| E27  Bought in professional services - curriculum  |     Teaching and Teaching support staff_Educational consultancy    |EducationalConsultancyCosts  ||  
|N/A - computed|   |   |TotalNonEducationalSupportStaffCosts  |Computed as the sum of Non-educational support staff and services_Administrative and clerical staff, Non-educational support staff and services_Other staff, Non-educational support staff and services_Professional services (non-curriculum)    |
|maintained_schools_master_list| E23  Other insurance premiums  |     Other costs_Other insurance premiums    |OtherInsurancePremiumsCosts  ||  
|     1a| 1b  |     1c    |1d  |   1e |
|     1a| 1b  |     1c    |1d  |   1e |
|     1a| 1b  |     1c    |1d  |   1e |
|     1a| 1b  |     1c    |1d  |   1e |
|     1a| 1b  |     1c    |1d  |   1e |
|     1a| 1b  |     1c    |1d  |   1e |
|     1a| 1b  |     1c    |1d  |   1e |


### Non-financial

|  raw file name |  raw column name |  pre-processing column name | sql table column name |  notes on transformation |
|------------------|----------------|-----------------------------|-----------------------|--------------------------|
|     census_pupils| fte pupils  |     Number of pupils    |TotalPupils  |    |
|     census_pupils| % of pupils known to be eligible for free school meals  |     Percentage Free school meals    |PercentFreeSchoolMeals  |    |
|     sen| Total pupils  |     N/A - used in computation    |N/A - used in computation  |    |
|     sen| SEN support  |N/A - used in computation   |N/A - used in computation  |    |
|  sen| EHC plan  |  N/A - used in computation |N/A - used in computation  |    |
|     N/A - computed|   |     Percentage SEN| PercentSpecialEducationNeeds|   Percentage SEN computed as ((EHC plan + SEN support) / Total pupils) * 100.0 |
|     cdc| GIFA  |     Total Internal Floor Area    |TotalInternalFloorArea  |   TotalInternalFloorArea is the aggregated sum of the floor area of each building associated with a single URN |
|     census_workforce| Total School Workforce (Full-Time Equivalent)  |     Total School Workforce (Full-Time Equivalent)    |WorkforceFTE  ||  
| census_workforce    | Total Number of Teachers (Full-Time Equivalent)  |     Total Number of Teachers (Full-Time Equivalent)    |TeachersFTE  ||  
| census_workforce    | Teachers with Qualified Teacher Status (%) (Headcount)  |     Teachers with Qualified Teacher Status (%) (Headcount)    |PercentTeacherWithQualifiedStatus  ||  
| census_workforce    | Total Number of Teachers in the Leadership Group (Full-time Equivalent)  |     Total Number of Teachers in the Leadership Group (Full-time Equivalent)    |SeniorLeadershipFTE  ||  
|   census_workforce  | Total Number of Teaching Assistants (Full-Time Equivalent)  |     Total Number of Teaching Assistants (Full-Time Equivalent)    |TeachingAssistantFTE  ||  
|  census_workforce   | Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)  |     NonClassroomSupportStaffFTE    |NonClassroomSupportStaffFTE  ||  
|  census_workforce   | Total Number of Auxiliary Staff (Full-Time Equivalent)  |     Total Number of Auxiliary Staff (Full-Time Equivalent)    |AuxiliaryStaffFTE  ||  
|   census_workforce  | Total School Workforce (Headcount)  |     Total School Workforce (Headcount)    |WorkforceHeadcount  ||  

### School


|  raw file name |  raw column name |  pre-processing column name | sql table column name |  notes on transformation |
|------------------|----------------|-----------------------------|-----------------------|--------------------------|
|     gias| URN  |     URN    |URN  |    |
|     gias| Street  |     Street    |AddressStreet  |    |
|     gias| Locality  |     Locality    |AddressLocality  |    |
|     gias| Address3  |     Address3    |AddressLine3  |    |
|     gias| Town  |     Town    |AddressTown  |    |
|     gias| County (name)  |     County (name)    |AddressCounty  |    |
|     gias| Postcode  |     Postcode    |AddressPostcode  |    |
|     gias| TelephoneNum  |     TelephoneNum    |Telephone  |    |
|     gias| LA (name)  |     LA Name    |LAName  |    |
|     gias| SchoolWebsite  |     SchoolWebsite    |Website  |    |