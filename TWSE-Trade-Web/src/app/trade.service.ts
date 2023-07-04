import { TradeQuery } from './dataModel/trade-query';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { TradeInfo } from './dataModel/trade-info';
import { TradeQueryResp } from './dataModel/trade-query-resp';
import { TwseResp } from './dataModel/twse-resp';
import { StockInfo } from './dataModel/stock-info';
@Injectable({
  providedIn: 'root',
})
export class TradeService {
  Uri:string = 'https://localhost:44309/api/Trade'
  TwseuUri:string = 'https://localhost:44309/api/Twse'
  StockUri:string = 'https://localhost:44309/api/Stock'

  constructor(private httpClient: HttpClient) { }

  GetTradeQuery(tradeQuery:TradeQuery): Observable<TradeQueryResp> {
    return this.httpClient.get<TradeQueryResp>(`${this.Uri}/${tradeQuery.toQueryString()}`)
  }

  GetTradeById(id: number): Observable<TradeInfo> {
    return this.httpClient.get<TradeInfo>(`${this.Uri}/${id}`)
  }
  UpdateTradeById(id:number, payload:TradeInfo) {
    return this.httpClient.put(`${this.Uri}/${id}`,payload)
  }
  DeleteTradeById(id:number) {
    return this.httpClient.delete(`${this.Uri}/${id}`)
  }
  UpdateDatabase(date:string) {
    return this.httpClient.get<TwseResp>(`${this.TwseuUri}/${date}`)
  }
  GetStockById(stockIdAndName:string) {
    var stockId = stockIdAndName.split(" ")[0]
    return this.httpClient.get<StockInfo>(`${this.StockUri}/${stockId}`)
  }

}
