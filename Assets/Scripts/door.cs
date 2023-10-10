using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class door : MonoBehaviour
{
    // Start is called before the first frame update

    public string sceneLoad;

    public Vector3 playerPosition;

    public float playerRotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hi");
            SceneManager.LoadScene(sceneLoad);
            
        }
        
    }
}
