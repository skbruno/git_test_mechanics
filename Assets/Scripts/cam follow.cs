using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camfollow : MonoBehaviour
{

    public float smoothing;
    public float rotsmoothing;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotsmoothing);
    }
}
