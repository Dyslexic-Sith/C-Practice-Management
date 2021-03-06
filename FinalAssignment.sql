USE [E:\PETER\DATAACCESS\PRACTICEMANAGEMENT\PRACTICEMANAGEMENT\PRACTICEMANAGEMENT\BIN\DEBUG\PRACTICEMANAGEMENT_SC.MDF]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 23/11/2016 1:45:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[Patient_ID] [nchar](8) NOT NULL,
	[FirstName] [nvarchar](15) NULL,
	[Surname] [nvarchar](20) NULL,
	[Gender] [char](1) NULL,
	[Date_Of_Birth] [datetime] NULL,
	[Address] [nvarchar](50) NULL,
	[Suburb] [nvarchar](15) NULL,
	[Postcode] [nvarchar](4) NULL,
	[Medicare_No] [nvarchar](10) NULL,
	[Phone] [nvarchar](10) NULL,
	[Private_Health] [nvarchar](25) NULL,
	[Disease_History] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Patient_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
