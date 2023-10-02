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
        CreateFolders();
    }
    
    
    
    public void MainSaver(string[] Subfolders, List<Object>[] lst_Lists_Obj)
    {
        string MainFolder = default;
        FolderPaths(MainFolder,Subfolders);
        CreateFolders();
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



    void CreateFolders()
    {
        if (!Directory.Exists(MainPath))
        {
            Directory.CreateDirectory(MainPath);
        }
        else
        {
            Debug.Log(MainPath);
        }

        foreach (var subPath in SubPaths)
        {
            if (!Directory.Exists(subPath))
            {
                Directory.CreateDirectory(subPath);
            }
            else
            {
                Debug.Log(subPath);
            }
        }
    }
    
    
    
    
    #endregion
    
    
    
    #region DataLoader
    
    
    
    
    
    #endregion

    
    #endregion



    #region Corrutines



    #endregion


}
