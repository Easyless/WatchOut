using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {
	

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
	public void Scene0()
	{
		SceneManager.LoadScene("Menu 3D");
	}
	public void Scene1()
    {
		SceneManager.LoadScene("climbing");
	}

	public void Scene2()
	{
		SceneManager.LoadScene("running");
	}

	public void Scene3()
	{
		SceneManager.LoadScene("Manual");
	}

	public void Scene4()
	{
		SceneManager.LoadScene("Manual2");
	}

}
