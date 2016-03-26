﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
public class Grid : MonoBehaviour {

    public int xSize;
    public int ySize;

    private Vector3[] vertices;
    private Mesh mesh;

    private void Awake()
    {
       Generate();
    }

   private void Generate()
    {
       
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(xSize+1)*(ySize+1)];
        Vector2[] uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y < ySize; y++) 
        {
            for (int x = 0; x < xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2(x / xSize, y / ySize);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[xSize*ySize*6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = vi + xSize;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = vi + xSize + 1;
                
                
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
