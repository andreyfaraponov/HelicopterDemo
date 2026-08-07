using UnityEngine;

namespace HelicopterDemo.Configs
{
    [CreateAssetMenu(menuName = "Configs/Roll", fileName = "RollConfig")]
    public class RollConfig : ScriptableObject
    {
        [field: SerializeField] public float TorquePower { get; private set; }
        [field: SerializeField] public float Damping { get; private set; }
        [field: SerializeField] public float MaxAngularSpeed { get; private set; }
        [field: SerializeField] public float InputDeadZone { get; private set; }
        [field: SerializeField] public float Direction { get; private set; }
    }
}