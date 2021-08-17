using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityRelojMedicion : MonoBehaviour {
    void Start () {
        gameObject.SetActive (false);
    }

    void Update () { }

    public void MakeVisible () {
        Debug.Log ("Reloj medicion visible.");
        gameObject.SetActive (true);

    }

    public void MakeInvisible () {
        Debug.Log ("Reloj medicion invisible.");
        gameObject.SetActive (false);
    }
}