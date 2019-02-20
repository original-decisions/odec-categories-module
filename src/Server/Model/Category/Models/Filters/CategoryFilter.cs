using System;
using odec.Framework.Generic;
using odec.Framework.Infrastructure.Enumerations;

namespace odec.Server.Model.Category.Filters
{
    public class CategoryFilter:FilterBase
    {
        public CategoryFilter()
        {
            Rows = 20;
            Name = new StringFilter
            {
                Comparison =StringComparison.CurrentCultureIgnoreCase,
                CompareOperation = StringCompareOperation.Contains
            };
            IsOnlyApproved = true;
        }
        public StringFilter Name { get; set; }
        public string Code { get; set; }
        public bool IsOnlyApproved { get; set; }
        public bool IsOnlyActive { get; set; }
    }

    public class StringFilter
    {
        public string Target { get; set; }
        public StringComparison Comparison { get; set; }
        public StringCompareOperation CompareOperation { get; set; }
    }
}
