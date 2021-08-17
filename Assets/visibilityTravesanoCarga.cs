using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityTravesanoCarga : MonoBehaviour {

    public MeshRenderer m_rend;

    void Start () {
        Debug.Log (m_rend.enabled);
    }

    void Update () { }

    public void MakeVisible () {
        Debug.Log ("Medidor visible.");
        m_rend = GetComponent<MeshRenderer> ();
        m_rend.material.color = Color.green;
        //m_rend.enabled = true;
    }

    public void MakeInvisible () {
        Debug.Log ("Medidor invisible.");
        m_rend = GetComponent<MeshRenderer> ();
        m_rend.material.color = Color.grey;
        //m_rend.enabled = false;
    }
}