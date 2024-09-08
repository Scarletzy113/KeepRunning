using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    //public GameObject Starting_section;
    public GameObject[] sectionPrefabs;
    public GameObject[] sectionPrefabs_mid;
    public GameObject[] sectionPrefabs_last;

    // Reference to the last instantiated section
    private GameObject lastSection;

    // Reference to the player movement script
    private playerMovement player;

    private void Start()
    {
        // Identify the existing section with "StartingSection" tag
        lastSection = GameObject.FindWithTag("StartingSection");

        if (lastSection == null)
        {
            Debug.LogWarning("Starting section not found. Ensure it is tagged correctly or assigned in the Inspector.");
        }

        // Get the playerMovement script reference from the player object
        player = FindObjectOfType<playerMovement>();
        if (player == null)
        {
            Debug.LogWarning("playerMovement script not found on the player object.");
        }
    }

//code inspired and adapted from Rigor Mortis Tortoise https://www.youtube.com/watch?v=Ldyw5IFkEUQ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Section_trigger"))
        {
            // Check if there are any prefabs in the array
            if (sectionPrefabs.Length > 0)
            {
                GameObject randomSectionPrefab = null;

                // Select a section based on the player's score
                if (player != null)
                {
                    if (player.score < 200)
                    {
                        // Select a random prefab from the sectionPrefabs array
                        int randomIndex = Random.Range(0, sectionPrefabs.Length);
                        randomSectionPrefab = sectionPrefabs[randomIndex];
                    }
                    else if (player.score >= 200 && player.score < 400)
                    {
                        // Select a random prefab from the sectionPrefabs_mid array
                        int randomIndex = Random.Range(0, sectionPrefabs_mid.Length);
                        randomSectionPrefab = sectionPrefabs_mid[randomIndex];
                    }
                    else
                    {
                        // Select a random prefab from the sectionPrefabs_last array
                        int randomIndex = Random.Range(0, sectionPrefabs_last.Length);
                        randomSectionPrefab = sectionPrefabs_last[randomIndex];
                    }
                }

                if (randomSectionPrefab != null)
                {
                    // Determine the position for the new section
                    Vector3 spawnPosition = Vector3.zero;

                    if (lastSection != null)
                    {
                        // Get the EndPoint of the last section
                        Transform lastSectionEndPoint = lastSection.transform.Find("endPoint");
                        if (lastSectionEndPoint != null)
                        {
                            // Calculate the offset
                            Vector3 offset = randomSectionPrefab.transform.Find("startPoint").localPosition;

                            // Set spawn position to align the StartPoint of the new section with the EndPoint of the last section
                            spawnPosition = lastSectionEndPoint.position - offset;
                        }
                    }

                    // Instantiate the selected prefab at the calculated position
                    GameObject newSection = Instantiate(randomSectionPrefab, spawnPosition, Quaternion.identity);

                    // Check if the new section has a MeshCollider and Rigidbody
                    MeshCollider meshCollider = newSection.GetComponent<MeshCollider>();
                    Rigidbody rb = newSection.GetComponent<Rigidbody>();

                    // If the new section has a MeshCollider and Rigidbody, make the Rigidbody kinematic
                    if (meshCollider != null && rb != null)
                    {
                        rb.isKinematic = true;
                    }

                    // Update the reference to the last section
                    lastSection = newSection;
                }
            }
            else
            {
                Debug.LogWarning("No section prefabs assigned to the array.");
            }
        }
    }
}
