using API.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthsDataController : ControllerBase
    {
        public readonly MonthsDataContext context;        
        public MonthsDataController(MonthsDataContext _context)
        {
            context= _context;
            
        }

        [HttpGet("GetListOfMonths")]
        public IActionResult GetListOfMethods()
        {
            var monthsList = (from x in context.MonthsData
                              group x by new
                              {
                                  monthYear = x.MonthYear,
                                  monthNumber = x.MonthNumber
                              }
                              into monthsGroup
                              orderby monthsGroup.Key.monthYear.Length ascending,
                                        monthsGroup.Key.monthYear ascending,
                                        monthsGroup.Key.monthNumber.Length ascending,
                                        monthsGroup.Key.monthNumber ascending
                              select monthsGroup.Key).ToList();                            
            return Ok(monthsList);
        }

        [HttpGet("GetTableData")]
        public IActionResult GetTableData(string monthYear, string monthNumber, string tableName)
        {
            int ans = 0;
            var tableData = (from x in context.MonthsData
                             where x.MonthYear == monthYear && x.MonthNumber == monthNumber && x.TableName == tableName
                             select new
                             {
                                 id = x.Id,
                                 date = x.Date,
                                 name = x.Name,
                                 amount = x.Amount
                             }).ToList();
            return Ok(tableData);
        }

        [HttpPost("InsertTableRow")]
        public IActionResult InsertTableRow(MonthsData monthsDataFromFrontEnd)
        {
            int ans = 0;

            context.MonthsData.Add(monthsDataFromFrontEnd);
            context.SaveChanges();
            var id = context.MonthsData.OrderByDescending(p => p.Id).FirstOrDefault().Id;
            
            return Ok(id);
        }

        [HttpDelete("DeleteTableRow/{id}")]
        public IActionResult DeleteTableRow(int id)
        {
            var x = context.MonthsData.Where(item => item.Id == id).FirstOrDefault();
            context.MonthsData.Remove(x);
            context.SaveChanges();
            return Ok("success");
        }


    }
    
    
    

    
}    
