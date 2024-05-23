using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    public enum Location { BuyScene, FarmXC, SellScene, }
    public Location currentLocation;

    Transform playerPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnLocationLoad;

        playerPoint = FindObjectOfType<PlayerScript>().transform;
    }
    public void SwitchLocation(Location locationToSwitch)
    {
        SceneManager.LoadScene(locationToSwitch.ToString());

    }

    public void OnLocationLoad(Scene scene, LoadSceneMode mode)
    {
        Location oldLocation = currentLocation;
        Location newLocation = (Location)Enum.Parse(typeof(Location), scene.name);

        if (currentLocation == newLocation) return;
        Transform startPoint = LocationManager.Instance.GetPlayerStartingPosition(oldLocation);
        CharacterController playerCaracter = playerPoint.GetComponent<CharacterController>();
        playerCaracter.enabled = false;

        playerPoint.position = startPoint.position;
        playerPoint.rotation = startPoint.rotation;

        playerCaracter.enabled = true;

        currentLocation = newLocation;
    }
}

