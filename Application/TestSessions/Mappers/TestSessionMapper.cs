using System;
using Application.TestSessions.Commands;
using Application.TestSessions.DTOs;
using Application.TestSessions.Queries;
using Domain.Entities;
using Domain.Filters;

namespace Application.TestSessions.Mappers;

public static class TestSessionMapper
{
    public static TestSession ToEntity(this CreateTestSessionCommand command)
    {
        return new TestSession 
        {
            SectionId=command.SectionId,
            UserId=command.UserId,
            StartedAt=DateTime.UtcNow,
            TotalQuestions=10
        };
    }

    public static void MapFrom(this UpdateTestSessionCommand command, TestSession testSession, decimal percent, int correct, bool IsPassed)
    {
        testSession.CompletedAt=DateTime.UtcNow;
        testSession.ScorePercent=percent;
        testSession.CorrectAnswersCount=correct;
        testSession.IsPassed=IsPassed;
    }

    public static TestSessionFilter ToFilter(this GetTestSessionsQuery query)
    {
        return new TestSessionFilter
        {
            TopicId=query.TopicId,
            SectionId=query.SectionId,
            UserId=query.UserId,
            DueDate=query.DueDate,
            ScorePercent=query.ScorePercent,
            IsPassed=query.IsPassed,
            Page=query.Page,
            Size=query.Size
        };
    }
    public static GetTestSessionDto ToDto(this TestSession testSession)
    {
        return new GetTestSessionDto
        {
            Id=testSession.Id,
            SectionId=testSession.SectionId,
            StartedAt=testSession.StartedAt,
            CompletedAt=testSession.CompletedAt,
            ScorePercent=testSession.ScorePercent,
            CorrectAnswersCount=testSession.CorrectAnswersCount,
            TotalQuestions=testSession.TotalQuestions,
            IsPassed=testSession.IsPassed
        };
    }
    public static List<GetTestSessionDto> ToDto(this List<TestSession> testSessions)
    {
        return testSessions.Select(ts=>new GetTestSessionDto
        {
            Id=ts.Id,
            SectionId=ts.SectionId,
            StartedAt=ts.StartedAt,
            CompletedAt=ts.CompletedAt,
            ScorePercent=ts.ScorePercent,
            CorrectAnswersCount=ts.CorrectAnswersCount,
            TotalQuestions=ts.TotalQuestions,
            IsPassed=ts.IsPassed
        }).ToList();
    }

    public static GetUpdateTestSessionResponseDto ToUpdatedDto(this TestSession testSession)
    {
        return new GetUpdateTestSessionResponseDto
        {
            Id=testSession.Id,
            SectionId=testSession.SectionId,
            StartedAt=testSession.StartedAt,
            CompletedAt=testSession.CompletedAt,
            ScorePercent=testSession.ScorePercent,
            CorrectAnswersCount=testSession.CorrectAnswersCount,
            TotalQuestions=testSession.TotalQuestions,
            IsPassed=testSession.IsPassed
        };
    }

}
