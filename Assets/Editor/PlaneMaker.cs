using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneMaker : EditorWindow
{
    int resolution = 10;
    string path = "Assets/MyPlane.mesh";

    [MenuItem("Window/Plane Maker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlaneMaker));
    }

    void OnGUI()
    {
        GUILayout.Label("Generate a plane of set resolution", EditorStyles.boldLabel);
        resolution = EditorGUILayout.IntField("Resolution", resolution);
        path = EditorGUILayout.TextField("Path", path);

        if (GUILayout.Button("Generate"))
        {
            Mesh mesh = MakePlane();
            AssetDatabase.CreateAsset(mesh, path);
            AssetDatabase.SaveAssets();
        }
    }

    Mesh MakePlane()
    {
        Vector3[] vertices;
        Vector2[] uvs;
        int[] triangles;

        // --- Generate vertices & uvs ---
        vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        uvs = new Vector2[(resolution + 1) * (resolution + 1)];

        int i = 0;
        for (int z = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                Vector3 vertPos = new(x, 0f, z);
                vertPos *= 10f / resolution;
                vertPos -= new Vector3(5f, 0f, 5f);

                vertices[i] = vertPos;
                uvs[i] = new Vector2((float)x / resolution, (float)z / resolution);
                i++;
            }
        }

        // --- Generate triangles ---
        triangles = new int[resolution * resolution * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < resolution; z++)
        {
            for (int x = 0; x < resolution; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + resolution + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + resolution + 1;
                triangles[tris + 5] = vert + resolution + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }


        return ConstructMesh(vertices, triangles, uvs);
    }

    Mesh ConstructMesh(Vector3[] vertices, int[] triangles, Vector2[] uvs)
    {
        Mesh mesh = new();
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }
}
