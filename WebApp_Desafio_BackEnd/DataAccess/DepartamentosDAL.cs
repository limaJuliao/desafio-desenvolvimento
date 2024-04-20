using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.DataAccess
{
    public class DepartamentosDAL : BaseDAL
    {
        public IEnumerable<Departamento> ListarDepartamentos()
        {
            IList<Departamento> lstDepartamentos = new List<Departamento>();

            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {

                    dbCommand.CommandText = "SELECT * FROM departamentos ";

                    dbConnection.Open();

                    using (SQLiteDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        var departamento = Departamento.Empty;

                        while (dataReader.Read())
                        {
                            departamento = new Departamento();

                            if (!dataReader.IsDBNull(0))
                                departamento.ID = dataReader.GetInt32(0);
                            if (!dataReader.IsDBNull(1))
                                departamento.Descricao = dataReader.GetString(1);

                            lstDepartamentos.Add(departamento);
                        }
                        dataReader.Close();
                    }
                    dbConnection.Close();
                }

            }

            return lstDepartamentos;
        }

        internal bool GravarDepartamento(int id, string descricao)
        {
            using (SQLiteConnection dbConnection = new SQLiteConnection(CONNECTION_STRING))
            {
                using (SQLiteCommand dbCommand = dbConnection.CreateCommand())
                {
                    if (id == 0)
                    {
                        dbCommand.CommandText =
                            "INSERT INTO departamentos (Descricao) VALUES(@descricao);";
                    }
                    else
                    {
                        dbCommand.CommandText =
                            @"UPDATE departamentos 
                            SET Descricao=@descricao 
                            WHERE ID=@id;";
                    }

                    dbCommand.Parameters.AddWithValue("@descricao", descricao);
                    dbCommand.Parameters.AddWithValue("id", id);

                    dbConnection.Open();
                    return dbCommand.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}