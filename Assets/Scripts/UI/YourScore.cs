using UnityEngine;
using UnityEngine.UI;


public class YourScore : MonoBehaviour
{
    [SerializeField] Text _scoreText;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetText(int score)
    {
        _scoreText.text = score.ToString();
    }
}
