import { Component} from '@angular/core';
import { TradeService } from '../trade.service';
import { TradeQuery } from '../dataModel/trade-query';
import { TradeInfo } from '../dataModel/trade-info';

@Component({
  selector: 'app-trade-main',
  templateUrl: './trade-main.component.html',
  styleUrls: ['./trade-main.component.css']
})
export class TradeMainComponent {
  isTableVisible: boolean = false;
  displayedColumns: string[] = ['成交日期', '證券代號名稱', '交易方式', '成交數量', '成交費率', '成交日收盤價', '約定還券日期', '約定借券天數'];
  columnArrows: string[] = ['','','','','','','',''];
  displayedTradeItems: TradeInfo[] = [];
  totalCount: number = 0;
  tradeQuery = new TradeQuery();

  constructor(
    private tradeService: TradeService,
  ){}

  InitArrows(){
    this.columnArrows = ['','','','','','','','']
  }

  Query(){
    this.tradeService.GetTradeQuery(this.tradeQuery).subscribe((data)=>{
      this.displayedTradeItems = data.items
      this.totalCount = data.totalCount
      console.log(data.totalCount)
    });
  }

  QueryWithFilter(){
    this.InitArrows()
    this.tradeQuery.sortColumn = 'Id'
    this.tradeQuery.sortDirection = 'ASC'
    this.Query()
    this.isTableVisible = true;
  }

  QueryWithSort(col:string){
    this.InitArrows()
    switch (col) {
      case this.displayedColumns[0]:
        this.columnArrows[0] = this.tradeQuery.setSortColumn('TradeDate')
        break
      case this.displayedColumns[1]:
        this.columnArrows[1] = this.tradeQuery.setSortColumn('StockId')
        break
      case this.displayedColumns[2]:
        this.columnArrows[2] = this.tradeQuery.setSortColumn('Type')
        break
      case this.displayedColumns[3]:
        this.columnArrows[3] = this.tradeQuery.setSortColumn('Volume')
        break
      case this.displayedColumns[4]:
        this.columnArrows[4] = this.tradeQuery.setSortColumn('Fee')
        break
      case this.displayedColumns[5]:
        this.columnArrows[5] = this.tradeQuery.setSortColumn('ClosingPrice')
        break
      case this.displayedColumns[6]:
        this.columnArrows[6] = this.tradeQuery.setSortColumn('ReturnDate')
        break
      case this.displayedColumns[7]:
        this.columnArrows[7] = this.tradeQuery.setSortColumn('LendingPeriod')
        break
      default:
        this.tradeQuery.sortColumn = 'Id'
    }
    this.Query()
  }

  DeleteById(id:number){
    this.tradeService.DeleteTradeById(id).subscribe(() => {
      this.QueryWithFilter()
    });
  }
}

