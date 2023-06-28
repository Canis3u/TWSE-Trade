export class TradeQuery {
  startDate:string = '';
  endDate:string = '';
  tradeType:string = '';
  stockId:string = '';
  sortColumn:string = 'Id';
  sortDirection:string = 'ASC';
  currentPage:number = 1;
  pageSize:number = 10;

  public toQueryString():string {
    const queryString = "?" +
                        `StartDate=${this.startDate}&` +
                        `EndDate=${this.endDate}&` +
                        `TradeType=${this.tradeType}&` +
                        `StockId=${this.stockId}&` +
                        `SortColumn=${this.sortColumn}&` +
                        `SortDirection=${this.sortDirection}&` +
                        `CurrentPage=${this.currentPage}&` +
                        `PageSize=${this.pageSize}`
    return queryString;
  }
}
