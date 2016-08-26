using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class ResourceLoader {

	private static ResourceLoader _instance;
	public static ResourceLoader Instance { 
		get 
		{
			if (_instance == null) {
				_instance = new ResourceLoader();
			}
			return _instance;
		}
	}

	Dictionary<string, UnityEngine.Object> cachedResources = new Dictionary<string, Object>();

	public UnityEngine.Object GetResource(string name) {
		if (cachedResources.ContainsKey(name)) {
			return cachedResources[name];
		} else {
			UnityEngine.Object resource = Resources.Load(name);
			Assert.IsNotNull(resource);
			cachedResources.Add(name, resource);
			return resource;
		}
	}

}