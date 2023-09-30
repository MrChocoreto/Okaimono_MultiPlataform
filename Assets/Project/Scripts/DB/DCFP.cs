using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class DCFP
{


    #region Variables

    const string stg_IMF = "<mdf>\n"; // IMF = Input Main Flag
                                // mdf = Main Data Flag
    const string stg_OMF = "\n</mdf>\n"; // OMF = Output Main Flag

    #endregion



    #region Public_Methods


    /// <summary>
    /// Convert a obt_Cls into String Format like a Json
    /// </summary>
    /// <param name="obt_Cls">obt_Cls of any stg_Type</param>
    /// <returns>obt_Cls converted to String</returns>
    public string ToString(Object obt_Cls)
    {
        return stg_CCTS(obt_Cls);
    }



    /// <summary>
    /// Convert a String into obt_Cls
    /// </summary>
    /// <param name="stgData">String that contain the stgData</param>
    /// <returns>String converted into stg_Type obt_Cls</returns>
    public static T ToClass<T>(string stgData)
    {
        return (T)CSTC(stgData, typeof(T));
    }


    #endregion


    
    #region Private_Methods


    #region Text_To_Class


    // stg_CCTS = Convert obt_Cls To String
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



    // Extract Items From Class/List
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


    // IGL = Is a Generic List
    static bool bol_IGL(FieldInfo fieldInfo)
    {
        Type fieldType = fieldInfo.FieldType;
        return (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>));
    }
    
    

    // OIE = Obtain Id by Element
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


    //CSTC = Convet String To obt_Cls
    static Object CSTC(string stgData, Type type)
    {
        Object stgResult = "";

        return stgResult;
    }


    #endregion


    #endregion

}
