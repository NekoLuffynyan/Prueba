// Archivo: AutoSave.cs
// Carpeta: Assets/Editor/

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class AutoSave
{
    private static double lastKeyPressTime;
    private static double lastCheckTime;
    private static readonly double checkInterval = 5.0; // segundos
    private static readonly double idleThreshold = 10.0; // segundos
    private static bool hasSavedWhileIdle = false;

    static AutoSave()
    {
        lastKeyPressTime = EditorApplication.timeSinceStartup;
        lastCheckTime = EditorApplication.timeSinceStartup;
        EditorApplication.update += Update;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (e != null && e.isKey)
        {
            lastKeyPressTime = EditorApplication.timeSinceStartup;
            hasSavedWhileIdle = false;
        }
    }

    private static void Update()
    {
        double now = EditorApplication.timeSinceStartup;
        if (now - lastCheckTime < checkInterval)
            return;

        lastCheckTime = now;

        // No guardar si está en modo Play
        if (EditorApplication.isPlaying)
            return;

        if (!hasSavedWhileIdle && (now - lastKeyPressTime) > idleThreshold)
        {
            SaveProject();
            hasSavedWhileIdle = true;
        }
    }

    private static void SaveProject()
    {
        Debug.Log("AutoSave: Guardado todo el proyecto...");
        EditorApplication.ExecuteMenuItem("File/Save");
        EditorApplication.ExecuteMenuItem("File/Save Project");
    }
}
