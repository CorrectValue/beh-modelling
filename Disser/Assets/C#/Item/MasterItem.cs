using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MasterItem : MonoBehaviour
{
    private Renderer MR;                    //Ссылка на компонент отвечающий за отображение 
    private Vector3 WorldActorLocation;     //Координаты объекта при старте игры
    public int Type = 0;                    // Тип объекта 0 - очки, 1 - Еда, 2 - Вода
    void Start()    // Эта функция запускается перед самым стартом проекта
        {
            MR = GetComponent<Renderer>();              //Взятие ссылки на компонент отображения 
            WorldActorLocation = transform.position;    //Сохранение координат объекта
        }
    public void Interact(HealthStats HS)                // Функция взаимодействия
    {
        MR.enabled = false;                             //Отключение видимости объекта
        transform.localPosition = WorldActorLocation - new Vector3(0,40,0); // Перемещение объекта под карту по координате Y
        HS.ChangePStats(Type);                          //Прибавление очков по типу объекта
        print("Used");
    }



    

    
    void Update()   // Вызывается каждый кадр
    {
        
    }
}
