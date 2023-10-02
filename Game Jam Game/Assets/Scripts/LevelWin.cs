using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    public string endScreen;
    public ContactFilter2D ContactFilter;
    public float detectionRadius = 1.0f;

    void Update () {
        // get surroundings
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, detectionRadius, ContactFilter.NoFilter(), results);

        foreach (Collider2D hit in results) {

            if (hit.gameObject.tag.Equals("Player")) {
                SceneManager.LoadScene(endScreen, LoadSceneMode.Single);
            }
        } 
    }
}
