using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public string JoystickHorizontal = "p1_ps4_horizontal";
    public string JoystickSelection = "p1_ps4_confirm";
    public string JoystickBack = "p1_ps4_cancel";
    public bool hasChosen = false;  //Identifica se o jogaodr já escolheu o personagem
    public string chosenChar;  //Identifica o jogador escolhido
    public bool hasSelected = false;  //Bool usada para impedi o sprite de mudar quando o personagem for escolhido

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
    public GameObject[] PLayerOp;

    [Header("UI References")]
    //[SerializeField] private TextMeshProUGUI Character1_Name;
    [SerializeField] private Image PLayerSplash;
    [SerializeField] private Image BackgroundColor;
    [SerializeField] private GameObject PLayerSpawn;

    private void Start()
    {
        UpdateCharacterSelectionScreen();
    }

    private void FixedUpdate()
    {
        if (!hasSelected)
        {
            _horizontal = Input.GetAxis(JoystickHorizontal);

            //esquerda
            if (_horizontal < -0.1f && _canSelect)
            {
                FMODUnity.RuntimeManager.PlayOneShot(ChangeLeft);
                selecterCharacterIndex--;
                _canSelect = false;
            }
            //direita
            else if (_horizontal > 0.1f && _canSelect)
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

            //se o jogador pressionar X será salvo o personagem que ele escolheu e uma bool para indicar que já ouve a selação vira true
            if (Input.GetButton(JoystickSelection))
            {
                if (selecterCharacterIndex == 0)
                    Instantiate(PLayerOp[0], PLayerSpawn.transform.position,  PLayerSpawn.transform.rotation);
                else if (selecterCharacterIndex == 1)
                    Instantiate(PLayerOp[1], PLayerSpawn.transform.position,  PLayerSpawn.transform.rotation);
                else if (selecterCharacterIndex == 2)
                    Instantiate(PLayerOp[2], PLayerSpawn.transform.position,  PLayerSpawn.transform.rotation);
                else if (selecterCharacterIndex == 3)
                    Instantiate(PLayerOp[3], PLayerSpawn.transform.position,  PLayerSpawn.transform.rotation);
                hasSelected = true;
                hasChosen = true;
                Debug.Log("Jogador escolheu");
            }
        }

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
