using System.Collections.Generic;

public class DataBase 
{
    public List<Anime> AnimeList = new();
    public List<Manga> MangaList = new();
}


public class Anime
{
    public string Id = "Unknown";
    public string Name = "Unknown";
    public List<string> Tags = new() { "None" };
    public bool InLive = false;
    public string NextNewCap = "Never";
    public int MaxCaps = 0;
    public int LastViewCap = 0;
    public List<string> Prequels = new() { "None" };
    public List<string> Sequels = new() { "None" };
    public List<string> Movies = new() { "None" };
    public List<string> SpinOffs = new() { "None" };
    public float Ovas = 0;
}


public class Manga
{
    public string Id = "Unknown";
    public string Name = "Unknown";
    public List<string> Tags = new() { "None" };
    public bool OnGoing = false;
    public int MaxCaps = 0;
    public int LastViewCap = 0;
    public List<string> Prequels = new() { "None" };
    public List<string> Sequels = new() { "None" };
    public List<string> SpinOffs = new() { "None" };
}

