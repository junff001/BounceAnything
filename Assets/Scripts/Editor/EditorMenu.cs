using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorMenu : MonoBehaviour
{
    [MenuItem("Katamari/Scenes/GameScene")]
    static void EditorMenu_LoadInGameScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Polygon.unity");
        
    }
    [MenuItem("Katamari/Scenes/EffectScene")]
    static void EditorMenu_LoadInEffectScene()
    {
        EditorSceneManager.OpenScene("Assets/JMO Assets/Cartoon FX/Demo/CFX Free Demo.unity");
    }
}
