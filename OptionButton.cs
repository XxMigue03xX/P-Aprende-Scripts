using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class OptionButton : MonoBehaviour
{
    private Text v_text = null;
    private Button v_button = null;
    private Image v_image = null;
    private Color v_originalColor = Color.black;
    public Option Option { get; set; }

    public void Awake()
    {
        v_button = GetComponent<Button>();
        v_image = GetComponent<Image>();
        v_text = transform.GetChild(0).GetComponent<Text>();
        v_originalColor = v_image.color;
    }
    public void Construte(Option v_option, Action<OptionButton> CallBack)
    {
        v_text.text = v_option.text;
        v_button.enabled = true;
        Option = v_option;
        v_button.onClick.AddListener(delegate
        {
            CallBack(this);
        });
    }
    public void SetColor(Color c)
    {
        v_button.enabled = false;
        v_image.color = c;
    }
}
