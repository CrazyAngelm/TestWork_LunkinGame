using System;
using System.Collections;
using System.Collections.Generic;
using Signal;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float time = 1f;
        
        public bool IsActive { get; private set; }
        public List<Color> skills { get; private set; }

        private Vector3 _startPos;
        private SignalObject _targetSignal;
        private SignalHandler _signalHandler;


        private IEnumerator _moveCoroutine;

        private void Awake()
        {
            skills ??= new List<Color>();
            _signalHandler = GameObject.FindObjectOfType<SignalHandler>();
            
            _startPos = transform.position;
        }

        private void Start()
        {
            InitSkills();
        }
        
        public void SetSignal(SignalObject signalObject)
        {
            _targetSignal = signalObject;

            _moveCoroutine = MoveCoroutine(_startPos, _targetSignal.placeToPlayer.position, true);
            StartCoroutine(_moveCoroutine);
        }

        private void InitSkills()
        {
            foreach (Transform child in transform)
            {
                var skillObject = child.GetComponent<SkillObject>();
                skills.Add(skillObject.ColorSkill);
            }
        }

        private IEnumerator MoveCoroutine(Vector3 startPos, Vector3 finalPos, bool toFix)
        {
            IsActive = true;
            
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            IsActive = false;
            if (toFix)
            {
                FixSignal();
                MoveBack();
            }
        }

        private void FixSignal()
        {
            _targetSignal.DisableSignal();
            _targetSignal = null;
            _signalHandler.StartNewSignal();
        }

        private void MoveBack()
        {
            _moveCoroutine = MoveCoroutine(transform.position, _startPos, false);
            StartCoroutine(_moveCoroutine);
        }
    }
}