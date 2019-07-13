using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthStats : MonoBehaviour
{
    public float Hungry;         //Голод, если 0, то это очень плохо     
    public float Water;          //Жажда, если 0, то это очень плохо 
    public float Health;         //Здоровье, если 0, то это очень плохо 
    public int Point = 0;               //Кол-во очков
    private float MaxValue = 100;
    private bool Stunh = false;
    private StateMachine SM;            //Ссылка на Конченный AI 
    
    
    void Start()                        // Эта функция запускается перед самым стартом проекта
    {
        SM = GetComponent<StateMachine>(); //Ссылка на Древовидное AI 
    }

    // Функция получающая значение из функции ChangeStats, и если она равна нулю начинается уменьшение Health, также возвращает флаг жива ли наша капсула или нет.
    private bool ChangeHealth(int i)
        {
            switch (i)
            {
                case 0:
                break;

                case 1:
                Health -= (Mathf.Exp(Health/25))/(Health*10);
                break;

                case 2:
                Health -= 2.0f*(Mathf.Exp(Health/25))/(Health*10);
                break;

                default:
                break;
            }
            if(Health <= 0.02f)
            { Health = 0.0f; Stunh = true;
            SM.Stunned(Stunh); return true;}                  //Если здоровья меньше минимального, то AI умер
            else {Stunh = false; return false;}
        }
    public void EventRestart()
    {
        Hungry = MaxValue;
        Health = MaxValue;
        Water = MaxValue;
        Stunh = false;
    }
    public void ChangePStats(int Type)  // Функция изменяющая параметры еды или воды в зависимости от типа подбираемого объекта
        {
            if (Type == 1)
            {
                Water -= 5f;
                if((Health +15f)<MaxValue)
                Health +=15f;
                else Health = MaxValue;
                if ((Hungry + 30f)<MaxValue)
                Hungry +=  35f;
                else Hungry = MaxValue;
            }
            else if(Type == 2)
            {
                if((Health +10.0f)<MaxValue)
                Health +=10.0f;
                else Health = MaxValue;
                if ((Water + 50.0f)<MaxValue)
                Water +=  50.0f;
                else Water = MaxValue;
            }
            else Point++;
        }
    private int ChangeStats()
        {
            if((Hungry <= 0.1f)&&(Water <= 0.1f))
            {
                SM.SetHungry(true);
                SM.SetWater(true);
                return 2;
            }
            if(Hungry > 50.0f) SM.SetHungry(false);
            
            else  SM.SetHungry(true);

            if(Hungry >= 0.01f)  Hungry -= 0.03f;
            
            else  Hungry = 0;
            
            if(Water > 40.0f) SM.SetWater(false);
            
            else SM.SetWater(true);
            
            if(Water >= 0.01f) Water -= 0.07f;

            else Water = 0;
            
            if((Hungry <= 0.1f)||(Water <= 0.1f))
                return 1;
            
        return 0;
        }

    void Update()   // Вызывается каждый кадр
    {   
        if(!Stunh)
        ChangeHealth(ChangeStats());
            
    }
}
