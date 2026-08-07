using UnityEngine;

namespace HelicopterDemo.Configs
{
    [CreateAssetMenu(menuName = "Configs/HelicopterConfig", fileName = "HelicopterConfig")]
    public class HelicopterConfig : ScriptableObject
    {
        [field: SerializeField] public LiftConfig LiftConfig { get; private set; }
        [field: SerializeField] public PitchConfig PitchConfig { get; private set; }
        [field: SerializeField] public YawConfig YawConfig { get; private set; }
        [field: SerializeField] public RollConfig RollConfig { get; private set; }
        [field: SerializeField] public LandingHelperConfig LandingHelperConfig { get; private set; }
        
    }
}