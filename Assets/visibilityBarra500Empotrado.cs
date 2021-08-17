using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityBarra500Empotrado : MonoBehaviour {
    // Start is called before the first frame update

    void Start () {
        gameObject.SetActive (false);
    }

    void Update () { }

    public void MakeVisible () {
        Debug.Log ("Barra empotrado visible.");
        gameObject.SetActive (true);

    }

    public void MakeInvisible () {
        Debug.Log ("Barra empotrado invisible.");
        gameObject.SetActive (false);
    }
}