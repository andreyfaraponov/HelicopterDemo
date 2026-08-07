using Unity.Cinemachine;
using UnityEngine;

namespace HelicopterDemo.Configs
{
    [CreateAssetMenu(menuName = "Configs/Yaw", fileName = "YawConfig")]
    public class YawConfig: ScriptableObject
    {
        [field: SerializeField] public float TorquePower { get; private set; }
        [field: SerializeField] public float DampingPower { get; private set; }
        [field: SerializeField] public float MaxAngularSpeed { get; private set; }
        [field: SerializeField] public float InputDeadZone { get; private set; }
        [field: SerializeField] public float Direction { get; private set; }
    }
}