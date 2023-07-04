using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }    //싱글톤
    private Rigidbody playerRB;
    private Animator playerAnimator;

    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float moveSpeed;


    public Vector3 position { get { return playerRB.position; } } // transform.position보다 rigidbody.position의 비용이 적다.
    public bool isLive { get { return gameObject.activeSelf; } }
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    { 
    }
    void FixedUpdate()
    {
        playerRB.velocity = velocity;
    }
    public void Initialize()
    {
        direction = Vector3.zero;
        velocity = Vector3.zero;
        gameObject.SetActive(true);
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Move(Action)의 Control Type이 Vector2이다.
        Vector2 v = ctx.ReadValue<Vector2>();
        direction = new Vector3(v.x, 0, v.y);

        velocity = direction * moveSpeed; // 방향과 속력을 이용하여 속도(velocity)를 구한다.
    }
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        playerAnimator.SetTrigger("Attack_1");
    }
}
