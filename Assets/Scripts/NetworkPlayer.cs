using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    public float speed;
    private Vector3 direction;
    private Animator animator;

    public override void OnNetworkSpawn()
    {
        if (IsOwner) {
            InitializeCamera();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(0, 0, 0);
        if (Input.GetKey("w"))
        {
            direction.z = 1;
        }
        if (Input.GetKey("s"))
        {
            direction.z = -1;
        }
        if (Input.GetKey("a"))
        {
            direction.x = -1;
        }
        if (Input.GetKey("d"))
        {
            direction.x = 1;
        }

        if(direction.x != 0 || direction.y != 0 || direction.z != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void InitializeCamera()
    {
        Camera mainCamera = Camera.main;
        mainCamera.transform.SetParent(transform);
        mainCamera.transform.localPosition = new Vector3(0, 5, -6);
        mainCamera.transform.localEulerAngles = new Vector3(20, 0, 0);
    }
}
