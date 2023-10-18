using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarScene(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
