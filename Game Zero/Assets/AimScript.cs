﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    public Transform playerLookAtObject;

    public Vector3 offset;

    public float yRotation; 

    Animator animator;
    Transform chest;

    ThirdPersonMovement controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        controller = GetComponent<ThirdPersonMovement>();        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (controller.isAiming && !controller.isRunning)
        {
            Vector3 direction = (playerLookAtObject.position - transform.position).normalized;

            Quaternion newLookRotation = Quaternion.LookRotation(direction) * Quaternion.Inverse(transform.rotation);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // lookRotation = Quaternion.Euler(lookRotation.x, newLookRotation.y, lookRotation.z);

            if (newLookRotation.eulerAngles.y > 90f && newLookRotation.eulerAngles.y < 270f)
            {
                chest.SetPositionAndRotation(chest.transform.position, Quaternion.Euler(lookRotation.eulerAngles.x, newLookRotation.eulerAngles.y + transform.eulerAngles.y, lookRotation.eulerAngles.z));
            }
            if (newLookRotation.eulerAngles.y > 0f && newLookRotation.eulerAngles.y <= 90)
            {
                chest.SetPositionAndRotation(chest.transform.position, Quaternion.Euler(lookRotation.eulerAngles.x, 90f + transform.eulerAngles.y, lookRotation.eulerAngles.z));
            }
            if (newLookRotation.eulerAngles.y >= 270f && newLookRotation.eulerAngles.y < 360f)
            {
                chest.SetPositionAndRotation(chest.transform.position, Quaternion.Euler(lookRotation.eulerAngles.x, 270f + transform.eulerAngles.y, lookRotation.eulerAngles.z));
            }

        }
    }

    public void ResetYRotation()
    {
        yRotation = transform.eulerAngles.y;
    }
}
