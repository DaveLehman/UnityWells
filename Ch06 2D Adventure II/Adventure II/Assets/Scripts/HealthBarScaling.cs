using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScaling : MonoBehaviour
{

    public float MaxSpeed = 50f;
    private RectTransform ThisTransform = null;
    void Awake()
    {
        //Get transform component
        ThisTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set Start Health
        if (PlayerController.PlayerInstance != null)
        {
            ThisTransform.sizeDelta = new Vector2(Mathf.Clamp(PlayerController.Health, 0, 100), ThisTransform.sizeDelta.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update health property
        float HealthUpdate = 0f;

        if (PlayerController.PlayerInstance != null)
        {
            HealthUpdate = Mathf.MoveTowards(ThisTransform.rect.width, PlayerController.Health, MaxSpeed);
        }

        ThisTransform.sizeDelta = new Vector2(Mathf.Clamp(HealthUpdate, 0, 100), ThisTransform.sizeDelta.y);
    }

}
