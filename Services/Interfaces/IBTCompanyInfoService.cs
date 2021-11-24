using BugTrace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Services.Interfaces
{
    public interface IBTCompanyInfoService
    {
        public Task<List<BTUser>> GetAllMembersAsync(int companyId);
    }
}
