using UnityEngine;
using System.Collections;

public class sight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public float collectibleInSight;
    public Vector3 lastCollectibleSighting;

    private UnityEngine.AI.NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
   // private LastSighting lastSighting;
    private GameObject collectible;

    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
       // lastSighting
    }
}
