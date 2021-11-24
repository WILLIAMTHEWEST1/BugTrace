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
    public class BTicketService : IBTTicketService
    {
        private readonly ApplicationDbContext _context;

        public BTicketService(ApplicationDbContext context)
        {
              _context = context;  
        }


        public async Task AddNewTicketAsync(Ticket ticket)
        {
            try
            {
               await _context.AddAsync(ticket);
               await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task AddTicketAttachmentAsync(TicketAttachment ticketAttachment)
        {
            throw new NotImplementedException();
        }

        public Task AddTicketCommentAsync(TicketComment ticketComment)
        {
            throw new NotImplementedException();
        }

        public async Task ArchiveTicketAsync(Ticket ticket)
        {
            try
            {
                ticket.Archived = true;
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task AssignTicketAsync(int ticketId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Ticket>> GetAllTicketsByCompanyAsync(int companyId)
        {
            try
            {
                List<Ticket> tickets = await _context.Projects.
               Where(p => p.CompanyId == companyId).
               SelectMany(p => p.Tickets).
                Include(t => t.Attachments).
                Include(t => t.Comments).
                Include(t => t.DeveloperUser).
                Include(t => t.History).
                Include(t => t.OwnerUser).
                Include(t => t.TicketPriority).
                Include(t => t.TicketStatus).
                Include(t => t.TicketType).
                Include(t => t.Project).
                ToListAsync();

                return tickets;
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        public Task<List<Ticket>> GetAllTicketsByPriorityAsync(int companyId, string priorityName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsByStatusAsync(int companyId, string statusName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTicketsByTypeAsync(int companyId, string typeName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetArchivedTicketsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByPriorityAsync(string priorityName, int companyId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByRoleAsync(string role, string userId, int projectId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByStatusAsync(string statusName, int companyId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetProjectTicketsByTypeAsync(string typeName, int companyId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketAsNoTrackingAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<TicketAttachment> GetTicketAttachmentByIdAsync(int ticketAttachmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                return await _context.Tickets.
                    Include(t => t.DeveloperUser).
                    Include(t => t.OwnerUser).
                    Include(t => t.Project).
                    Include(t => t.TicketPriority).
                    Include(t => t.TicketStatus).
                    Include(t => t.TicketType).
                    Include(t => t.Comments).
                    Include(t => t.Attachments).
                    Include(t => t.History).
                    FirstOrDefaultAsync(t => t.Id == ticketId);
            }
            catch (Exception)
            {

                throw;
            }  
        }

        public Task<BTUser> GetTicketDeveloperAsync(int ticketId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetTicketsByRoleAsync(string role, string userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetTicketsByUserIdAsync(string userId, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetUnassignedTicketsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<int?> LookupTicketPriorityIdAsync(string priorityName)
        {
            throw new NotImplementedException();
        }

        public Task<int?> LookupTicketStatusIdAsync(string statusName)
        {
            throw new NotImplementedException();
        }

        public Task<int?> LookupTicketTypeIdAsync(string typeName)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
