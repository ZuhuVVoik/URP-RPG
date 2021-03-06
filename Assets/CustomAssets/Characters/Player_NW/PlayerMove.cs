﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator anim; // переменная для ссылки на контроллер анимации
    CharacterController controller; // переменная для обращения к контроллеру
    public float speedMove = 3f; // переменная для управления скоростью перемещения персонажа
    public float sprintMove = 6f;
    public float speedTurn = 320f;
    bool isSprinting = false;

    Camera camera;
    float rotateSpeed = 5f;

    float baseStaminaPoints = 100f;
    float avaibleStaminaPoints = 0f;
    float currentStaminaPoints = 0f;
    float sprintStaminaCostPerSecond = 15f;

    int staminaDelayDuration = 5;
    bool outOfStaminaDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        camera = GetComponent<Camera>();

        anim.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        int h = (int)Input.GetAxis("Horizontal");
        int v = (int)Input.GetAxis("Vertical");
        Move(v);
        Turn(h);
        Animate(v);
    }



    void FixedUpdate()
    {

    }

    private void Move(int v)
    {
            Vector3 movement = new Vector3(0f, -1f, v);
            // вычисляем вектор направления движения (-1f для эффекта гравитации) 

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement = movement * sprintMove * Time.deltaTime;
           
            }
            else
            {
                movement = movement * speedMove * Time.deltaTime; // учитываем скорость и время

                if (currentStaminaPoints < avaibleStaminaPoints)
                {
                    currentStaminaPoints += Time.deltaTime * sprintStaminaCostPerSecond;
                    if (currentStaminaPoints > avaibleStaminaPoints)
                    {
                        currentStaminaPoints = avaibleStaminaPoints;
                    }
                }
            }

            controller.Move(transform.TransformDirection(movement));
            // применяем смещение к контроллеру для передвижения
        
    }
    private void Turn(int h)
    {
        float turn = h * speedTurn * Time.deltaTime; // выч-м величину поворота
        transform.Rotate(0f, turn, 0f); // поворачиваем на нужный угол
    }


    IEnumerator NoStaminaDelay()
    {
        yield return new WaitForSeconds(staminaDelayDuration);
        currentStaminaPoints = avaibleStaminaPoints;
        outOfStaminaDelay = false;
    }


    private void Animate(float v)
    {
        v *= 10000000;
        bool walk;
        if (v != 0)
        {
            walk = true; // если v не равен 0, есть движение 
            anim.SetBool("IsWalking", true); // переключаем анимацию

            if (Input.GetKey(KeyCode.LeftShift) && !outOfStaminaDelay)
            {
                anim.SetBool("IsRunning", true);
                isSprinting = true;
            }
            else
            {
                anim.SetBool("IsRunning", false);
                isSprinting = false;
            }

        }
        else
        {
            walk = false;
            anim.SetBool("IsWalking", false); // переключаем анимацию
            anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play("Salute");
        }
    }
}

