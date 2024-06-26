using Animancer;
using KinematicCharacterController;
using UnityEngine;

namespace RPG.Gameplay
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] protected RuntimeAnimatorController defaultController;

        protected KinematicCharacterMotor motor;
        protected CharacterModel currentCharacter;
        protected RuntimeAnimatorController currentController;

        public CharacterModel CharacterModel => currentCharacter;
        public HybridAnimancerComponent CurrentAnimator => currentCharacter.Animator;
        public RuntimeAnimatorController CurrentController => currentController;
        public KinematicCharacterMotor Motor => motor;

        protected virtual void Awake()
        {
            motor = GetComponent<KinematicCharacterMotor>();

            var startingCharacter = GetComponentInChildren<CharacterModel>();

            if (startingCharacter != null)
                UpdateCharacter(startingCharacter, false);
        }

        /// <summary>
        /// Change the character model with a different one. Keeps the current animator running.
        /// </summary>
        /// <param name="isPrefab">Whether the model provided is a prefab or already instantiated.</param>
        public virtual void UpdateCharacter(CharacterModel character, bool isPrefab = true)
        {
            if (currentCharacter != null)
                Destroy(currentCharacter.gameObject);

            currentCharacter = isPrefab ? Instantiate(character) : character;
            currentCharacter.transform.SetParent(transform);
            currentCharacter.transform.localPosition = Vector3.zero;
            currentCharacter.transform.localRotation = Quaternion.identity;

            UpdateAnimatorController(currentController);
        }

        /// <summary>
        /// Switch out to a new animator, usually when switching weapons.
        /// Persists even when the character is changed.
        /// </summary>
        public virtual void UpdateAnimatorController(RuntimeAnimatorController controller)
        {
            if (controller == null)
                currentController = defaultController;
            else
                currentController = controller;
            
            CurrentAnimator.runtimeAnimatorController = currentController;
            CurrentAnimator.Controller = currentController;
            CurrentAnimator.PlayController();
        }
    }
}
