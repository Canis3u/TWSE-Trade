import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TradeMainComponent } from './trade-main/trade-main.component';

const routes: Routes = [
  {
    path: '',
    component: TradeMainComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
