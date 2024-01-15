using UnityEngine;

public class Clear : MonoBehaviour
{
    public void Clearing()
    {
        var children = new System.Collections.Generic.List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);

        children.ForEach(child => DestroyImmediate(child));
    }
}