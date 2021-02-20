using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public float walkingSpeed = 1.5f;
    public float runningspeed = 5f;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.5f;
    public LayerMask groundLayerMask;

    float speed;
    [SerializeField]
    float movementMagnitude;
    Vector3 moveDirection;
    CharacterController cController;
    Animator animator;
    Vector3 velocity;
    [SerializeField]
    bool isGrounded;

    void Awake()
    {
        cController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    float vertical, horizontal;
    // Update is called once per frame
    void Update()
    {
        // Ground check
        // Créer un layer pour le personnage pour qu'il évite de se détecter lui-même
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayerMask, QueryTriggerInteraction.Ignore);

        // La gravité continue d'accélérer le personnage
        if (isGrounded && velocity.y < 0)
            velocity.y = 0f;

        // 1.2 Inputs
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        // Magnitude du mouvement
        movementMagnitude = Mathf.Abs(vertical) + Mathf.Abs(horizontal);

        // Courir
        if (movementMagnitude > 0.1f && Input.GetKey(KeyCode.LeftShift))
            speed = Mathf.Lerp(speed, runningspeed, 0.075f);
        else
            speed = Mathf.Lerp(speed, walkingSpeed, 0.075f);

        // Déplacements
        moveDirection = transform.forward * vertical;
        moveDirection += transform.right * horizontal;
        
        // Appliquer le déplacement au CharacterController
        cController.Move(moveDirection.normalized * Time.deltaTime * speed);

        // 1.1 Animations de mouvements
        animator.SetFloat("Forward", vertical * speed);
        animator.SetFloat("Strafe", horizontal * speed);
        // ------------------------------------------------------------

        // Jump -------------------------------------------------------
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Indiquer au animator si on bouge ou non
        bool _isMoving = movementMagnitude > 0.1f;
        animator.SetBool("isMoving", _isMoving);

        // YMCA -------------------------------------------------------
        if (!_isMoving && Input.GetKeyDown(KeyCode.Q) && isGrounded)
            animator.SetTrigger("YMCA");

        // Gravité --------------------------------------------------
        velocity.y += gravity * Time.deltaTime;

        cController.Move(velocity * Time.deltaTime);

        // Respawn ------------------------------------------------
        if (transform.position.y < -15f)
            transform.position = Vector3.zero;
    }

    public void Jump()
    {
        velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
