using leaseEase.DAL;
using leaseEase.Domain.Enum.User;
using leaseEase.Domain.Models.Off;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.BL.Repos
{
    public class leaseEaseRepository : ILeaseEaseRepository
    {
        private readonly leaseEaseContext _context;
        public leaseEaseRepository(leaseEaseContext context) { 
        this._context = context;
        }

        //office
        public async Task<Office> AddOfficeAsync(Office office)
        {
            _context.Offices.Add(office);
            await _context.SaveChangesAsync();
            return office;
        }
        public async Task<List<Office>> GetAllOfficesAsync()
        {
            return await _context.Offices.ToListAsync();
        }
        public async Task<Office> GetOfficeByIdAsync(int officeId)
        {
            return await _context.Offices.FirstOrDefaultAsync(m => m.Id == officeId);
        }
        public async Task RemoveOfficeAsync(int officeId)
        {
            var office = await _context.Offices.FirstOrDefaultAsync(p => p.Id == officeId);
            if (office != null)
            {
                _context.Offices.Remove(office);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Office> UpdateOfficeAsync(Office office)
        {
            _context.Set<Office>().Attach(office);
            _context.Entry(office).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return office;
        }

        //facility and type
        public async Task<List<Facility>> GetAllFacilitiessAsync()
        {
            return await _context.Facilities.ToListAsync();
        }
        public async Task<Facility> GetFacilityByIdAsync(int facilityId)
        {
            return await _context.Facilities.FirstOrDefaultAsync(m => m.Id == facilityId);
        }
        public async Task<List<TypesOfOffice>> GetAllTypesAsync()
        {
            return await _context.TypesOfOffice.ToListAsync();
        }
        public async Task<TypesOfOffice> GetTypeByIdAsync(int typeId)
        {
            return await _context.TypesOfOffice.FirstOrDefaultAsync(m => m.Id == typeId);
        }

        //user
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int typeId)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Id == typeId);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Email == email);
        }
        public async Task<User> GetUserByEmailAndPwAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
        }
        public async Task<User> AddUserAsync(UserRegisterData userData)
        {
            User user = new User { 
            Name = userData.Name,
            Email = userData.Email,
            Password = PwManager.Md5Crypt(userData.Password),
            LastLogin = userData.LastLogin,
            UserIp = userData.UserIp,
            Created = DateTime.Now,
            Role = Roles.User
            };
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
