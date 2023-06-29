export interface TradeInfo {
  id: number;
  stockIdAndName: string;
  tradeDate: string;
  type: string;
  volume: number;
  fee: number;
  closingPrice: number;
  lendingPeriod: number;
  returnDate: string;
}

/*
  "id": 3,
  "stockId": "4736",
  "stockName": "泰博",
  "tradeDate": "2023-01-03T00:00:00",
  "type": "定價",
  "volume": 150,
  "fee": 2,
  "closingPrice": 182,
  "lendingPeriod": 185,
  "returnDate": "2023-07-07T00:00:00"
*/
