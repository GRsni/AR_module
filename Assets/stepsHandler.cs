using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stepsHandler : MonoBehaviour {
    public enum Type {
        TORSION,
        PANDEO,
        FLEXION,
        TRACCION,
        ERROR
    }

    string[] TorsionLines = {
        "1: Se monta la barra de torsión en el marco. Al hacerlo, se coloca el extremo de sujección de la barra en la clavija superior del soporte de la izquierda...",
        "... y el extremo suelto de la barra se apoya sobre el soporte del lado opuesto. Se observa que la barra se encuentra en posición horizontal.",
        "2: Para conectar el amplificador a la barra, conecta el extremo más grueso del cable de conexión al punto de conexión en la barra.",
        "3: Conecta el otro extremo al puerto del instrumento de medición(amplificador).",
        "4: A continuación, enciende el instrumento de medición.",
        "5: Pon a cero la indicación del display por medio del ajustador de offset.",
        "6: Suspender las pesas del brazo de palanca generando el momento de torsión. Anotar la indicación del display.",
        "Enhorabuena, has completado el experimento de torsión. Ahora podras realizar el cálculo del momento de torsión teórico para compararlo con el experimental."
    };

    string[] PandeoLines = {
        "1: Se colocan en el pórtico las entalladuras en V, y se fija con los tornillos de sujección.",
        "2: Se coloca la barra en las entalladuras/soporte del pórtico.",
        "3: El travesaño de carga debe estar fijado a las columnas guía, de manera que aún queden aprox. 5 mm de espacio hasta la pieza de presión superior.",
        "4: Alinear la barra de prueba de manera que su sentido de pandeo sea hacia una de las columnas guía. Aquí, las cuñas deben encontrarse en posición transversal al travesaño de carga.",
        "5: Pretensar la barra de prueba con un peso leve y no mensurable.",
        "6: Alinear el reloj de medición con las abrazaderas en la mitad vertical de la barra de pruebas. El reloj de medición debe estar emplazado en ángulo recto, en relación al sentido de pandeo.",
        "7: Pretensar el reloj de medición con el soporte regulable a una desviación de 10 mm.",
        "8: Cargar lentamente la barra de prueba con la tuerca de carga.",
        "9: Leer la desviación y anotarla junto con la fuerza aplicada cada 0,25 mm hasta llegar a 1 mm.",
        "10: A partir de 1 mm de desviación, es suficiente con anotar la desviación y la fuerza cada 0,5 mm.",
        "¡Atención! No pasar de una desviación total de 6 mm, se corre peligro de deformación plástica y daño de la barra de prueba.",
        "11: El experimento puede finalizar cuando la fuerza ya no se modifique, a pesar de que se tenga más carga.",
        "12: Para finalizar la práctica, endereza lentamente la barra.",
        "Enhorabuena, has completado el experimento de pandeo. Ahora podrás realizar el cálculo de la carga de pandeo teórica para compararla con la experimental."
    };

    string[] FlexionLines = { "Flexion 1", "Flexion 2" };

    string[] TraccionLines = { "Traccion 1", "Traccion 2" };

    string DataLoadError = "Error al leer los datos desde la aplicación Android. Por favor reinicia la aplicación.";

    //handlers torsion en laboratorio
    public visibilityBarraTorsion vBTorsionLabScript;
    public visibilityConectorAmpli vConecAmpLabScript;
    public visibilityConectorAmpliBarra vConecAmpliBarraLabScript;
    public visibilityPesasTorsion vPesasTLabScript;
    public visibilityArrow vFlechaTorsionLabEncender;
    public visibilityArrow vFlechaTorsionLabPantalla;

    //handlers torsion fuera
    public visibilityBarraTorsion vBTorsionFueraScript;
    public visibilityConectorAmpli vConecAmpFueraScript;
    public visibilityConectorAmpliBarra vConecAmpliBarraFueraScript;
    public visibilityPesasTorsion vPesasTFueraScript;

    //handlers pandeo en laboratorio
    public visibilityBarra500Empotrado vB500EmLabScript;
    public visibilityBarra500Articulado vB500ArtLabScript;
    public visibilityArrow vFlechaPandeoLabTuerca;
    public visibilityArrow vFlechaPandeoLabReloj;

    //handlers pandeo fuera
    public visibilityBarra500Empotrado vB500EmFueraScript;
    public visibilityBarra500Articulado vB500ArtFueraScript;
    public visibilityTravesanoCarga vTravCargaFueraScript;
    public visibilityTuercaCargaPandeo vTuercaPandeoScript;
    public visibilityRelojMedicion vRelojMedFueraScript;

    public Text textShowed = null;
    public Button ExitButton;

    Type type = Type.TORSION;
    int stepsCounter = -1;
    string data = "";
    bool inLab = false;
    bool finished = false;

    public void initializeData (int typeInt, string data, bool inLab) {
        Debug.Log ("TypeInt: " + typeInt + ", data: " + data);
        setType (typeInt);
        if (this.type == Type.ERROR) {
            finished = true;
        }
        this.data = data;
        this.inLab = inLab;
    }

    private void setType (int t) {
        switch (t) {
            case 1:
                this.type = Type.TORSION;
                break;
            case 2:
                this.type = Type.PANDEO;
                break;
            case 3:
                this.type = Type.FLEXION;
                break;
            case 4:
                this.type = Type.TRACCION;
                break;
            default:
                this.type = Type.ERROR;
                break;

        }
    }

    public void nextStep () {
        if (finished) {
            Application.Quit ();
        }
        updateCounter (true);
        updateText ();
        updateModels ();
    }

    public void prevStep () {
        updateCounter (false);
        updateText ();
        updateModels ();
    }

    private void updateCounter (bool increase) {
        switch (type) {
            case Type.TORSION:
                if (increase) {
                    if (stepsCounter < TorsionLines.Length - 1) {
                        stepsCounter++;
                    }
                } else {
                    if (stepsCounter > 0) {
                        stepsCounter--;
                    }
                }
                finished = (stepsCounter == TorsionLines.Length - 1);

                break;
            case Type.PANDEO:
                if (increase) {
                    if (stepsCounter < PandeoLines.Length - 1) {
                        stepsCounter++;
                    }
                } else {
                    if (stepsCounter > 0) {
                        stepsCounter--;
                    }
                }
                finished = (stepsCounter == PandeoLines.Length - 1);
                break;
            case Type.FLEXION:
                if (increase) {
                    if (stepsCounter < FlexionLines.Length - 1) {
                        stepsCounter++;
                    }
                } else {
                    if (stepsCounter > 0) {
                        stepsCounter--;
                    }
                }
                finished = (stepsCounter == FlexionLines.Length - 1);
                break;
            case Type.TRACCION:
                if (increase) {
                    if (stepsCounter < TraccionLines.Length - 1) {
                        stepsCounter++;
                    }
                } else {
                    if (stepsCounter > 0) {
                        stepsCounter--;
                    }
                }
                finished = (stepsCounter == TraccionLines.Length - 1);
                break;
        }
        Debug.Log ("Steps: " + stepsCounter + ", Finished: " + finished);
    }

    private void updateText () {
        if (finished) {
            ExitButton.GetComponentInChildren<Text> ().text = "Salir";
        } else {
            ExitButton.GetComponentInChildren<Text> ().text = "Siguiente";
        }
        if (stepsCounter != -1) {
            switch (type) {
                case Type.TORSION:
                    textShowed.text = TorsionLines[stepsCounter];
                    break;
                case Type.PANDEO:
                    textShowed.text = PandeoLines[stepsCounter];
                    break;
                case Type.FLEXION:
                    textShowed.text = FlexionLines[stepsCounter];
                    break;
                case Type.TRACCION:
                    textShowed.text = TraccionLines[stepsCounter];
                    break;
                default:
                    textShowed.text = DataLoadError;
                    break;
            }
        }

    }

    private void updateModels () {
        switch (type) {
            case Type.TORSION:
                updateModelsTorsion ();
                break;
            case Type.PANDEO:
                updateModelsPandeo ();
                break;
            case Type.FLEXION:
                break;
            case Type.TRACCION:
                break;
        }
    }

    private void updateModelsTorsion () {
        switch (stepsCounter) {
            case 0:
                if (!inLab) {
                    vBTorsionFueraScript.MakeVisible ();

                    //----
                    vConecAmpliBarraFueraScript.MakeInvisible ();
                } else {
                    vBTorsionLabScript.MakeVisible ();

                    //----
                    vConecAmpliBarraLabScript.MakeInvisible ();
                }
                break;
            case 2:
                if (!inLab) {
                    vConecAmpliBarraFueraScript.MakeVisible ();

                    //----
                    vConecAmpFueraScript.MakeInvisible ();
                } else {
                    vConecAmpliBarraLabScript.MakeVisible ();

                    //----
                    vConecAmpLabScript.MakeInvisible ();
                }
                break;
            case 3:
                if (!inLab) {
                    vConecAmpFueraScript.MakeVisible ();
                } else {
                    vConecAmpLabScript.MakeVisible ();

                    //----
                    vFlechaTorsionLabEncender.MakeInvisible ();
                }
                break;
            case 4:
                if (!inLab) {

                } else {
                    vFlechaTorsionLabEncender.MakeVisible ();

                    //----
                    vFlechaTorsionLabPantalla.MakeInvisible ();
                }
                break;
            case 5:
                if (!inLab) {

                    //----
                    vPesasTFueraScript.MakeInvisible ();
                } else {
                    vFlechaTorsionLabEncender.MakeInvisible ();
                    vFlechaTorsionLabPantalla.MakeVisible ();

                    //----
                    vPesasTLabScript.MakeInvisible ();
                }
                break;
            case 6:
                if (!inLab) {
                    vPesasTFueraScript.MakeVisible ();
                } else {
                    vFlechaTorsionLabPantalla.MakeInvisible ();
                    vPesasTLabScript.MakeVisible ();
                }
                break;
        }
    }

    private void updateModelsPandeo () {

        switch (stepsCounter) {
            case 0:
                if (!inLab) {

                    //----
                    vB500ArtFueraScript.MakeInvisible ();
                    vB500EmFueraScript.MakeInvisible ();
                } else {

                    //----
                    vB500EmLabScript.MakeInvisible ();
                    vB500ArtLabScript.MakeInvisible ();
                }
                break;
            case 1:
                if (!inLab) {
                    if (data.Contains ("A")) {
                        vB500ArtFueraScript.MakeVisible ();
                    } else {
                        vB500EmFueraScript.MakeVisible ();
                    }

                    //----
                    vTravCargaFueraScript.MakeInvisible ();
                } else {
                    if (data.Contains ("A")) {
                        vB500ArtLabScript.MakeVisible ();
                    } else {
                        vB500EmLabScript.MakeVisible ();
                    }
                }
                break;
            case 2:
                if (!inLab) {
                    vTravCargaFueraScript.MakeVisible ();

                    //----

                } else {

                }
                break;
            case 3:
                if (!inLab) {
                    vTravCargaFueraScript.MakeInvisible ();
                } else {

                    //----
                    vFlechaPandeoLabTuerca.MakeInvisible ();
                }

                break;
            case 4:
                if (!inLab) {

                    //----
                    vRelojMedFueraScript.MakeInvisible ();
                } else {
                    vFlechaPandeoLabTuerca.MakeVisible ();

                    //----
                    vFlechaPandeoLabReloj.MakeInvisible ();
                }
                break;
            case 5:
                if (!inLab) {
                    vRelojMedFueraScript.MakeVisible ();
                } else {
                    vFlechaPandeoLabTuerca.MakeInvisible ();
                    vFlechaPandeoLabReloj.MakeVisible ();

                }
                break;
            case 6:
                if (!inLab) {

                    //----
                    vTuercaPandeoScript.MakeInvisible ();
                } else {

                    //----
                    vFlechaPandeoLabTuerca.MakeInvisible ();
                    vFlechaPandeoLabReloj.MakeVisible ();
                }
                break;
            case 7:
                if (!inLab) {
                    vTuercaPandeoScript.MakeVisible ();
                } else {
                    vFlechaPandeoLabTuerca.MakeVisible ();
                    vFlechaPandeoLabReloj.MakeInvisible ();
                }
                break;
            case 8:
                if (!inLab) {

                } else {

                    //----
                    vFlechaPandeoLabTuerca.MakeVisible ();
                }
                break;
            case 9:
                if (!inLab) {

                    //----
                    vTuercaPandeoScript.MakeVisible ();
                } else {
                    vFlechaPandeoLabTuerca.MakeInvisible ();
                }
                break;
            case 10:
                if (!inLab) {
                    vTuercaPandeoScript.MakeInvisible ();
                }
                break;
        }
    }

    private void updateModelsFlexion () { }

    private void updateModelsTraccion () { }
}