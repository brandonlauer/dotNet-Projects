using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {
    
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text usernameText;


    void Start()
    {
        if (healthBarRect == null)
        {
            Debug.LogError("No health bar object referenced!");
        }

        if (usernameText == null)
        {
            Debug.LogError("No username text object referenced!");
        }
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
    }

    public void SetUsername(string username)
    {
        usernameText.text = username;
    }
}
