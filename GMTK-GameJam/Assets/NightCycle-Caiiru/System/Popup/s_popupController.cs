using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class s_popupController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Header("Animation Settings")]
    [Tooltip("In seconds"),SerializeField] private float _durationAnimation = 0.5f;

    [SerializeField] private float _moveY = 10f;
    
    // Components
    private LTDescr _tweenAnimation;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Create(string textValue)
    { 
        _text = GetComponent<TextMeshProUGUI>();
        _tweenAnimation = this.transform.gameObject.LeanMoveY(transform.position.y + _moveY, _durationAnimation);
        _text.text = textValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (_tweenAnimation == null) return;
        if (!gameObject.activeInHierarchy) return;
        if (_tweenAnimation.passed >= _tweenAnimation.time - 0.1f)
        {
            this.gameObject.SetActive(false);
            LeanTween.cancel(_tweenAnimation.id);
        }
    }
}
