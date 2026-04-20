using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class TestSceneSetupEditor : Editor
{
    // tạo 1 cái menu ấn để set up scene cho lẹ
    [MenuItem("Tools/Setup PlayMode Test Scene")]
    public static void SetupTestScene()
    {
        // tạo scene mới
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        
        // ném player vô scene
        GameObject player = new GameObject("Player_Demo");
        player.AddComponent<CharacterControllerExam>();
        player.transform.position = Vector3.zero;

        // r lưu ở ngoài nha
        string path = "Assets/Scenes/CharacterTestScene.unity";
        
        // tạo foder nếu chư có 
        if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
        {
            AssetDatabase.CreateFolder("Assets", "Scenes");
        }

        EditorSceneManager.SaveScene(newScene, path);
        Debug.Log("Set up scene xong roi nhe thay");
    }
}
