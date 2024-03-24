using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonControlador : MonoBehaviour
{
    private SpriteRenderer theSprite;

    public int thisButtonNumber;

    private GameManager theGM;

    private AudioSource theSound;


    void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();
        theGM = FindObjectOfType<GameManager>();
        theSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 1f);
        theSound.Play();
    }

    private void OnMouseUp()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 0.54f);
        theGM.ColorPesionado(thisButtonNumber);
        theSound.Stop();
    }
}
