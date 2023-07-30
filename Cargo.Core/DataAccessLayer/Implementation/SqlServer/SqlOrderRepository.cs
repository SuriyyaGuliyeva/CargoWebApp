using Cargo.Core.DataAccessLayer.Abstract;
using Cargo.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cargo.Core.DataAccessLayer.Implementation.SqlServer
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public SqlOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Order order)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = @"insert into orders (Link, Count, Price, CargoPrice, Size, Color, Note, TotalCount, TotalAmount, Status, UserId, CreationDateTime)
                                 output inserted.Id
                                 values (@Link, @Count, @Price, @CargoPrice, @Size, @Color, @Note, @TotalCount, @TotalAmount, @Status, @UserId, @CreationDateTime)";

                con.Open();

                var cmd = new SqlCommand(query, con);

                AddParameters(cmd, order);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "update Orders set status = 5 where id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public Order Get(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select o.*, u.Name, u.Email from orders o inner join users u on o.userId = u.Id where o.Id = @id";

                con.Open();

                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetFromReader(reader);
                }

                return null;
            }
        }

        public IList<Order> GetAll()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "select o.*, u.Name, u.Email from orders as o join Users as u on o.UserId = u.Id";

                con.Open();

                var cmd = new SqlCommand(query, con);

                var reader = cmd.ExecuteReader();

                List<Order> orders = new();

                while (reader.Read())
                {
                    var order = GetFromReader(reader);

                    orders.Add(order);
                }

                return orders;
            }
        }

        public bool Update(Order order)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = @"update orders set link = @link, count = @count, price = @price, cargoprice = @cargoprice,
                                 size = @size, color = @color, note = @note, totalcount = @totalcount, totalamount = @totalamount
                                 where id = @Id";

                con.Open();

                var cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("Id", order.Id);

                AddParameters(cmd, order);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }


        #region private methods
        private void AddParameters(SqlCommand cmd, Order order)
        {
            cmd.Parameters.AddWithValue("Link", order.Link);
            cmd.Parameters.AddWithValue("Count", order.Count);
            cmd.Parameters.AddWithValue("Price", order.Price);
            cmd.Parameters.AddWithValue("CargoPrice", order.CargoPrice);
            cmd.Parameters.AddWithValue("Size", order.Size);
            cmd.Parameters.AddWithValue("Color", order.Color);
            cmd.Parameters.AddWithValue("Note", order.Note);
            cmd.Parameters.AddWithValue("TotalCount", order.TotalCount);
            cmd.Parameters.AddWithValue("TotalAmount", order.TotalAmount);
            cmd.Parameters.AddWithValue("Status", order.Status);
            cmd.Parameters.AddWithValue("UserId", order.UserId);
            cmd.Parameters.AddWithValue("CreationDateTime", order.CreationDateTime);
        }

        private Order GetFromReader(SqlDataReader reader)
        {
            Order order = new Order();

            order.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            order.Link = reader.GetString(reader.GetOrdinal("Link"));
            order.Count = reader.GetInt32(reader.GetOrdinal("Count"));
            order.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
            order.CargoPrice = reader.GetDecimal(reader.GetOrdinal("CargoPrice"));
            order.Size = reader.GetInt32(reader.GetOrdinal("Size"));
            order.Color = reader.GetString(reader.GetOrdinal("Color"));
            order.Note = reader.GetString(reader.GetOrdinal("Note"));
            order.TotalCount = reader.GetInt16(reader.GetOrdinal("TotalCount"));
            order.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
            order.Status = reader.GetInt32(reader.GetOrdinal("Status"));
            order.CreationDateTime = reader.GetDateTime(reader.GetOrdinal("CreationDateTime"));

            order.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
            order.User = new User()
            {
                Id = order.UserId,
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Email = reader.GetString(reader.GetOrdinal("Email"))
            };

            return order;
        }
        #endregion
    }
}
