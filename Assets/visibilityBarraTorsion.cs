using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityBarraTorsion : MonoBehaviour {
    void Start () {
        gameObject.SetActive (false);
    }

    void Update () { }

    public void MakeVisible () {
        Debug.Log ("Barra torsion visible.");
        gameObject.SetActive (true);

    }

    public void MakeInvisible () {
        Debug.Log ("Barra torsion invisible.");
        gameObject.SetActive (false);
    }
}