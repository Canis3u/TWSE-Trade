import { TradeQueryResp } from './../dataModel/trade-query-resp';
import { Component, OnInit} from '@angular/core';
import { TradeService } from '../trade.service';
import { TradeQuery } from '../dataModel/trade-query';
import { TradeInfo } from '../dataModel/trade-info';

@Component({
  selector: 'app-trade-main',
  templateUrl: './trade-main.component.html',
  styleUrls: ['./trade-main.component.css']
})
export class TradeMainComponent {
  displayedColumns: string[] = ['成交日期', '證券代號名稱', '交易方式', '成交數量', '成交費率', '成交日收盤價', '約定還券日期', '約定借券天數'];
  tradeTypeSelectList: string[] = ['定價','競價','議借']
  displayedTradeItems: TradeInfo[] = [];
  tradeQuery = new TradeQuery();

  constructor(
    private tradeService: TradeService
  ){}

  Query(){
    this.tradeService.getTradeQuery(this.tradeQuery).subscribe((data)=>{
      this.displayedTradeItems = data.items
    });
  }

  SortQuery(col:string){
    switch (col) {
      case this.displayedColumns[0]:
        this.tradeQuery.setSortColumn('TradeDate')
        break
      case this.displayedColumns[1]:
        this.tradeQuery.setSortColumn('StockId')
        break
      case this.displayedColumns[2]:
        this.tradeQuery.setSortColumn('Type')
        break
      case this.displayedColumns[3]:
        this.tradeQuery.setSortColumn('Volume')
        break
      case this.displayedColumns[4]:
        this.tradeQuery.setSortColumn('Fee')
        break
      case this.displayedColumns[5]:
        this.tradeQuery.setSortColumn('ClosingPrice')
        break
      case this.displayedColumns[6]:
        this.tradeQuery.setSortColumn('RetrunDate')
        break
      case this.displayedColumns[7]:
        this.tradeQuery.setSortColumn('LendingPeriod')
        break
      default:
        this.tradeQuery.sortColumn = 'Id'
    }
    this.Query()
  }
}

