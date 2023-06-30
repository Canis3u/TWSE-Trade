import { TradeService } from './../trade.service';
import { Component,OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TradeInfo } from '../dataModel/trade-info';

@Component({
  selector: 'app-trade-edit',
  templateUrl: './trade-edit.component.html',
  styleUrls: ['./trade-edit.component.css']
})
export class TradeEditComponent implements OnInit {
  editId:number = 0;
  viewTradeInfo:TradeInfo = {
    id:0,
    stockIdAndName: '',
    tradeDate: '',
    type: '',
    volume: 0,
    fee: 0,
    closingPrice: 0,
    lendingPeriod: 0,
    returnDate: ''
  };

  constructor(
    private tradeService:TradeService,
    private router: Router,
    private route: ActivatedRoute,
  ){}

  ngOnInit(): void {
    this.route.paramMap.subscribe((param) => {
      this.editId = Number(param.get('id'));
      this.GetTradeById();
    })
  }

  GetTradeById() {
    this.tradeService.GetTradeById(this.editId).subscribe((data) => {
      this.viewTradeInfo = data;
      switch(this.viewTradeInfo.type){
        case '定價':
          this.viewTradeInfo.type = 'F'
          break
        case '競價':
          this.viewTradeInfo.type = 'C'
          break
        case '議借':
          this.viewTradeInfo.type = 'N'
          break
      }
    });
  }

  UpdateTrade() {
    this.tradeService.UpdateTradeById(this.editId, this.viewTradeInfo).subscribe(() => {
      this.router.navigate(['/']);
    });
  }

}
