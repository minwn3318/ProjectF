using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameMgr1 : MonoBehaviour
{
    public static GameMgr1 Instance { get; private set; }
    [SerializeField] PlayerInput playerInput;

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
   
    void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        playerInput.actions["Move"].performed += PlayerController1.Instance.OnMove;
        playerInput.actions["Move"].canceled += PlayerController1.Instance.OnMove;

        playerInput.actions["AttackA"].performed += PlayerController1.Instance.OnAttackAButton;
        playerInput.actions["AttackS"].performed += PlayerController1.Instance.OnAttackSButton;

    }
    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= PlayerController1.Instance.OnMove;
        playerInput.actions["Move"].canceled -= PlayerController1.Instance.OnMove;

        playerInput.actions["AttackA"].performed -= PlayerController1.Instance.OnAttackAButton;
        playerInput.actions["AttackS"].performed -= PlayerController1.Instance.OnAttackSButton;
    }
}
