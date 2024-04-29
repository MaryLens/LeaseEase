﻿using leaseEase.Domain.Models.Off;
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

        //user
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmailAndPwAsync(string email, string password);
        Task<User> GetUserByIdAsync(int typeId);
        Task<User> AddUserAsync(UserRegisterData userData);
    }
}
