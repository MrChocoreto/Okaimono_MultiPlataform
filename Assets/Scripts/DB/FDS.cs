using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FDS : MonoBehaviour
{


    #region Variables
    [HideInInspector] public string stgDOKDB_JsonObj; //DOKDB = Default OKaimono DataBase
    [HideInInspector] public string stgApplication_Path; //Path de la carpeta donde se guardara el archivo
    [SerializeField] DataBase BD;
    //[HideInInspector] public bool bolChanges;
    const string stgDOKDBF = "DOKDB.dcdf"; //DOKDB = Default OKaimono DataBase File

    #endregion



    #region Unity_Methods

    //Detecta si hay bolChanges con cada frame que pasa 
    //y si detecta bolChanges pasa a guardar la informacion en el "stgDOKDB_JsonObj"

    //void FixedUpdate()
    //{
    //    if (bolChanges)
    //    {
    //        LoadData();
    //        bolChanges = false;
    //    }
    //}

    //Se crea una instancia de la BD dentro de este script 
    //y mando llamar el metodo de creacion de stgDOKDB_JsonObj
    
    //public void Awake()
    //{
    //    BD = new DataBase();
    //    CreateJson();
    //}


    public void Start()
    {
        //Se le da una path a la variable stgApplication_Path desde el Start
        //ya que de este modo me aseguro de tener la ruta correcta y no tener
        //problemas con la carga de datos

        stgApplication_Path = Application.dataPath + "/Okaimono/";
        LoadData();
        // D:\Unity Proyects\Okaimono_MultiPlataform\Assets\Scripts\Snippets



    }

    #endregion



    //MDB(Manage DataBase)
    #region Methods_of_MDB


    public void SaveData()
    {
        //le pasamos los datos de la BD al stgDOKDB_JsonObj
        stgDOKDB_JsonObj = JsonUtility.ToJson(BD);
        
        //guarda los datos de la BD publica en el Archivo "stgDOKDB_JsonObj" creado
        File.WriteAllText(stgApplication_Path + stgDOKDBF, stgDOKDB_JsonObj);
    }



    public void LoadData()
    {
        //busca el archivo en la ruta que le pasamos
        stgDOKDB_JsonObj = File.ReadAllText(stgApplication_Path + stgDOKDBF);

        //y aqui le pasa los valores a la BD publica 
        BD = JsonUtility.FromJson<DataBase>(stgDOKDB_JsonObj);
    }



    public void CreateJson()
    {
        //Aqui ponemos el nombre del Archivo junto a su extension, en mi caso por facilidad y comprension
        //añadi la variable donde esta la ruta junto al nombre del archivo 
        string create = stgApplication_Path + stgDOKDBF;


        //Si existe la carpeta pero el archivo no, solo creara el json
        //y a su vez borrara el Highscore que hayamos tenido
        if (File.Exists(stgApplication_Path) && !File.Exists(create))
        {
            PlayerPrefs.DeleteKey("HighScore");

            stgDOKDB_JsonObj = JsonUtility.ToJson(BD);
            File.WriteAllText(create, stgDOKDB_JsonObj);
            Debug.Log("json create");
        }

        //Si no existe la carpeta ni el archivo creara ambos
        //y a su vez borrara el Highscore que hayamos tenido
        else if (!File.Exists(stgApplication_Path) && !File.Exists(create))
        {
            PlayerPrefs.DeleteKey("HighScore");

            Directory.CreateDirectory(stgApplication_Path);
            stgDOKDB_JsonObj = JsonUtility.ToJson(BD);
            File.WriteAllText(create, stgDOKDB_JsonObj);

            stgDOKDB_JsonObj = JsonUtility.ToJson(BD);
            File.WriteAllText(create, stgDOKDB_JsonObj);
            Debug.Log("json create & folder create");
        }

        //Si existen ambos solo cargara los datos
        else if (File.Exists(stgApplication_Path) && File.Exists(create))
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
