using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_popupManager : MonoBehaviour
{
    [SerializeField] private int _popupCache;
    [SerializeField] private GameObject[] _popupsTexts;
    [SerializeField] private GameObject _popupPrefab;
    
    void Start()
    {
        if (_popupPrefab == null)
        {
            Debug.LogWarning("Popup Prefab is null");
            return;
        } 
        _popupsTexts = new GameObject[_popupCache];
        for (int i = 0; i < _popupCache; i++)
        {
            var _instance = Instantiate(_popupPrefab);
            _instance.gameObject.SetActive(false);
            _instance.transform.SetParent(this.transform.GetChild(0)); // World canvas  
            _popupsTexts[i] = _instance;
        }    
        
    }

    public GameObject GetPopoup()
    {
        foreach (var _instance in _popupsTexts)
        {
            if (!_instance.gameObject.activeInHierarchy)
                return _instance;
        }

        return null;
    }
    
    #region Singleton
    public static s_popupManager Instance;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Debug.LogWarning("instance already exist");
            Destroy(this.gameObject);
        }

        Instance = this;
    }
    #endregion
    
}
