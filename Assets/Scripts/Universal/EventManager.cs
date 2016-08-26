using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
	public delegate void Handler(GameEvent e);
}

public class EventManager
{
	static EventManager instanceInternal = null;
	public static EventManager instance
	{
		get
		{
			if (instanceInternal == null)
			{
				instanceInternal = new EventManager();
			}

			return instanceInternal;
		}
	}

	// Here is the core of the system: a dictionary that maps Type (specifically Event types in this case)
	// to Event.Handlers
	public delegate void EventDelegate<T> (T e) where T : GameEvent;
	private delegate void EventDelegate (GameEvent e);

	private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
	private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

	// This is where you can add handlers for events. We use generics for 2 reasons:
	// 1. Passing around Type objects can be tedious and verbose
	// 2. Using generics allows us to add a little type safety, by getting
	// the compiler to ensure we're registering for an Event type and not
	// some other random type
	public void AddListener<T> (EventDelegate<T> del) where T : GameEvent
	{	
		// Early-out if we've already registered this delegate
		if (delegateLookup.ContainsKey(del))
			return;

		// Create a new non-generic delegate which calls our generic one.
		// This is the delegate we actually invoke.
		EventDelegate internalDelegate = (e) => del((T)e);
		delegateLookup[del] = internalDelegate;

		EventDelegate tempDel;
		if (delegates.TryGetValue(typeof(T), out tempDel))
		{
			delegates[typeof(T)] = tempDel += internalDelegate; 
		}
		else
		{
			delegates[typeof(T)] = internalDelegate;
		}
//		Debug.Log(del+" added");
	}

	public void RemoveListener<T> (EventDelegate<T> del) where T : GameEvent
	{
		EventDelegate internalDelegate;
		if (delegateLookup.TryGetValue(del, out internalDelegate))
		{
			EventDelegate tempDel;
			if (delegates.TryGetValue(typeof(T), out tempDel))
			{
				tempDel -= internalDelegate;
				if (tempDel == null)
				{
					delegates.Remove(typeof(T));
				}
				else
				{
					delegates[typeof(T)] = tempDel;
				}
			}

			delegateLookup.Remove(del);
		}
	}

	public void Raise (GameEvent e)
	{
	//	Debug.Log(e+" raised");
		EventDelegate del;
		if (delegates.TryGetValue(e.GetType(), out del))
		{
			del.Invoke(e);
		}
	}

	List<GameEvent> queuedEvents = new List<GameEvent>();
	public void QueueEvent(GameEvent e) {
		// insert at the head of the line since the
		// queue will be processed in reverse order
		queuedEvents.Insert(0, e);

		// NOTE: To my knowledge this method makes 
		// NO guarantees regarding thread safety and you
		// should not use this with multiple threads 
	}

	public void ProcessQueuedEvents() {
		// NOTE: processing the queue in reverse order to 
		// avoid a concurrent modification exception if 
		// an event causes another event to be queued
		// while processing the events
		for (int i = queuedEvents.Count - 1; i >= 0; --i) {
			Raise(queuedEvents[i]);
			queuedEvents.RemoveAt(i);
		}
	}

}

