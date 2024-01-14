using UnityEngine;
using UnityEngine.UI;

public class RebuildLayout : TaskExecutor<RebuildLayout>
{
    public void ForcedDenote()
    {
        Denote();
    }
    public void Rebuild()
    {
        LayoutGroup topLayoutGroup = GetComponent<LayoutGroup>();

        if (topLayoutGroup != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(topLayoutGroup.GetComponent<RectTransform>());
        }

        RebuildLayoutRecursive(transform);
    }

    private void RebuildLayoutRecursive(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            LayoutGroup layoutGroup = child.GetComponent<LayoutGroup>();
            if (layoutGroup != null)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
            }
            RebuildLayoutRecursive(child);
        }
    }
}