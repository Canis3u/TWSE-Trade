using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
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
        // select information by 1 id
        public async Task<TradeRespServiceModel> SelectTradeInformatoinByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectString);
            var result = await conn.QueryFirstOrDefaultAsync<TradeRespServiceModel>(this.SelectTradeInformatoinByIdAsyncSql(id));
            return result;
        }
        // update trade information by 1 id
        public async Task<int> UpdateTradeByIdAsync(int id, string user, Trade trade)
        {
            // 押上 UpdateUser UpdateDate
            trade.UpdateUser = user;
            trade.UpdateDate = DateTime.Now;
            using var conn = new SqlConnection(_connectString);
            var rowschange = await conn.ExecuteAsync(this.UpdateTradeByIdAsyncSql(id), trade);
            return rowschange;
        }
        // update trade.status to 2(deleted status) by 1 id
        public async Task<int> UpdateTradeStatusWithDeletedCodeByIdAsync(int id, string user)
        {
            // 押上 UpdateUser UpdateDate
            using var conn = new SqlConnection(_connectString);
            var rowschange = await conn.ExecuteAsync(this.UpdateTradeStatusWithDeletedCodeByIdAsyncSql(id), new {UpdateUser=user,UpdateDate=DateTime.Now});
            return rowschange;
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
