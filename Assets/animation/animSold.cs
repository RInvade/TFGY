using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animSold : MonoBehaviour
{
    private Animator animator;
    public GameObject registro;
    private RegistroController cntr;
    private bool depie;
    // Start is called before the first frame update
    void Start()
    {
        depie = true;
        animator = this.GetComponent<Animator>();
        cntr = registro.GetComponent<RegistroController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (depie)
            {
                animator.SetTrigger("walk");
            }
            else
            {
                animator.SetTrigger("crouchwalk");
            }
            cntr.anadirRegistro("walking", 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (depie)
            {
                animator.SetTrigger("turnr");

            }
            else
            {
                animator.SetTrigger("crouchturnr");
            }
            cntr.anadirRegistro("turnr", 1, 1, 1);

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (depie)
            {
                animator.SetTrigger("turnl");

            }
            else
            {
                animator.SetTrigger("crouchturnl");
            }
            cntr.anadirRegistro("turnl", 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (depie)
            {
                animator.SetTrigger("turn");

            }
            else
            {
                animator.SetTrigger("crouchturn");
            }
            cntr.anadirRegistro("turn", 1, 1, 1);

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (depie)
            {
                animator.SetTrigger("jump");
                cntr.anadirRegistro("jump", 1, 1, 1);
            }
            else {
                animator.SetTrigger("stand");
                cntr.anadirRegistro("stand", 1, 1, 1);
                depie = true;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("crouch");
            cntr.anadirRegistro("crouch", 1, 1, 1);
            depie = false;

        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetTrigger("stand");
            cntr.anadirRegistro("stand", 1, 1, 1);

        }
    }
    public void avanzar()
    {
        if (depie)
        {
            animator.SetTrigger("walk");
        }
        else
        {
            animator.SetTrigger("crouchwalk");
        }
        cntr.anadirRegistro("walking", 1, 1, 1);


    }
    public void agacharse()
    {

        if (depie)
        {

            animator.SetTrigger("crouch");
            cntr.anadirRegistro("standing to crouch", 1, 1, 1);
            depie = false;
        }
        else
        {

        }
    }
    public void levantarse()
    {

        if (!depie)
        {
            animator.SetTrigger("stand");
            cntr.anadirRegistro("jump", 1, 1, 1);
            depie = true;
        }
        else
        {
            animator.SetTrigger("jump");
            cntr.anadirRegistro("jump", 1, 1, 1);
        }
        
    }
    public void girard()
    {

        if (depie)
        {
            animator.SetTrigger("turnr");

        }
        else
        {
            animator.SetTrigger("crouchturnr");
        }
        cntr.anadirRegistro("right turn", 1, 1, 1);


    }
    public void girari()
    {
        if (depie)
        {
            animator.SetTrigger("turnl");

        }
        else
        {
            animator.SetTrigger("crouchturnl");
        }
        cntr.anadirRegistro("turnl", 1, 1, 1);
    }
    public void girar()
    {
        if (depie)
        {
            animator.SetTrigger("turn");

        }
        else
        {
            animator.SetTrigger("crouchturn");
        }
        cntr.anadirRegistro("turn", 1, 1, 1);


    }
}
