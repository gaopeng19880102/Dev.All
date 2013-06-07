﻿// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IDocFile.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.IO;

namespace Dev.Framework.FileServer
{
    public interface IDocFile
    {
        string Save(Stream stream, string fileName);

        string Update(Stream stream, string fileKey);

        string GetDocUrl(string fileKey);
    }
}