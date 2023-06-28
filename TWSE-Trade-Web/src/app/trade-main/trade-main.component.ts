import { Component, OnInit} from '@angular/core';
import { TradeService } from '../trade.service';

import { Trade } from '../dataModel/trade-info';
import { TradeQuery } from '../dataModel/trade-query';

@Component({
  selector: 'app-trade-main',
  templateUrl: './trade-main.component.html',
  styleUrls: ['./trade-main.component.css']
})
export class TradeMainComponent implements OnInit {
  displayedColumns: string[] = ['成交日期', '證券代號名稱', '交易方式', '成交數量', '成交費率', '成交日收盤價', '約定還券日期', '約定借券天數'];

  tradeQuery = new TradeQuery();

  constructor(
    private tradeService: TradeService
  ){}

  ngOnInit(): void {}

  test(){
    console.log(`http://${this.tradeQuery.toQueryString()}`)
  }
}

