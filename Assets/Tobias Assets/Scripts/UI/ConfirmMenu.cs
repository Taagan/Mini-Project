using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmMenu : MonoBehaviour
{
    [SerializeField] private Button m_YesButton = null;
    [SerializeField] private Button m_NoButton = null;

    public void YesAction(UnityAction yesAction)
    {
        m_YesButton.onClick.RemoveAllListeners();
        m_YesButton.onClick.AddListener(yesAction);
    }

    public void NoAction(UnityAction noAction)
    {
        m_NoButton.onClick.RemoveAllListeners();
        m_NoButton.onClick.AddListener(noAction);
    }
}
