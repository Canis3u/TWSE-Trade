import { TradeQuery } from './dataModel/trade-query';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { TradeInfo } from './dataModel/trade-info';
import { TradeQueryResp } from './dataModel/trade-query-resp';
@Injectable({
  providedIn: 'root',
})
export class TradeService {
  uri:string = 'https://localhost:44309/api/Trade'

  constructor(private httpClient: HttpClient) { }

  GetTradeQuery(tradeQuery:TradeQuery): Observable<TradeQueryResp> {
    return this.httpClient.get<TradeQueryResp>(`${this.uri}/${tradeQuery.toQueryString()}`)
  }

  GetTradeById(id: number): Observable<TradeInfo> {
    return this.httpClient.get<TradeInfo>(`${this.uri}/${id}`)
  }
  UpdateTradeById(id:number, payload:TradeInfo) {
    return this.httpClient.put(`${this.uri}/${id}`,payload)
  }
  DeleteTradeById(id:number) {
    return this.httpClient.delete(`${this.uri}/${id}`)
  }

}
