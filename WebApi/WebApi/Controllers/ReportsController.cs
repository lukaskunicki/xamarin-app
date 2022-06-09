#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;


        public ReportsController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetReport(int id, string action)
        {

            if(action == "getTicketsAssignedToEmployee")
            {
                var res = getTicketsAssignedToEmployee(id);
                if (res == null) return NotFound();
                return Ok(res);
            }
            if (action == "getTicketsReportedByEmployee")
            {
                var res = getTicketsReportedByEmployee(id);
                if (res == null) return NotFound();
                return Ok(res);
            }
            if (action == "getTicketsFromSprint")
            {
                var res = getTicketsFromSprint(id);
                if (res == null) return NotFound();
                return Ok(res);
            }


            return BadRequest();
        }

        private IEnumerable<Ticket> getTicketsAssignedToEmployee(int employeeId)
        {
            var result = _context.Ticket
            .Where(s => s.assignedEmployeeemployeeId == employeeId)
            .Include(s => s.priority)
            .Include(s => s.assignedEmployee)
            .Include(s => s.assignedEmployee.team)
            .Include(s => s.reporter)
            .Include(s => s.reporter.team)
            .Include(s => s.sprint)
            .ToList();

            if (result == null)
            {
                return null;
            }

            var getTickets = _mapper.Map<IList<Ticket>>(result);

            return getTickets;
        }

        private IEnumerable<Ticket> getTicketsReportedByEmployee(int employeeId)
        {
            var result = _context.Ticket
            .Where(s => s.reporteremployeeId == employeeId)
            .Include(s => s.priority)
            .Include(s => s.assignedEmployee)
            .Include(s => s.assignedEmployee.team)
            .Include(s => s.reporter)
            .Include(s => s.reporter.team)
            .Include(s => s.sprint)
            .ToList();

            if (result == null)
            {
                return null;
            }

            var getTickets = _mapper.Map<IList<Ticket>>(result);

            return getTickets;
        }

        private IEnumerable<Ticket> getTicketsFromSprint(int sprintId)
        {
            var result = _context.Ticket
            .Where(s => s.sprintId == sprintId)
            .Include(s => s.priority)
            .Include(s => s.assignedEmployee)
            .Include(s => s.assignedEmployee.team)
            .Include(s => s.reporter)
            .Include(s => s.reporter.team)
            .Include(s => s.sprint)
            .ToList();

            if (result == null)
            {
                return null;
            }

            var getTickets = _mapper.Map<IList<Ticket>>(result);

            return getTickets;
        }
    }
}
