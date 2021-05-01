using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public GameObject levelcomplete,levelfailed,confeti,player;
    public float speed;
    Vector3 cameraInitialPosition;
    private float shakeMagnetude =1f, shakeTime = 3f;
    public Camera mainCamera;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if(levelcomplete.activeInHierarchy==false && levelfailed.activeInHierarchy == false)
        {
            transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            Debug.Log("degdi");
            StartCoroutine(Bekle());
        }
        

    }
    IEnumerator Bekle()
    {
        ShakeIt();
        speed = 0f;
        yield return new WaitForSeconds(1f);
        levelfailed.SetActive(true);
        Time.timeScale = 0f;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("complete"))
        {
            speed = 0;
            levelcomplete.SetActive(true);
            confeti.SetActive(true);
            player.SetActive(false);

        }
    }
    public void Replay()
    {
        SceneManager.LoadScene("game");
    }

    private void Start()
    {
        Time.timeScale = 1f;
        confeti.SetActive(false);
        player.SetActive(true);
    }




















    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }
    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
}
