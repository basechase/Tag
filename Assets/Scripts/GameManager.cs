using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;
    [SerializeField]
    private GameObject _winTextBackground;


    public UnityEvent OnGameWin;

    private TimerSystem _player1Timer;
    private TimerSystem _player2Timer;
    private TagSystem _player1TagSytem;
    private TagSystem _player2TagSytem;
    private PlayerController _player1Controller;
    private PlayerController _player2Controller;

    private Rigidbody _player1RigidBody;
    private Rigidbody _player2RigidBody;

    private bool _gameWon = false;
    private void Start()
    {
        if (_player1)
        {

            if (!_player1.TryGetComponent(out _player1Timer))
                Debug.LogError("Could not get player1 Timer");

            if (!_player1.TryGetComponent(out _player1TagSytem))
                Debug.LogError("Could not get player1 Tagsystemr");

            if (!_player1.TryGetComponent(out _player1Controller))
                Debug.LogError("Could not get player1 Controller");

            if (!_player1.TryGetComponent(out _player1RigidBody))
                Debug.LogError("Could not get player1 Rigidbody");

            /*
            _player1Timer = _player1.GetComponent<TimerSystem>();
            _player1TagSytem = _player1.GetComponent<TagSystem>();
            _player1Controller = _player1.GetComponent<PlayerController>();
            */
             

        }
        else
            Debug.LogError("Gamemanager: player1 not assigned");

        if (_player2)
        {


            if (!_player2.TryGetComponent(out _player2Timer))
                Debug.LogError("Could not get player2 Timer");

            if (!_player2.TryGetComponent(out _player2TagSytem))
                Debug.LogError("Could not get player2 Tagsystemr");

            if (!_player2.TryGetComponent(out _player2Controller))
                Debug.LogError("Could not get player2 Controller");

            if (!_player1.TryGetComponent(out _player2RigidBody))
                Debug.LogError("Could not get player2 Rigidbody");

            /* 

        _player2Timer = _player2.GetComponent<TimerSystem>();
        _player2TagSytem = _player2.GetComponent<TagSystem>();
        _player2Controller = _player2.GetComponent<PlayerController>();
             */
        }
        else
            Debug.LogError("Gamemanager: player2 not assigned");
        if (!_winTextBackground)
            Debug.LogWarning("GMANGER: WIN TEXT BACKGROUND NOT ASSINGED BRO");
    }



    private void Update()
    {

        //if either timer is not assinged do nothing
        if (!(_player1Timer && _player2Timer))
            return;


        //check if either timer is finsiuhed
        //win game if so
        //
        // if game has already been won dont do a thing g 
        if (_gameWon)
            return;

        if (_player1Timer.TimeRemaining <= 0)
            Win("player 1 wins");

        if (_player2Timer.TimeRemaining <= 0)
            Win("player 2 wins");
    }
    private void Win(string winText)
    {
        //enabvle win screen UI text set text to wintext

        if(_winTextBackground)
        {
            _winTextBackground.SetActive(true);
            TextMeshProUGUI text = _winTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);

            if (text)
            {
                text.text = winText;
            }
        }


        //turn off player controllers and timer
        if (_player1Timer)
            _player1Timer.enabled = false;

        if (_player1TagSytem)
            _player1TagSytem.enabled = false;

        if (_player1Controller)
            _player1Controller.enabled = false;

        if (_player1RigidBody)
            _player1RigidBody.isKinematic = true;
            


        if (_player2Timer)
            _player2Timer.enabled = false;

        if (_player2TagSytem)
            _player2TagSytem.enabled = false;

        if (_player2Controller)
            _player2Controller.enabled = false;

        if (_player2RigidBody)
            _player2RigidBody.isKinematic = true;


        _gameWon = true;
        OnGameWin.Invoke();
        
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

}
