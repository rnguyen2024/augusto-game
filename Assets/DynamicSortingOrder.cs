using UnityEngine;
using UnityEngine.Rendering;

public class DynamicSortingGroupOrder : MonoBehaviour
{
    private SortingGroup sortingGroup;

    void Start()
    {
        // Get the SortingGroup component attached to the player
        sortingGroup = GetComponent<SortingGroup>();
        if (sortingGroup == null)
        {
            Debug.LogWarning("SortingGroup not found! Ensure you attach this script to a GameObject with a SortingGroup component.");
        }
    }

    void Update()
    {
        // Update the sorting order based on the Y position of the player
        if (sortingGroup != null)
        {
            sortingGroup.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + 10; // Increased offset
            Debug.Log($"Player Y Position: {transform.position.y}, Sorting Order: {sortingGroup.sortingOrder}");
        }
    }
}
