using System;

public class Artist
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public DateTime DateBirthday {get; set;}
    public string Country {get; set;} = string.Empty;
    public string? Description {get; set;}
}