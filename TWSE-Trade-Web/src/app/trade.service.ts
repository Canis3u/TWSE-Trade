import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Trade } from './dataModel/trade-info';
@Injectable({
  providedIn: 'root',
})
export class TradeService {
  uri:string = 'https://localhost:44309/api/Trade'

  constructor(private httpClient: HttpClient) { }

  getTradeById(id: number): Observable<Trade> {
    let resp = this.httpClient.get<Trade>(`${this.uri}/${id}`)
    resp.subscribe((data)=>{
      console.log(data)
    })
    return resp
  }

}
