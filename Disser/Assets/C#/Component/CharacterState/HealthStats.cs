using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthStats : MonoBehaviour
{
    public float Hungry = 100f;       private float MaxHungry = 100f;
    public float Thirst = 100f;     private float MaxWater = 100f;
    public float Health = 100f;  
    private float MaxHealth = 100f; private float MinHealth = 1f;
    public int Point = 0;
    private StateMachine SM;
    
    
    void Start()
    {
        SM = GetComponent<StateMachine>();
    }
    
    
    // Функция получающая значение из функции ChangeStats, и если он равен нулю начинает уменьшение Health, также возвращает флаг жива ли наша капсула или нет.
    private bool ChangeHealth(int i)
        {
            if(i!=0)
            Health -= (Mathf.Exp(Health/25))/(Health*10);

            if(Health < MinHealth)
            return true;

            else return false;
        }
    public bool State()
        {
            if ((Hungry > 30)&&(Thirst > 30)&&(Health > 90))
            return true;
            return false;
        }
    // Функция возвращающая ноль, если еда или вода капсулы достигает нуля.
    private int ChangeStats()
        {
            if(Hungry > 0)
            {
                Hungry -=  0.005f;
                SM.SetHungry(false);
            }
            else
            {
                SM.SetHungry(true);
                Hungry -=  0.005f;
                return 1;
            } 
            if(Thirst > 30f)
            {
                SM.SetThirst(false);
                Thirst -=  0.01f;
            }  
            else
            {
                SM.SetThirst(true);
                Thirst -=  0.01f;
                return 2;
            } 
        return 0;
        }
        
        public bool GetStat(int i)
        {
            if((i == 1)&& (Hungry > 30f))
            return false;
            else return true;
            if((i == 2)&& (Hungry > 30f))
            return false;
            else return true;
            if((i == 0)&& (Health > 90f))

        }
    // Функция изменяющая параметры еды или воды в зависимости от типа подбираемого 
    public void ChangePStats(int Type)
        {
            if (Type == 1)
            {
                Thirst -= 5f;
                if((Health +15f)<MaxHealth)
                Health +=15f;
                else Health = MaxHealth;
                if ((Hungry + 30f)<MaxHungry)
                Hungry +=  30f;
                else Hungry = MaxHungry;
            }
            else if(Type == 2)
            {
                if((Health +10f)<MaxHealth)
                Health +=10f;
                else Health = MaxHealth;
                if ((Thirst + 50f)<MaxWater)
                Thirst +=  50f;
                else Thirst = MaxWater;
            }
            else Point++;

        }
    // Start is called before the first frame update
    public void CalculateHealth(float health)
    {
        Health -= health;
        return;
    }
    public float HowDamage()
        {
            return (((Hungry+Thirst)*Health)/1000);
        }
    
    // Update is called once per frame
    void Update()
    {
        //OnGUI();
        //Если функция ChangeHealth возвращает true, то капсула умерла
        if(ChangeHealth(ChangeStats()))
        Debug.Log("Dead");
    }
}
