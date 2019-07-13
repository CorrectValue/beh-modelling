using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractSystem : MonoBehaviour
{
    private NavigationSystem NS;
    private HealthStats HS;
    private MasterItem MI;
    private StateMachine SM;
    private GameObject GO;
    public  List<Collider> MIList = new List<Collider>();
    private int SearchType = 0;
    private bool CheckList=true;
    private int c;

    
    void Start()    // Эта функция запускается перед самым стартом проекта
    {
        NS = GetComponent<NavigationSystem>();      //Инициализация 
        HS = GetComponent<HealthStats>();
        SM = GetComponent<StateMachine>();
        SetSearchType(0);
    }
    
    public void SetSearchType(int i)            //Установка поискового типа
        {
            SearchType = i;
        }

    void OnTriggerEnter(Collider other)             //Если объект пересекает Collider
        {
            c = 0;
            CheckList = true;                       //Если нет объектов в листе
            do
            {
                if(MIList.Count > 0)                // Проверка что лист не пустой
                {
                    if (MIList[c].gameObject.name == other.gameObject.name)      //Проверка на совпадение объектов (Из листа и новый)
                    {
                        CheckList = false;          //Если одинаковые, то не добавлять
                        break;                      //Выход из цикла do while
                    }
                    else
                    {
                    CheckList = true;               //Иначе, добавлять
                    }
                }
                c++;                                //Переход к следующему объекту в листе
            } while (c<MIList.Count);
            if (CheckList) MIList.Add(other);       //Добавление в конец листа нового объекта
        }

    void OnTriggerExit(Collider other)              //Если объект выходит из Collider
    {
        int i;      //Запоминание номера в листе
        for(i = 0; i<MIList.Count; i++)         //Цикл по всем объектам листа
        {
                if (MIList[i].gameObject.name == other.gameObject.name)      //Проверка на одинаковость объектов (В листе и выходящего)
                {
                    break;                      //Выход из цикла for
                }
        }
        MIList.RemoveAt(i);                     //Удаление предмета по номеру
    }



    public bool SearchingItem()                            //Проверка на искомый объект в зоне видимости
    {
        for(int i = 0; i < MIList.Count; i++)   //Цикл по всем объектам в листе
            if (MIList.Count>0)     //Список не пустой
            {   
                if(MIList[i].gameObject.GetComponent<MasterItem>() != null)    
                    if(MIList[i].gameObject.GetComponent<MasterItem>().Type == SearchType)   //Проверка искомых типов (сверяет тип объета и искомый тип (по состоянию AI))
                    {
                        NS.SetPoint(MIList[i].transform.position);            //Установка новой точки для NavigationSystem
                        MI = MIList[i].GetComponent<MasterItem>();            //Взятие ссылки на объект 
                        ItemInteract();
                        return true;                                   
                    }

                }
            
            return false;
    }
    public bool ItemInteract()
    {
        if(getDistance())       //Дистанция до объекта
        {
            print("Interact");  //Для отладки
            MI.Interact(HS);    //Использование объекта
            MI = null;          //Сброс ссылки для избежания повторного использования
            NS.SetStop(false);  //Сброс флага поиска новой точки
            NS.newPoint(); 
            return true;        //Поиск новой точки
        }
        else return false;
    }
    private bool getDistance()                          //Дистанция от персонажа до точки
        {
            double dis = Mathf.Sqrt(Mathf.Abs((MI.transform.position.x  - transform.position.x) +
                                (MI.transform.position.z - transform.position.z)));
            if(dis < 1) return true;    //Возвращаем true, если дистанция меньше 1.3
            return false;
        }
    void Update()   // Вызывается каждый кадр
    {
    }
}
