using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APICONECTION : MonoBehaviour
{
    // Start is called before the first frame update

    public static APICONECTION Instance;
    private void Awake()
    {

        //Si hay m�s de una instancia, destruye la otra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Establece la estancia estatica
            Instance = this;
        }
    }

    public void Start()
    {
        StartCoroutine(LevelUpload(PlayerStats.Money,PlayerStats.Level));
        //StartCoroutine(UploadCampo());
        //StartCoroutine(UploadTemporada());
    }

    [System.Obsolete]
    IEnumerator LevelUpload(int puntaje, int nivel)
    {
        string url = "http://www.bluelearn.somee.com/api/Jugador?Puntaje=" + puntaje + "&Nivel=" + nivel;
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, ""))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error " + request.error);
            }
            else
            {
                Debug.Log("Form upload complete!" + puntaje + nivel);
            }
        }
    }

   
}
    


