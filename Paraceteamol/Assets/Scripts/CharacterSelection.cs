using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public string JoystickHorizontal = "p1_ps4_horizontal";
    public string JoystickSelection = "p1_ps4_confirm";
    public bool hasChosen = false;
    public string chosenChar;

    [FMODUnity.EventRef]
    public string ChangeLeft;
    [FMODUnity.EventRef]
    public string ChangeRight;

    private int selecterCharacterIndex = 0;
    private Color desiredColor;
    private float _horizontal;
    private bool _canSelect = true;

    [Header("List of character")]
    [SerializeField] List<ChacterSelectObject> characterList = new List<ChacterSelectObject>();
    [SerializeField] private GameObject[] PLayerOp;

    [Header("UI References")]
    //[SerializeField] private TextMeshProUGUI Character1_Name;
    [SerializeField] private Image PLayerSplash;
    [SerializeField] private Image BackgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip backgroundMusic;

    private void Start()
    {
        UpdateCharacterSelectionScreen();
    }

    private void FixedUpdate()
    {
        _horizontal = Input.GetAxis(JoystickHorizontal);

        if (_horizontal < 0f && _canSelect)
        {
            FMODUnity.RuntimeManager.PlayOneShot(ChangeLeft);
            selecterCharacterIndex--;
            _canSelect = false;
        }
        else if (_horizontal > 0f && _canSelect)
        {
            FMODUnity.RuntimeManager.PlayOneShot(ChangeRight);
            selecterCharacterIndex++;
            _canSelect = false;
        }
        else if (_horizontal == 0)
            _canSelect = true;

        // Verifica a integridade da variavel
        if (selecterCharacterIndex < 0)
            selecterCharacterIndex = characterList.Count - 1;
        else if (selecterCharacterIndex > characterList.Count - 1)
            selecterCharacterIndex = 0;

        UpdateCharacterSelectionScreen();
    }

    private void UpdateCharacterSelectionScreen()
    {
        PLayerSplash.sprite = characterList[selecterCharacterIndex].splash;
        desiredColor = characterList[selecterCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class ChacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
    }
}
