import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TradeMainComponent } from './trade-main.component';

describe('TradeMainComponent', () => {
  let component: TradeMainComponent;
  let fixture: ComponentFixture<TradeMainComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TradeMainComponent]
    });
    fixture = TestBed.createComponent(TradeMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
