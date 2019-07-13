using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationSystem : MonoBehaviour
{
    
    public float    minX,   minZ;                   // Минимальное значение для оси Z и X
    public float    maxX,   maxZ;                   // Максимальное значение для оси Z и X
    private Vector3 Point;                          //Точка куда следуем
    private bool Stun = false;
    private NavMeshAgent agent;                     //Ссылка на NavMeshAgent
    private bool Stop = false;                      //Переменная для отключения создания новых точек при вызове SetPoint

    void Start()    // Эта функция запускается перед самым стартом проекта
        {
            agent = GetComponent<NavMeshAgent>();   //Получение ссылки на компонент AI NavMeshAgent (Перемещение персонажа по локации)
            Stop = false;
        }  
    public void SetStop(bool s){Stop = s;}
    public void SetStun(bool d)
        {
            Stun = d;
            Stop = d;
            Point.x = transform.position.x;
            Point.z = transform.position.z;
        }
    public void newPoint()                          //Создание новой рандомной координаты на локации 
        {
            if(!Stop) //Если stop = true, новую точку не даем
            {
            Point.x = Random.Range(minX, maxX);     // Рандомная координата X
            Point.z = Random.Range(minZ, maxZ);     // Рандомная координата Z
            Debug.Log("newPoint");
            }

        }

    public void SetPoint(Vector3 ActorLocation)//Для перемещения до нужного объекта
        {
            Point.x = ActorLocation.x; //Установка новой координаты X
            Point.z = ActorLocation.z; //Установка новой координаты Z
            Stop = true;               //Перестать искать новую точку
            Debug.Log("newPointOK!");
        }

    bool getDistance() //Дистанция от персонажа до точки
        {
            double dis = Mathf.Sqrt(Mathf.Abs((Point.x - transform.position.x) + 
                                    (Point.z - transform.position.z)));    //Математическая формула пути (От точки до точки)
            if (dis < 0.5)        //Если дистанция < 1 метра
            {
                Stop = false;   //Начать искать новую точку
                return true;    //Если дистанция < 1 метра, возвращаем true
            }
            return false;
        }



    // Update is called once per frame

    void Update() // Вызывается каждый кадр
        {   
            agent.SetDestination(Point);    //Следовать до точки
            if(getDistance()&&!Stun)        //В зоне действия (От AI до объекта)
            {
                newPoint();                 //Создание новой рандомной координаты на локации              
            }
        }
}
