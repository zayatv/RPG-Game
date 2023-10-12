using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO PlayerData { get; private set; }

    [field: Header("Collisions")]
    [field: SerializeField] public PlayerCapsuleColliderUtility ColliderUtility { get; private set; }
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

    [field: Header("Cameras")]
    [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [field: Header("Stats")]
    [field: SerializeField] public PlayerStats Stats { get; private set; }

    [field: Header("Playable Characters")]
    [field: SerializeField] public PlayerCharacterDataSO CharacterData { get; private set; }

    [field: Header("Weapons")]
    [field: SerializeField] public WeaponSO CurrentEquippedWeapon { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }

    [field: Header("Items")]
    [field: SerializeField] public PlayerInventoryData InventoryData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; set; }
    public Transform WeaponParentTransform { get; set; }

    public Transform MainCameraTransform { get; private set; }

    public PlayerInput Input { get; private set; }

    public bool IsAttacking { get; set; }
    public bool CanAttack { get; set; }

    private PlayerMovementStateMachine movementStateMachine;
    private PlayerAttackingStateMachine attackingStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        WeaponParentTransform = FindChild.RecursiveFindChild(transform.GetChild(0), "Weapon");

        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
        
        CameraUtility.Initialize();
        AnimationData.Initialize();
        InventoryData.Initialize();
        Stats.Initialize();
        WeaponHandler.Initialize(this);

        MainCameraTransform = Camera.main.transform;

        IsAttacking = false;
        CanAttack = false;

        movementStateMachine = new PlayerMovementStateMachine(this);
        attackingStateMachine = new PlayerAttackingStateMachine(this);
    }

    private void OnValidate() 
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.IdlingState);

        attackingStateMachine.ChangeState(attackingStateMachine.AttackingIdleState);
    }

    private void OnTriggerEnter(Collider collider) 
    {
        movementStateMachine.OnTriggerEnter(collider);

        attackingStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider) 
    {
        movementStateMachine.OnTriggerExit(collider);

        attackingStateMachine.OnTriggerExit(collider);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();
        movementStateMachine.Update();

        attackingStateMachine.HandleInput();
        attackingStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();

        attackingStateMachine.PhysicsUpdate();
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        movementStateMachine.OnAnimationEnterEvent();

        attackingStateMachine.OnAnimationEnterEvent();
    }

    public void OnMovementStateAnimationExitEvent()
    {
        movementStateMachine.OnAnimationExitEvent();

        attackingStateMachine.OnAnimationExitEvent();
    }

    public void OnMovementStateAnimationTransitionEvent()
    {
        movementStateMachine.OnAnimationTransitionEvent();

        attackingStateMachine.OnAnimationTransitionEvent();
    }

    public void OnEnableWeaponCollider()
    {
        movementStateMachine.EnableWeaponCollider();

        attackingStateMachine.EnableWeaponCollider();
    }

    public void OnDisableWeaponCollider()
    {
        movementStateMachine.DisableWeaponCollider();

        attackingStateMachine.DisableWeaponCollider();
    }
}
