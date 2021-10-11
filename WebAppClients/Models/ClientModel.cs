using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;

namespace WebAppClients.Models
{
    public class ClientModel
    {
        public List<ClientDTO> ListClients(int? id = null)
        {
            try
            {
                var clientDB = new ClientDAO();
                return clientDB.ListClientsDB(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro ao listar Clientes: Erro = ${ex.Message}");
            }
        }

        public void Insert(ClientDTO client)
        {
            try
            {
                var clientDB = new ClientDAO();
                clientDB.InsertClientDB(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir cliente: Erro = ${ex.Message}");
            }
        }

        public void Update(ClientDTO client)
        {
            try
            {
                var clientDB = new ClientDAO();
                clientDB.UpdateClientDB(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar cliente: Erro = ${ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var clientDB = new ClientDAO();
                clientDB.DeleteClientDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar cliente: Erro = ${ex.Message}");
            }
        }
    }
}