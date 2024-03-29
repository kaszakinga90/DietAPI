﻿using Application.DTOs.MealTimeToXYAxisDTO;

public class DietPostDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PatientId { get; set; }
    public int DieteticianId { get; set; }
    public int numberOfMeals { get; set; }
    public List<MealTimeToXYAxisPostDTO> MealTimesToXYAxisDTO { get; set; }
}

