using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class CDFM 
{
    #region Variables

    public List<Object>[] Lists_Obj;
    string MainPath;
    string[] SubPaths;
    string SimplePath;
    
    #endregion

    #region MyMethods

    #region DataSaver
    
    public void MainSaver(string MainFolder, string[] Subfolders, List<Object>[] lst_Lists_Obj)
    {
        FolderPaths(MainFolder,Subfolders);
        CreateJars(lst_Lists_Obj);
    }
    
    
    
    public void MainSaver(string[] Subfolders, List<Object>[] lst_Lists_Obj)
    {
        string MainFolder = default;
        FolderPaths(MainFolder,Subfolders);
        CreateJars(lst_Lists_Obj);
    }
    
    
    
    void FolderPaths(string MainFol, string[] SubFol = default)
    {
        MainPath = MainFol == "" ? $"{Application.persistentDataPath}/{MainPath}" : $"{Application.persistentDataPath}/DataBase";
        Func<string[],string[]> NewPaths = (a) =>{
            for (var i = 0; i < a.Length; i++)
                SubPaths[i] = $"{MainPath}/{a[i]}";
            return a;
        };
        SubPaths = SubFol == default ? NewPaths(SubFol) : new string[] { $"{MainPath}/Anime", $"{MainPath}/Manga"};
    }



    void CreateJars(List<Object>[] Capsules)
    {
        if (!Directory.Exists(MainPath))
            Directory.CreateDirectory(MainPath);

        foreach (var subPath in SubPaths)
        {
            if (!Directory.Exists(subPath))
                Directory.CreateDirectory(subPath);
        }
        CreateCapsules(SubPaths, Capsules);
    }


    
    void CreateCapsules(string[] Paths, List<Object>[] Capsules)
    {
        int counte = 0;
        foreach (var capsule in Capsules)
        {
            capsule.ForEach(x =>
            {
                IList<object> lst_Capsule_Class = new List<object>();
                if (x is IEnumerable<object> enumerable)
                {
                    lst_Capsule_Class = enumerable.ToList();
                    foreach (var w in lst_Capsule_Class)
                    {
                        foreach (var newpath in Paths)
                        {
                            if(newpath.Contains(w.GetType().Name))
                            SimplePath = newpath;
                        }
                        CapsuleDirectory(SimplePath, $"{w.GetType().Name}_{counte}", w);
                        counte++;
                    }
                }
                
            });
            
            counte = 0;
        }
    }

    
    void CapsuleDirectory(string Path, string NameFile, Object Capsule)
    {
        string Full_Path = $"{Path}/{NameFile}";
        StreamWriter streamWriter;
        streamWriter = new StreamWriter(Full_Path);
        streamWriter.Write(JsonUtility.ToJson(Capsule));
        streamWriter.Close();
        
    }
    
    
    #endregion
    
    #region DataLoader

    public DataBase MainLoader(string MainFolder, string[] Subfolders)
    {
        DataBase cls_BD = new();
        FolderPaths(MainFolder,Subfolders);
        cls_BD = LoadCapsule();
        return cls_BD;
    }
    
    public DataBase MainLoader(string[] Subfolders)
    {
        DataBase cls_BD = new();
        string MainFolder = default;
        FolderPaths(MainFolder,Subfolders);
        cls_BD = LoadCapsule();
        return cls_BD;
    }

    DataBase LoadCapsule()
    {
        DataBase cls_BD = new();
        string str_Json = default;
        string[] Files = default;
        StreamReader streamReader;
        
        if (Directory.Exists(MainPath))
            foreach (var subPath in SubPaths)
            {
                if (!File.Exists(subPath))
                {
                    Files = Directory.GetFiles(subPath);

                    if (subPath.Contains("Anime"))
                        foreach (var file in Files){
                            streamReader = new(file);
                            str_Json = streamReader.ReadToEnd();
                            cls_BD.AnimeList.Add(JsonUtility.FromJson<Anime>(str_Json));
                            streamReader.Close();
                        }
                    
                    else if (subPath.Contains("Manga"))
                        foreach (var file in Files){
                            streamReader = new(file);
                            str_Json = streamReader.ReadToEnd();
                            cls_BD.MangaList.Add(JsonUtility.FromJson<Manga>(str_Json));
                            streamReader.Close();
                        }
                }
            }
        return cls_BD;
    }

    #endregion
    
    #endregion

}
