using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTankController : MonoBehaviour
{
    private CharacterController tankCC;

    //Character speed
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    [SerializeField] int gunDamage = 50;

    private void Awake() 
    {
        tankCC = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //WASD Tank controls
        if (Input.GetKey(KeyCode.W))
        {
            tankCC.Move(transform.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            tankCC.Move(-transform.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }

        //Tank Combat Controls
        if (Input.GetKey(KeyCode.F))
        {
            //Prepare to shoot
            //Drastically slow forward and backward movement
            //Minimally slow left and right movement
            //Raycast to show projectile path
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                //only if gun is also readied
                //"fire" "projectile"

                //this will later be put into a gun script
                //each gun will have their own
                if(hit.transform.gameObject.TryGetComponent(out IDamageable damageableObject))
                {
                    damageableObject.Damage(gunDamage);
                    Debug.Log("Hit");
                    //*BUG* enemy damage function gets called 10 times in 1 frame!
                }    
            }
        }
    }
}
