﻿namespace SchoolHubApi.Domain.Entities;

public class Parent : Entity
{
    public List<Pupil> Children { get; set; } = new();
    public UserData UserData { get; set; }
}