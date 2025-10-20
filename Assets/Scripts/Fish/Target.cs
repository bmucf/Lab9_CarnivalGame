using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Target : MonoBehaviour, ISubject
{
    public virtual int pointValue => 0;
    public virtual float speed => 5f;
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
            observer.OnNotify(this);
    }

    public virtual void OnHit()
    {
        // Called when bullet hits this target
        NotifyObservers();
        GetComponent<SpriteRenderer>().color = Color.red; // Visual feedback
    }
}
