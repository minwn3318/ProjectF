using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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

        attackCombo++;
        if (attackCombo == 1)
        {
            animator.SetBool("Attack_A_1", true);
            isAttack = true;
        }
        else if (attackCombo >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("A")|| animator.GetCurrentAnimatorStateInfo(0).IsName("S")))
        {
            animator.SetBool("Attack_A_1", false);
            animator.SetBool("Attack_S_1", false);
            animator.SetBool("Attack_A_2", true);

            isAttack = true;
        }

        else if (attackCombo >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("OA") || animator.GetCurrentAnimatorStateInfo(0).IsName("OS")))
        {
            animator.SetBool("Attack_A_2", false);
            animator.SetBool("Attack_S_2", false);
            animator.SetBool("Attack_A_3", true);
            isAttack = true;


        }
        if(isAttack)
        {
            CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
            collider.radius = 1;
            collider.center = new Vector3(0, 1, 0.5f);
        }
    }
    public void OnAttackSButton(InputAction.CallbackContext context)
    {

        attackCombo++;
        if (attackCombo == 1)
        {
            animator.SetBool("Attack_S_1", true);
            isAttack = true;
        }

        else if (attackCombo >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("A") || animator.GetCurrentAnimatorStateInfo(0).IsName("S")))
        {
            animator.SetBool("Attack_A_1", false);
            animator.SetBool("Attack_S_1", false);
            animator.SetBool("Attack_S_2", true);
            isAttack = true;
        }

        else if (attackCombo >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && (animator.GetCurrentAnimatorStateInfo(0).IsName("OA") || animator.GetCurrentAnimatorStateInfo(0).IsName("OS")))
        {
            animator.SetBool("Attack_A_2", false);
            animator.SetBool("Attack_S_2", false);
            animator.SetBool("Attack_S_3", true);
            isAttack = true;
        }
    }

    public void Attack()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && animator.GetBool("Attack_A_1"))
        {
            animator.SetBool("Attack_A_1", false);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetBool("Attack_S_1"))
        {
            animator.SetBool("Attack_S_1", false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetBool("Attack_A_2"))
        {
            animator.SetBool("Attack_A_2", false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetBool("Attack_S_2"))
        {
            animator.SetBool("Attack_S_2", false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && animator.GetBool("Attack_A_3"))
        {
            animator.SetBool("Attack_A_3", false);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && animator.GetBool("Attack_S_3"))
        {
            animator.SetBool("Attack_S_3", false);

        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
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
