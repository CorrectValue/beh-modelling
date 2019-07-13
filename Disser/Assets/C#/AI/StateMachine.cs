using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Hungry = false;
    private bool Water = false;
    private bool Stun = false;
    private int searchType = 0;
    private InteractSystem IS;
    private NavigationSystem NS;
    private HealthStats HS;
    public Vector3 Position = new Vector3(0,0,0);
    private Vector3 Position1;
    
    void Start()
        {
            NS = GetComponent<NavigationSystem>();
            HS = GetComponent<HealthStats>();
            IS = GetComponent<InteractSystem>();
            Position1 = Position + new Vector3(100,0,0);
            SearchType(0);
        }


    public void Stunned(bool d)
        {
            Stun = d;
            print("Stun!");
            NS.SetStun(Stun);
            InvokeRepeating("Restart", 15.0f, 0.0f);
        }

    private void Restart()
        {
            Stun = false;
            print("Not Stunned!");
            HS.EventRestart();
            NS.SetStun(Stun);
        } 

    public void SetHungry(bool hungry)
        {
            if(!Stun)
            {
                Hungry = hungry;
                CheckStatsState();
            }
            
        }

    public void SetWater(bool water)
        {
            if(!Stun)
            {
            Water = water;
            CheckStatsState();
            
            }
        }

    private void CheckStatsState()
        {
            if(Water)
            {
                searchType = 2;
                SearchType(searchType);
                return;
            }
            else if(Hungry)
            {
                searchType = 1;
                SearchType(searchType);
                return;
            }
            else
            {
                searchType = 0;
                SearchType(searchType);
                return;
            } 
        }

    private void SearchType(int i)
        {
            IS.SetSearchType(i);
        }
    public bool GetStun()
    {
        return Stun;
    }
    public void ItemFound()
        {
            CheckStatsState();
            if(IS.SearchingItem())
                IS.ItemInteract();
        }
    void OnGUI() 
        {
            GUIStyle myStyle = new GUIStyle(); 
            myStyle.fontSize = 32; 
            myStyle.normal.textColor = Color.black; 
            GUI.Label(new Rect(Position.x, Position.y, 100, 20), gameObject.name, myStyle);
            GUI.Label(new Rect(Position1.x, Position1.y, 100, 20), HS.Point.ToString(), myStyle);
        }
    // Update is called once per frame
    void Update()
    {
        
        if(!Stun)
            IS.SearchingItem();
    }
}
