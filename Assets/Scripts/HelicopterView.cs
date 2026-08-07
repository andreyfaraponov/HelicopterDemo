using System.Collections.Generic;
using HelicopterDemo.Configs;
using HelicopterDemo.Forces;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo
{
    public interface IHelicopterView
    {
        void Initialize(MovementModel movementModel);
    }

    public class HelicopterView : MonoBehaviour, IHelicopterView
    {
        [SerializeField] private Rigidbody mainRigidbody;

        [Header("Forces configs")] [SerializeField]
        private LiftConfig liftConfig;

        [SerializeField] private RollConfig rollConfig;
        [SerializeField] private YawConfig yawConfig;
        [SerializeField] private PitchConfig pitchConfig;

        [Header("View")] [SerializeField] private HelicopterVisual visual;

        private readonly List<IForce> _forces = new();

        private ForceModel _forceModel;
        private MovementModel _movementModel;

        private void FixedUpdate()
        {
            _forceModel.Clear();
            for (int i = 0; i < _forces.Count; i++)
            {
                _forces[i].ApplyForce(_forceModel, _movementModel);
            }

            if (_forceModel.AllZero)
            {
                return;
            }

            mainRigidbody.AddForce(_forceModel.Force, ForceMode.Force);
            mainRigidbody.AddRelativeTorque(_forceModel.Torque, ForceMode.Force);
        }

        public void Initialize(MovementModel movementModel)
        {
            _movementModel = movementModel;
            _forceModel = new ForceModel();
            visual.Initialize(movementModel);
            PrepareForces();
        }

        private void PrepareForces()
        {
            _forces.Add(new LiftForce(mainRigidbody, transform, liftConfig));
            _forces.Add(new RollForce(mainRigidbody, transform, rollConfig));
            _forces.Add(new YawForce(mainRigidbody, transform, yawConfig));
            _forces.Add(new PitchForce(mainRigidbody, transform, pitchConfig));
        }
    }
}