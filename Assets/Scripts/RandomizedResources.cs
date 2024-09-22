using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedResources : MonoBehaviour
{
    // Array of item prefabs to be randomly placed
    public GameObject[] itemsToPlace;

    // Array of empty GameObjects representing the preset locations
    public Transform[] placementLocations;

    // Minimum and maximum number of items to place
    public int minItemsToPlace = 1;
    public int maxItemsToPlace;

    void Start()
    {
        PlaceRandomItems();
    }

    void PlaceRandomItems()
    {
        // Shuffle the placement locations array to randomize the positions
        Transform[] randomizedLocations = ShuffleArray(placementLocations);

        // Shuffle the items to ensure different items are chosen randomly
        GameObject[] randomizedItems = ShuffleArray(itemsToPlace);

        // Determine how many items to place (random between min and max)
        int itemsToPlaceCount = Random.Range(minItemsToPlace, Mathf.Min(maxItemsToPlace, randomizedItems.Length) + 1);

        // Loop through and place the items
        for (int i = 0; i < itemsToPlaceCount && i < randomizedLocations.Length; i++)
        {
            // Instantiate the item at the randomized location
            Instantiate(randomizedItems[i], randomizedLocations[i].position, randomizedLocations[i].rotation);
        }
    }

    // Generic function to shuffle any array
    T[] ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }
}