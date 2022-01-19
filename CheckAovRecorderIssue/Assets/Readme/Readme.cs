using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Readme : ScriptableObject {
	public Texture2D               icon;
	public string                  title;
	public Section[]               sections;
	public PackageInstallSection[] optionalPackages;
	public Scene[]                 scenes;
	
	
	[Serializable]
	public class Section {
		public string heading;
		public string text;
		public string linkText;
		public string url;
	}
	

	[Serializable]
	public class PackageInstallSection : Section {
		public string installText;
		public string installURL;
	}
	
}
