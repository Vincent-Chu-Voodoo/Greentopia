using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEGardenNutritionAnimation : MonoBehaviour
{
    public float moveSpeed;
    public Transform targetAnchor;

    public GameEvent OnConsumedNutrition;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetAnchor.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetAnchor.position) < 0.01f)
        {
            OnConsumedNutrition.Invoke(this);
            Destroy(gameObject);
        }
    }
}
