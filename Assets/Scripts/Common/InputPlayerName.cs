using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPlayerName : MonoBehaviour
{
    [SerializeField] InputField currentPlayerName;

    void Start()
    {
        //Changes the character limit in the main input field.
        currentPlayerName.characterLimit = 5;
    }
}
