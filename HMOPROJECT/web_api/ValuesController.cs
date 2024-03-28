using data_connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace web_api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        public IEnumerable<USERES_HMO> Get()
        {
            var users = DataAccess.getUSERES_HMO();
            users.Reverse();
            return users.Take(5);
        }
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<USERES_HMO> GetAll()
        {
            var users = DataAccess.getUSERES_HMO();
            return users;
        }
        
        //[HttpGet]
        //[ActionName("GetById")]
        //public CoronaDetails GetByID(string id)
        //{
        //    return DataAccess.getByIdUSERES_HMO(id);
        //}
        [HttpPost]
        public string Post([FromBody]string value)
        {
            var res = JsonConvert.DeserializeObject<USERES_HMO>(value);
            return DataAccess.addUser(res.ID, res.First_name, res.Last_name, res.Birth_date, res.City, res.Street, res.building_number, res.cellphone, res.phone);
        }
        [HttpGet]
        [ActionName("GetDelete")]
        public string GetDelete(string id)
        {
            return DataAccess.deleteUser(id);
        }
        [HttpGet]
        [ActionName("GetCor")]
        public string GetCor(string id)
        {
            return DataAccess.getCor(id);

        }
        [HttpPost]
        [ActionName("PostUp")]
        public string PostUp([FromBody]string value)
        {
            var res = JsonConvert.DeserializeObject<USERES_HMO>(value);
            return DataAccess.updateUser(res.ID, res.First_name, res.Last_name, res.Birth_date, res.City, res.Street, res.building_number, res.cellphone, res.phone);
        }
        

    }
}
