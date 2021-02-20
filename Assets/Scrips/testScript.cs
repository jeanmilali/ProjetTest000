using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public float speed = 2f;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.5f;
    public LayerMask groundLayerMask;

    Vector3 moveDirection;
    CharacterController cController;

    Vector3 velocity;
    [SerializeField]
    bool isGrounded;

    void Awake()
    {
        cController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check --------------------------------------------
        // Créer un layer pour le personnage pour qu'il évite de se détecter lui-même
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayerMask, QueryTriggerInteraction.Ignore);

        // La gravité continue d'accélérer le personnage ----------
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        // Movement -----------------------------------
        /* Version 1 (celle du Shrek)
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = 0f;
        moveDirection.z = Input.GetAxis("Vertical");
        */

        // Version 2 (1ere ou 3e personne)
        moveDirection = transform.forward * Input.GetAxis("Vertical");
        moveDirection += transform.right * Input.GetAxis("Horizontal");

        GetComponent<Animator>().SetFloat("Forward", Input.GetAxis("Vertical"));
        GetComponent<Animator>().SetFloat("Strafe", Input.GetAxis("Horizontal"));

        cController.Move(moveDirection * Time.deltaTime * speed);
        // ------------------------------------------------------------

        // Jump ----------------------------------------------------
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            GetComponent<Animator>().SetTrigger("Jump");
        }

        // Gravité --------------------------------------------------
        //velocity.y += gravity * Time.deltaTime;

        cController.Move(velocity * Time.deltaTime);
    }
}
