using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MasterItem : MonoBehaviour
{
    private Renderer MR;
    private Vector3 WorldActorLocation;
    public int Type = 0;
    void Start()
        {
            MR = GetComponent<Renderer>();
            WorldActorLocation = transform.position;
        }
    public void ToStorage(HealthStats HS)
    {
        MR.enabled = false;
        transform.localPosition = WorldActorLocation - new Vector3(0,40,0);
        HS.ChangePStats(Type);
        print("Used");
    }


    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
