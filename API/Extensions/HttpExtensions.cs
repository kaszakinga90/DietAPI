﻿using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int totalPages, int totalCount, int pageSize)
        {
            var paginationHeader = new
            {
                currentPage,
                totalPages,
                totalCount,
                pageSize
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
