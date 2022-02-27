using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

namespace Signal
{
    public class SignalHandler : MonoBehaviour
    {
        [SerializeField] private float timeRepeatSecond = 5f;
        [SerializeField] private List<PlayerController> players;
        
        private List<SignalObject> _signalObjects;
        private int CountEnableSignals
        {
            get { return _signalObjects.Count(x => x.IsSignal); }
        }
        
        private IEnumerator _signalCoroutine;

        private void Start()
        {
            _signalObjects ??= new List<SignalObject>();

            foreach (Transform child in transform)
            {
                var signalObject = child.GetComponent<SignalObject>();
                _signalObjects.Add(signalObject);
            }

            StartNewSignal();
        }

        public void StartNewSignal()
        {
            if (CountEnableSignals > 0) return;
            
            _signalCoroutine = EnableRandomSignalsCoroutine();
            StartCoroutine(_signalCoroutine);
        }

        private IEnumerator EnableRandomSignalsCoroutine()
        {
            yield return new WaitForSeconds(timeRepeatSecond);
            EnableRandomSignals();
        }

        private void EnableRandomSignals()
        {
            var rand = Random.Range(0, _signalObjects.Count);
            var signalObject = _signalObjects[rand];
            var color = signalObject.EnableSignal();

            var playerToFixSignal = players.First(x => !x.IsActive && x.skills.Contains(color));
            playerToFixSignal.SetSignal(signalObject);
        }
    }
}
