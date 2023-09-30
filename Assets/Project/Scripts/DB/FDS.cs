using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class FDS : MonoBehaviour
{


    #region Variables

    [HideInInspector] public string stg_DOKDB_Json; //DOKDB = Default OKaimono DataBase
    [HideInInspector] public string stg_Application_Path; //Path de la carpeta donde se guardara el archivo
    //[HideInInspector] public bool bolChanges;
    const string stgDOKDBF = "DOKDB.dcuf"; //DOKDBF = Default OKaimono DataBase File
                                          //.dcuf = Dot Choco Universal File
    DataBase cls_BD;
    DCFP cls_DCFP;

    #endregion



    #region Unity_Methods


    void Awake()
    {
        cls_DCFP = new();
        cls_BD = new();
    }



    void Start()
    {
        cls_BD.AnimeList.Add(new());
        cls_BD.MangaList.Add(new());
        //Se le da una path a la variable stgApplication_Path desde el Start
        //ya que de este modo me aseguro de tener la ruta correcta y no tener
        //problemas con la carga de datos
        stg_Application_Path = Application.persistentDataPath + "/Okaimono/";
        //Debug.Log(stgApplication_Path);
        //CreateFile();
        SaveData();
    }


    #endregion



    //MDB(Manage DataBase)
    #region Methods_of_MDB

    [ContextMenu("Pruebas")]
    public void SaveData()
    {
        stg_Application_Path = Application.persistentDataPath + "/Okaimono/";
        cls_DCFP = new();
        cls_BD = new();
        Anime anime = new();
        // anime.Tags.Add("Xd");
        cls_BD.AnimeList.Add(anime);
        cls_BD.AnimeList.Add(anime);

        //le pasamos los datos de la clsBD al stgDOKDB_JsonObj
        stg_DOKDB_Json = cls_DCFP.ToString(cls_BD); //DCFP = Dot Choco File Parse
        // JsonUtility.ToJson(stg_DOKDB_Json);

        //guarda los datos de la clsBD publica en el Archivo "stgDOKDB_JsonObj" creado
        File.WriteAllText(stg_Application_Path + stgDOKDBF, stg_DOKDB_Json);
        Debug.Log(stg_DOKDB_Json);
    }

    public void LoadData()
    {
        
        //busca el archivo en la ruta que le pasamos
        stg_DOKDB_Json = File.ReadAllText(stg_Application_Path + stgDOKDBF);
        
        //y aqui le pasa los valores a la clsBD publica 
        cls_BD = DCFP.ToClass<DataBase>(stg_DOKDB_Json); //DCFP = Dot Choco File Parse
        //clsBD = JsonUtility.FromJson<DataBase>(stgDOKDB_JsonObj);
    }



    public void CreateFile()
    {
        //Aqui ponemos el nombre del Archivo junto a su extension, en mi caso por facilidad y comprension
        //a�adi la variable donde esta la ruta junto al nombre del archivo 
        string DOKDB = stg_Application_Path + stgDOKDBF;

        //Si existe la carpeta pero el archivo no, solo creara el json
        //y a su vez borrara el Highscore que hayamos tenido
        if (File.Exists(stg_Application_Path) && !File.Exists(DOKDB))
        {
            PlayerPrefs.DeleteKey("HighScore");

            stg_DOKDB_Json = JsonUtility.ToJson(cls_BD);
            File.WriteAllText(DOKDB, stg_DOKDB_Json);
            Debug.Log("json create");
        }

        //Si no existe la carpeta ni el archivo creara ambos
        //y a su vez borrara el Highscore que hayamos tenido
        else if (!File.Exists(stg_Application_Path) && !File.Exists(DOKDB))
        {
            PlayerPrefs.DeleteKey("HighScore");

            Directory.CreateDirectory(stg_Application_Path);
            stg_DOKDB_Json = JsonUtility.ToJson(cls_BD);
            File.WriteAllText(DOKDB, stg_DOKDB_Json);

            stg_DOKDB_Json = JsonUtility.ToJson(cls_BD);
            File.WriteAllText(DOKDB, stg_DOKDB_Json);
            Debug.Log("json create & folder create");
        }

        //Si existen ambos solo cargara los datos
        else if (File.Exists(stg_Application_Path) && File.Exists(DOKDB))
        {
            LoadData();
            Debug.Log("json load");
        }


        //-------------------------------------------------------------------------------------------


    }


    #endregion



    #region Corrutines


    #endregion


    

}
