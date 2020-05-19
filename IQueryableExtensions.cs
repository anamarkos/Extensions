using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Needs testing
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return Queryable.Where(source, predicate);
            else
                return source;
        }


        /// <summary>
        /// Usage: IQueryable<T> query = IQueryableExtensions.WhereConditional(query, active, x => x.Active == active.Value);
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="nullablePropertyValue">The nullable condition.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereConditional<TSource, TProperty>(this IQueryable<TSource> source, Nullable<TProperty> nullablePropertyValue, Expression<Func<TSource, bool>> predicate) where TProperty : struct, IComparable
        {
            if (nullablePropertyValue.HasValue)
            {
                return Queryable.Where(source, predicate);
            }
            else
            {
                return source;
            }
        }


        /// <summary>
        /// query = IQueryableExtensions.WhereConditional(query, object.propList, x => object.propList.Contains(x.aValue));
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="LstOfPropertyValues">The condition.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereConditional<TSource>(this IQueryable<TSource> source, List<string> LstOfPropertyValues, Expression<Func<TSource, bool>> predicate)
        {
            if (LstOfPropertyValues != null && LstOfPropertyValues.Any())
            {
                return Queryable.Where(source, predicate);
            }
            else
            {
                return source;
            }
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="orderByProperty">The order by property.</param>
        /// <param name="desc">if set to <c>true</c> [desc].</param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc) where TEntity : class
        {
            string command = desc ? "OrderByDescending" : "OrderBy";

            var type = typeof(TEntity);

            var property = type.GetProperty(orderByProperty);

            var parameter = Expression.Parameter(type, "p");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },

                                   source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="desc">if set to <c>true</c> [desc].</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity, TKey>(this IQueryable<TEntity> source, Expression<Func<TEntity, TKey>> expression, bool desc) where TEntity : class
        {
            if (desc)
                return source.OrderByDescending(expression);
            else
                return source.OrderBy(expression);
        }

        /// <summary>
        /// Thens the by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="desc">if set to <c>true</c> [desc].</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> ThenBy<TEntity, TKey>(this IOrderedQueryable<TEntity> source, Expression<Func<TEntity, TKey>> expression, bool desc) where TEntity : class
        {
            if (desc)
                return source.ThenByDescending(expression);
            else
                return source.ThenBy(expression);
        }
    }
}
