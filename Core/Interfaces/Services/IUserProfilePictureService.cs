﻿using Core.ViewModels.User;
using System.Drawing;

namespace Core.Interfaces.Services
{
    public interface IUserProfilePictureService
    {
        public Task<string> UploadAsync(
            Image image,
            string email,
            string imageFormat);

        public Task DeleteAsync(string imageLink);
    }
}