using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualControls : MonoBehaviour
{
    public GameObject buttonS, buttonA, buttonZ, buttonE, buttonR;
    private bool pressedS, pressedA, pressedZ, pressedE, pressedR;
    // Use this for initialization
    void Start()
    {
        pressedS = buttonS.GetComponent<InputController>().getPressed();
        pressedA = buttonA.GetComponent<InputController>().getPressed();
        pressedZ = buttonZ.GetComponent<InputController>().getPressed();
        pressedE = buttonE.GetComponent<InputController>().getPressed();
        pressedR = buttonR.GetComponent<InputController>().getPressed();
    }

    void Update()
    {
        pressedS = buttonS.GetComponent<InputController>().getPressed();
        pressedA = buttonA.GetComponent<InputController>().getPressed();
        pressedZ = buttonZ.GetComponent<InputController>().getPressed();
        pressedE = buttonE.GetComponent<InputController>().getPressed();
        pressedR = buttonR.GetComponent<InputController>().getPressed();
    }

    public bool getA()
    {
        return pressedA;
    }
    public bool getZ()
    {
        return pressedZ;
    }
    public bool getE()
    {
        return pressedE;
    }
    public bool getR()
    {
        return pressedR;
    }
    public bool getS()
    {
        return pressedS;
    }
}
