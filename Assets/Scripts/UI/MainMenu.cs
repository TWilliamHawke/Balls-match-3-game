using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _newGameBtn;
    [SerializeField] Button _loadGameButton;


    // Start is called before the first frame update
    void Start()
    {
        if (GameScore.HasSavedScore())
        {
            _loadGameButton.Select();
        }
        else
        {
            _loadGameButton.interactable = false;
            _newGameBtn.Select();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
