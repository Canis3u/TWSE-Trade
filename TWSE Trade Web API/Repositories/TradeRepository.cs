using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Repositories
{
    public class TradeRepository
    {
        private readonly string _connectString;
        private readonly IConfiguration _config;
        public TradeRepository(IConfiguration config)
        {
            _config = config;
            _connectString = _config.GetConnectionString("TWSETradeDatabase");
        }
        public async Task<List<TradeRespServiceModel>>SelectTradesInformationAsync(TradeQueryServiceModel serviceModel)
        {
            var sqlString = this.SelectTradesInformationAsyncSql(serviceModel);
            using var connection = new SqlConnection(_connectString);
            var items = (await connection.QueryAsync<TradeRespServiceModel>(sqlString)).ToList();
            return items;
        }
        public async Task<int> SelectTradesCountAsync(TradeQueryServiceModel serviceModel)
        {
            var sqlString = this.SelectTradesCountAsyncSql(serviceModel);
            using var connection = new SqlConnection(_connectString);
            var count = (await connection.QueryAsync<int>(sqlString)).FirstOrDefault();
            return count;
        }
        public async Task<TradeRespServiceModel> SelectTradeInformatoinByIdAsync(int id)
        {
            var sqlString = this.SelectTradeInformatoinByIdAsyncSql(id);
            using var connection = new SqlConnection(_connectString);
            var item = await connection.QueryFirstOrDefaultAsync<TradeRespServiceModel>(sqlString);
            return item;
        }
        public async Task<int> UpdateTradeByIdAsync(int id, string user, Trade trade)
        {
            var sqlString = this.UpdateTradeByIdAsyncSql(id);
            trade.UpdateUser = user;
            trade.UpdateDate = DateTime.Now;
            using var connection = new SqlConnection(_connectString);
            var rowschange = await connection.ExecuteAsync(sqlString, trade);
            return rowschange;
        }
        public async Task<int> UpdateTradeStatusWithDeletedCodeByIdAsync(int id, string user)
        {
            var sqlString = this.UpdateTradeStatusWithDeletedCodeByIdAsyncSql(id);
            using var connection = new SqlConnection(_connectString);
            var rowschange = await connection.ExecuteAsync(sqlString, new {UpdateUser=user,UpdateDate=DateTime.Now});
            return rowschange;
        }

        private string SelectTradesInformationAsyncSql(TradeQueryServiceModel serviceModel)
        {
            var pageIndex = serviceModel.CurrentPage < 1 ? 1 : serviceModel.CurrentPage;
            var startIndex = (pageIndex - 1) * serviceModel.PageSize;
            var sqlSelectString = $"SELECT " +
                                  $"T.Id, T.StockId, " +
                                  $"S.Name as StockName, " +
                                  $"T.TradeDate, T.Type, T.Volume, T.Fee, " +
                                  $"C.Price as ClosingPrice, " +
                                  $"T.LendingPeriod ";
            if (serviceModel.SortColumn=="ReturnDate")
                sqlSelectString += $", dateadd(day,T.LendingPeriod,T.TradeDate) as ReturnDate ";

            var sqlFromString = $"FROM Trade T " +
                                $"LEFT JOIN Stock S on T.StockId = S.StockId " +
                                $"LEFT JOIN ClosingPrice C on C.StockId = S.StockId AND C.TradeDate = T.TradeDate ";
            
            var sqlWhereString = $"WHERE T.Status != 2 ";
            if (serviceModel.StartDate != null)
                sqlWhereString += $"AND T.TradeDate>=\'{serviceModel.StartDate}\' ";
            if (serviceModel.EndDate != null)
                sqlWhereString += $"AND T.TradeDate<=\'{serviceModel.EndDate}\' ";
            if (serviceModel.TradeType != null)
                sqlWhereString += $"AND T.Type=\'{serviceModel.TradeType}\' ";
            if (serviceModel.StockId != null)
                sqlWhereString += $"AND T.StockId=\'{serviceModel.StockId}\' ";
            
            var sqlOrderString = $"ORDER BY {serviceModel.SortColumn} {serviceModel.SortDirection} ";
            var sqlOffsetString = $"OFFSET {startIndex} ROWS FETCH NEXT {serviceModel.PageSize} ROWS ONLY";

            return sqlSelectString + sqlFromString + sqlWhereString + sqlOrderString + sqlOffsetString;
        }
        private string SelectTradesCountAsyncSql(TradeQueryServiceModel serviceModel)
        {
            var sqlString = $"SELECT COUNT(1) " +
                            $"FROM Trade T " +
                            $"WHERE T.Status != 2 ";
            if (serviceModel.StartDate != null)
                sqlString += $"AND T.TradeDate>=\'{serviceModel.StartDate}\' ";
            if (serviceModel.EndDate != null)
                sqlString += $"AND T.TradeDate<=\'{serviceModel.EndDate}\' ";
            if (serviceModel.TradeType != null)
                sqlString += $"AND T.Type=\'{serviceModel.TradeType}\' ";
            if (serviceModel.StockId != null)
                sqlString += $"AND T.StockId=\'{serviceModel.StockId}\' ";

            return sqlString;
        }
        private string SelectTradeInformatoinByIdAsyncSql(int id)
        {
            return $"SELECT " +
                   $"T.Id, T.StockId, " +
                   $"S.Name as StockName, " +
                   $"T.TradeDate, T.Type, T.Volume, T.Fee, " +
                   $"C.Price as ClosingPrice, " +
                   $"T.LendingPeriod " +
                   $"FROM Trade T " +
                   $"LEFT JOIN Stock S on T.StockId = S.StockId " +
                   $"LEFT JOIN ClosingPrice C on C.StockId = S.StockId AND C.TradeDate = T.TradeDate " +
                   $"WHERE T.Id = {id} And Status != 2";
        }
        private string UpdateTradeByIdAsyncSql(int id)
        {
            return $"UPDATE Trade SET " +
                   $"Type=@Type, Volume=@Volume, Fee=@Fee, LendingPeriod=@LendingPeriod, " +
                   $"Status=@Status, UpdateUser=@UpdateUser, UpdateDate=@UpdateDate " +
                   $"WHERE Id= {id} AND Status != 2";
        }
        private string UpdateTradeStatusWithDeletedCodeByIdAsyncSql(int id)
        {
            return $"UPDATE Trade SET " +
                   $"Status=2, UpdateUser=@UpdateUser, UpdateDate=@UpdateDate " +
                   $"WHERE Id= {id} AND Status != 2";
        }
    }
}
