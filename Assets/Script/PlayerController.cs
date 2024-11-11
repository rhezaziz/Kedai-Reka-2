using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InteractionObject;

public class PlayerController : MonoBehaviour
{
    /*
    public float collisionOffSet = 0.05f;

    public float moveSpeed;

    public ContactFilter2D movementFilter;

    private Vector2 moveInput;

    private List<RaycastHit2D> castCollison = new List<RaycastHit2D>();

    private Rigidbody2D rb;

    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        anim = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        
        if(moveInput != Vector2.zero)
        {
            bool succes = MovePlayer(moveInput);
            

            if(!succes)
            {
                succes = MovePlayer(new Vector2(moveInput.x, 0));

                if (!succes)
                {
                    succes = MovePlayer(new Vector2(0, moveInput.y));
                }
            }
            anim.SetBool("IsMoving", succes);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

    }


    public bool MovePlayer(Vector2 dirt)
    {
        int count = rb.Cast(
            dirt,
            movementFilter,
            castCollison,
            moveSpeed * Time.fixedDeltaTime + collisionOffSet);

        if(count >= 0)
        {
            Vector2 moveVector = dirt * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            foreach(RaycastHit2D hit in castCollison)
            {
                print(hit.ToString());
            }
            return false;
        }
    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        // Only set the animation direction if the player is trying to move
        if (moveInput != Vector2.zero)
        {
            anim.SetFloat("XInput", moveInput.x);
            anim.SetFloat("YInput", moveInput.y);
        }
    }
    */


    public playerProfil profil;

    public float moveSpeed = 5f;

    public Rigidbody rb;
    public bool isMoving = true;
    public Animator anim;
    public SpriteRenderer sp;
    public float interactDistance = 2f;
    Vector3 Movement;
    public Vector3 lastInteractDir;

    public GameObject textInteraction;
    [SerializeField] private LayerMask countersLayerMask;

    [SerializeField] private Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        //transform.position = profil.position;
    }
    void Update()
    {
        if(isMoving)
            handleInteraction();
    }


    public void masukToilet(int Energy)
    {

        profil.Energy += Energy;

        if(profil.Energy >= 4)
        {
            profil.Energy = 4;
        }
    }

    public void tidur()
    {
        profil.Energy = 4;
        profil.jmlHari += 1;
    }


    public void startAnimation(string animationName)
    {
        _anim.SetTrigger(animationName);
        isMoving = false;
    }

    public void endAnimation()
    {
        isMoving = true;
    }

    void movementPlayer()
    {
        
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.z = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);

        if (Movement.x != 0 || Movement.z != 0)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("Xinput", Movement.x);
            anim.SetFloat("yInput", Movement.z);

            if (Movement.x != 0 && Movement.x < 0)
            {
                sp.flipX = false;
            }
            else if (Movement.x != 0 && Movement.x > 0)
            {
                sp.flipX = true;
            }
        }
        else if(Movement.x == 0 && Movement.z == 0)
        {
            anim.SetBool("IsMoving", false);
        }       
    }

    void handleInteraction()
    {
        Vector3 moveDir = new Vector3(Movement.x, 0f , Movement.z);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        //Debug.Log(moveDir);
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            //Debug.Log(raycastHit.transform.gameObject.name);
            if(raycastHit.transform.TryGetComponent(out InteractionObject interaction))
            {
                //interactionType type = interaction.type;

                textInteraction.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    interaction.interaction();
                   // Debug.Log(type);
                }
            }
            else
            {
               
            }
            /*
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
              //  if (baseCounter != selectedCounter)
               // {
                   // SetSelectedCounter(baseCounter);
              //  }
            }
            else
            {
               // SetSelectedCounter(null);

            }
            */
        }
        else
        {
            textInteraction.SetActive(false);
            // SetSelectedCounter(null);
        }
    }

    void FixedUpdate()
    { 
        if(isMoving)
            movementPlayer();
       
    }


}
