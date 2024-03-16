using LaboratorioPeriodo2.DTOs;
using System.Data.SqlClient;

namespace LaboratorioPeriodo2.DAOs
{
    public class ProductoDAO
    {
        private string ConnectionString =
            "Data Source=ALFREDOALAS;" +
            "Initial Catalog=laboratorio3;" +
            "Integrated Security=True;";

        public List<ProductoDTO> GetAllProductos()
        {
            var productos = new List<ProductoDTO>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM producto", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new ProductoDTO
                            {
                                codProducto = Convert.ToInt32(reader["codigoprod"]),
                                nombreProducto = reader["nombreprod"].ToString(),
                                nombreProveedor = reader["nombreprov"].ToString(),
                                precioUnitario = Convert.ToDecimal(reader["preciounit"]),
                                unidades = Convert.ToInt32(reader["unidades"])
                            });
                        }
                    }
                }
            }
            return productos;
        }

        public ProductoDTO GetById(int codigoProd)
        {
            ProductoDTO producto = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM producto WHERE codigoprod = @codigoProd", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", codigoProd);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new ProductoDTO
                            {
                                codProducto = Convert.ToInt32(reader["codigoprod"]),
                                nombreProducto = reader["nombreprod"].ToString(),
                                nombreProveedor = reader["nombreprov"].ToString(),
                                precioUnitario = Convert.ToDecimal(reader["preciounit"]),
                                unidades = Convert.ToInt32(reader["unidades"])
                            };
                        }
                    }
                }
            }
            return producto;
        }

        public int Add(ProductoDTO producto)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("INSERT INTO producto (codigoprod, nombreprod, nombreprov, preciounit, unidades) VALUES (@codigoProd, @nombreProd, @nombreProv, @precioUnit, @unidades)", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", producto.codProducto);
                    command.Parameters.AddWithValue("@nombreProd", producto.nombreProducto);
                    command.Parameters.AddWithValue("@nombreProv", producto.nombreProveedor);
                    command.Parameters.AddWithValue("@precioUnit", producto.precioUnitario);
                    command.Parameters.AddWithValue("@unidades", producto.unidades);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        public int Edit(ProductoDTO producto)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("UPDATE producto SET nombreprod = @nombreProd, nombreprov = @nombreProv, preciounit = @precioUnit, unidades = @unidades WHERE codigoprod = @codigoProd", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", producto.codProducto);
                    command.Parameters.AddWithValue("@nombreProd", producto.nombreProducto);
                    command.Parameters.AddWithValue("@nombreProv", producto.nombreProveedor);
                    command.Parameters.AddWithValue("@precioUnit", producto.precioUnitario);
                    command.Parameters.AddWithValue("@unidades", producto.unidades);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        public int Delete(int codigoProd)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand("DELETE FROM producto WHERE codigoprod = @codigoProd", connection))
                {
                    command.Parameters.AddWithValue("@codigoProd", codigoProd);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }

}
