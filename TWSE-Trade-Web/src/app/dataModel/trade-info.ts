export interface Trade {
  id: number;
  stockId: string;
  stockName: string;
  tradeDate: Date;
  type: string;
  volume: number;
  fee: number;
  closingPrice: number;
  lendingPeriod: number;
  returnDate: Date;
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
