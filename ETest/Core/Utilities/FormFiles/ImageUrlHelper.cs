using System;

namespace Core.Utilities.FormFiles
{
    public class ImageUrlHelper
    {
        private const string UserImageUrl = "images/users";
        public static string CreateUserImageUrl() => UserImageUrl;

        public static string GuidImage()=> Guid.NewGuid() + ".jpeg";
    }
}