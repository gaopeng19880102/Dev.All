﻿// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年07月18日 9:29
//  
//  修改于：2013年07月19日 9:35
//  文件名：CasClient/Dev.CasClient/CasClientModule.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Web;
using Dev.CasClient.User;
using Dev.CasClient.Utils;

namespace Dev.CasClient
{
    internal class CasClientModule : IHttpModule
    {
        #region Readonly & Static Fields

        private readonly CasClient casClient = new CasClient();

        #endregion



        #region Instance Methods

        private void Write(string w)
        {
            HttpContext.Current.Response.Write(w);
        }

        #endregion

        #region Event Handling

        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var request = context.Request;


        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var request = context.Request;



            //else if (request.Path == Configuration.CasClientConfiguration.Config.LocalLogOffPath)
            //{
            //    bool local = Dev.Comm.Web.DevRequest.Get<bool>("local", false);

            //    string handedReturl;
            //    var ret = this.casClient.LoginOut(out handedReturl);

            //    if (local)
            //        context.Response.Write("Local");
            //    else
            //        context.Response.Redirect(handedReturl);

            //    context.ApplicationInstance.CompleteRequest();
            //    return;
            //}


        }

        private void OnEndRequest(object sender, EventArgs e)
        {

        }

        #endregion

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.OnBeginRequest;
            context.AuthenticateRequest += this.OnAuthenticateRequest;
            context.AcquireRequestState += this.OnAcquireRequestState;
            context.EndRequest += this.OnEndRequest;
        }

        void OnAcquireRequestState(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var request = context.Request;
            //只有在请求路径一是设置路径的时候才进行处理
            if (request.Path.ToLower() == Configuration.CasClientConfiguration.Config.LocalLoginPath.ToLower())
            {
                string returnUrl = Dev.Comm.Web.DevRequest.GetString("returnUrl");
                string ticket = Dev.Comm.Web.DevRequest.GetString("ticket");
                bool local = Dev.Comm.Web.DevRequest.Get("local", false);

                var handedReturl = Urls.GetReturnUrl(returnUrl);
                string strRedirectUrl, strUserName, strErrorText;

                //去除增加returl,同时删除ticket参数
                var strService = Urls.BuildServiceUrl(handedReturl);
                // HttpServerInfo.RebuildUrl("returnUrl", HttpUtility.UrlEncode(handedReturl), "ticket");

                if (local)
                {
                    strService = Dev.Comm.Web.DevRequest.GetString("service");
                }

                if (this.casClient.Login(ticket, strService, out strRedirectUrl, out strUserName, out strErrorText))
                {

                    if (CasClient.LoginSucess != null)
                    {
                        CasClient.LoginSucess();
                    }

                    if (local)
                    {
                        context.Response.ContentType = "text/html;charset=UTF-8";
                        context.Response.Write("OK");
                        context.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    if (string.IsNullOrEmpty(strRedirectUrl))
                    {
                        //对于 .Html 结尾的页面加一个 随机数，用以清除浏览器缓存，这样可以在一定程度上解决页面反复刷新的问题，
                        //这仅是一个补丁，不是最终解决方案，最终方案，应该是使用 Ajax方式，读取当前用户状态，然后显示于页面

                        //var jumpurl = handedReturl;

                        //var uncachekey = "uncache=" + System.DateTime.Now.Ticks;

                        //if (jumpurl.IndexOf('?') > 0)
                        //    jumpurl = jumpurl.TrimEnd('&') + "&" + uncachekey;
                        //else
                        //    jumpurl = jumpurl + "?" + uncachekey;


                        context.Response.Redirect(handedReturl);

                    }
                    else
                        context.Response.Redirect(strRedirectUrl);
                }
                else
                {
                    if (local)
                    {
                        context.Response.Write(strErrorText);
                        context.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    context.Response.Write(strErrorText);
                }


                context.ApplicationInstance.CompleteRequest();
                return;
            }
            else if (request.Path.ToLower() == Configuration.CasClientConfiguration.Config.LocalCheckPath.ToLower())
            {
                string responsestr;
                if (UserAuthenticate.UserAuthenticateManager.Provider.GetUserIsAuthenticated())
                {
                    var username = UserInfo.GetCurrentUserName();
                    responsestr = Dev.Comm.JsonConvert.ToJsonStrDyn(new { state = true, username = username });
                }
                else
                {
                    responsestr = Dev.Comm.JsonConvert.ToJsonStrDyn(new { state = false });
                }

                context.Response.ContentType = "application/json";

                context.Response.Write(responsestr);

                context.ApplicationInstance.CompleteRequest();
                return;
            }
            else if (request.Path.ToLower() == Configuration.CasClientConfiguration.Config.LocalLogOffPath.ToLower())
            {
                bool local = Dev.Comm.Web.DevRequest.Get<bool>("local", false);
                var s = Dev.Comm.Web.DevRequest.GetString("local");
                string handedReturl;
                var ret = this.casClient.LoginOut(out handedReturl);

                if (local)
                    context.Response.Write("Local");
                else
                    context.Response.Redirect(handedReturl);

                context.ApplicationInstance.CompleteRequest();
                return;

            }
        }


        public void Dispose()
        {
        }

        #endregion
    }
}