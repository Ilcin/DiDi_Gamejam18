using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSequence : MonoBehaviour
{

    public ScreenController screen;
    public GameObject Player;
    public Transform cam;

    public Vector3 zoomPosition;
    private Quaternion ZoomRotation;
    private AudioSource damda;
    public float zoomSpeed;
    public void Start()
    {
        damda = GetComponent<AudioSource>();
        damda.Stop();
    }
    public void StartEndSequence()
    {
        print("endSeq");
        //screen.SetImage(0);
        screen.SetFinalFace();
        StartCoroutine(ZoomToFace());

        ZoomRotation = Quaternion.Euler(0, 180, 0);
    }

    IEnumerator ZoomToFace()
    {
        GameObject target = new GameObject();
        target.transform.SetParent(Player.transform);
        target.transform.localPosition = zoomPosition;
        target.transform.LookAt(screen.screen.transform.position + Vector3.up * 0.8f, Vector3.up);
        //target.transform.LookAt(cam.position - cam.forward, Vector3.up);//Quaternion.Euler(0, 180,0);

        yield return new WaitForSeconds(5);
        damda.Play();
        while (true)
        {
            
            cam.position = Vector3.Lerp(cam.position, target.transform.position, zoomSpeed * Time.deltaTime);
            cam.rotation = Quaternion.Lerp(cam.rotation, target.transform.rotation, zoomSpeed * Time.deltaTime);
            yield return null;
        }

    }
}
