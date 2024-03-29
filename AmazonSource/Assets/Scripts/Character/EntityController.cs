﻿using System.Timers;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class EntityController : MonoBehaviour
    {
        [Header("Character Values")]
        //current speed
        [Tooltip("The speed at to which the character moves")]
        [SerializeField] private float m_speed = 0;
        [SerializeField] private CustomTimer m_invulTimer;
        [SerializeField] private Range m_screenRange = null;
        
        [SerializeField] private int m_health;
        [Header("Collision Layers")] 
        [SerializeField] private int m_normalLayer;
        [SerializeField] private int m_invulLayer;

        [Header("Components")]
        [SerializeField] private Rigidbody2D m_rigidbody2D = null;
        
        //current direction
        private float m_direction = 1;
        private bool m_invul;

        // Start is called before the first frame update
        void Start()
        {
            SetSpeed();
        }

        // Update is called once per frame
        void Update()
        {
            ChangeDirections();
            Invulnerable();
        }

        private void SetSpeed()
        {
            //sets speed while keeping y velocity 
            m_rigidbody2D.velocity = new Vector2(m_speed*m_direction, m_rigidbody2D.velocity.y);
        }
        
        private void ChangeDirections()
        {
            //finds the position of the object the script is attached to in viewport space 
            var viewportPosition = CameraTools.GetViewportPosition(transform.position);
            //Debug.Log(viewportPosition);
            //when avatar hits edge of the screen, will make it change directions
            if (viewportPosition.x > m_screenRange.MAX && m_direction > 0)
            {
                m_direction = -1;
                SetSpeed();
            }
            if (viewportPosition.x < m_screenRange.MIN && m_direction < 0)
            {
                m_direction = 1;
                SetSpeed();
            }
        }

        private void Invulnerable()
        {
            if (!m_invul) return;
            if (!m_invulTimer.Tick(Time.deltaTime)) return;

            m_invul = false;
            gameObject.layer = m_normalLayer;
        }

        public void PlayerHit()
        {
            m_direction *= -1;
            SetSpeed();
            m_invul = true;
            gameObject.layer = m_invulLayer;
        }
    }
}
