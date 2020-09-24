﻿using System;
using AngeloExamples.UI;
using Character;
using Tools.Managers;
using UnityEngine;

namespace Managers
{
    public class GameManager : BaseGameManager
    {
        [SerializeField] private int m_scorePerBox = 100;
        [SerializeField] private TrackMouse m_aimCursor;

        private int m_currentScore = 0;

        protected override void Awake()
        {
            base.Awake();
            
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public static GameManager Manager()
        {
            return _instance as GameManager;
        }

        public static TrackMouse AimCursor
        {
            get
            {
                if (_instance != null)
                {
                    return Manager().m_aimCursor;
                }
                
                throw new Exception("PLEASE PUT GAME MANAGER IN THE SCENE");
            }
        }

        public static void UpdateCurrentScore()
        {
            var curManager = Manager();
            curManager.m_currentScore += curManager.m_scorePerBox;
            UIController.UpdateScore(curManager.m_currentScore);
        }
    }
}
