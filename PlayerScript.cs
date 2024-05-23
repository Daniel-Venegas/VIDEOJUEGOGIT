using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Movement Components
    private CharacterController controller;
    private Animator animator;

    private float moveSpeed = 4f;

    [Header("Movement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;

    private float gravity = 9.81f;

    //Interaction components
    PlayerInteractor playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        //Get movement components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //Get interaction component
        playerInteraction = GetComponentInChildren<PlayerInteractor>();

    }

    // Update is called once per frame
    void Update()
    {
        //Runs the function that handles all movement
        Move();

        //Runs the function that handles all interaction
        Interact();


        //Debugging purposes only
        //Skip the time when the right square bracket is pressed
        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    public void Interact()
    {
        //Tool interaction
        if (Input.GetButtonDown("Fire1"))
        {
            //Interact
            playerInteraction.Interact();
        }

        //Item interaction
        if (Input.GetButtonDown("Fire2"))
        {
            playerInteraction.ItemInteract();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            playerInteraction.ItemKeep();
        }
    }


    public void Move()
    {
        //Get the horizontal and vertical inputs as a number
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Direction in a normalised vector
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        if(controller.isGrounded)
        {
            velocity.y = 0;
        }
        velocity.y -= Time.deltaTime * gravity;

        //Is the sprint key pressed down?
        if (Input.GetButton("Sprint"))
        {
            //Set the animation to run and increase our movespeed
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            //Set the animation to walk and decrease our movespeed
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }


        //Check if there is movement
        if (dir.magnitude >= 0.1f)
        {
            //Look towards that direction
            transform.rotation = Quaternion.LookRotation(dir);

            //Move
            controller.Move(velocity);

        }

        //Animation speed parameter
        animator.SetFloat("Speed", dir.magnitude);



    }
}

/*using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    
    private CharacterController controller;
    private Animator animator;

    private float moveSpeed = 4f;

    [Header("Movement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;

    PlayerInteractor playerInteractor;

    // Start is called before the first frame update
    void Start()
    {
        //Componentes del movimiento
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInteractor = GetComponentInChildren<PlayerInteractor>();  
    }

    // Update is called once per frame
    void Update()
    {
        //Función que contiene todo el movimiento
        Move();
        Interact();

        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    public void Interact()
    {
        //Herramientas interacciion
        if (Input.GetButtonDown("Fire1"))
        {
            //Interact
            playerInteractor.Interact();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            playerInteractor.ItemInteract();
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        if (Input.GetButton("Sprint"))
        {
            //Empieza la animación
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }

        //Comprobamos si movimiento en esa direccion
        if (dir.magnitude >= 0.01f) 
        {
            //Ver en todas las direcciones
            transform.rotation = Quaternion.LookRotation(dir);

            //Movimiento
            controller.Move(velocity);
        }

        animator.SetFloat("Speed", velocity.magnitude);
    }
}*/
