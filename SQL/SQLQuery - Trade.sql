USE [TWSETrade]
GO

/****** Object:  Table [dbo].[Trade]    Script Date: 2023/6/20 ¤U¤È 04:38:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Trade](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StockId] [varchar](10) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[Type] [char](1) NOT NULL,
	[Volume] [int] NOT NULL,
	[Fee] [float] NOT NULL,
	[LendingPeriod] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateUser] [varchar](20) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Stock] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stock] ([StockId])
GO

ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Stock]
GO