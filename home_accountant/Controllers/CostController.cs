using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace home_accountant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        IUnitOfWork unitOfWork;

        public CostController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET api/Cost/List
        [HttpGet("List")]
        public IEnumerable<Cost> GetCostList()
        {
            return unitOfWork.GetCostRepository().GetAll();
        }

        // GET api/Cost/List/Products
        [HttpGet("List/{name}")]
        public IEnumerable<Cost> GetCostListByName(string name)
        {
            var items = from item in unitOfWork.GetCostRepository().GetAll()
                        where item.CostName.Trim() == name
                        select item;
            return items;
        }

        // GET api/Cost/SumDay/12.04.2020
        [HttpGet("SumDay/{Date}")]
        public string GetDaySumForDate(string Date)
        {
            int result = 0;

            foreach (var item in unitOfWork.GetCostRepository().GetAll())
            {
                if (item.CostDate == Date)
                {
                    result += item.CostValue;
                }
            }

            return result.ToString();
        }

        // GET api/Cost/SumMonth/03.20
        [HttpGet("SumMonth/{Date}")]
        public string GetMonthSumForDate(string Date)
        {
            int result = 0;

            var list = from item in unitOfWork.GetCostRepository().GetAll()
                       where item.CostDate.EndsWith(Date)
                       select item;

            foreach (var item in list)
            {
                result += item.CostValue;
            }

            return result.ToString();
        }

        // POST api/Cost/Add
        [HttpPost("Add")]
        public void Post([FromForm] Cost value)
        {
            unitOfWork.RegisterNewObject(value);
            unitOfWork.Commit();
        }

        // DELETE api/Cost/Delete/Products
        [HttpDelete("Delete/{TypeName}")]
        public void Delete(string TypeName)
        {
            foreach (var item in unitOfWork.GetCostRepository().GetAll())
            {
                if (item.CostName.Trim() == TypeName)
                {
                    unitOfWork.RegisterRemovedObject(item);
                    unitOfWork.Commit();
                }
            }
        }
    }
}
