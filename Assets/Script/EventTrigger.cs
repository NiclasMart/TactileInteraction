using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTrigger : MonoBehaviour
{
    bool hasBeenTrigger = false;
    public TriggerVest Vest;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenTrigger) {

            string name = transform.name;

            switch (name) {
                case "Trigger1":
                    //TODO Paul Weste Start
                    Vest.timeLapsDamping -= 0.5f;
                    EventController.instance.TriggerFirstEvent();
                    break;
                case "Trigger2":
                    Vest.timeLapsDamping += 0.25f;
                    StartCoroutine(EventController.instance.TriggerSecondEvent());
                    break;
                case "Trigger3":
                    Vest.timeLapsDamping -= 0.5f;
                    Vest.VestJumpscare();
                    StartCoroutine(EventController.instance.TriggerThirdEvent());
                    break;
                case "Trigger4":
                    Vest.timeLapsDamping += 0.25f;
                    EventController.instance.TriggerFourthEvent();
                    break;
                case "Trigger5":
                    Vest.timeLapsDamping += 0.25f;
                    EventController.instance.TriggerFifthEvent();
                    break;
                case "Trigger6":
                    Vest.timeLapsDamping += 0.5f;
                    EventController.instance.TriggerSixthEvent();
                    break;
                case "Trigger7":
                    Vest.timeLapsDamping -= 1f;
                    StartCoroutine(EventController.instance.TriggerSeventhEvent());
                    break;
            }
            hasBeenTrigger = true;
        }
        
    }
}
