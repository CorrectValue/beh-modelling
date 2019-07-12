using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanic : MonoBehaviour
{
    //Параметры персонажа
    public float MaxWalkSpeed;      //Максимальная скорость
    public float JumpPower;         //Сила прыжка


    //Параметры геймплейя для персонажа
    private float gravityForce;     //Гравитация персонажа
    private Vector3 MoveVector;     //Направление движения персонажа


    private CharacterController ch_controller;
    // Start is called before the first frame update
    private void Start()
    {
        ch_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        Gravity();
    }


    private void CharacterMove()
    {
        MoveVector = Vector3.zero;
        MoveVector.x = Input.GetAxis("Horizontal") * MaxWalkSpeed;
        MoveVector.z = Input.GetAxis("Vertical") * MaxWalkSpeed;
        if(Vector3.Angle(Vector3.forward, MoveVector)>1f || Vector3.Angle(Vector3.forward, MoveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, MoveVector, MaxWalkSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        MoveVector.y = gravityForce;
        ch_controller.Move(MoveVector * Time.deltaTime);

    }
    private void Gravity()
    {
        if(!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
    }
}
