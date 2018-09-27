USE [MLG-Supply01]
GO

/****** Object:  Table [dbo].[Newspaper]    Script Date: 27.09.2018 13:43:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Newspaper](
	[Newspaper_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[NewspaperName] [nvarchar](255) NOT NULL,
	[addressEdition] [varchar](255) NULL,
	[chiefEditor] [varchar](255) NULL,
	[founder] [varchar](255) NULL,
	[id_Category] [int] NULL,
	[id_EditionType] [int] NULL,
	[periodicity] [varchar](255) NULL,
	[stripsAmount] [int] NULL,
	[id_Format] [int] NULL,
	[generalCirculation] [int] NULL,
	[influenceGroup] [varchar](255) NULL,
	[editionPhone] [varchar](50) NULL,
	[id_TownEdition] [int] NULL,
	[pageURL] [varchar](255) NULL,
	[influence] [varchar](50) NULL,
	[newsPaperInfo] [nvarchar](max) NULL,
	[beginDate] [smalldatetime] NOT NULL,
	[endDate] [smalldatetime] NULL,
	[status] [tinyint] NOT NULL,
	[priorityGroup] [tinyint] NOT NULL,
	[pictureType] [varchar](50) NULL,
	[pictureData] [image] NULL,
	[newsPaperStatus] [tinyint] NOT NULL,
	[code] [varchar](10) NULL,
	[rep] [int] NULL,
	[levelId] [int] NOT NULL,
	[priority] [tinyint] NOT NULL,
	[processing] [int] NULL,
	[activation_date] [datetime] NULL,
	[group_issues] [bit] NOT NULL,
	[gallup_fed] [float] NULL,
	[gallup_mos] [float] NULL,
	[version] [int] NULL,
	[publisherId] [smallint] NULL,
	[operativity] [int] NULL,
	[Country_ID] [int] NOT NULL,
	[Lang_ID] [int] NOT NULL,
	[Attached] [int] NOT NULL,
	[supplier_id] [int] NULL,
	[deleteDupes] [bit] NOT NULL,
	[object_id] [int] NULL,
	[ext_supplier_id] [int] NOT NULL,
	[BranchId] [int] NULL,
	[articlesType] [tinyint] NULL,
	[legalize] [tinyint] NOT NULL,
	[visible] [tinyint] NOT NULL,
	[PublishingHouse_ID] [int] NULL,
	[DefaultGenre] [int] NOT NULL,
	[id_group] [int] NOT NULL,
	[client] [varchar](200) NULL,
	[IssueTime] [datetime] NULL,
	[ClosedReason_Id] [int] NULL,
	[CustomProperties] [xml](DOCUMENT [dbo].[BDic_Newspapers_CustomProperties]) NOT NULL,
	[ServerId] [int] NOT NULL,
	[email] [varchar](255) NULL,
	[comments] [varchar](max) NULL,
	[LastUpdateDate] [datetime] NULL,
	[SourceTypeId] [int] NOT NULL,
	[LifestyleId] [int] NOT NULL,
	[ImageId] [uniqueidentifier] NULL,
	[ReceiveChannel] [nvarchar](500) NULL,
	[VkId] [bigint] NULL,
	[autoGeneralCirculation] [int] NULL,
	[AlexaData] [xml] NULL,
	[UserId] [int] NULL,
	[CreatedByUserId] [int] NULL,
	[ClosedReason_Comment] [varchar](2000) NULL,
 CONSTRAINT [PK_BDic_NewspapersNew] PRIMARY KEY CLUSTERED 
(
	[Newspaper_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

