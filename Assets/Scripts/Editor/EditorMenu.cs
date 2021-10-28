using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorMenu : MonoBehaviour
{
    [MenuItem("Katamari/Scenes/MainScene")]
    static void EditorMenu_LoadInMainSceneScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Demo.unity");
    }
}
