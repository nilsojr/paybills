using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BillRepository : RepositoryBase, IBillRepository
    {
        public BillRepository(DataContext context) : base(context) { }

        public void Create(Bill bill) => _context.Bills.Add(bill);

        public void Delete(Bill bill) => _context.Bills.Remove(bill);

        public async Task<PagedList<Bill>> GetBillsAsync(string username, UserParams userParams)
        {
            var query =  _context.Bills
                .Include(b => b.BillType)
                .Include(b => b.Users)
                .Where(b => b.Users.Count(u => u.UserName == username) > 0)
                .OrderBy(b => b.Id)                
                .AsNoTracking();     

            return await PagedList<Bill>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        } 

        public async Task<Bill> GetBillByIdAsync(int id) => await _context.Bills.Include(b => b.BillType).SingleAsync(b => b.Id == id);

        public void Update(Bill bill) => _context.Entry(bill).State = EntityState.Modified;
    }
}