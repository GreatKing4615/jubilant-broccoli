﻿namespace JubilantBroccoli.Infrastructure.UnitOfWork.Contracts;

public interface IRepositoryFactory
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}