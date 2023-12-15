using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class CDFM 
{
    #region Variables

    string MainPath;
    string[] SubPaths;
    
    #endregion


    #region MyMethods

    #region Save_Data
    
    public void DataSaver(string[] Subfolders, List<Object>[] Package, string MainFolder = default)
    {
        FolderPaths(MainFolder,Subfolders);
        CreateJars(Package);
    }


    Func<string[], string, string[]> NewPaths = (array, MainFolder) => {
        array.ToList().ForEach(str => str = $"{MainFolder}/{str}");
        return array;
    };


    void FolderPaths(string MainFol, string[] SubFol = default)
    {
        MainPath = MainFol == default ? $"{Application.persistentDataPath}/{MainPath}" 
                                 : $"{Application.persistentDataPath}/DataBase";

        SubPaths = SubFol == default ? NewPaths(SubFol, MainPath) 
                 : new string[] { $"{MainPath}/Anime", $"{MainPath}/Manga"};
    }



    void CreateJars(List<Object>[] Package)
    {
        if (!Directory.Exists(MainPath))
            Directory.CreateDirectory(MainPath);

        foreach (var subPath in SubPaths)
            if (!Directory.Exists(subPath))
                Directory.CreateDirectory(subPath);

        CreateCapsules(SubPaths, Package);
    }


    
    void CreateCapsules(string[] Paths, List<Object>[] Package)
    {
        int counter = 0;
        Package.ToList().ForEach(Box =>{
            Box.ForEach(Capsules =>{
                IList<object> ListOfCapsules = new List<object>();
                if (Capsules is IEnumerable<object> EnumCapsules)
                    Save(EnumCapsules, ListOfCapsules, Paths, counter);
            });
            counter = 0;
        });
    }

    void Save(IEnumerable<object> enumerable, IList<object> Capsules, string[] Paths, int counter)
    {
        string SimplePath = default;
        Capsules = enumerable.ToList();
        Capsules.ToList().ForEach(Capsule => {
            Paths.ToList().ForEach(path => {
                if (path.Contains(Capsule.GetType().Name))
                    SimplePath = $"{path}/{Capsule.GetType().Name}_{counter}.json";
            });
            WriteCapsule(SimplePath, Capsule);
            counter++;
        });
    }



    void WriteCapsule(string Path, Object Capsule)
    {
        StreamWriter streamWriter = new(Path);
        streamWriter.Write(JsonUtility.ToJson(Capsule));
        streamWriter.Close();
    }
    
    
    #endregion


    
    #region Load_Data

    public DataBase DataLoader(string[] Subfolders, string MainFolder = default)
    {
        FolderPaths(MainFolder,Subfolders);
        return LoadCapsule();
    }


    DataBase LoadCapsule()
    {
        DataBase BD = new();
        string str_Json = default;
        string[] Files = default;
        StreamReader streamReader;
        
        if (Directory.Exists(MainPath))
            SubPaths.ToList().ForEach(subPath =>{
                if (!File.Exists(subPath)){
                    Files = Directory.GetFiles(subPath);

                    if (subPath.Contains("Anime"))
                        Files.ToList().ForEach(file =>{
                            streamReader = new(file);
                            str_Json = streamReader.ReadToEnd();
                            BD.AnimeList.Add(JsonUtility.FromJson<Anime>(str_Json));
                            streamReader.Close();
                        });

                    else if (subPath.Contains("Manga"))
                        Files.ToList().ForEach(file =>{
                            streamReader = new(file);
                            str_Json = streamReader.ReadToEnd();
                            BD.MangaList.Add(JsonUtility.FromJson<Manga>(str_Json));
                            streamReader.Close();
                        });
                }
            });
        return BD;
    }

    #endregion
    
    #endregion

}
