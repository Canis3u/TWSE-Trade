USE [TWSETrade]
GO

/****** Object:  Table [dbo].[ClosingPrice]    Script Date: 2023/6/20 ¤U¤È 04:36:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ClosingPrice](
	[StockId] [varchar](10) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[Price] [float] NULL,
	[CreateUser] [varchar](20) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUser] [varchar](20) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_ClosingPrice] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC,
	[TradeDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ClosingPrice]  WITH CHECK ADD  CONSTRAINT [FK_ClsingPrice_Stock] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stock] ([StockId])
GO

ALTER TABLE [dbo].[ClosingPrice] CHECK CONSTRAINT [FK_ClsingPrice_Stock]
GO