using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Raycastgun : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserorigin;
    public float gunRange = 50f;
    public float laserDuration = 0.05f;
    public float fireRate = 0.2f;
    LineRenderer laserline;
    float fireTimer;
    private void Awake()
    {
        
        laserline= GetComponent<LineRenderer>();
    }
    private void Update()
    {
        fireTimer +=Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && fireTimer>fireRate)
        {
            fireTimer = 0;
            laserline.SetPosition(0, laserorigin.position);
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(rayOrigin,playerCamera.transform.forward,out hit,gunRange))
            {
                laserline.SetPosition(1, hit.point);
                Destroy(hit.transform.gameObject);
            }
            else
            {
                laserline.SetPosition(1,rayOrigin+(playerCamera.transform.forward*gunRange));
            }
            StartCoroutine(Shootlaser());
        }
    }
    IEnumerator Shootlaser()
    {
        laserline.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserline.enabled = false;
    }
}
