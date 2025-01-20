Feature: Insights files endpoints

    Scenario: Sending a valid AAR transparency files request
        Given an AAR transparency files request
        When I submit the insights files request
        Then the result should be ok and contain:
          | Label       | FileName                    |
          | AAR 2014/15 | SFR27_2016_Main_Tables.xlsx |
          | AAR 2015/16 | SFR32_2017_Main_Tables.xlsx |
          | AAR 2016/17 | AAR_2016-17_download.xlsx   |
          | AAR 2017/18 | AAR_2017-18_download.xlsx   |
          | AAR 2018/19 | AAR_2018-19_download.xlsx   |
          | AAR 2019/20 | AAR_2019-20_download.xlsx   |
          | AAR 2020/21 | AAR_2020-21_download.xlsx   |
          | AAR 2021/22 | AAR_2021-22_download.xlsx   |
          | AAR 2022/23 | AAR_2022-23_download.xlsx   |

    Scenario: Sending a valid CFR transparency files request
        Given an CFR transparency files request
        When I submit the insights files request
        Then the result should be ok and contain:
          | Label       | FileName                            |
          | CFR 2014/15 | CFR_2014-15_Full_Data_Workbook.xlsx |
          | CFR 2015/16 | CFR_2015-16_Full_Data_Workbook.xlsx |
          | CFR 2016/17 | CFR_2016-17_Full_Data_Workbook.xlsx |
          | CFR 2017/18 | CFR_2017-18_Full_Data_Workbook.xlsx |
          | CFR 2018/19 | CFR_2018-19_Full_Data_Workbook.xlsx |
          | CFR 2019/20 | CFR_2019-20_Full_Data_Workbook.xlsx |
          | CFR 2020/21 | CFR_2020-21_Full_Data_Workbook.xlsx |
          | CFR 2021/22 | CFR_2021-22_Full_Data_Workbook.xlsx |
          | CFR 2022/23 | CFR_2022-23_Full_Data_Workbook.xlsx |