using System;
using System.Collections.Generic;
using HelicopterDemo.Configs;
using HelicopterDemo.Forces;
using HelicopterDemo.HelicopterMono;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo
{
    public interface IHelicopterView
    {
        void Initialize(MovementModel movementModel, HelicopterConfig helicopterConfig);
    }

    public class HelicopterView : MonoBehaviour, IHelicopterView
    {
        [SerializeField] private Rigidbody mainRigidbody;
        [SerializeField] private GroundDetector groundDetector;
        [SerializeField] private HelicopterVisual visual;

        private HelicopterConfig _helicopterConfig;

        private readonly List<IForce> _forces = new();
        private LiftForce _liftForce;
        private LandingHelper _landingHelper;

        private MovementModel _movementModel;
        private ForceModel _forceModel;
        private bool _grounded;

        private void Start()
        {
            groundDetector.GroundDetectedEvent += GroundDetected;
            _grounded = true;
        }

        private void FixedUpdate()
        {
            _forceModel.Clear();

            if (!_grounded)
            {
                for (int i = 0; i < _forces.Count; i++)
                {
                    _forces[i].ApplyForce(_forceModel, _movementModel);
                }
            }
            else
            {
                _liftForce.ApplyForce(_forceModel, _movementModel);
                _landingHelper.ApplyForce(_forceModel);
            }

            if (_forceModel.AllZero)
            {
                return;
            }

            mainRigidbody.AddForce(_forceModel.Force, ForceMode.Force);
            mainRigidbody.AddRelativeTorque(_forceModel.Torque, ForceMode.Force);
        }

        public void Initialize(MovementModel movementModel, HelicopterConfig helicopterConfig)
        {
            _movementModel = movementModel;
            _helicopterConfig = helicopterConfig;
            _forceModel = new ForceModel();
            visual.Initialize(movementModel);
            PrepareForces();
        }

        private void PrepareForces()
        {
            _liftForce = new LiftForce(mainRigidbody, transform, _helicopterConfig.LiftConfig);
            _landingHelper = new LandingHelper(mainRigidbody, transform, _helicopterConfig.LandingHelperConfig);

            _forces.Add(_liftForce);
            _forces.Add(new RollForce(mainRigidbody, transform, _helicopterConfig.RollConfig));
            _forces.Add(new YawForce(mainRigidbody, transform, _helicopterConfig.YawConfig));
            _forces.Add(new PitchForce(mainRigidbody, transform, _helicopterConfig.PitchConfig));
        }

        private void GroundDetected(bool detected)
        {
            if (detected)
            {
                _grounded = true;
            }
            else
            {
                _grounded = false;
            }
        }
    }
}