using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    private int selecterCharacterIndex;
    private Color desiredColor;

    [Header("List of character")]
    [SerializeField] List<ChacterSelectObject> characterList = new List<ChacterSelectObject>();

    [Header("UI References")]
    //[SerializeField] private TextMeshProUGUI Character1_Name;
    [SerializeField] private Image PLayer1Splash;
    [SerializeField] private Image BackgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip backgroundMusic;

    private void Start()
    {
        UpdateCharacterSelectionScreen();
    }

    private void LeftArrow()
    {
        selecterCharacterIndex--;
        if (selecterCharacterIndex < 0)
        {
            selecterCharacterIndex = characterList.Count - 1;
        }
        UpdateCharacterSelectionScreen();
    }

    private void RightArrow()
    {
        selecterCharacterIndex++;
        if (selecterCharacterIndex == characterList.Count)
        {
            selecterCharacterIndex = 0;
        }
        UpdateCharacterSelectionScreen();
    }

    private void UpdateCharacterSelectionScreen()
    {
        PLayer1Splash.sprite = characterList[selecterCharacterIndex].splash;
        desiredColor = characterList[selecterCharacterIndex].characterColor;
    }

    [System.Serializable]
    public class ChacterSelectObject{
        public Sprite splash;
        public string characterName;
        public Color characterColor;
   }
}
