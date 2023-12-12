using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor {

    SaveManager saveManager;

    private void OnEnable()
    {
        saveManager = (SaveManager)target;
    }

    public override void OnInspectorGUI() // Переопределяем метод который рисует испектор
    {
        EditorGUILayout.BeginVertical();

        saveManager.isBanSave = EditorGUILayout.Toggle("Ban save", saveManager.isBanSave);

        EditorGUILayout.Space();

        saveManager.nameProject = EditorGUILayout.TextField("Name Project", saveManager.nameProject);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Working with keys");
        saveManager.enKeyType = (SaveManager.TypeOfKey)EditorGUILayout.EnumPopup("Key Type", saveManager.enKeyType);
        saveManager.key = EditorGUILayout.TextField("Key", saveManager.key);
        saveManager.value = EditorGUILayout.TextField("Value", saveManager.value);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Delete Key"))
        {
            saveManager.EditorDeleteKey(saveManager.key);
        }

        if (GUILayout.Button("Add Key"))
        {
            if (saveManager.enKeyType == SaveManager.TypeOfKey.FLOAT)
            {
                saveManager.EditorAddKey(saveManager.key, float.Parse(saveManager.value));
            }
            else if(saveManager.enKeyType == SaveManager.TypeOfKey.INTEGER)
            {
                saveManager.EditorAddKey(saveManager.key, int.Parse(saveManager.value));
            }
            else if(saveManager.enKeyType == SaveManager.TypeOfKey.STRING)
            {
                saveManager.EditorAddKey(saveManager.key, saveManager.value);
            }   
        }

        if (GUILayout.Button("Get Key"))
        {

            if (saveManager.enKeyType == SaveManager.TypeOfKey.FLOAT)
            {
                saveManager.EditorGetKey<float>(saveManager.key);
            }
            else if (saveManager.enKeyType == SaveManager.TypeOfKey.INTEGER)
            {
                saveManager.EditorGetKey<int>(saveManager.key);
            }
            else if (saveManager.enKeyType == SaveManager.TypeOfKey.STRING)
            {
                saveManager.EditorGetKey<string>(saveManager.key);
            }

            
        }

        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Delete All Key"))
        {
            PlayerPrefs.DeleteAll();
        }

        EditorGUILayout.EndVertical();
    }
}
