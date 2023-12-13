using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip hitWallSound;
    [SerializeField] AudioClip hitEnemySound;

    private Camera cam;
    public static int totalScore;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        totalScore = 0;

        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
               
                if (target != null)
                {
                    totalScore++;


                    target.ReactToHit();
                    soundSource.PlayOneShot(hitEnemySound);
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                }
                else
                {

                    StartCoroutine(SphereIndicator(hit.point));
                    soundSource.PlayOneShot(hitWallSound);
                }
            }

        }
    }

    private IEnumerator SphereIndicator(Vector3 pos) 
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
