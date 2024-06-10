using leaseEase.Domain.Models.Off;
using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.BL.Repos
{
    public interface ILeaseEaseRepository
    {
        //office
        Task<List<Office>> GetAllOfficesAsync();
        Task<Office> GetOfficeByIdAsync(int officeId);
        Task<Office> AddOfficeAsync(Office office);
        Task<Office> UpdateOfficeAsync(Office office);
        Task RemoveOfficeAsync(int officeId);

        //facility and type
        Task<List<Facility>> GetAllFacilitiessAsync();
        Task<List<TypesOfOffice>> GetAllTypesAsync();
        Task<Facility> GetFacilityByIdAsync(int facilityId);
        Task<TypesOfOffice> GetTypeByIdAsync(int typeId);


        //reviews
        Task<List<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<List<Review>> GetReviewsByOfficeAsync(int officeId);
        Task<Review> AddReviewAsync(Review review);
        Task RemoveReviewAsync(int reviewId);

        //Office images
        Task<OfficeImg> AddOffImageAsync(OfficeImg image);
        Task RemoveOffImageAsync(int imageId);
        Task<OfficeImg> GetOffImageByIdAsync(int imageId);

        //bookings
        
        Task<List<Booking>> GetAllBookinsAsync();
        Task<Booking> AddBookingAsync(Booking booking);
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task RemoveBookingAsync(int bookingId);
        Task<Booking> UpdateBookingAsync(Booking booking);


        ////users!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //sessions
        Task<UDbSession> GetSessionByEmailAsync(string Email);
        Task<UDbSession> GetSessionByCookieAsync(string cookie);
        Task<UDbSession> UpdateSessionAsync(UDbSession session);
        Task RemoveSessionAsync(UDbSession session);
        Task<UDbSession> AddNewSessionAsync(UDbSession session);

        //user
        Task<TobeCreatorData> GetCreatorByIdAsync(int typeId);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmailAndPwAsync(string email, string password);
        Task<User> GetUserByIdAsync(int typeId);
        Task<User> AddUserAsync(UserRegisterData userData);
        Task<User> UpdateUserAsync(User user);
    }


}
