using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityConectorAmpli : MonoBehaviour {
    void Start () {
        gameObject.SetActive (false);
    }

    void Update () { }

    public void MakeVisible () {
        Debug.Log ("Conector ampli visible.");
        gameObject.SetActive (true);

    }

    public void MakeInvisible () {
        Debug.Log ("Conector ampli invisible.");
        gameObject.SetActive (false);
    }
}