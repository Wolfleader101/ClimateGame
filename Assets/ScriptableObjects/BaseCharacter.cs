using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace ScriptableObjects.Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "Character")]
    public class BaseCharacter : ScriptableObject
    {
        [HorizontalGroup("Split", .5f, Title = "Stats")] 
        
        [SerializeField, BoxGroup("Split/Health"), LabelWidth(100f)]
        private float startingHealth = 100f;
        
        [SerializeField, BoxGroup("Split/Health"), LabelWidth(100f)]
        private float maxHealth = 100f;

        [SerializeField, BoxGroup("Split/Stamina"), LabelWidth(100f)]
        private float startingStamina = 100f;
        
        [SerializeField, BoxGroup("Split/Stamina"), LabelWidth(100f)]
        private float maxStamina = 100f;
        
        
        [HorizontalGroup("Movement", 1f, Title = "Movement")]
        [SerializeField, BoxGroup("Movement/1", false)]
        private float movementSpeed = 10f;
        
        [SerializeField, BoxGroup("Movement/1")]
        private float jumpHeight = 2f;
        
        [SerializeField, BoxGroup("Movement/1")]
        private float gravityScaleMultiplier = 1.5f;
        
        public float StartingHealth => startingHealth;
        public float MaxHealth => maxHealth;
        public float StartingStamina => startingStamina;
        public float MaxStamina => maxStamina;
        public float MovementSpeed => movementSpeed;
        public float JumpHeight => jumpHeight;
        public float GravityScaleMultiplier => gravityScaleMultiplier;
    }
}