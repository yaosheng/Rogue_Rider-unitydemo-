using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image gameoverUI;
    public Image passUI;
    public RectTransform speedUI;
    public Text speedText;
    private GameProcess gp
    {
        get {
            return FindObjectOfType(typeof(GameProcess)) as GameProcess;
        }
    }

    void Start( )
    {
        gameoverUI.gameObject.SetActive(false);
    }

    void Update( )
    {
        speedUI.sizeDelta = new Vector2(gp.vSpeed * 100, 30);
        SpeedNumber(gp.vSpeed * 100);
    }

    public void OpenGameOverUI( )
    {
        gameoverUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenGamePassUI( )
    {
        passUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void SpeedNumber(float speed )
    {
        speedText.text = Mathf.Floor(speed).ToString();
    }
}
