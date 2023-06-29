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

  getTradeQuery(tradeQuery:TradeQuery): Observable<TradeQueryResp> {
    console.log(`${tradeQuery.toQueryString()}`)
    return this.httpClient.get<TradeQueryResp>(`${this.uri}/${tradeQuery.toQueryString()}`)
  }

  getTradeById(id: number): Observable<TradeInfo> {
    let resp = this.httpClient.get<TradeInfo>(`${this.uri}/${id}`)
    resp.subscribe((data)=>{
      console.log(data)
    })
    return resp
  }

}
