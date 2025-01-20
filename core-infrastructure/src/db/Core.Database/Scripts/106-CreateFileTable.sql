IF NOT EXISTS(SELECT *
              FROM INFORMATION_SCHEMA.TABLES
              WHERE table_name = 'File')
    BEGIN
        CREATE TABLE [dbo].[File]
        (
            Type      nvarchar(50)   NOT NULL,
            Label     nvarchar(50)   NOT NULL,
            FileName  nvarchar(255)  NOT NULL,
            ValidFrom datetimeoffset NOT NULL DEFAULT GETUTCDATE(),
            ValidTo   datetimeoffset NULL

                CONSTRAINT PK_File PRIMARY KEY (Type, Label)
        );

        INSERT INTO [dbo].[File] (Type, Label, FileName)
        VALUES ('transparency-cfr', 'CFR 2022/23', 'CFR_2022-23_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2021/22', 'CFR_2021-22_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2020/21', 'CFR_2020-21_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2019/20', 'CFR_2019-20_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2018/19', 'CFR_2018-19_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2017/18', 'CFR_2017-18_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2016/17', 'CFR_2016-17_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2015/16', 'CFR_2015-16_Full_Data_Workbook.xlsx'),
               ('transparency-cfr', 'CFR 2014/15', 'CFR_2014-15_Full_Data_Workbook.xlsx'),
               ('transparency-aar', 'AAR 2022/23', 'AAR_2022-23_download.xlsx'),
               ('transparency-aar', 'AAR 2021/22', 'AAR_2021-22_download.xlsx'),
               ('transparency-aar', 'AAR 2020/21', 'AAR_2020-21_download.xlsx'),
               ('transparency-aar', 'AAR 2019/20', 'AAR_2019-20_download.xlsx'),
               ('transparency-aar', 'AAR 2018/19', 'AAR_2018-19_download.xlsx'),
               ('transparency-aar', 'AAR 2017/18', 'AAR_2017-18_download.xlsx'),
               ('transparency-aar', 'AAR 2016/17', 'AAR_2016-17_download.xlsx'),
               ('transparency-aar', 'AAR 2015/16', 'SFR32_2017_Main_Tables.xlsx'),
               ('transparency-aar', 'AAR 2014/15', 'SFR27_2016_Main_Tables.xlsx');

        -- intentionally insert these files as available in the distant future
        INSERT INTO [dbo].[File] (Type, Label, FileName, ValidFrom)
        VALUES ('transparency-cfr', 'CFR 2023/24', 'CFR_2023-24_Full_Data_Workbook.xlsx', DATEADD(year, 1, GETUTCDATE())),
               ('transparency-aar', 'AAR 2023/24', 'AAR_2023-24_download.xlsx', DATEADD(year, 1, GETUTCDATE()));
    END;
