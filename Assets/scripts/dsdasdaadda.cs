using UnityEngine;

public class dsdasdaadda : MonoBehaviour
{
    public Transform sde;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 sd = new Vector3(sde.position.x, sde.position.y + 2.73f, sde.position.z);
        transform.position = sd;
    }
}
