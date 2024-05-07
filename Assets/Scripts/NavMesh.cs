using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMesh : MonoBehaviour
{
    public NavMeshSurface nm;

    // Update is called once per frame
    void Update()
    {
        nm.UpdateNavMesh(nm.navMeshData);
    }
}
