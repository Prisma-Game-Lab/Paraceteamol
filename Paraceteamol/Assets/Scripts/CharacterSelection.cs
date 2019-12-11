﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public string JoystickHorizontal = "p1_ps4_horizontal";
    [FMODUnity.EventRef]
    public string ChangeLeft;
    [FMODUnity.EventRef]
    public string ChangeRight;

    private int selecterCharacterIndex;
    private Color desiredColor;
    private float _horizontal;

    [Header("List of character")]
    [SerializeField] List<ChacterSelectObject> characterList = new List<ChacterSelectObject>();

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
    }

    private void LeftArrow()
    {
        if (_horizontal < 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(ChangeLeft);
            selecterCharacterIndex--;
            if (selecterCharacterIndex < 0)
                selecterCharacterIndex = characterList.Count - 1;
        }
        UpdateCharacterSelectionScreen();
    }

    private void RightArrow()
    {
        if (_horizontal > 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(ChangeRight);
            selecterCharacterIndex++;
            if(selecterCharacterIndex > characterList.Count - 1)
                selecterCharacterIndex = 0;

        }
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
