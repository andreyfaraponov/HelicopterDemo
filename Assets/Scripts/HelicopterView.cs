using UnityEngine;

namespace HelicopterDemo
{
    public interface IHelicopterView
    {
        void SetMovementModel(MovementModel movementModel);
    }

    public class HelicopterView : MonoBehaviour, IHelicopterView
    {
        public void Initialize()
        {
        }

        public void SetMovementModel(MovementModel movementModel)
        {
            throw new System.NotImplementedException();
        }
    }
}