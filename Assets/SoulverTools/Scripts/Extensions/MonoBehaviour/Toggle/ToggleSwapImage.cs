using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Toggle), typeof(Image))]
public class ToggleSwapImage : MonoBehaviour
{
    [SerializeField] private Sprite activeToggle;
    [SerializeField] private Sprite deactiveToggle;
    private Image _image;

    private void Awake()
    {
        Toggle toggle = GetComponent<Toggle>();
        _image = GetComponent<Image>();
        SwapImage(toggle.isOn);
        toggle.onValueChanged.AddListener(SwapImage);
    }

    private void SwapImage(bool active)
    {
        _image.sprite = active ? activeToggle : deactiveToggle;
    }
}
