using UnityEngine;
using UnityEditor;
using System.Diagnostics;

[CustomEditor(typeof(UserDataManager))]
public class Inspector_UserDataManager : Editor
{
    static string SetDataKeyLabel = "";
    static string SetDataLabel = "";
    static string GetDataLabel = "";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UserDataManager generator = (UserDataManager)target;

        #region GetData
        EditorGUILayout.BeginHorizontal();
        GetDataLabel = EditorGUILayout.TextField("GetData", GetDataLabel);

        if (GUILayout.Button("GetData"))
        {
            var Data = generator.GetData(GetDataLabel);
            GameUtils.Log($"{GetDataLabel} : {Data}");
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        #region SetData

        SetDataKeyLabel = EditorGUILayout.TextField("Key", SetDataKeyLabel);
        SetDataLabel = EditorGUILayout.TextField("Value", SetDataLabel);

        if (GUILayout.Button("SetData"))
        {
            generator.SetData(SetDataKeyLabel, SetDataLabel);
            GameUtils.Log($"{SetDataKeyLabel} : {SetDataLabel}");
        }

        #endregion

        GUILayout.Space(30);

        #region ResetButton

        var ResetAllDataOption = new GUILayoutOption[] {
            GUILayout.Height(60),
        };
        if (GUILayout.Button("ResetAllData", ResetAllDataOption))
        {
            FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        }
        #endregion

        #region

        if (GUILayout.Button("OpenDataFolder",new GUILayoutOption[] {GUILayout.Height(30) }))
        {
            Process.Start(Application.persistentDataPath);
        }

        #endregion


    }
}
