using UnityEngine;

namespace HelicopterDemo.Configs
{
    
    [CreateAssetMenu(menuName = "Configs/LandingHelperConfig", fileName = "LandingHelperConfig")]
    public class LandingHelperConfig : ScriptableObject
    {
        [field: SerializeField] public float UpRightPower { get; private set; }
        [field: SerializeField] public float DampingPower { get; private set; }
    }
}