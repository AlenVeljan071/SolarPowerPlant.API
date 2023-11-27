using System.Linq.Expressions;

namespace SolarPowerPlant.Core.Specifications.BaseSpecification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludesStrings { get; } = new List<string>();

        public Expression<Func<T, object>>? GroupBy { get; set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public List<Tuple<Expression<Func<T, object>>, bool>> OrderBy { get; set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeExpression)
        {
            IncludesStrings.Add(includeExpression);
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression, bool isAscending)
        {
            if (OrderBy == null)
            {
                OrderBy = new List<Tuple<Expression<Func<T, object>>, bool>>();
            }
            OrderBy.Add(new Tuple<Expression<Func<T, object>>, bool>(orderByExpression, isAscending));
        }

        protected void AddGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }

        public IQueryable<T> OrderByMultiple(IQueryable<T> query, List<Tuple<Expression<Func<T, object>>, bool>> orderBy)
        {
            var first = true;
            foreach (var order in orderBy)
            {
                if (first)
                {
                    if (order.Item2)
                        query = query.OrderBy(order.Item1);
                    else
                        query = query.OrderByDescending(order.Item1);
                    first = false;
                }
                else
                {
                    if (order.Item2)
                        query = ((IOrderedQueryable<T>)query).ThenBy(order.Item1);
                    else
                        query = ((IOrderedQueryable<T>)query).ThenByDescending(order.Item1);
                }
            }
            return query;
        }

    }
}
