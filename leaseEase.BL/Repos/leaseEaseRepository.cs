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
            List<Office> offices = await _context.Offices.Include(o => o.Reviews).ToListAsync();
            foreach (Office office in offices) {
                office.Type = await GetTypeByIdAsync(office.TypeId);
            }
            return offices;
        }
        public async Task<Office> GetOfficeByIdAsync(int officeId)
        {
            Office office = await _context.Offices.Include(o=>o.Reviews).FirstOrDefaultAsync(m => m.Id == officeId);
            if (office == null)
            {
                return null; 
            }
            office.Type = await GetTypeByIdAsync(office.TypeId);
            return office;
        }
        public async Task RemoveOfficeAsync(int officeId)
        {
            var office = await _context.Offices.FirstOrDefaultAsync(p => p.Id == officeId);
            if (office != null)
            {
                foreach(Review review in office.Reviews)
                {
                        await RemoveReviewAsync(review.Id);

                }
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

            return await _context.Users.FirstOrDefaultAsync(m => m.Email == email).ConfigureAwait(false);
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
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        //reviews
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            List<Review> reviews = await _context.Reviews.ToListAsync();
            return reviews;
        }
        public async Task<Review> AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            Office office = await GetOfficeByIdAsync(review.OfficeId);
            office.Reviews.Add(review);
            office.Rating = office.Reviews.Average(r => r.Rating);
            await UpdateOfficeAsync(office);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<List<Review>> GetReviewsByOfficeAsync(int officeId)
        {
            List<Review> reviews = await _context.Reviews.Where(r => r.OfficeId == officeId).ToListAsync();
            return reviews;
        }
        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            Review review = await _context.Reviews.FirstOrDefaultAsync(m => m.Id == reviewId);
            return review;
        }
        public async Task RemoveReviewAsync(int reviewId)
        {
            Review review = await _context.Reviews.FirstOrDefaultAsync(p => p.Id == reviewId);
            if (review != null)
            {
                review.Office = await GetOfficeByIdAsync(review.OfficeId);
                Office office = review.Office;
                office.Reviews.Remove(review);
                _context.Reviews.Remove(review);
                await UpdateOfficeAsync(office);
                await _context.SaveChangesAsync();
            }
        }
    }
}
