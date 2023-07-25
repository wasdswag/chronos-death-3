using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BeatClock : MonoBehaviour
{

    [SerializeField] private GameObject petalPrefab;
    [SerializeField] private float radius;
    [FormerlySerializedAs("_petals")] [SerializeField] private Petal[] petals = new Petal[16];


    [SerializeField] private Text tactDisplay;
    [SerializeField] private Text passDisplay;
    [SerializeField] private Text loopDisplay;

    [SerializeField] Image background;


    private void Start()
    {
        Events.OnCompleteLoop += Reset;
        Events.OnFail += Reset;
        Events.OnPassBall += SetPetal;
        Events.OnPassBall += UpdateDisplay;

        float r = Screen.width / radius;

        float angle = 360f / 16f;
        for(int i = 0; i < 16; i++)
        {
            //var pos = new Vector3(transform.position.x + Mathf.Sin(i * (Mathf.Deg2Rad * angle)) * r, 
            //transform.position.y + Mathf.Cos(i * (Mathf.Deg2Rad * angle) ) * r, 0f);

            var rot = Quaternion.Euler(Vector3.back * i * 22.5f);
            petals[i] = Instantiate(petalPrefab, transform).GetComponent<Petal>();
            petals[i].transform.rotation = rot;

        }
         
        Reset();
    }


    private void SetPetal(Paddle paddle, Color color)
    {
      //  background.fillAmount =  (float)Events.BeatCounter / (float)_petals.Length;
        petals[Events.beatCounter].gameObject.SetActive(true);
        petals[Events.beatCounter].Activate(paddle.HeightIndex + 2, color);
    }




    private void Reset()
    {
        foreach (var petal in petals)
            petal.gameObject.SetActive(false);

        tactDisplay.text = "0";
        passDisplay.text = "0";
    }


    private void UpdateDisplay(Paddle paddle, Color color)
    {
        int loop = Events.loopsCounter + 1;
        int beat = Events.beatCounter + 1;
        int variation = paddle.HeightIndex + 2;

        tactDisplay.text =  beat.ToString();
        passDisplay.text =  variation.ToString();
        loopDisplay.text = loop.ToString();
          //variation > 9 ? variation.ToString() : "0" + variation.ToString();
        passDisplay.color = color;
    }


    private void OnDestroy()
    {
        Events.OnCompleteLoop -= Reset;
        Events.OnFail -= Reset;
        Events.OnPassBall -= SetPetal;
        Events.OnPassBall -= UpdateDisplay;
    }


}
