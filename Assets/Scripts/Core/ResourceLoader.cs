using UnityEngine;

public static class ResourceLoader
{
	public static T Load<T>(string path, string resourceName, string resourceFormat) where T: Object
	{
		Log.Message($"Загрузка ресурса {resourceName}({typeof(T)}) по пути {path}.");
		
		var fullPath = path + resourceName + resourceFormat;
		T loadedResource = Resources.Load<T>(fullPath);

		if (loadedResource == null)
		{
			Log.Warning($"Ресурс по пути {fullPath} отсутствует!");
			
			return null;
		}

		return loadedResource;
	}
}
