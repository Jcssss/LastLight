using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healtbar : MonoBehaviour
{

    public GameObject heartPrefab;
    public playerHealth playerHp;
    List<heartHealth> hearts = new List<heartHealth> ();

    public void OnEnable()
    {
        playerHealth.OnPlayerDamage += CreateHearts;
    }

    public void OnDisable()
    {
        playerHealth.OnPlayerDamage -= CreateHearts;
    }

    public void CreateHearts()
    {
        ClearHearts (); 

        //determine hearts to create
        int heartsToMake = playerHp.maxHealth;
        int heartsCount = playerHp.curHealth;

        for (int i = 0;i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < heartsCount; i++)
        {
            hearts[i].SetHeartStatus(heartStatus.Full);
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        heartHealth heartComponent = newHeart.GetComponent<heartHealth>();
        heartComponent.SetHeartStatus(heartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<heartHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
