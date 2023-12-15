using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = System.Object;

public class FDS : MonoBehaviour
{


    #region Variables

    [HideInInspector] const string PATH = "/Okaimono/"; //Path de la carpeta donde se guardara el archivo
    const string DBFILE = "DOKDB.dcuf"; //DBFILE = Default OKaimono DataBase File
                                          //.dcuf = Dot Choco Universal File
    [SerializeField] DataBase DB; //BD = DataBase
    CDFM FileManager; //CDFM = Capsule Data File Manager

    #endregion



    #region Unity_Methods

    private void Start()
    {
        FileManager = new();
        DB = new();
        COLD();
    }

    #endregion



    //MDB(Manage_Data_Base)
    #region Methods_of_MDB

    [ContextMenu("SaveData")]
    public void SaveData()
    {
        List<Object>[] Lists_Obj = new List<Object>[2];
        Lists_Obj[0] = new List<object>(){DB.AnimeList};
        Lists_Obj[1] = new List<object>(){DB.MangaList};
        string[] Subfolders = new string[] { "Anime", "Manga" };
        
        FileManager.DataSaver(Subfolders, Lists_Obj);
    }
    
    
    
    [ContextMenu("LoadData")]
    public void LoadData()
    {
        string[] Subfolders = new string[] { "Anime", "Manga" };
        DB =  FileManager.DataLoader(Subfolders);
    }


    //COLD = Create Or Load Data
    void COLD()
    {
        //Si no existe la carpeta de la aplicacion la creara y guardara los datos
        if (!Directory.Exists(Application.persistentDataPath + PATH))
            SaveData();
        //Caso contrario solo cargara los datos
        else 
            LoadData();
    }


    #endregion


}

