using HelicopterDemo.Models;

namespace HelicopterDemo.Forces
{
    public interface IForce
    {
        void ApplyForce(ForceModel forceModel, MovementModel movementModel);
    }
}