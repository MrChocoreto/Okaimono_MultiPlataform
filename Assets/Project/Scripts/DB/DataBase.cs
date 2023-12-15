using System.Collections.Generic;

[System.Serializable]
public class DataBase 
{
    public List<Anime> AnimeList = new();
    public List<Manga> MangaList = new();
}


[System.Serializable]
public class Anime
{
    public string Name = "Unknown";
    public List<string> Tags = new() { "None" };
    public bool InLive = false;
    public string ViewState = "n"; // n = None, w = Watching, p = Pendent, c = Completed, d = Dropped
    public string NextNewCap = "n"; // n = Never, m = Monday, t = Tuesday, w = Wednesday, r = Thursday, f = Friday, s = Saturday, u = Sunday
    public int MaxCaps = 0;
    public int LastSeenCap = 0;
    public float Ovas = 0;
    public List<string> Prequels = new() { "None" };
    public List<string> Sequels = new() { "None" };
    public List<string> Movies = new() { "None" };
    public List<string> SpinOffs = new() { "None" };
}


[System.Serializable]
public class Manga
{
    public string Name = "Unknown";
    public List<string> Tags = new() { "None" };
    public bool OnGoing = false;
    public string ViewState = "n"; // n = None, w = Watching, p = Pendent, c = Completed, d = Dropped
    public int LastReadCap = 0;
    public int MaxCaps = 0;
    public List<string> Prequels = new() { "None" };
    public List<string> Sequels = new() { "None" };
    public List<string> SpinOffs = new() { "None" };
}
 
