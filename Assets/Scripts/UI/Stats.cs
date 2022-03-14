using UnityEngine;
using UnityEngine.UI;
using PlayerScripts;

namespace Stats
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private Player _player;
    
        [SerializeField] private Text _stepsCountText;
        [SerializeField] private Text _speedText;

        private int _steps;
        private float _speed;


        private void OnEnable()
        {
            _player.MakeStep += IncrementSteps;
        }

        private void OnDisable()
        {
            _player.MakeStep -= IncrementSteps;
        }

        private void Update()
        {
            _speed= _player.GetSpeed();
            DisplayStats();
        }


        private void IncrementSteps()
        {
            _steps++;
        }

        private void DisplayStats()
        {
            _stepsCountText.text = _steps.ToString();
            _speedText.text = _speed.ToString();
        }
        
        
    }
}
