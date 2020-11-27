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
    public class IncomeController : ControllerBase
    {
        IUnitOfWork unitOfWork;

        public IncomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET api/Income/List
        [HttpGet("List")]
        public IEnumerable<Income> GetIncomeList()
        {
            return unitOfWork.GetIncomeRepository().GetAll();
        }

        // GET api/Income/List/Products
        [HttpGet("List/{name}")]
        public IEnumerable<Income> GetIncomeListByName(string name)
        {
            var items = from item in unitOfWork.GetIncomeRepository().GetAll()
                        where item.IncomeName.Trim() == name
                        select item;
            return items;
        }

        // GET api/Income/Day/12.04.2020
        [HttpGet("Day/{Date}")]
        public string GetDaySumForDate(string Date)
        {
            int result = 0;

            foreach (var item in unitOfWork.GetIncomeRepository().GetAll())
            {
                if (item.IncomeDate == Date)
                {
                    result += item.IncomeValue;
                }
            }

            return result.ToString();
        }

        // GET api/Income/Day/03.20
        [HttpGet("Month/{Date}")]
        public string GetMonthSumForDate(string Date)
        {
            int result = 0;

            var list = from item in unitOfWork.GetIncomeRepository().GetAll()
                       where item.IncomeDate.EndsWith(Date)
                       select item;

            foreach (var item in list)
            {
                result += item.IncomeValue;
            }

            return result.ToString();
        }

        // POST api/Income/Add
        [HttpPost("Add")]
        public void Post([FromForm] Income value)
        {
            unitOfWork.RegisterNewObject(value);
            unitOfWork.Commit();
        }

        // DELETE api/Income/Delete/Products
        [HttpDelete("Delete/{TypeName}")]
        public void Delete(string TypeName)
        {
            foreach (var item in unitOfWork.GetIncomeRepository().GetAll())
            {
                if (item.IncomeName.Trim() == TypeName)
                {
                    unitOfWork.RegisterRemovedObject(item);
                    unitOfWork.Commit();
                }
            }
        }
    }
}
