using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPos : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private TMP_Text _text;


    private void FixedUpdate()
    {
        _text.text = (_gameObject.transform.position).ToString();
    }

}
