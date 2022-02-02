using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IGS520b.starter.SampleGame
{    
    public class GameManager : MonoBehaviour
    {
        [Tooltip("Distance to move before game starts.")]
        [Range(0.0f, 5f)]
        public float startDistance;
        [Tooltip("Game time limit in seconds.")]
        public int timeLimit = 120;

        [Tooltip("Points text.")]
        public TextMeshProUGUI pointsText;
        [Tooltip("Time text.")]
        public TextMeshProUGUI timeText;

        private enum GameState
            {
                initiated,
                stopped,
                started
            };
        
        private Transform _characterTransform;
        private GameState _gameState;
        private Vector3 _startPosition;
        private float _gameStartTime;
        private float _timeRemaining;
        private float _points;
        private float _maxPoints;

        // Start is called before the first frame update
        void Start()
        {
            CharacterController[] characterControllers = FindObjectsOfType<CharacterController>();
            if (characterControllers.Length != 1)
            {
                Debug.LogError("Expecting only one `CharacterController`");
            }

            _characterTransform = characterControllers[0].transform;
            timeText.text = "Move to start";
            pointsText.text = "";
        }

        void OnPointScored(GamePoint gamePoint)
        {
            _points += gamePoint.points;

           

             //GameObject.Find("Sphere").GetComponent<Renderer>().material.color = new Color(0, 204, 102);


            
                        
             Destroy(gamePoint.gameObject);

            //GameObject.Find("Sphere").GetComponent<Renderer>().material.color = new Color(0, 204, 102);

            // foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
            // {
            //   if (gameObj.name == "Sphere")
            //  {
            //     gameObj.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
            // }
            // }

            // GameObject.Find("obstacle1").GetComponent<Renderer>().material.color = new Color(0, 204, 102);
            //GameObject.Find("obstacle2").GetComponent<Renderer>().material.color = new Color(0, 0, 0);

            //GameObject.Find("Sphere").GetComponent<Renderer>().material.color = new Color(0, 0, 0)(gamePoint.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            switch (_gameState)
            {
                // The timer/game-level is not started until the character moves by startDistance
                case GameState.initiated:
                    if ((_startPosition - _characterTransform.transform.position).magnitude > startDistance)
                    {
                        Debug.Log($"Game Started");
                        _gameState = GameState.started;
                        _gameStartTime = Time.fixedTime;
                        _timeRemaining = timeLimit;

                        foreach (GamePoint gamePoint in FindObjectsOfType<GamePoint>())
                        {
                            gamePoint.OnTriggerEnterAction += OnPointScored;
                            _maxPoints += gamePoint.points;
                        }
                    }
                    break;
                case GameState.started:
                    _timeRemaining = timeLimit - (Time.fixedTime - _gameStartTime);

                    timeText.text = $"Time remaining: {Mathf.FloorToInt(_timeRemaining / 60)}:{_timeRemaining % 60.0f}";
                    pointsText.text = $"Points: {_points}/{_maxPoints}";
                    
                    if (_timeRemaining <= 0 || _points == _maxPoints)
                    {
                        _gameState = GameState.stopped;
                        if (_timeRemaining <= 0)
                        {
                            timeText.text += "\nTime out";
                        }
                        else
                        {
                            timeText.text += "\nMax Score Reached";
                        }
                        
                        foreach (GamePoint gamePoint in FindObjectsOfType<GamePoint>())
                        {
                            gamePoint.OnTriggerEnterAction -= OnPointScored;
                        }
                    }

                    break;
                case GameState.stopped:
                    break;
            }
        }
    }
}
