using System;
using System.Collections.Generic;

namespace NGravatar
{
    public enum Rating
    {
        G, PG, R, X   
    }
    
    public class Gravatar
    {
        private static readonly int MinSize = 1;
        private static readonly int MaxSize = 512;
        
        private int _Size = 80;
        private Rating _MaxRating = Rating.PG;
        
        public string DefaultImage { get; set; }

        public int Size            
        {
            get { return _Size; }
            set
            {
                if (value < MinSize || value > MaxSize)
                    throw new ArgumentOutOfRangeException("Size", "The allowable range for 'Size' is '" + MinSize + "' to '" + MaxSize + "', inclusive.");
                _Size = value;
            }
        }
        
        public Rating MaxRating
        {
            get { return _MaxRating; }
            set { _MaxRating = value; }
        }
        
        public string Render(string email)
        {
            return Render(email, null);   
        }

        public string GetImageSource(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email.Trim()))
                throw new ArgumentException("The email is empty.", "email");

            var imageUrl = "http://www.gravatar.com/avatar.php?";
            var encoder = new System.Text.UTF8Encoding();
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(email.ToLower()));
            var sb = new System.Text.StringBuilder(hashedBytes.Length * 2);

            for (var i = 0; i < hashedBytes.Length; i++)
                sb.Append(hashedBytes[i].ToString("X2"));

            imageUrl += "gravatar_id=" + sb.ToString().ToLower();
            imageUrl += "&rating=" + MaxRating.ToString();
            imageUrl += "&size=" + Size.ToString();

            if (!string.IsNullOrEmpty(DefaultImage))
                imageUrl += "&default=" + System.Web.HttpUtility.UrlEncode(DefaultImage);

            return imageUrl;
        }

        public string Render(string email, IDictionary<string, string> htmlAttributes)
        {
            var imageUrl = GetImageSource(email);
            
            var attrs = "";
            if (htmlAttributes != null)
            {
                htmlAttributes.Remove("src");
                htmlAttributes.Remove("width");
                htmlAttributes.Remove("height");
                foreach (var kvp in htmlAttributes)
                    attrs += kvp.Key + "=\"" + kvp.Value + "\" ";   
            }
            
            var img = "<img " + attrs;
            img += "src=\"" + imageUrl + "\" ";
            img += "width=\"" + Size + "\" ";
            img += "height=\"" + Size + "\" ";
            img += "/>";
                        
            return img;
        }
    }
}
