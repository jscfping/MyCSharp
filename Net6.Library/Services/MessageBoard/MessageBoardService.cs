using Core31.Library.Response;
using Dapper;
using Net6.Library.Models.MessageBoard;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6.Library.Services.MessageBoard
{
    public class MessageBoardService : IMessageBoardService
    {
        private readonly string _postgresConnectionString;

        public MessageBoardService(Net6Setting net6Setting)
        {
            _postgresConnectionString = net6Setting.PostgresConnectionString;
        }

        public async Task<AppResponse> CreateMessageAsync(Message aMessage)
        {
            using var connection = new NpgsqlConnection(_postgresConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            var sql = "INSERT INTO messages(content) VALUES (@Content) RETURNING Id";
            int id = await connection.ExecuteAsync(sql, aMessage, transaction);
            transaction.Commit();
            return new AppResponse($"message:{id} created.");
        }

        public async Task<AppResponse<IEnumerable<Message>>> GetAllMessages()
        {
            using var connection = new NpgsqlConnection(_postgresConnectionString);
            connection.Open();
            var sql = "SELECT * FROM messages;";
            var q =  await connection.QueryAsync<Message>(sql);
            return new AppResponse<IEnumerable<Message>>(null, q);
        }

        public async Task<AppResponse> UpdateMessage(Message aMessage)
        {
            using var connection = new NpgsqlConnection(_postgresConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            var sql = @"
UPDATE messages
SET content=@Content
WHERE id=@Id;";
            await connection.ExecuteAsync(sql, aMessage, transaction);
            transaction.Commit();
            return new AppResponse($"message:{aMessage.Id} updated.");
        }

        public async Task<AppResponse> DeleteMessage(int id)
        {
            using var connection = new NpgsqlConnection(_postgresConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            var sql = @"DELETE FROM messages WHERE id=@id;";
            await connection.ExecuteAsync(sql, new { id }, transaction);
            transaction.Commit();
            return new AppResponse($"message:{id} deleted.");
        }


    }
}
