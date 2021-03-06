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
    
    private PlayerHealth m_PlayerHealth;

    private void Start()
    {
        m_PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void LateUpdate()
    {
        Notify();
    }

    public void Notify()
    {
        m_KeysText.text = GameVariables.keysInventory.ToString();
        m_HealthBar.value = (float)m_PlayerHealth.HP / (float)m_PlayerHealth.maxHP;
    }
}
