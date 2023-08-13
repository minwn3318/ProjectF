using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }


    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    Animator animator;
    Vector2 input;
    Vector3 moveVec;
    [SerializeField] float speed;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private bool isMove;
    public int attackCombo = 0;
    public bool isAttack;
    public bool canAttack;

    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;


    public float damage;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Attack();
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if(!isAttack)
        {
            moveVec = new Vector3(input.x, 0, input.y).normalized;
            isMove = moveVec.magnitude != 0;
            if (isMove)
            {
                float targetAngle = Mathf.Atan2(moveVec.x, moveVec.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            transform.position += moveVec * speed * Time.deltaTime;

            animator.SetBool("IsRun", isMove);
        }
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    public void OnAttackAButton(InputAction.CallbackContext context)
    {
        if(canAttack)
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            Debug.Log(noOfClicks);

            if (noOfClicks == 1)
            {
                animator.SetBool("Attack_A_1", true);
                isAttack = true;
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
            if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("A") || animator.GetCurrentAnimatorStateInfo(0).IsName("S")))
            {
                animator.SetBool("Attack_A_1", false);
                animator.SetBool("Attack_S_1", false);
                animator.SetBool("Attack_A_2", true);

                isAttack = true;
            }

            if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("OA") || animator.GetCurrentAnimatorStateInfo(0).IsName("OS")))
            {
                animator.SetBool("Attack_A_2", false);
                animator.SetBool("Attack_S_2", false);
                animator.SetBool("Attack_A_3", true);
                isAttack = true;


            }
        }
      
    }
    public void OnAttackSButton(InputAction.CallbackContext context)
    {
        if(canAttack)
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                animator.SetBool("Attack_S_1", true);
                isAttack = true;
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
            if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("A") || animator.GetCurrentAnimatorStateInfo(0).IsName("S")))
            {
                animator.SetBool("Attack_A_1", false);
                animator.SetBool("Attack_S_1", false);
                animator.SetBool("Attack_S_2", true);
                isAttack = true;
            }

           if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("OA") || animator.GetCurrentAnimatorStateInfo(0).IsName("OS")))
            {
                animator.SetBool("Attack_A_2", false);
                animator.SetBool("Attack_S_2", false);
                animator.SetBool("Attack_S_3", true);
                isAttack = true;
            }
        }
    
    }

    public void Attack()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("Attack_A_1"))
        {
            animator.SetBool("Attack_A_1", false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("Attack_S_1"))
        {
            animator.SetBool("Attack_S_1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("Attack_A_2"))
        {
            animator.SetBool("Attack_A_2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("Attack_S_2"))
        {
            animator.SetBool("Attack_S_2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("Attack_A_3"))
        {
            animator.SetBool("Attack_A_3", false);
            noOfClicks = 0;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetBool("Attack_S_3"))
        {
            animator.SetBool("Attack_S_3", false);
            noOfClicks = 0;
        }
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            canAttack = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            attackCombo = 0;
            isAttack = false;
            CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
            collider.radius = 0.4f;
            collider.center = new Vector3(0, 1, 0f);
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            if (controller.AttackCheck())
            {
            }
        }
    }
    public bool AttackCheck()
    {
        if(isAttack)
        {
            return true;
        }
        return false;
    }
}
