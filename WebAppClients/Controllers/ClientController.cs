using App.Domain;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAppClients.Models;

namespace WebAppClients.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAll()
        {
            try
            {
                ClientModel client = new ClientModel();

                return Ok(client.ListClients());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                ClientModel clients = new ClientModel();

                ClientDTO client = clients.ListClients(id).FirstOrDefault();
                if (client == null)
                {
                    return NotFound();
                } 
                else
                {
                    return Ok(client);
                }              
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }         
        }

        [HttpPost]
        public IHttpActionResult Insert(ClientDTO client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ClientModel _client = new ClientModel();

                _client.Insert(client);

                return Ok(_client.ListClients());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]ClientDTO client)
        {
            try
            {
                ClientModel _client = new ClientModel();
                client.Id = id;
                _client.Update(client);
                return Ok(_client.ListClients(id).FirstOrDefault());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ClientModel _client = new ClientModel();
                _client.Delete(id);
                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
