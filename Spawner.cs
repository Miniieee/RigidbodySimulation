using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Spawner : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int length = 5;
    public int width = 5;
    public int height = 5;

    // Reference to the cube prefab.
    public GameObject cubePrefab;
    public Mesh modelMesh;

    // Cube size for spacing.
    public float cubeSize = 1f;

    // Cube counter
    public int cubeCount = 0;

    [ContextMenu("SpawnMesh")]
    public void SpawnToVertecies()
    {
        Vector3[] verts = modelMesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            Instantiate(cubePrefab, verts[i] + transform.position, Quaternion.identity, transform);
            cubeCount++;
        }
    }

    [ContextMenu("SpawnCubes")]
    public void SpawnCubes()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube prefab is not set in the inspector.");
            return;
        }

        // Reset cube counter
        cubeCount = 0;

        // Remove old cubes
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // Create a hollow rectangular prism.
        for (int h = 0; h < height; h++)
        {
            for (int l = 0; l < length; l++)
            {
                for (int w = 0; w < width; w++)
                {
                    // Only spawn cubes at the edges, leaving the inside empty.
                    // Adjusted condition: if h == 0 or h == height - 1, only spawn if it's also an edge cube in the x or z direction.
                    bool isEdge = l == 0 || l == length - 1 || w == 0 || w == width - 1 || (h > 0 && h < height && (l == 0 || l == length - 1 || w == 0 || w == width - 1));

                    if (isEdge)
                    {
                        Vector3 position = new Vector3(l * cubeSize, h * cubeSize, w * cubeSize);
                        GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity, transform);
                        //cube.name = "Cube"; // Naming the cubes the same.

                        // Increase the cube counter
                        cubeCount++;
                    }
                }
            }
        }

        Debug.Log("Spawned cubes: " + cubeCount);
        //text.text = cubeCount.ToString();
    }

    public void RemoveCubes()
    {
        // Get the current child count.
        int numChildren = transform.childCount;

        // Iterate backwards through the child objects.
        // This is important as removing children changes the order.
        for (int i = numChildren - 1; i >= 0; --i)
        {
            // Destroy each child object.
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
