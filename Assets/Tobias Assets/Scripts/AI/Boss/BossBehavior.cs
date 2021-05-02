using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private BossModel model;
    private BossView view;

    private void Start()
    {
        model = GetComponent<BossModel>();
        view = GetComponent<BossView>();
    }

    private void Update()
    {
        
    }
}
