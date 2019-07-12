using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    
    public float    minX,   minZ;
    public float    maxX,   maxZ;
    public float    X,      Z;
    Vector3 Point;
    private NavMeshAgent agent;
    public void newPoint()
    {
        float X = Random.Range(minX, maxX); // позиция
        float Z = Random.Range(minZ, maxZ); // позиция 

    }



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        newPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
