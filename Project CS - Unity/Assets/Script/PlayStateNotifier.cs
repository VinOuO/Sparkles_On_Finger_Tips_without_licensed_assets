using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[InitializeOnLoad]
public static class PlayStateNotifier
{
    static PlayStateNotifier()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }

    private static void LogPlayModeState(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.ExitingPlayMode)
        {
            Camera.main.GetComponent<Import_HR>().ExitPlayMode();
        }
    }
}
#endif

