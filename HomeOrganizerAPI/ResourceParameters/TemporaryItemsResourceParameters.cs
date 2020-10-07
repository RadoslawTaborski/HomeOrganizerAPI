﻿namespace HomeOrganizerAPI.ResourceParameters
{
    public class TemporaryItemsResourceParameters : Parameters
    {
        public override int MaxPageSize { get; set; } = 50;
        protected override int PageSize { get; set; } = 25;
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
    }
}
