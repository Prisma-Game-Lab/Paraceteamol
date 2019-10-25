using UnityEngine;
using UnityEngine.SceneManagement;
public class MapSelection : MonoBehaviour
{
    public string Map;
    public void ReturnMenu()
    {
        Debug.Log("Loading scene...");
        SceneManager.LoadScene("Tela_inicial");
    }
    public void ChooseMap() {
        SceneManager.LoadScene(Map);

    }

}
