using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class resetScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
