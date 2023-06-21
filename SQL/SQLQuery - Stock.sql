USE [TWSETrade]
GO

/****** Object:  Table [dbo].[Stock]    Script Date: 2023/6/19 ¤W¤È 09:48:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Stock](
	[StockId] [varchar](10) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[CreateUser] [varchar](20),
	[CreateDate] [datetime],
	[UpdateUser] [varchar](20),
	[UpdateDate] [datetime],
	PRIMARY KEY (StockId) /*PK*/
)
GO