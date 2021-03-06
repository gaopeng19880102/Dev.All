// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IEFFetchingRepository.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Data.Objects.DataClasses;
namespace Kt.Framework.Repository.Data.EntityFramework
{
    public interface IEFFetchingRepository<TEntity, TFetch> : IRepository<TEntity> where TEntity : EntityObject
    {
        EFRepository<TEntity> RootRepository { get; }

        string FetchingPath { get; }
    }
}