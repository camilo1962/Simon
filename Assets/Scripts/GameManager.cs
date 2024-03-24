using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public SpriteRenderer[] colores;
    public AudioSource[] sonidosBoton;

    private int colorSelect;

    public float stayLit;
    private float stayLitCounter;

    public float waitBetweenLights;
    public float waitBetweenCounter;

    private bool shouldBeLit;
    private bool shouldBeDark;

    public List<int> activeSequence;
    private int positionInSequence;

    private bool gameActive;
    private int inputInSequence;

    public AudioSource correcto;
    public AudioSource incorrecto;

    public TMP_Text scoreText;
    public TMP_Text recordText;

    public GameObject panelGameOver;
    

   

    void Start()
    {
       
        if (!PlayerPrefs.HasKey("record"))
        {
            PlayerPrefs.SetInt("record", 0);
        }
        scoreText.text = "0";
        recordText.text = PlayerPrefs.GetInt("record").ToString();
        panelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;

            if(stayLitCounter < 0)
            {
                colores[activeSequence[positionInSequence]].color = new Color(colores[activeSequence[positionInSequence]].color.r, colores[activeSequence[positionInSequence]].color.g, colores[activeSequence[positionInSequence]].color.b, 0.52f);
                sonidosBoton[activeSequence[positionInSequence]].Stop();
                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLights;

                positionInSequence++;
            }        
           
        }
        if (shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if(positionInSequence >= activeSequence.Count)
            {
                shouldBeDark = false;
                gameActive = true;
            }
            else
            {
                if(waitBetweenCounter < 0)
                {
                    

                    colores[activeSequence[positionInSequence]].color = new Color(colores[activeSequence[positionInSequence]].color.r, colores[activeSequence[positionInSequence]].color.g, colores[activeSequence[positionInSequence]].color.b, 1f);
                    sonidosBoton[activeSequence[positionInSequence]].Play();

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        panelGameOver.SetActive(false);
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;

        colorSelect = Random.Range(0, colores.Length);

        activeSequence.Add(colorSelect);

        colores[activeSequence[positionInSequence]].color = new Color(colores[activeSequence[positionInSequence]].color.r, colores[activeSequence[positionInSequence]].color.g, colores[activeSequence[positionInSequence]].color.b, 1f);
        sonidosBoton[activeSequence[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;
        scoreText.text = "0";
        recordText.text = PlayerPrefs.GetInt("record").ToString();
    }

    public void ColorPesionado(int whichButton)
    {
        if (gameActive)
        {


            if (activeSequence[inputInSequence] == whichButton)
            {
                Debug.Log("Correcto");
                

                inputInSequence++;

                if(inputInSequence >= activeSequence.Count)
                {

                    scoreText.text = activeSequence.Count.ToString();
                    if(activeSequence.Count > PlayerPrefs.GetInt("record"))
                    {
                        PlayerPrefs.SetInt("record", activeSequence.Count);
                    }
                    recordText.text = PlayerPrefs.GetInt("record").ToString();

                    positionInSequence = 0;
                    inputInSequence = 0;
                    colorSelect = Random.Range(0, colores.Length);

                    activeSequence.Add(colorSelect);

                    colores[activeSequence[positionInSequence]].color = new Color(colores[activeSequence[positionInSequence]].color.r, colores[activeSequence[positionInSequence]].color.g, colores[activeSequence[positionInSequence]].color.b, 1f);
                    sonidosBoton[activeSequence[positionInSequence]].Play();

                    stayLitCounter = stayLit;
                    shouldBeLit = true;

                    gameActive = false;
                    Invoke("Esperar", .4f);
                   
                    
                   
                }
            }
            else
            {
                Debug.Log("Incorrecto");
                incorrecto.Play();
                gameActive = false;
                panelGameOver.SetActive(true);
            }
        }
    }
    
    public void Esperar()
    {
        correcto.Play();
    }

    public void Salir()
    {
        Application.Quit();
    }
  
}
