using DefaultNamespace;
using Input;
using Scriptables;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(InputProcessor))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController s_instance;
    private static readonly int s_isWalkingHash = Animator.StringToHash("IsWalking");

    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private GameStateManager m_gameStateManager;

    private Vector3 m_moveDirection;
    private CharacterController m_characterController;
    private InputProcessor m_inputProcessor;
    private Animator m_animator;


    public static PlayerController Instance => s_instance;
    
    
    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        
        this.m_inputProcessor = this.GetOrAddComponent<InputProcessor>();
        this.m_characterController = this.GetComponent<CharacterController>();
        this.m_animator = this.GetComponentInChildren<Animator>();
        if (this.m_gameStateManager == null)
        {
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (this.m_gameStateManager.CurrentState != GameStateManager.State.Playing)
            return;
        
        this.Move();
        this.Rotate();
    }

    private void LateUpdate()
    {
        this.UpdateAnimator();
    }
    
    protected void Move()
    {

        var moveSpeed = this.m_moveDirection * this.m_playerData.MovementSpeed;

        if (this.m_inputProcessor.IsBoosting)
        {
            moveSpeed *= this.m_playerData.RunSpeedScale;
        }
        
        this.m_moveDirection = new Vector3(this.m_inputProcessor.Movement.x, Physics.gravity.y, this.m_inputProcessor.Movement.y);
        this.m_characterController.Move(moveSpeed * Time.deltaTime);
    }
        
    private void Rotate()
    {
        var targetDir = this.m_moveDirection;
        targetDir.y = 0f;

        if (targetDir == Vector3.zero)
            targetDir = this.transform.forward;
    
        this.RotateTowards(targetDir);
    }

    private void RotateTowards(Vector3 dir)
    {
        var lookRotation = Quaternion.LookRotation(dir.normalized);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, this.m_playerData.RotationSpeed * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        this.m_animator.SetBool(s_isWalkingHash, this.m_moveDirection != Physics.gravity);
    }
}
