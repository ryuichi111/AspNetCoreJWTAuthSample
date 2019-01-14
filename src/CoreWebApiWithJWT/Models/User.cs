using System.Collections.Generic;
using System.Linq;

namespace CoreWebApiWithJWT.Models
{
    public class User
    {
        public string ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }

        /// <summary>
        /// 対象ユーザのリフレッシュトークンを保持
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// ユーザー情報一覧
        /// Database接続のないサンプルにするためにstaticでオンメモリに強制保存。
        /// </summary>
        private static List<User> Users = new List<User>();

        static User()
        {
            User.Users.Add(
                new User()
                {
                    ID = "123456789",
                    Email = "daigo@testtest.jp",
                    NickName = "ryuichi111std",
                    Password = "P@ssword",
                    RefreshToken = ""
                });
        }

        /// <summary>
        /// emailとパスワードでユーザ認証を行います。
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User Authenticate(string email, string password)
        {
            User user = User.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// ユーザIDからUserを取得します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User GetByID(string id)
        {
            User user = User.Users.Where(u => u.ID == id).FirstOrDefault();
            return user;
        }
    }
}
