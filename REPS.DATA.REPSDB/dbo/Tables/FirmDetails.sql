CREATE TABLE [dbo].[FirmDetails]
(
	[FirmID] int NOT NULL IDENTITY (1,1) PRIMARY KEY,
    [FirmName] varchar(255),
    [FirmAddress] varchar(255),
    [FirmTelNo] varchar(15),
    [FirmFaxNo] varchar(15),
	[FirmwebURL] varchar(255),
	[DirectorName] varchar(100),
	[DirectorSurname] varchar(100),
	[CCManagerName] varchar(100),
	[CCManagerSurname] varchar(100),
	[CCTitle] varchar(5),
	[CCContactNumber] varchar(15),
	[CCEmail] varchar(255),
	[CCAddress] varchar(255),
	[CCWeb] varchar(255)
)