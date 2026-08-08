using System;
using System.Collections.Generic;
using HelicopterDemo.Configs;
using HelicopterDemo.Forces;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo.HelicopterMono
{
    public interface IHelicopterView
    {
        event Action CrashedEvent;
        void Initialize(MovementModel movementModel, HelicopterConfig helicopterConfig);
        void ResetAll();
        Rigidbody MainRigidbody { get; }
    }

    public class HelicopterView : MonoBehaviour, IHelicopterView
    {
        public event Action CrashedEvent;
        
        [SerializeField] private Rigidbody mainRigidbody;
        [SerializeField] private GroundDetector groundDetector;
        [SerializeField] private GroundDetector crashGroundDetector;
        [SerializeField] private HelicopterVisual visual;
        
        public Rigidbody MainRigidbody => mainRigidbody;

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
            crashGroundDetector.GroundDetectedEvent += CrashDetected;
            _grounded = true;
        }

        private void FixedUpdate()
        {
            if (_forceModel == null)
            {
                return;
            }
            
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
            PrepareForces();
            _forceModel = new ForceModel();
            visual.Initialize(movementModel);
        }

        public void ResetAll()
        {
            _forces.Clear();
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            mainRigidbody.linearVelocity = Vector3.zero;
            mainRigidbody.angularVelocity = Vector3.zero;
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

        private void CrashDetected(bool crashed)
        {
            if (!crashed)
            {
                return;
            }
            
            CrashedEvent?.Invoke();
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