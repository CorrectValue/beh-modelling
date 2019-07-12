using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationSystem : MonoBehaviour
{
    
    public float    minX,   minZ;           // Минимальное значение для оси Z и X
    public float    maxX,   maxZ;           // Максимальное значение для оси Z и X
    private Vector3 Point;                  //Точка для следования
    private Vector3 Position;               //Текущая позиция
    private NavMeshAgent agent;             //Ссылка на NavMeshAgent
    private Interaction IS;                 //
    private bool Stop;
    void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            newPoint();
            Point.x = 0;
            Point.z = 0;
            Stop = false;
        }                      //
    public void SetStop(bool s){Stop = s;}
    
    public void newPoint()
        {
            if(!Stop)
            {
            Point.x = Random.Range(minX, maxX); // Рандомная позиция X
            Point.z = Random.Range(minZ, maxZ); // Рандомная позиция Z
            Debug.Log("newPoint");
            }

        }

    public void SetPoint(Vector3 ActorLocation)
        {
            Point.x = ActorLocation.x; //Установка новой координаты X
            Point.z = ActorLocation.z; //Установка новой координаты Z
            Stop = true;
            Debug.Log("newPointOK!");
        }

    bool getDistance() //Дистанция от персонажа до точки
        {
            double dis = Mathf.Sqrt(Mathf.Abs((Point.x - transform.position.x) + 
                                    (Point.z - transform.position.z)));    //Математическая формула пути (От точки до точки)
            if(dis < 1 ){Stop =  false; return true; }
            return false;
        }



    // Update is called once per frame

    void Update()
        {   
            agent.SetDestination(Point);    //Следовать до точки
            if(getDistance())             //Если врадиусе точки
            {
                newPoint();                 //Новая точка
                Stop =  false;         
            }
        }
}
