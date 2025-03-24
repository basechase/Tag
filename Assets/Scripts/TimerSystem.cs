using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TagSystem))]
public class TimerSystem : MonoBehaviour
{
    [SerializeField]
    private float _startingTime =30f;

    private float _timeRemaining;

    [SerializeField]
    private TextMeshProUGUI _timer;


    private TagSystem _tagSystem;


    private void Start()
    {
        _timeRemaining = _startingTime;
        _tagSystem = GetComponent<TagSystem>();

    }
    private void Update()
    {
        if (!_tagSystem.Tagged) return;
                                


        _timeRemaining -= Time.deltaTime;
        _timeRemaining = Mathf.Clamp(_timeRemaining, 0, _startingTime);
    }
}
