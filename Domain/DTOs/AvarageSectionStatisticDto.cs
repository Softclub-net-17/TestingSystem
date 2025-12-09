using System;

namespace Domain.DTOs;

public class AvarageSectionStatisticDto
{
    public int SectionId{get;set;}
    public string SectionName{get;set;}=string.Empty;
    public int UserCount{get;set;}
    public int TotalTestPassedUser{get;set;}
    public decimal AverageOfScorePercent{get;set;}
}
