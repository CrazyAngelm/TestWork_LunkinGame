using UnityEngine;
using Random = UnityEngine.Random;

namespace Signal
{
    public class SignalObject : MonoBehaviour
    {
        [SerializeField] public Transform placeToPlayer;
        
        public bool IsSignal { get; private set; }

        private Renderer _renderer;
        
        private ColorHandler _colorHandler;
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        private void Awake()
        {
            _colorHandler = GameObject.FindObjectOfType<ColorHandler>();
            _renderer = gameObject.GetComponent<Renderer>();
            
            DisableSignal();
        }

        public Color EnableSignal()
        {
            IsSignal = true;
            return SetRandomColor();
        }

        public void DisableSignal()
        {
            IsSignal = false;
            _renderer.material.SetColor(Color1, _colorHandler.disableColor);
        }

        private Color SetRandomColor()
        {
            var colors = _colorHandler.colors;
            var randColor = colors[Random.Range(0, colors.Count)];
            
            _renderer.material.SetColor(Color1, randColor);
            return randColor;
        }
    }
}