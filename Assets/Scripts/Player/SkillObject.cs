using System;
using UnityEngine;

namespace Player
{
    public class SkillObject : MonoBehaviour
    {
        [SerializeField] public ESkill skill;
        public Color ColorSkill { get; private set; }

        private ColorHandler _colorHandler;
        
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        
        private void Start()
        {
            _colorHandler = GameObject.FindObjectOfType<ColorHandler>();
            
            var renderer = GetComponent<Renderer>();
            ColorSkill = _colorHandler.colors[(int) skill];
            renderer.material.SetColor(Color1, ColorSkill);
        }
    }
}