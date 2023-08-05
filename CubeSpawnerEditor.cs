using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class CubeSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        Spawner script = (Spawner)target;
        if (GUILayout.Button("Spawn Cubes"))
        {
            script.SpawnCubes();
        }
        
        if (GUILayout.Button("Remove Cubes"))
        {
            script.RemoveCubes();
        }
        if (GUILayout.Button("SpawnToMesh"))
        {
            script.SpawnToVertecies();
        }
    }
}
#endif
