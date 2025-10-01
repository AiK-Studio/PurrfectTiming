using UnityEngine;
using TMPro;

public class PlayerCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerCountText;

    void OnEnable()
    {
        if (SessionManager.I != null)
            SessionManager.I.OnPlayerCountChanged += UpdateUI;
    }

    void OnDisable()
    {
        if (SessionManager.I != null)
            SessionManager.I.OnPlayerCountChanged -= UpdateUI;
    }

    private void UpdateUI(int count)
    {
        if (playerCountText != null)
            playerCountText.text = $"Players: {count}";
    }
}
