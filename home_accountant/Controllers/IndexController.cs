using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace home_accountant
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET api/Index
        [HttpGet]
        public string Index()
        {
            return "It's Home Accountant.\n\n" +
                "Available endpoints:\n" +
                "GET api/Cost/List - Show list of costs\n" +
                "GET api/Cost/List/{TypeName} - Show list of costs by TypeName\n" +
                "GET api/Cost/SumDay/{Date} - Show sum of day by Date\n" +
                "GET api/Cost/SumMonth/{MonthDate} - Show sum of month by Date\n" +
                "POST api/Cost/Add - Add new cost to database\n" +
                "DELETE api/Cost/Delete/{TypeName} - Delete cost from database\n\n" +
                "GET api/Income/List - Show list of incomes\n" +
                "GET api/Income/List/{TypeName} - Show list of incomes by TypeName\n" +
                "GET api/Income/SumDay/{Date} - Show sum of day by Date\n" +
                "GET api/Income/SumMonth/{MonthDate} - Show sum of month by Date\n" +
                "POST api/Income/Add - Add new income to database\n" +
                "DELETE api/Income/Delete/{TypeName} - Delete income from database";
        }
    }
}
