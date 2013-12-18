using System;
using System.Text;
using System.Security.Cryptography;

namespace NGravatar {

    internal class GravatarHasher {

        private readonly UTF8Encoding Encoding = new UTF8Encoding();
        private readonly MD5CryptoServiceProvider MD5CryptoServiceProvider = new MD5CryptoServiceProvider();

        public static GravatarHasher DefaultInstance {
            get {
                if (null == _DefaultInstance) _DefaultInstance = new GravatarHasher();
                return _DefaultInstance;
            }
            set {
                if (null == value) throw new ArgumentNullException("DefaultInstance");
                _DefaultInstance = value;
            }
        }
        private static GravatarHasher _DefaultInstance;

        public virtual string Hash(string emailAddress) {
            if (null == emailAddress) throw new ArgumentNullException("emailAddress");

            emailAddress = emailAddress.Trim().ToLower();

            var hashedBytes = MD5CryptoServiceProvider.ComputeHash(Encoding.GetBytes(emailAddress));
            var length = hashedBytes.Length;
            var sb = new StringBuilder(length * 2);

            for (var i = 0; i < length; i++) {
                sb.Append(hashedBytes[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}
