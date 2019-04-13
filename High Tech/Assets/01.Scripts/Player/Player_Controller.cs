using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Controller : MonoBehaviour
{
    public float hp = 100;

    public float speed = 6.0F;
    public float jumpSpeed = 7.0F;
    public float gravity = 25.0F;
    public bool PlayerOn = true;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;

    public static Player_Controller instance;

    void Awake()
    {
        if (Player_Controller.instance == null)
            Player_Controller.instance = this;
    }

    void Update()
    {
        if (gameObject.GetComponent<CharacterController>().enabled)
        {
            CharacterController controller = GetComponent<CharacterController>();
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }

    }


    public void Damage(int dmg)
    {
        hp -= dmg;
        Manager.instance.Hp.fillAmount = hp / 100;
    }

    public void playerOn(bool on)
    {
        gameObject.SetActive(on);
    }

    public void CharacterOn()
    {
        Invoke("Character", 0.5f);
    }

    void Character()
    {
        print("!");
        gameObject.GetComponent<CharacterController>().enabled = true;
    }
}