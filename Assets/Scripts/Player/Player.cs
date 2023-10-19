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

    public PlayerMovementStateMachine MovementStateMachine { get; private set; }
    public PlayerAttackingStateMachine AttackingStateMachine { get; private set; }

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

        MovementStateMachine = new PlayerMovementStateMachine(this);
        AttackingStateMachine = new PlayerAttackingStateMachine(this);
    }

    private void OnValidate() 
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimensions();
    }

    private void Start()
    {
        MovementStateMachine.ChangeState(MovementStateMachine.IdlingState);

        AttackingStateMachine.ChangeState(AttackingStateMachine.AttackingIdleState);
    }

    private void OnTriggerEnter(Collider collider) 
    {
        MovementStateMachine.OnTriggerEnter(collider);

        AttackingStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider) 
    {
        MovementStateMachine.OnTriggerExit(collider);

        AttackingStateMachine.OnTriggerExit(collider);
    }

    private void Update()
    {
        MovementStateMachine.HandleInput();
        MovementStateMachine.Update();

        AttackingStateMachine.HandleInput();
        AttackingStateMachine.Update();
    }

    private void FixedUpdate()
    {
        MovementStateMachine.PhysicsUpdate();

        AttackingStateMachine.PhysicsUpdate();
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        MovementStateMachine.OnAnimationEnterEvent();

        AttackingStateMachine.OnAnimationEnterEvent();
    }

    public void OnMovementStateAnimationExitEvent()
    {
        MovementStateMachine.OnAnimationExitEvent();

        AttackingStateMachine.OnAnimationExitEvent();
    }

    public void OnMovementStateAnimationTransitionEvent()
    {
        MovementStateMachine.OnAnimationTransitionEvent();

        AttackingStateMachine.OnAnimationTransitionEvent();
    }

    public void OnEnableWeaponCollider()
    {
        MovementStateMachine.EnableWeaponCollider();

        AttackingStateMachine.EnableWeaponCollider();
    }

    public void OnDisableWeaponCollider()
    {
        MovementStateMachine.DisableWeaponCollider();

        AttackingStateMachine.DisableWeaponCollider();
    }
}
