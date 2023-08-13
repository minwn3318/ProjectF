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

    //공격중 판정 부울 변수 선언
    public bool isAttack = false;
    //콤보가능 판정 부울 변수 선언
    public bool canCombo = false;

	//공격 콤보 변수 선언
	public int attackCombo = 0;
    //클릭 횟수 스테틱 변수 선언
    public static int noOfClicks = 0;

    //마지막 클릭 시간 저장 변수 선언
    //float lastClickedTime;
    //콤보 딜레이 변수 선언
    //float maxComboDelay = 1f;
    //데미지 변수 선언
    public float damage;

    void Start()
    {
        animator = GetComponent<Animator>();
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
    }
    private void FixedUpdate()
    {
        Idle();
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

    public void Idle()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.2f)
            {
                attackCombo = 0;
                noOfClicks = 0;
                isAttack = false;
                canCombo = false;
                //Debug.Log("combo : " + canCombo);
                CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
                collider.radius = 0.4f;
                collider.center = new Vector3(0, 1, 0f);
            }
        }

        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
            {
                canCombo = true;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    public void OnAttackAButton(InputAction.CallbackContext context)
    {
        noOfClicks++;
        Debug.Log(noOfClicks);
        if(noOfClicks == 1)
        {
            animator.SetTrigger("Attack_A");
            //Debug.Log("Attack A");
            isAttack = true;
        }

        else
        {
            if(canCombo)
            {
                switch(noOfClicks)
                {
                    case 2:
                        animator.SetTrigger("Attack_AA");
                        //Debug.Log("Attack AA");
                        break;
                    case 3:
                        if(animator.GetCurrentAnimatorStateInfo(0).IsName("OA"))
                        {
                            animator.SetTrigger("Attack_AAA");
                        }
                        if(animator.GetCurrentAnimatorStateInfo(0).IsName("OS"))
                        {
                            animator.SetTrigger("Attack_SSA");
                        }
                        break;
                    default:
                        break;
                }
            }

            else
            {
                animator.SetTrigger("Attack_A");
                noOfClicks = 1;
            }
        }
    }

    public void OnAttackSButton(InputAction.CallbackContext context)
    {
        noOfClicks++;
        if (noOfClicks < 2)
        {
            animator.SetTrigger("Attack_S");
            isAttack = true;
        }

        else
        {
            if (canCombo)
            {
                switch (noOfClicks)
                {
                    case 2:
                        animator.SetTrigger("Attack_SS");
                        break;
                    case 3:
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("OS"))
                        {
                            animator.SetTrigger("Attack_SSS");
                        }
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("OA"))
                        {
                            animator.SetTrigger("Attack_AAS");
                        }
                        break;
                    default:
                        break;
                }
            }

            else
            {
                animator.SetTrigger("Attack_S");
                noOfClicks = 1;
            }
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
