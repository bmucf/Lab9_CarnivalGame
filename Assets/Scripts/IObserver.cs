using UnityEngine;

public interface IObserver
{
    void OnNotify(Target target);
}

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void UnregisterObserver(IObserver observer);
    void NotifyObservers();

}
