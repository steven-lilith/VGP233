using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private int _value = 10;
    public int Value { get { return _value; } set { _value = value; } }

    public int Collect()
    {
        this.gameObject.SetActive(false);
        return Value;
    }
}
