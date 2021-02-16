using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Trigger : MonoBehaviour
{
    public GameObject GoalText;

    // Adds the given prefab to the canvas UI
    void OnTriggerEnter(Collider other)
    {
        GameObject newText = Instantiate(GoalText, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        newText.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
