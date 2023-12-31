﻿using Application.Core;

namespace Application.FiltersExtensions.DieticianMessages
{
    public class DieticianMessagesParams:PagingParams
    {
        public string OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public string PatientNames { get; set; }
    }
}
