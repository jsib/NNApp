USE [MLG-Supply01]
GO

/****** Object:  Table [dbo].[NewspaperMailMessage]    Script Date: 27.09.2018 13:40:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewspaperMailMessage](
	[MessageType] [int] NULL,
	[MessageText] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

