using UnityEngine;

namespace HelicopterDemo.Configs
{
    [CreateAssetMenu(menuName = "Configs/Pitch", fileName = "PitchConfig")]
    public class PitchConfig : ScriptableObject
    {
        [field: SerializeField] public float TorquePower { get; private set; }
        [field: SerializeField] public float DampingPower { get; private set; }
        [field: SerializeField] public float MaxAngularSpeed { get; private set; }
        [field: SerializeField] public float InputDeadZone { get; private set; }
        [field: SerializeField] public float Direction { get; private set; }
    }
}