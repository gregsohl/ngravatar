using System;
using System.Web.Mvc;

namespace NGravatar.Html
{
    public static class UrlHelperExtensions
    {
        public static string Gravatar(this UrlHelper urlHelper, string email, int? size, string defaultImage, Rating? maxRating)
        {
            var gravatar = new Gravatar();
            gravatar.DefaultImage = defaultImage;
            if (size.HasValue) gravatar.Size = size.Value;
            if (maxRating.HasValue) gravatar.MaxRating = maxRating.Value;
            
            return gravatar.GetImageSource(email);            
        }
        
        public static string Gravatar(this UrlHelper urlHelper, string email, int? size)
        {
            return Gravatar(urlHelper, email, size, null, null);   
        }
    }
}
