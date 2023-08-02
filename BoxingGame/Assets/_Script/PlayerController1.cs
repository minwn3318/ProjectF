using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    //플레이어컨트롤러 인스턴스 선언
    public static PlayerController1 Instance { get; private set; }

    //플레이어컨트롤러 싱글톤패턴 구현
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

    //애니메이션 컨트롤러 변수 선언
    Animator animator;

    //이동관련 평면벡터 변수 선언
    Vector2 input;
    //이동 중 회전관련 삼차원벡터 변수 선언
    Vector3 moveVec;

    //이동속력 변수 선언
    [SerializeField] float speed;
    //회전시간 변수 선언
    public float turnSmoothTime = 0.1f;
    //회전속력 변수 선언
    private float turnSmoothVelocity;

    //움직임 판정 부울 변수 선언
    private bool isMove;
    //공격 콤보 변수 선언
    public int attackCombo = 0;
    //공격중 판정 부울 변수 선언
    public bool isAttack;
    //공격 가능 판정 부울 변수 선언
    public bool canAttack;

    //공격 쿨타임 변수 선언
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;

    //클릭 횟수 스테틱 변수 선언
    public static int noOfClicks = 0;
    //마지막 클릭 시간 저장 변수 선언
    float lastClickedTime = 0;
    //콤보 딜레이 변수 선언
    float maxComboDelay = 1f;
    //데미지 변수 선언
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
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        if (canAttack)
        {
            lastClickedTime = Time.time;
            noOfClicks++;

            if (noOfClicks == 1)
            {
                animator.SetBool("Attack_A_1", true);
                isAttack = true;
            }

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
