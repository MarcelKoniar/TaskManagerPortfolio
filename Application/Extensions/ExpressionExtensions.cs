using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Extensions
{

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(left, parameter),
                Expression.Invoke(right, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

}
