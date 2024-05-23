using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance {  get; private set; }
    [Header("Internal CLock")]
    [SerializeField]
    GameTimestamp timestamp;

    public float timeScale = 1.0f;


    [Header("Day and Night cycle")]
    public Transform sunTransform;


    List<ITimeTracker> listeners = new List<ITimeTracker>();

    private void Awake()
    {

        //Si hay más de una instancia, destruye la otra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Establece la estancia estatica
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        timestamp = new GameTimestamp(0, GameTimestamp.Season.Primavera, 1, 6, 0);
        StartCoroutine(TimeUpdate());
        
    }

    public void LoadTime(GameTimestamp timestamp)
    {
        this.timestamp = new GameTimestamp(timestamp);
    }
    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1/timeScale);
           
        }
        
        
       
    }

    // Update is called once per frame
    public void Tick()
    {
        timestamp.UpdateClock();    
        foreach(ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }
        UpdateSunMovement();
        
    }

    public void SkipTime (GameTimestamp timeToSkipTo)
    {
        int timeToSkipInMinutes = GameTimestamp.TimestampInMinutes(timeToSkipTo);
        Debug.Log("Tiem to skip To" + timeToSkipInMinutes);
        int timeNowInMinutes = GameTimestamp.TimestampInMinutes(timestamp);
        Debug.Log("Time now " + timeNowInMinutes);

        int diferenceInMinutes = timeToSkipInMinutes - timeNowInMinutes;
        Debug.Log(diferenceInMinutes + "minutes will be advanced");

        if (diferenceInMinutes <= 0) return;

        for (int i = 0; i < diferenceInMinutes; i++)
        {
            Tick();
        }
    }

    void UpdateSunMovement()
    {
        int timeInMinutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;
        float sunAngle = .25f * timeInMinutes - 90;
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    public GameTimestamp GetGameTimestamp()
    {
        return new GameTimestamp(timestamp);
    }



    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener); 
    }

   
}
