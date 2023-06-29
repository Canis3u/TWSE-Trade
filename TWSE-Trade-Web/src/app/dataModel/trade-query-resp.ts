import { TradeInfo } from "./trade-info";
import { TradeQuery } from "./trade-query";
export interface TradeQueryResp {
  items:TradeInfo[];
  totalCount:number;
  queryParams:TradeQuery;
}
