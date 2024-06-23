using UnityEngine;

namespace RPG.Gameplay
{
    public struct MovementInputs
    {
        public Vector2 MoveInput { get; set; }
        public bool Jump { get; set; }
        public Quaternion CameraRotation { get; set; }
        public bool Walk {  get; set; }
        public bool Sprint { get; set; }
        public bool Dash {  get; set; }
    }
}
