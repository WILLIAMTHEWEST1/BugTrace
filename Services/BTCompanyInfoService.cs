using BugTrace.Data;
using BugTrace.Models;
using BugTrace.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Services
{
    public class BTCompanyInfoService : IBTCompanyInfoService

    {
        private readonly ApplicationDbContext _context;

        public BTCompanyInfoService(ApplicationDbContext context)
        {
            _context = context;
        }



    public async Task<List<BTUser>> GetAllMembersAsync(int companyId)
        {
            try
            {
                List<BTUser> result = new List<BTUser>();

                result = await _context.Users.Where(u => u.CompanyId == companyId).ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
