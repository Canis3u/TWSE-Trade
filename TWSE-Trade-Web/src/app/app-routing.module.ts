import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TradeMainComponent } from './trade-main/trade-main.component';
import { TradeEditComponent } from './trade-edit/trade-edit.component';

const routes: Routes = [
  {
    path: '',
    component: TradeMainComponent
  },
  {
    path: 'trade-edit/:id',
    component: TradeEditComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
