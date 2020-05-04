using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerModelAnimState
{
    Idle = 1, Move = 15, Walk, NormalATK = 20
}

public class PlayerModelController : MonoBehaviour
{
    public float moveSpeed;
    public float plantTime;

    public GameObject plantPrefab;
    public List<GameObject> plantPrefabList;
    public Camera referenceCamera;
    public Animator animator;

    private Coroutine actionRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = referenceCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, float.PositiveInfinity, LayerMask.GetMask(LayerEnum.Ground.ToString())))
            {
                Plant(hitInfo.point);
            }
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (actionRoutine != null)
            StopCoroutine(actionRoutine);
        actionRoutine = StartCoroutine(MoveToLoop(targetPosition));
    }

    IEnumerator MoveToLoop(Vector3 targetPosition)
    {
        var characterTrans = animator.transform;
        characterTrans.LookAt(targetPosition);
        animator.SetInteger("animation", (int)PlayerModelAnimState.Move);

        while (Vector3.Distance(targetPosition, characterTrans.position) > 0f)
        {
            characterTrans.position = Vector3.MoveTowards(characterTrans.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetInteger("animation", (int)PlayerModelAnimState.Idle);
    }

    public void Plant(Vector3 targetPosition)
    {
        if (actionRoutine != null)
            StopCoroutine(actionRoutine);
        actionRoutine = StartCoroutine(PlantLoop(targetPosition));
    }

    IEnumerator PlantLoop(Vector3 targetPosition)
    {
        yield return MoveToLoop(targetPosition);
        animator.SetInteger("animation", (int)PlayerModelAnimState.NormalATK);
        yield return new WaitForSeconds(plantTime);
        var targetPlant = plantPrefabList[Random.Range(0, plantPrefabList.Count)];
        var plant = Instantiate(targetPlant);
        var plants = plant.AddComponent<MI_Growth>();
        plants.growthSpeed = 2f;
        plants.targetScale = Vector3.one * 9f;
        plant.transform.position = animator.transform.position + animator.transform.forward * 0.2f;
        animator.SetInteger("animation", (int)PlayerModelAnimState.Idle);
    }
}
