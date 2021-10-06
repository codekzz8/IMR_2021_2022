using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System;

public class AR_Manager : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager imageManager;
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private float distanceThreshhold = 0.3f;
    private List<Tuple<GameObject, float>> cactusList = new List<Tuple<GameObject, float>>();
    [SerializeField]
    private Animator cactusAnimator;

    void OnEnable() => imageManager.trackedImagesChanged += OnChanged;
    void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;
    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        distanceText.text = "";
        foreach (var newImage in eventArgs.added)
        {
            cactusList.Add(Tuple.Create(newImage.gameObject, newImage.transform.position.x));
        }
        int index;
        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            index = cactusList.FindIndex(tuple => tuple.Item1.Equals(updatedImage.gameObject));
            cactusList[index] = Tuple.Create(updatedImage.gameObject, updatedImage.transform.position.x);
            distanceText.text += $"Image: {updatedImage.referenceImage.name} is at {updatedImage.transform.position}" + "\n";
        }

        if (Mathf.Abs(cactusList[0].Item2 - cactusList[1].Item2) <= distanceThreshhold)
        {
            distanceText.text += "Fight Animation!";
            cactusAnimator.SetBool("isMinDistance", true);
        }
        else
        {
            distanceText.text += "Idle Animation!";
            cactusAnimator.SetBool("isMinDistance", true);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }
}
