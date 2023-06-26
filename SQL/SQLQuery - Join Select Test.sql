SELECT Id
      ,T.StockId
	  ,S.Name
      ,T.TradeDate
      ,Type
      ,Volume
      ,Fee
	  ,C.Price
      ,LendingPeriod
      ,Status
FROM [TWSETrade].[dbo].[Trade] T
INNER JOIN [TWSETrade].[dbo].[Stock] S on T.StockId = S.StockId
INNER JOIN [TWSETrade].[dbo].[ClosingPrice] C on C.StockId = S.StockId AND C.TradeDate=T.TradeDate
WHERE Id=1