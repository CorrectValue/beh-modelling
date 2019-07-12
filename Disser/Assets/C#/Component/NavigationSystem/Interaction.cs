using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interaction : MonoBehaviour
{
    private NavigationSystem NS;
    private NavMeshAgent agent;
    private HealthStats HS;
    private HealthStats HSAgr;
    private MasterItem MI;
    private Vector3 ActorLocation;
    private StateMachine SM;
    public bool SeeItem = false;
    private int SearchType = 0;
    public List<Collider> MIList = new List<Collider>();
    private bool CheckList=true;
    private bool Agr = false;
    private bool Item = false; 
    public int CurrentAgr = 0;
    public int CurrentItem = 0;
    private int c;



    public void SetSearchType(int i)
    {
        SearchType = i;
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NS = GetComponent<NavigationSystem>();
        HS = GetComponent<HealthStats>();
        SM = GetComponent<StateMachine>();
        SeeItem = false;
        SetSearchType(0);
        //InvokeRepeating("GoRestartFunction", 0, 15);
    }
    
    
    /*void GoRestartFunction()
        {
            MI = null;
            SeeItem = false;
            NS.SetStop(false);
            NS.newPoint(); 
            print("Restart Function!"); 
        }*/
    
    
    
    
    
    
    
    
    
    void OnTriggerEnter(Collider other)
        {
            
                do
                {
                    if(MIList.Count>0)
                    {
                        if(MIList[c] == other)
                        CheckList = false;
                        else CheckList = true;
                    }
                    c++;
                } while (c<MIList.Count);
                c = 0;
                if (CheckList)  MIList.Add(other);
        }
    
    void OnTriggerStay(Collider other)
        {

            /*if(other.GetComponent<MasterItem>())
            {
                if(other.GetComponent<MasterItem>().Type == SearchType)
                {
                    if(!SeeItem)
                    {
                    print(other.gameObject.name);
                    ActorLocation = other.transform.position;
                    NS.SetPoint(ActorLocation);
                    MI = other.GetComponent<MasterItem>();
                    SeeItem = true;
                    }
                }
                
            }
            if(other.GetComponent<HealthStats>())
            {
                SM.Aggressive(other.GetComponent<HealthStats>());
            }*/
        }

    void OnTriggerExit(Collider other)
    {
        for(int i = 0; i<MIList.Count;i++)
        {
            if(MIList[i] == other)
            MIList.Remove(other);
        }

            /*
            MI = null;
            SeeItem = false;
            NS.SetStop(false);
            NS.newPoint();
            Debug.Log("ExitTrigger");
            */
    }
    bool getDistance() //Дистанция от персонажа до точки
        {
            double dis = Mathf.Sqrt(Mathf.Abs((MI.transform.position.x  - transform.position.x) +
                                (MI.transform.position.z - transform.position.z)));
            if(dis < 1.3) return true;
            return false;
        }
    private void Interact()
    {
        CurrentAgr = 0;
        CurrentItem = 0;
        Agr = false;
        Item = false;
        for(int i = 0; i<MIList.Count;i++)
        {
            if(MIList[i].GetComponent<HealthStats>())
            {
                Agr = true;
                CurrentAgr = i;
            }
            if(MIList[i].GetComponent<MasterItem>())
            {
                CurrentItem = i;
                Item = true;
            }
        }
        if(Agr && Item)
            {
                SM.Aggressive(MIList[CurrentAgr].GetComponent<HealthStats>());
                NS.SetPoint(MIList[CurrentAgr].transform.position);
                print("Fight!");
            }
            else if(Item)
                {
                    if(MIList[CurrentItem].GetComponent<MasterItem>().Type == SearchType)
                        {
                            ActorLocation = MIList[CurrentItem].transform.position;
                            NS.SetPoint(ActorLocation);
                            MI = MIList[CurrentItem].GetComponent<MasterItem>();
                            SeeItem = true;
                            
                            if(SeeItem && getDistance())
                            {
                                print("Interact");
                                MI.ToStorage(HS);
                                MI = null;
                                SeeItem = false;
                                NS.SetStop(false);
                                NS.newPoint(); 
                            }
                        }
                }
    }
    // Update is called once per frame
    void Update()
    {
        Interact();
        for(int i = 0; i<MIList.Count-1;i++)
        {
            if(MIList[i] == MIList[i+1])
            MIList.Remove(MIList[i+1]);
        }
    }
}
