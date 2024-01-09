﻿namespace Application.Core
{
    public class PagingParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 10;
        private int _pageSize=50;

        public int PageSize
        {
            get => _pageSize;
            set =>_pageSize=(value>MaxPageSize) ? MaxPageSize : value;
        }
    }
}

