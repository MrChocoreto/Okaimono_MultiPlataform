using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class DCFP
{


    #region Variables

    const string stg_IMF = "<mdf>\n"; // IMF = Input MainSaver Flag
                                      // mdf = MainSaver Data Flag
    const string stg_OMF = "\n</mdf>\n"; // OMF = Output MainSaver Flag

    #endregion



    #region Public_Methods


    /// <summary>
    /// Convert a Class into String Format like a XML 
    /// </summary>
    /// <param name="obt_Cls">The Class that you want convert</param>
    /// <returns>The Class converted to String</returns>
    public string ToString(Object obt_Cls)
    {
        return stg_CCTS(obt_Cls);
    }



    /// <summary>
    /// Convert a String into Class
    /// </summary>
    /// <param name="stgData">String that contain the Data</param>
    /// <returns>String converted into the Class that you need</returns>
    public static T ToClass<T>(string stgData) => (T)DCFP.ToClass(stgData, typeof(T));


    #endregion


    
    #region Private_Methods


    #region Text_To_Class


    /// <summary>
    /// stg_CCTS = Convert obt_Cls To String
    /// </summary>
    /// <param name="obt_Cls"> </param>
    /// <returns>Result of convert your class to Text</returns>
    string stg_CCTS(Object obt_Cls)
    {
        string stg_Result = default;
        if (obt_Cls != null && !(obt_Cls.GetType().Name == "String") && !obt_Cls.GetType().IsPrimitive)
        {
            stg_Result = stg_IMF;
            stg_Result += $"<{obt_Cls.GetType().Name}>";
            stg_Result += stg_EIFC(obt_Cls, obt_Cls.GetType());
            stg_Result += $"\n</{obt_Cls.GetType().Name}>";
        }
        else
            stg_Result = stg_IMF;
        return stg_Result + stg_OMF;
    }


    /// <summary>
    /// Extract Items From Class/List
    /// </summary>
    /// <param name="cls">Class/List</param>
    /// <param name="type">Type of the Object</param>
    /// <returns>Content of the Class</returns>
    string stg_EIFC(Object cls, Type type)
    {
        string stg_Class_List = default;
        string stg_Result = default;
        string stg_Item_Type = default;
        string stg_Item = default;
        // recupero todos los elementos ya sea de una clase o de una lista
        // en un Array de Fields Info para poder trabajar cada uno individualmente
        FieldInfo[] lst_Fields = type.GetFields();
        foreach (FieldInfo item in lst_Fields)
        {
            object obt_field = item.GetValue(cls);
            // comprueba si lo que se le esta pasando es una lista generica
            if (bol_IGL(item))
            {
                // stg_Result += $"\n\t<{item.Name}>";
                // en caso de ser cierto lo que hace es crear una lista generica
                IList<object> lst_ElementsList = new List<object>();
                // se compreba si el valor de la lista de elementos es un
                // IEnumerable de objetos ademas de que los convierte IEnumerable
                if (obt_field is IEnumerable<object> enumerable)
                {  
                    // de ser cierto crea una lista con los elementos en base
                    // a los IEnumerables
                    lst_ElementsList = enumerable.ToList();
                    
                    // se recorre cada elemento de la lista para ver si contiene mas
                    // elementos del mismo tipo dentro o son puros atributos/elementos
                    // de una lista
                    foreach (object obtElement in lst_ElementsList)
                    {
                        stg_Item_Type = $"{obtElement}";
                        stg_Item = stg_OIE(obtElement,obtElement.GetType());
                        
                        if (obtElement == lst_ElementsList.First())
                        {
                            stg_Result += $"\n\t<{item.Name}>({stg_Item})";
                            if (stg_Item_Type == stg_Item)
                                stg_Result += $"\n\t!<[{stg_Item}]>\n";
                        }
                        
                        if (stg_Item_Type != stg_Item)
                            stg_Result += $"\n\t\t![{obtElement}]";
                        
                        stg_Result += stg_EIFC(obtElement, obtElement.GetType());
                        if (obtElement == lst_ElementsList.Last())
                            if (stg_Item_Type == stg_Item)
                                stg_Result += $"\n\t</{item.Name}>\n\n";
                            else
                                stg_Result += $"\n\t</{item.Name}>";
                    }
                } 

            }
            else if (!bol_IGL(item) && item.Name != "Empty")
                stg_Result += $"\n\t<<{item.Name}: {item.GetValue(cls)}>>";
        }
        return stg_Result;
    }


    
    /// <summary>
    /// IGL = Is a Generic List
    /// </summary>
    /// <param name="fieldInfo">List</param>
    /// <returns>True or False if the field is generic or not</returns>
    static bool bol_IGL(FieldInfo fieldInfo)
    {
        Type fieldType = fieldInfo.FieldType;
        return (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>));
    }
    
    

    /// <summary>
    /// OIE = Obtain Id by Element
    /// </summary>
    /// <param name="obt_Element">Object</param>
    /// <param name="type">Type of the Object</param>
    /// <returns>The "ID" of the object</returns>
    string stg_OIE(Object obt_Element, Type type)
    {
        string stgResult = default;
        if (type == typeof(int))
        {
            stgResult = "int"; 
        }
        else if(type == typeof(double))
        {
            stgResult = "double";
        }
        else if(type == typeof(double))
        {
            stgResult = "double";
        }
        else if (type == typeof(float))
        {
            stgResult = "float";
        }
        else if (type == typeof(long))
        {
            stgResult = "long";
        }
        else if (type == typeof(short))
        {
            stgResult = "short";
        }
        else if (type == typeof(decimal))
        {
            stgResult = "decimal";
        }
        else if (type == typeof(byte))
        {
            stgResult = "byte";
        }
        else if (type == typeof(string))
        {
            stgResult = "string";
        }
        else if (type == typeof(char))
        {
            stgResult = "char";
        }
        else if (type == typeof(bool))
        {
            stgResult = "bool";
        }
        else
        {
            stgResult = type.Name;
        }
        //else if (Nullable.GetUnderlyingType(type) != null)
        //{
        //    stgResult = "null";
        //}
        return stgResult;
    }
    


    #endregion



    #region Class_To_Text


    //CSTC = Convet String To Class
    public static object ToClass(string stgData, Type type)
    {
        Object obj_Result = default;
        Object obj_Cls = default;
        
        if (stgData != null && !(type.Name == "String") && !type.IsPrimitive)
        {
            // Se pasa a convertir la data en el objeto
            // que se espera
            
            // obj_Result = 
        }
        else
        {
            // Regresa un objeto vacio del tipo que se
            // le esta pasando en caso de que no se
            // pueda convertir 
            return obj_Result = default;
        }
    

        return obj_Result;
    }
    
    
    
    
    
    
    


    #endregion


    #endregion

}
