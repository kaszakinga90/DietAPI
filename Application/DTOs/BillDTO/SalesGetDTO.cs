﻿namespace Application.DTOs.BillDTO
{
    public class SalesGetDTO
    {
        public int Id { get; set; }
        public int DietId { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }
        public string SalesDate { get; set; }
    }
}
