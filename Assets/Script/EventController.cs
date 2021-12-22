using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;



public class EventController : MonoBehaviour
{
    public static EventController instance;
    public PostProcessVolume volume;
    public Image blackScreen;
    public Light[] lights;
    public GameObject slender;

    public TriggerVest Vest;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        lights = FindObjectsOfType<Light>();
        
    }

    //flicker Light
    public void TriggerFirstEvent()
    {
        //start Sound
        AudioManager.instance.Play("background");

        for (int i = 0; i < lights.Length; i++) {
            lights[i].intensity = 1f;
            float rand = UnityEngine.Random.Range(0f, 1f);
            if (rand < 0.4f) {
                Animator ani = lights[i].GetComponent<Animator>();
                StartCoroutine(EnableFlicker(rand * 1.5f, ani));
            }
        }
        
    }

    //turn of light
    public IEnumerator TriggerSecondEvent()
    {
        AudioManager.instance.Play("ghostSwell");

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < lights.Length; i++) {
            lights[i].intensity = 0.3f;

            Animator ani = lights[i].GetComponent<Animator>();
            ani.enabled = false;
            ani.ResetTrigger("startFlicker");
            
        }
    }

    //show slenderman
    public IEnumerator TriggerThirdEvent()
    {
        Light l = Array.Find(lights, light => light.name == "Point LightJumpScare");

        l.intensity = 2f;
        AudioManager.instance.Play("jumpScare");
        AudioManager.instance.Play("darkSwell");
        slender.transform.position = new Vector3(0, -0.94f, -42f);

        yield return new WaitForSeconds(1f);

        l.intensity = 0.3f;
        Animator ani = l.GetComponent<Animator>();
        ani.enabled = true;
        ani.SetTrigger("startFlicker");

        yield return new WaitForSeconds(0.5f);

        slender.transform.position = new Vector3(0, 3, 0);
    }

    public void TriggerFourthEvent()
    {
        Animator ani = Array.Find(lights, light => light.name == "Point LightJumpScare2").GetComponent<Animator>();
        ani.enabled = true;
        ani.SetTrigger("startFlicker");
        ani = Array.Find(lights, light => light.name == "Point LightJumpScare3").GetComponent<Animator>();
        ani.enabled = true;
        ani.SetTrigger("startFlicker");


    }

    public void TriggerFifthEvent()
    {
        Array.Find(lights, light => light.name == "Point LightEnd").intensity = 2f;
    }

    public void TriggerSixthEvent()
    {
        Array.Find(lights, light => light.name == "Point LightEnd").intensity = 0.3f;
    }

    public IEnumerator TriggerSeventhEvent()
    {
        //door sound
        AudioManager.instance.SetVolume("door", 0.05f);
        AudioManager.instance.Play("door");
        

        yield return new WaitForSeconds(1);

        //light flicker 
        FindObjectOfType<PlayerController>().allowedToMove = false;

        for (int i = 0; i < lights.Length; i++) {
            float rand = UnityEngine.Random.Range(0f, 1f);
            if (rand < 0.6f && lights[i].name == "Point Light") {
                Animator ani = lights[i].GetComponent<Animator>();
                StartCoroutine(EnableFlicker(rand * 2f, ani));
            }
        }
        //violin sound
        AudioManager.instance.Play("violin");
        slender.SetActive(true);

        StartCoroutine(ChangePostProcessing(0.01f, 1f));

        //slender first appearence + light
        slender.transform.rotation = new Quaternion(0, 180, 0, 1);

        SpawnSlender(-60.8f, null, "Point LightS1");

        yield return new WaitForSeconds(3f);

        SpawnSlender(-66.8f, "Point LightS1", "Point LightS2");
        yield return new WaitForSeconds(3f);

        SpawnSlender(-72.8f, "Point LightS2", "Point LightS3");
        yield return new WaitForSeconds(2f);

        SpawnSlender(-78.8f, "Point LightS3", "Point LightS4");

        yield return new WaitForSeconds(1f);

        SpawnSlender(-84.8f, "Point LightS4", "Point LightS5");

        yield return new WaitForSeconds(0.5f);

        SpawnSlender(-87.5f, "Point LightS5", "Point LightEnd");

        yield return new WaitForSeconds(0.5f);

        AudioManager.instance.Play("endSound");

        Vest.VestJumpscare();

        blackScreen.enabled = true;

        Vest.playstate = false;

        yield return new WaitForSeconds(10f);
        AudioManager.instance.StopAll();
    }

    void SpawnSlender(float position, string lastLight, string nextLight)
    {
        if (lastLight != null) {
            Array.Find(lights, light => light.name == lastLight).intensity = 0.3f;
        }
        slender.transform.position = new Vector3(0, -0.94f, position);

        Array.Find(lights, light => light.name == nextLight).intensity = 2f;
    }

    IEnumerator EnableFlicker(float time, Animator ani)
    {
        yield return new WaitForSeconds(time);
        ani.enabled = true;
        ani.SetTrigger("startFlicker");
    }

    IEnumerator ChangePostProcessing(float steps, float value)
    {
        Vignette v;
        volume.profile.TryGetSettings(out v);

        while(v.intensity.value < value) {
            v.intensity.value += steps;
            yield return new WaitForSeconds(0.2f);
        }

    }
}
