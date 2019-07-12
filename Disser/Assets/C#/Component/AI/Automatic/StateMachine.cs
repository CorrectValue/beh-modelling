using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Hungry = false;
    private bool Thirst = false;
    private bool Death = false;
    private int searchType = 0;
    private Interaction IS;
    private NavigationSystem NS;
    private HealthStats HS;
    private CombatSystem CS;
    private bool AggressiveState = false;
    
    void Start()
        {
            NS = GetComponent<NavigationSystem>();
            HS = GetComponent<HealthStats>();
            IS = GetComponent<Interaction>();
            CS = GetComponent<CombatSystem>();
            SearchType(0);
        }

    public void SetHungry(bool hungry)
        {
            if(!Death)
            {
                Hungry = hungry;
                CheckStatsState();
            }
            
        }

    public void SetThirst(bool thirst)
        {
            if(!Death)
            {
            Thirst = thirst;
            CheckStatsState();
            }
        }

    private void CheckStatsState()
        {
            if(Thirst)
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
    // Combat System

    public void Aggressive(HealthStats hs1)
    {
        if (AggressiveState = HS.State())
        {
            //hs.transform.position;
            CS.Attack(hs1);
        }
        else return;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
