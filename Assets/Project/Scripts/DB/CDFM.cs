using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="MainFolder">Name of the MainSaver Data Folder</param>
    /// <param name="Subfolders">Name of the folders that contains Data</param>
    /// <returns></returns>
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
        MainPath = MainFol == "" ? $"{Application.dataPath}/{MainPath}" : $"{Application.dataPath}/DataBase";
        string[] NewPaths(string[] SubFoli){
            for (var i = 0; i < SubFoli.Length; i++)
                SubPaths[i] = $"{MainPath}/{SubFoli[i]}";
            return SubFoli;}
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
                        Debug.Log(w.GetType().Name);
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
    
    
    
    
    
    #endregion

    
    #endregion



    #region Corrutines



    #endregion


}
