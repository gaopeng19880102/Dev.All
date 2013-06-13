// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2013��02��19�� 14:15
// 
// �޸��ڣ�2013��02��19�� 15:10
// �ļ�����FormUserAuthenticate.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************

using System.Collections.Generic;

namespace Dev.CasClient.UserAuthenticate
{
    using System.Web;
    using System.Web.Security;

    /// <summary>
    /// 
    /// </summary>
    public class FormUserAuthenticate : IUserAuthenticate
    {
        #region Public Methods and Operators

        /// <summary>
        ///     �ǳ�
        /// </summary>
        public virtual void CurUserLoginOut()
        {
            HttpContext context = HttpContext.Current;

            // Necessary for ASP.NET MVC Support.
            if (context.User.Identity.IsAuthenticated)
            {
                ClearAuthCookie();
            }
        }

        /// <summary>
        ///     �Ƿ��Ѿ���֤ͨ��
        /// </summary>
        /// <returns></returns>
        public virtual bool GetUserIsAuthenticated()
        {
            HttpContext context = HttpContext.Current;

            bool result = (context.User != null && context.User.Identity.IsAuthenticated);

            return result;
        }

        public virtual void SignUserLogin(string strUserName, Dictionary<string, string> extDatas)
        {
            SetAuthCookie(strUserName);
        }



        #endregion

        #region Methods

        private static void ClearAuthCookie()
        {
            FormsAuthentication.SignOut();
        }

        private static void SetAuthCookie(string userName)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
        }

        #endregion
    }
}