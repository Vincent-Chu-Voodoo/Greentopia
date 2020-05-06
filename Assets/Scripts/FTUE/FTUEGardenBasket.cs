using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEGardenBasket : MonoBehaviour
{
    public float moveSpeed;
    public float collectionDelay;

    public Transform originalAnchor;
    public Transform collectionAnchor;

    public GameEvent OnReachedCollectionAnchor;
    public GameEvent OnEndCollection;

    IEnumerator Start()
    {
        while (Vector3.Distance(transform.position, collectionAnchor.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, collectionAnchor.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        OnReachedCollectionAnchor.Invoke(this);
        yield return new WaitForSeconds(collectionDelay);
        FTUENeighbourSceneController.haveApple = true;
        while (Vector3.Distance(transform.position, originalAnchor.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalAnchor.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        OnEndCollection.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
