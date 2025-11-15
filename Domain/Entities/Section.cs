using System;
using System.Net;

namespace Domain.Entities;

public class Section
{
    public int Id {get;set;}
    public string Name {get;set;} = null!;
    public bool IsActive{get;set;}

    public List<Topic> Topics{get;set;}=[];
    public List<Question> Questions{get;set;}=[];
    public List<TestSession> TestSessions{get;set;}=[];

}
