using UnityEngine;

namespace HelicopterDemo.Configs
{
    [CreateAssetMenu(menuName = "Configs/Lift", fileName = "LiftConfig")]
    public class LiftConfig : ScriptableObject
    {
        [field: SerializeField] public float ForcePower { get; private set; }
        [field: SerializeField] public float DampingPower { get; private set; }
        [field: SerializeField] public float InputDeadZone { get; private set; }
        [field: SerializeField] public float MinMultiplier { get; private set; }
        [field: SerializeField] public float MaxMultiplier { get; private set; }
    }
}