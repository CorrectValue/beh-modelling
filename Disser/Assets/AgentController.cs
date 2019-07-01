using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private int count; //count of collected items
    private Vector3 destination;
    private bool wandering;
    public UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        wandering = true;
        generateRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        agent.setDestination(destination);
    }

    void onTriggerEnter(Collider other)
    {
        SphereCollider sCol = other as SphereCollider;
        if (other.GameObject.tag == "CollectibleVision")
        {
            //spotted a collectible, gotta get it
            destination = sCol.center;
            wandering = false;
        }
        if (other.GameObject.tag == "collectible")
        {
            other.GameObject.setActive(false);
            count += 1;
            wandering = true;
        }
    }

    void generateRandomPoint()
    {
        Mesh planeMesh = gameObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        Vector3 newVec = new Vector3(Random.Range(minX, -minX),
                                     gameObject.transform.position.y,
                                     Random.Range(minZ, -minZ));
        destination = newVec;
    }
}
       