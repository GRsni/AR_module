using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityArrow : MonoBehaviour {

    public Vector3 direction;
    public float speed = 1f;
    public float Height = 0.5f;

    void Start () {
        gameObject.SetActive (false);
    }

    void Update () {
        Vector3 pos = transform.position;
        Debug.Log ("Direction: " + direction);
        float newVal = Mathf.Sin (Time.time * speed) * Height;
        Debug.Log ("newVal: " + newVal);
        Vector3 movement = direction * newVal;
        Debug.Log ("Arrow diff: " + movement.ToString ("F5"));
        transform.position = pos + movement;
    }

    public void MakeVisible () {
        Debug.Log ("Barra torsion visible.");
        gameObject.SetActive (true);

    }

    public void MakeInvisible () {
        Debug.Log ("Barra torsion invisible.");
        gameObject.SetActive (false);
    }
}