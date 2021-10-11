using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace App.Repository
{
    public class ClientDAO
    {
        private readonly string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private readonly IDbConnection connection;

        public ClientDAO()
        {
            
            connection = new SqlConnection(ConnectionString);

            connection.Open();
        }

        public List<ClientDTO> ListClientsDB(int? id)
        {
            try
            {
                var listClients = new List<ClientDTO>();

                IDbCommand selectCmd = connection.CreateCommand();

                if (id == null)
                {
                    selectCmd.CommandText = "select * from DBCLIENTS";

                }
                else
                {
                    selectCmd.CommandText = $"select * from DBCLIENTS where id = {id}";
                }

                IDataReader result = selectCmd.ExecuteReader();
                while (result.Read())
                {
                    ClientDTO client = new ClientDTO
                    {
                        Id = Convert.ToInt32(result["ID"]),
                        Name = result["NAME"].ToString(),
                        BirthDate = result["BIRTH_DATE"].ToString(),
                        CreateAt = result["CREATE_AT"].ToString(),
                        Cpf = result["CPF"].ToString(),
                        Income = Convert.ToDecimal(result["INCOME"])
                    };

                    listClients.Add(client);
                }
                return listClients;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }           
        }

        public void InsertClientDB(ClientDTO client)
        {
            try
            {
                IDbCommand insertCmd = connection.CreateCommand();
                insertCmd.CommandText = "INSERT INTO DBCLIENTS (NAME, CPF, BIRTH_DATE, CREATE_AT, INCOME) VALUES (@name, @cpf, @birthDate, @createAt, @income)";

                IDbDataParameter parameterName = new SqlParameter("name", client.Name);
                insertCmd.Parameters.Add(parameterName);

                IDbDataParameter parameterCpf = new SqlParameter("cpf", client.Cpf);
                insertCmd.Parameters.Add(parameterCpf);

                IDbDataParameter parameterBirthDate = new SqlParameter("birthDate", client.BirthDate);
                insertCmd.Parameters.Add(parameterBirthDate);

                IDbDataParameter parameterCreateAt = new SqlParameter("createAt", DateTime.Now.ToString());
                insertCmd.Parameters.Add(parameterCreateAt);

                IDbDataParameter parameterIncome = new SqlParameter("income", client.Income);
                insertCmd.Parameters.Add(parameterIncome);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateClientDB(ClientDTO client)
        {
            try
            {
                IDbCommand updateCmd = connection.CreateCommand();
                updateCmd.CommandText = "UPDATE DBCLIENTS SET NAME = @name, CPF = @cpf, BIRTH_DATE = @birthDate, INCOME = @income WHERE ID = @id";

                IDbDataParameter parameterName = new SqlParameter("name", client.Name);
                updateCmd.Parameters.Add(parameterName);

                IDbDataParameter parameterCpf = new SqlParameter("cpf", client.Cpf);
                updateCmd.Parameters.Add(parameterCpf);

                IDbDataParameter parameterBirthDate = new SqlParameter("birthDate", client.BirthDate);
                updateCmd.Parameters.Add(parameterBirthDate);

                IDbDataParameter parameterIncome = new SqlParameter("income", client.Income);
                updateCmd.Parameters.Add(parameterIncome);

                IDbDataParameter parameterId = new SqlParameter("id", client.Id);
                updateCmd.Parameters.Add(parameterId);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteClientDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = connection.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM DBCLIENTS WHERE ID = @id";

                IDbDataParameter parameterId = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(parameterId);
                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}