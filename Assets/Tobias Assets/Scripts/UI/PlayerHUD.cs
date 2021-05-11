using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[ExecuteInEditMode]
public class PlayerHUD : MonoBehaviour
{
    [Space(3), Header("HUD Items")]
    [SerializeField] private Slider m_HealthBar;
    [SerializeField] private Image m_KeysImage;
    [SerializeField] private Text m_KeysText;

    [Space(3), Header("Player")]
    [SerializeField] private GameObject m_Placeholder;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        Notify();
    }

    public void Notify()
    {
        m_KeysText.text = GameVariables.keysInventory.ToString();
    }
}
