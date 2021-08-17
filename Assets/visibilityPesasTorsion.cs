using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityPesasTorsion : MonoBehaviour {
    void Start () {
        gameObject.SetActive (false);
    }

    void Update () { }

    public void MakeVisible () {
        Debug.Log ("Pesas torsion visible.");
        gameObject.SetActive (true);

    }

    public void MakeInvisible () {
        Debug.Log ("Pesas torsion invisible.");
        gameObject.SetActive (false);
    }
}