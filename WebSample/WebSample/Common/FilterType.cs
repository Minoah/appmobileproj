using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSample.Common
{
    public static class FilterBuilder
    {
        public static IdFilter Merge(IdFilter parameter1, IdFilter parameter2)
        {
            if (parameter1 == null)
                parameter1 = new IdFilter();
            if (parameter2 != null)
            {
                if (parameter2.Equal != null)
                    parameter1.Equal = parameter2.Equal;
                if (parameter2.NotEqual != null)
                    parameter1.NotEqual = parameter2.NotEqual;
                if (parameter2.In != null)
                {
                    if (parameter1.In == null)
                        parameter1.In = parameter2.In;
                    else
                        parameter1.In.AddRange(parameter2.In);
                }
                if (parameter2.NotIn != null)
                {
                    if (parameter1.NotIn == null)
                        parameter1.NotIn = parameter2.NotIn;
                    else
                        parameter1.NotIn.AddRange(parameter2.NotIn);
                }
            }
            return parameter1;
        }
        public static GuidFilter Merge(GuidFilter parameter1, GuidFilter parameter2)
        {
            if (parameter1 == null)
                parameter1 = new GuidFilter();
            if (parameter2 != null)
            {
                if (parameter2.Equal != null)
                    parameter1.Equal = parameter2.Equal;
                if (parameter2.NotEqual != null)
                    parameter1.NotEqual = parameter2.NotEqual;
                if (parameter2.In != null)
                {
                    if (parameter1.In == null)
                        parameter1.In = parameter2.In;
                    else
                        parameter1.In.AddRange(parameter2.In);
                }
                if (parameter2.NotIn != null)
                {
                    if (parameter1.NotIn == null)
                        parameter1.NotIn = parameter2.NotIn;
                    else
                        parameter1.NotIn.AddRange(parameter2.NotIn);
                }
            }
            return parameter1;
        }
        public static StringFilter Merge(StringFilter parameter1, StringFilter parameter2)
        {
            if (parameter1 == null)
                parameter1 = new StringFilter();
            if (parameter2 != null)
            {
                if (parameter2.Equal != null)
                    parameter1.Equal = parameter1.Equal;

                if (parameter2.NotEqual != null)
                    parameter1.NotEqual = parameter1.NotEqual;

                if (parameter2.Contain != null)
                    parameter1.Contain = parameter1.Contain;

                if (parameter2.NotContain != null)
                    parameter1.NotContain = parameter1.NotContain;

                if (parameter2.StartWith != null)
                    parameter1.StartWith = parameter1.StartWith;

                if (parameter2.NotStartWith != null)
                    parameter1.NotStartWith = parameter1.NotStartWith;

                if (parameter2.EndWith != null)
                    parameter1.EndWith = parameter1.EndWith;

                if (parameter2.NotEndWith != null)
                    parameter1.NotEndWith = parameter1.NotEndWith;
            }
            return parameter1;
        }
        public static LongFilter Merge(LongFilter parameter1, LongFilter parameter2)
        {
            if (parameter1 == null)
                parameter1 = new LongFilter();
            if (parameter2 != null)
            {
                if (parameter2.Equal != null)
                    parameter1.Equal = parameter1.Equal;

                if (parameter2.NotEqual != null)
                    parameter1.NotEqual = parameter1.NotEqual;

                if (parameter2.Less != null)
                    parameter1.Less = parameter1.Less;

                if (parameter2.LessEqual != null)
                    parameter1.LessEqual = parameter1.LessEqual;

                if (parameter2.Greater != null)
                    parameter1.Greater = parameter1.Greater;

                if (parameter2.GreaterEqual != null)
                    parameter1.GreaterEqual = parameter1.GreaterEqual;
            }
            return parameter1;
        }

        public static DecimalFilter Merge(DecimalFilter parameter1, DecimalFilter parameter2)
        {
            if (parameter1 == null)
                parameter1 = new DecimalFilter();
            if (parameter2 != null)
            {
                if (parameter2.Equal != null)
                    parameter1.Equal = parameter1.Equal;

                if (parameter2.NotEqual != null)
                    parameter1.NotEqual = parameter1.NotEqual;

                if (parameter2.Less != null)
                    parameter1.Less = parameter1.Less;

                if (parameter2.LessEqual != null)
                    parameter1.LessEqual = parameter1.LessEqual;

                if (parameter2.Greater != null)
                    parameter1.Greater = parameter1.Greater;

                if (parameter2.GreaterEqual != null)
                    parameter1.GreaterEqual = parameter1.GreaterEqual;
            }
            return parameter1;
        }

        public static DateFilter Merge(DateFilter parameter1, DateFilter parameter2)
        {
            if (parameter1 == null)
                parameter1 = new DateFilter();
            if (parameter2 != null)
            {
                if (parameter2.Equal != null)
                    parameter1.Equal = parameter1.Equal;

                if (parameter2.NotEqual != null)
                    parameter1.NotEqual = parameter1.NotEqual;

                if (parameter2.Less != null)
                    parameter1.Less = parameter1.Less;

                if (parameter2.LessEqual != null)
                    parameter1.LessEqual = parameter1.LessEqual;

                if (parameter2.Greater != null)
                    parameter1.Greater = parameter1.Greater;

                if (parameter2.GreaterEqual != null)
                    parameter1.GreaterEqual = parameter1.GreaterEqual;
            }
            return parameter1;
        }
    }


    public class StringFilter
    {
        public string Equal { get; set; }
        public string NotEqual { get; set; }
        public string Contain { get; set; }
        public string NotContain { get; set; }
        public string StartWith { get; set; }
        public string NotStartWith { get; set; }
        public string EndWith { get; set; }
        public string NotEndWith { get; set; }

        public StringFilter ToLower()
        {
            Equal = Equal?.ToLower();
            NotEqual = NotEqual?.ToLower();
            Contain = Contain?.ToLower();
            NotContain = NotContain?.ToLower();
            StartWith = StartWith?.ToLower();
            NotStartWith = NotStartWith?.ToLower();
            EndWith = EndWith?.ToLower();
            NotEndWith = NotEndWith?.ToLower();
            return this;
        }

        public StringFilter ToUpper()
        {
            Equal = Equal?.ToUpper();
            NotEqual = NotEqual?.ToUpper();
            Contain = Contain?.ToUpper();
            NotContain = NotContain?.ToUpper();
            StartWith = StartWith?.ToUpper();
            NotStartWith = NotStartWith?.ToUpper();
            EndWith = EndWith?.ToUpper();
            NotEndWith = NotEndWith?.ToUpper();
            return this;
        }
        public bool HasValue
        {
            get
            {
                return !(string.IsNullOrWhiteSpace(Equal) && string.IsNullOrWhiteSpace(NotEqual) && string.IsNullOrWhiteSpace(Contain)
                    && string.IsNullOrWhiteSpace(NotContain) && string.IsNullOrWhiteSpace(StartWith) && string.IsNullOrWhiteSpace(NotStartWith)
                    && string.IsNullOrWhiteSpace(EndWith) && string.IsNullOrWhiteSpace(NotEndWith));
            }
        }
    }

    public class LongFilter
    {
        public long? Equal { get; set; }
        public long? NotEqual { get; set; }
        public long? Less { get; set; }
        public long? LessEqual { get; set; }
        public long? Greater { get; set; }
        public long? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class DecimalFilter
    {
        public decimal? Equal { get; set; }
        public decimal? NotEqual { get; set; }
        public decimal? Less { get; set; }
        public decimal? LessEqual { get; set; }
        public decimal? Greater { get; set; }
        public decimal? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class DateFilter
    {
        public DateTime? Equal { get; set; }
        public DateTime? NotEqual { get; set; }
        public DateTime? Less { get; set; }
        public DateTime? LessEqual { get; set; }
        public DateTime? Greater { get; set; }
        public DateTime? GreaterEqual { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || Less.HasValue || LessEqual.HasValue || Greater.HasValue || GreaterEqual.HasValue;
            }
        }
    }

    public class GuidFilter
    {
        public Guid? Equal { get; set; }
        public Guid? NotEqual { get; set; }
        public List<Guid> In { get; set; }
        public List<Guid> NotIn { get; set; }
        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || In != null || NotIn != null;
            }
        }
    }

    public class IdFilter
    {
        public long? Equal { get; set; }
        public long? NotEqual { get; set; }
        public List<long> In { get; set; }
        public List<long> NotIn { get; set; }

        public bool HasValue
        {
            get
            {
                return Equal.HasValue || NotEqual.HasValue || In != null || NotIn != null;
            }
        }
    }

   

    public static class FilterExtension
    {
        public static string ChangeToEnglishChar(this string str)
        {
            string[] VietNamChar = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
            //Replace   
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }

            return str;
        }
        public static Guid ToGuid(this string name)
        {
            MD5 md5 = MD5.Create();
            Byte[] myStringBytes = ASCIIEncoding.Default.GetBytes(name);
            Byte[] hash = md5.ComputeHash(myStringBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return Guid.Parse(sb.ToString());
        }
        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, StringFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Equal))
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.ToLower()));

            if (!string.IsNullOrEmpty(filter.NotEqual))
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.ToLower()));

            if (!string.IsNullOrEmpty(filter.Contain))
                source = source.Where(BuildPredicate(propertyName, "Contains", filter.Contain.ToLower()));

            if (!string.IsNullOrEmpty(filter.NotContain))
                source = source.Where(BuildPredicate(propertyName, "NotContains", filter.NotContain.ToLower()));

            if (!string.IsNullOrEmpty(filter.StartWith))
                source = source.Where(BuildPredicate(propertyName, "StartsWith", filter.StartWith.ToLower()));

            if (!string.IsNullOrEmpty(filter.NotStartWith))
                source = source.Where(BuildPredicate(propertyName, "NotStartsWith", filter.NotStartWith.ToLower()));

            if (!string.IsNullOrEmpty(filter.EndWith))
                source = source.Where(BuildPredicate(propertyName, "EndsWith", filter.EndWith.ToLower()));

            if (!string.IsNullOrEmpty(filter.NotEndWith))
                source = source.Where(BuildPredicate(propertyName, "NotEndsWith", filter.NotEndWith.ToLower()));
            return source;
        }


        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, DecimalFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }

        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, LongFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }
        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, DateFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.Less.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<", filter.Less.Value.ToString()));
            if (filter.LessEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "<=", filter.LessEqual.Value.ToString()));
            if (filter.Greater.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">", filter.Greater.Value.ToString()));
            if (filter.GreaterEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, ">=", filter.GreaterEqual.Value.ToString()));

            return source;
        }

        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, GuidFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.In != null)
                source = source.Where(BuildPredicate(propertyName, "In", filter.In));
            if (filter.NotIn != null)
                source = source.Where(BuildPredicate(propertyName, "NotIn", filter.In));
            return source;
        }

        public static IQueryable<TSource> Where<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> propertyName, IdFilter filter)
        {
            if (filter.Equal.HasValue)
                source = source.Where(BuildPredicate(propertyName, "==", filter.Equal.Value.ToString()));
            if (filter.NotEqual.HasValue)
                source = source.Where(BuildPredicate(propertyName, "!=", filter.NotEqual.Value.ToString()));
            if (filter.In != null)
                source = source.Where(BuildPredicate(propertyName, "In", filter.In));
            if (filter.NotIn != null)
                source = source.Where(BuildPredicate(propertyName, "NotIn", filter.NotIn));
            return source;
        }

        public static bool Contains(this Enum source, Enum destination)
        {
            long sourceValue = Convert.ToInt64(source);
            long destValue = Convert.ToInt64(destination);
            return (sourceValue & destValue) == destValue;
        }


        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = enumerator.Private("_relationalCommandCache");
            var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }

        private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TKey, TValue>(Expression<Func<TSource, TKey>> propertyName, string comparison, List<TValue> values)
        {
            var micontain = typeof(List<TValue>).GetMethod("Contains");
            var mc = Expression.Call(Expression.Constant(values), micontain, propertyName.Body);
            Expression body;
            switch (comparison)
            {
                case "NotIn":
                    body = Expression.Not(mc);
                    break;
                default:
                    body = mc;
                    break;
            }

            return Expression.Lambda<Func<TSource, bool>>(body, propertyName.Parameters);
        }

        public static Expression<Func<TSource, bool>> BuildPredicate<TSource, TKey>(Expression<Func<TSource, TKey>> propertyName, string comparison, string value)
        {
            var left = propertyName.Body;
            if (left.Type == typeof(string))
            {
                left = Expression.Call(left, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            }

            Expression body;
            switch (comparison)
            {
                case "NotContains":
                    body = Expression.Not(MakeComparison(left, "Contains", value));
                    break;
                case "NotStartsWith":
                    body = Expression.Not(MakeComparison(left, "StartsWith", value));
                    break;
                case "NotEndsWith":
                    body = Expression.Not(MakeComparison(left, "EndsWith", value));
                    break;
                default:
                    body = MakeComparison(left, comparison, value);
                    break;
            }
            if (left.Type == typeof(string))
            {
                var nullCheck = Expression.NotEqual(left, Expression.Constant(null, left.Type));
                left = Expression.And(nullCheck, body);
            }
            return Expression.Lambda<Func<TSource, bool>>(body, propertyName.Parameters);
        }

        private static Expression MakeComparison(Expression left, string comparison, string value)
        {
            switch (comparison)
            {
                case "==":
                    return MakeBinary(ExpressionType.Equal, left, value);
                case "!=":
                    return MakeBinary(ExpressionType.NotEqual, left, value);
                case ">":
                    return MakeBinary(ExpressionType.GreaterThan, left, value);
                case ">=":
                    return MakeBinary(ExpressionType.GreaterThanOrEqual, left, value);
                case "<":
                    return MakeBinary(ExpressionType.LessThan, left, value);
                case "<=":
                    return MakeBinary(ExpressionType.LessThanOrEqual, left, value);
                case "Contains":
                case "StartsWith":
                case "EndsWith":
                    return Expression.Call(MakeString(left), comparison, Type.EmptyTypes, Expression.Constant(value, typeof(string)));
                default:
                    throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
            }
        }

        private static Expression MakeString(Expression source)
        {
            return source.Type == typeof(string) ? source : Expression.Call(source, "ToString", Type.EmptyTypes);
        }

        private static Expression MakeBinary(ExpressionType type, Expression left, string value)
        {
            object typedValue = value;
            if (left.Type != typeof(string))
            {
                if (string.IsNullOrEmpty(value))
                {
                    typedValue = null;
                    if (Nullable.GetUnderlyingType(left.Type) == null)
                        left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
                }
                else
                {
                    var valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                    typedValue = valueType.IsEnum ? Enum.Parse(valueType, value) :
                        valueType == typeof(Guid) ? Guid.Parse(value) :
                        Convert.ChangeType(value, valueType);
                }
            }
            var right = Expression.Constant(typedValue, left.Type);
            return Expression.MakeBinary(type, left, right);
        }
    }
    public class HintCommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            if (command.CommandText.Contains("INSERT") ||
                command.CommandText.Contains("UPDATE") ||
                command.CommandText.Contains("DELETE"))
                return result;

            command.CommandText += " OPTION (FORCE ORDER)";
            return result;
        }

        }
}

