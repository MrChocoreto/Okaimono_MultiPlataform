using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FDS : MonoBehaviour
{


    #region Variables

    [HideInInspector] const string stg_App_Path = "/Okaimono/"; //Path de la carpeta donde se guardara el archivo
    const string stgDOKDBF = "DOKDB.dcuf"; //DOKDBF = Default OKaimono DataBase File
                                          //.dcuf = Dot Choco Universal File
    DataBase cls_BD; //BD = DataBase
    DCFP cls_DCFP; //DCFP = Dot Choco File Parse
    CDFM cls_CDFM; //CDFM = Capsule Data File Manager

    #endregion



    #region Unity_Methods


    void Awake()
    {
        cls_CDFM = new();
        cls_BD = new();
    }

    void Start()
    {
        COLD();
    }


    #endregion



    //MDB(Manage_Data_Base)
    #region Methods_of_MDB

    [ContextMenu("SaveData")]
    public void SaveData()
    {
        Anime anime= new();
        cls_DCFP = new();
        cls_BD = new();

        anime.Name = "Dragon Ball";
        cls_BD.AnimeList.Add(anime);
        // cls_CDFM.Lists_Obj = new List<object>[2];
        // cls_CDFM.Lists_Obj[0] = new List<object>(){cls_BD.AnimeList};
        // cls_CDFM.Lists_Obj[1] = new List<object>(){cls_BD.MangaList};
        // string[] Subfolders = new string[] { "Anime", "Manga" };
        
        // cls_CDFM.MainSaver(Subfolders, cls_CDFM.Lists_Obj);
        Debug.Log(cls_DCFP.ToDataFile(cls_BD));
        DCFP dCFP = new();
        dCFP.ToDataClass<DataBase>("");
        JsonUtility.FromJson<DataBase>("");

    }
    
    
    
    [ContextMenu("LoadData")]
    public void LoadData()
    {
        string[] Subfolders = new string[] { "Anime", "Manga" };
        cls_BD =  cls_CDFM.MainLoader(Subfolders);
    }


    //COLD = Create Or Load Data
    void COLD()
    {
        //Si no existe la carpeta de la aplicacion la creara y guardara los datos
        if (!Directory.Exists(Application.persistentDataPath + stg_App_Path))
            SaveData();
        //Caso contrario solo cargara los datos
        else 
            LoadData();
    }


    #endregion


}
