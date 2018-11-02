using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;       

    private Vector3 offset;         
    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    
    public void UpdatePoition()
    {
        offset = transform.position - player.transform.position;
    }
    public void GoToLocation()
    {
        transform.position = player.transform.position + offset;
    }
}