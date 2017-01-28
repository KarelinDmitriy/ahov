using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AhovRepository.Entity;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace AhovRepository
{
	public interface IDatabaseProvider
	{
		List<TEntity> GetAll<TEntity>() where TEntity : class;
		List<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predidate) where TEntity : class;
		void Insert<TEntity>(TEntity entity) where TEntity : class;
		TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
		void Update<TEntity>(TEntity entity) where TEntity : class;
	}

	public class MySqlDatabaseProvider : IDatabaseProvider
	{
		private readonly ISessionFactory _sessionFactory;

		public MySqlDatabaseProvider()
		{
			_sessionFactory = Fluently
					.Configure()
					.Database(MySQLConfiguration
						.Standard
						.ConnectionString(x => x
							.Server("us-cdbr-azure-west-c.cloudapp.net")
							.Database("ahov_database")
							.Username("b337ffe844fbeb")
							.Password("f2e08696")
						)
						.ShowSql())
					.Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserEntity>())
					.BuildSessionFactory();
		}

		private ISession OpenSession()
		{
				
			return _sessionFactory.OpenSession();
		}

		public List<TEntity> GetAll<TEntity>() where TEntity : class
		{
			var result = new List<TEntity>();
			using (var session = OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					var entities = session.QueryOver<TEntity>().List();
					result.AddRange(entities);
					transaction.Commit();
				}
			}
			return result;
		}

		public List<TEntity> Where<TEntity>(Expression<Func<TEntity, bool>> predidate) where TEntity : class
		{
			var result = new List<TEntity>();
			using (var session = OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					var entities = session.QueryOver<TEntity>().Where(predidate);
					result.AddRange(entities.List());
					transaction.Commit();
				}
			}
			return result;
		}

		public void Insert<TEntity>(TEntity entity) where TEntity : class
		{
			using (var session = OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					session.Save(entity);
					transaction.Commit();
				}
			}
		}

		public TEntity GetOne<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
		{
			TEntity entity = default(TEntity);
			using (var session = OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					entity = session.QueryOver<TEntity>().Where(predicate).SingleOrDefault();
					transaction.Commit();
				}
			}
			return entity;
		}

		public void Update<TEntity>(TEntity entity) where TEntity : class
		{
			using (var session = OpenSession())
			{
				using (var transaction = session.BeginTransaction())
				{
					session.Update(entity);
					transaction.Commit();
				}
			}
		}
	}
}