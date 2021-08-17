using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class receiveDataFromAndroid : MonoBehaviour {
    public Text TextBoxTextLandscape;
    public Text TextBoxTextPortrait;

    private bool hasExtra = false;
    public AndroidJavaObject extras;

    public stepsHandler stepsHandlerScriptLandscape;
    public stepsHandler stepsHandlerScriptPortrait;

    string WelcomeMessage = "Bienvenido a la aplicación de realidad aumentada. Pulsa el botón para comenzar.";
    string DataLoadError = "Error al leer los datos desde la aplicación Android. Por favor reinicia la aplicación.";

    private string Data = "";
    private int PracticaType = 0;
    private bool InLab = false;

    public AndroidJavaObject intent;

    public Canvas PortraitCanvas;
    public Canvas LandscapeCanvas;

    private Canvas correctCanvas;
    private Text correctTextBox;
    private stepsHandler correctStepsHandler;

    // Use this for initialization
    void Start () {

        getCorrectCanvas ();
        try {
            AndroidJavaClass UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
            intent = currentActivity.Call<AndroidJavaObject> ("getIntent");
            hasExtra = intent.Call<bool> ("hasExtra", "data");
            Debug.Log ("start");

            if (hasExtra) {
                correctTextBox.text = WelcomeMessage;
                Debug.Log ("has extra");
                extras = intent.Call<AndroidJavaObject> ("getExtras");

                Data = extras.Call<string> ("getString", "data");
                PracticaType = extras.Call<int> ("getInt", "type");
                InLab = extras.Call<bool> ("getBoolean", "place");
                Debug.Log ("TypeInt: " + PracticaType + ", data: " + Data);
                correctStepsHandler.initializeData (PracticaType, Data, InLab);

            } else {
                correctTextBox.text = DataLoadError;
                Debug.Log ("no extra");
                correctStepsHandler.initializeData (-1, Data, InLab);
            }
        } catch (Exception e) {
            Debug.Log ("Exception: " + e.Message);
            correctTextBox.text = DataLoadError;
            correctStepsHandler.initializeData (-1, Data, InLab);
        }
    }

    void getCorrectCanvas () {
        if (Screen.orientation == ScreenOrientation.Portrait) {
            correctCanvas = PortraitCanvas;
            correctStepsHandler = stepsHandlerScriptPortrait;
            LandscapeCanvas.enabled = false;
        } else {
            correctCanvas = LandscapeCanvas;
            correctStepsHandler = stepsHandlerScriptLandscape;
            PortraitCanvas.enabled = false;
        }

        correctTextBox = correctCanvas.transform.Find ("TextBox").GetComponentInChildren<Text> ();
        Debug.Log ("canvas: " + correctCanvas.name + ", textBox: " + correctCanvas.transform.Find ("TextBox").GetComponentInChildren<Text> ().name);
    }

    void FixedUpdate () {

        if (Application.platform == RuntimePlatform.Android) {
            if (Input.GetKey (KeyCode.Escape)) {
                Application.Quit ();
            }
        }
    }
}