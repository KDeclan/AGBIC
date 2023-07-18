using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTankController : MonoBehaviour
{
    private CharacterController tankCC;

    private Animator anim;

    //Character speed
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [SerializeField] int gunDamage = 50;

    public bool isControllable = true;
    public bool canTurn = true;

    //referring to being able to move with inventory open
    public bool canMove;

    private void Awake() 
    {
        tankCC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        canMove = true;
    }

    private void OnEnable() 
    {
        EventBus.Instance.onGameplayPaused += () => canMove = false;
        EventBus.Instance.onGameplayResumed += () => canMove = true;
    }

    private void OnDisable() 
    {
        EventBus.Instance.onGameplayPaused -= () => canMove = false;
        EventBus.Instance.onGameplayResumed -= () => canMove = true;
    }

    private void Update()
    {
        if(!canMove) return;

        if (isControllable)
        {
                //WASD Tank controls
            if (Input.GetKey(KeyCode.W))
            {
                anim.ResetTrigger("walkback");
                anim.ResetTrigger("turnLeft");

                if (anim != null)
                {
                    canTurn = false;
                    tankCC.Move(transform.forward * 1f * Time.deltaTime);

                    anim.SetTrigger("isWalk");
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    tankCC.Move(transform.forward * 1.6f * Time.deltaTime);
                    anim.ResetTrigger("isWalk");

                    anim.SetTrigger("isRun");
                }
                else
                {
                    anim.ResetTrigger("isRun");
                }
            }
            else
            {
                anim.ResetTrigger("isWalk");
                anim.ResetTrigger("isRun");
                canTurn = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                anim.SetTrigger("walkback");

                canTurn = false;
                tankCC.Move(-transform.forward * 0.5f * Time.deltaTime);
            }
            else
            {
                anim.ResetTrigger("walkback");
            }
        }

        if (canTurn)
        {
                //*TODO* make only able to turn 90 degrees
            if (Input.GetKey(KeyCode.A))
            {
                anim.SetTrigger("turnLeft");
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }
            else
            {
                anim.ResetTrigger("turnLeft");
            }

            if (Input.GetKey(KeyCode.D))
            {
                anim.SetTrigger("turnRight");
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }
            else
            {
                anim.ResetTrigger("turnRight");
            }
        }

        //Tank Combat Controls
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.ResetTrigger("isWalk");
            anim.ResetTrigger("isRun");
            anim.ResetTrigger("walkback");
            anim.ResetTrigger("turnLeft");
            anim.ResetTrigger("turnRight");

            //Prepare to shoot
            //Drastically slow forward and backward movement
            //Minimally slow left and right movement
            //Raycast to show projectile path

            isControllable = false;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            if (Input.GetMouseButtonDown(0))
            {
                //only if gun is also readied
                //"fire" "projectile"

                Shoot();
            }
        }
        else 
        {
            isControllable = true;
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    
            //this will later be put into a gun script
            //each gun will have their own
                
            if(hit.transform.gameObject.TryGetComponent(out IDamageable damageableObject))
            {
                damageableObject.Damage(gunDamage);
                Debug.Log("Hit");
            }
        }
            else
        {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
}
