using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAssetPreviewToImage : MonoBehaviour
{
#if UNITY_EDITOR
    [Space(3)]
    [SerializeField] private bool m_Save = false;

    [Space(5)]
    [SerializeField] private string m_ImageName = "image";
    [SerializeField] private string m_Path = @"C:\";
    [SerializeField] private GameObject m_Object = null;

    private void OnValidate()
    {
        if (m_Save)
        {
            Texture2D texture = UnityEditor.AssetPreview.GetAssetPreview(m_Object);
            byte[] bytes = texture.EncodeToPNG();

            System.IO.File.WriteAllBytes(m_Path + m_ImageName + ".png", bytes);

            Debug.Log("successfully saved to " + m_Path + m_ImageName + ".png");

            m_Save = false;
        }
    }
#endif
}
