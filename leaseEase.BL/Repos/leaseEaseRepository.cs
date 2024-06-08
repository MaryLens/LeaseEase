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
            List<Office> offices = await _context.Offices.Include(o => o.Reviews).Include(o => o.Bookings).Include(o => o.Facilities).Include(o=>o.Images).ToListAsync();
            foreach (Office office in offices) {
                office.Type = await GetTypeByIdAsync(office.TypeId);
                office.Creator = await GetCreatorByIdAsync(office.CreatorId);
            }
            return offices;
        }
        public async Task<Office> GetOfficeByIdAsync(int officeId)
        {
            Office office = await _context.Offices.Include(o=>o.Reviews).Include(o => o.Reviews).Include(o=>o.Facilities).Include(o => o.Images).FirstOrDefaultAsync(m => m.Id == officeId);
            if (office == null)
            {
                return null; 
            }
            office.Creator = await GetCreatorByIdAsync(office.CreatorId);
            office.Type = await GetTypeByIdAsync(office.TypeId);
            return office;
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
        //reviews
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            List<Review> reviews = await _context.Reviews.ToListAsync();
            foreach (Review review in reviews)
            {
                review.Office = await GetOfficeByIdAsync(review.OfficeId);
                review.Creator = await GetUserByIdAsync(review.CreatorId);
            }
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
            foreach(Review review in reviews)
            {
                review.Office = await GetOfficeByIdAsync(review.OfficeId);
                review.Creator = await GetUserByIdAsync(review.CreatorId);
            }
            return reviews;
        }
        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            Review review = await _context.Reviews.FirstOrDefaultAsync(m => m.Id == reviewId);
            review.Office = await GetOfficeByIdAsync(review.OfficeId);
            review.Creator = await GetUserByIdAsync(review.CreatorId);
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

        //Office image
        public async Task<OfficeImg> AddOffImageAsync(OfficeImg image)
        {
            _context.OfficeImages.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }
        public async Task<OfficeImg> GetOffImageByIdAsync(int imageId)
        {
            OfficeImg img = await _context.OfficeImages.FirstOrDefaultAsync(m => m.Id == imageId);
            return img;
        }
        public async Task RemoveOffImageAsync(int imageId)
        {
            OfficeImg image = await _context.OfficeImages.FirstOrDefaultAsync(p => p.Id == imageId);
            if (image != null)
            {
                image.Office = await GetOfficeByIdAsync(image.OfficeId);
                Office office = image.Office;
                office.Images.Remove(image);
                _context.OfficeImages.Remove(image);
                await UpdateOfficeAsync(office);
                await _context.SaveChangesAsync();
            }
        }

        //bookings
        public async Task<List<Booking>> GetAllBookinsAsync()
        {
            List<Booking> bookings = await _context.Bookings.ToListAsync();
            foreach (Booking booking in bookings)
            {
                booking.Office = await GetOfficeByIdAsync(booking.OfficeId);
                booking.Creator = await GetUserByIdAsync(booking.CreatorId);
            }
            return bookings;
        }
        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            Office office = await GetOfficeByIdAsync(booking.OfficeId);
            office.Bookings.Add(booking);
            await UpdateOfficeAsync(office);
            await _context.SaveChangesAsync();
            return booking;
        }
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            Booking booking = await _context.Bookings.FirstOrDefaultAsync(m => m.Id == bookingId);
            booking.Office = await GetOfficeByIdAsync(booking.OfficeId);
            booking.Creator = await GetUserByIdAsync(booking.CreatorId);
            return booking;
        }
        public async Task RemoveBookingAsync(int bookingId)
        {
            Booking booking = await _context.Bookings.FirstOrDefaultAsync(p => p.Id == bookingId);
            if (booking != null)
            {
                booking.Office = await GetOfficeByIdAsync(booking.OfficeId);
                Office office = booking.Office;
                office.Bookings.Remove(booking);
                _context.Bookings.Remove(booking);
                await UpdateOfficeAsync(office);
                await _context.SaveChangesAsync();
            }
        }

        //////users!!!!!!!!!!!!!!!!!!!!!!!!!
        //sessions
        public async Task<UDbSession> GetSessionByEmailAsync(string Email)
        {
            UDbSession session = await _context.Sessions.FirstOrDefaultAsync(m => m.Email == Email);
            return session;
        }
        public async Task<UDbSession> GetSessionByCookieAsync(string cookie)
        {
            UDbSession session = await _context.Sessions.FirstOrDefaultAsync(m => m.CookieString == cookie);
            return session;
        }
        public async Task<UDbSession> UpdateSessionAsync(UDbSession session)
        {
            _context.Entry(session).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return session;
        }
        public async Task RemoveSessionAsync(UDbSession session)
        {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
        }


        public async Task<UDbSession> AddNewSessionAsync(UDbSession session)
        {
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        //user
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u=>u.MyBookings).Include(u => u.MyReviews).Include(u=>u.creatorData).Include(u=>u.WishList).ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int typeId)
        {
            return await _context.Users.Include(u => u.MyBookings).Include(u => u.MyReviews).Include(u => u.creatorData).Include(u => u.WishList).FirstOrDefaultAsync(m => m.Id == typeId);
        }
        public async Task<TobeCreatorData> GetCreatorByIdAsync(int typeId)
        {
            return await _context.tobeCreatorDatas.Include(u => u.MyOffices).FirstOrDefaultAsync(m => m.Id == typeId);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {

            return await _context.Users.Include(u => u.MyBookings).Include(u => u.MyReviews).Include(u => u.creatorData).Include(u => u.WishList).FirstOrDefaultAsync(m => m.Email == email).ConfigureAwait(false);
        }
        public async Task<User> GetUserByEmailAndPwAsync(string email, string password)
        {
            return await _context.Users.Include(u => u.MyBookings).Include(u => u.MyReviews).Include(u => u.creatorData).Include(u => u.WishList).FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
        }
        public async Task<User> AddUserAsync(UserRegisterData userData)
        {
            User user = new User
            {
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

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
