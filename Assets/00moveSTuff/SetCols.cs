using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;


public class SetCols : MonoBehaviour {

	// Use this for initialization
	//public Camera cam;
	public GameObject colorObject;
	public string col1Name;
	public string col2Name;
	public bool skybox=false;

	public string skyboxCol1;
	public string skyboxCol2;

	 public Color reaktC1;
	 public Color reaktC2;

	[SerializeField]
	Color _color1 = Color.blue;

	public Color color1 {
		get { return _color1; }
		set { _color1 = value; }
	}

	public bool reakt=false;
	public bool particles=false;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//,transform.position.y,transform.position.z
		//cam.gameObject.GetComponent<Contour>().lineColor= HSVtoRGB.HSVToRGB(transform.position.x,.8f,.8f);
		float h1=transform.position.y;
		float h2=h1+.33f;
		if(h2>=1f){
			h2-=1f;
		}

		if(skybox){
			RenderSettings.skybox.SetColor(skyboxCol1,HSVtoRGB.HSVToRGB(h1,.8f,.8f));
			RenderSettings.skybox.SetColor(skyboxCol2,HSVtoRGB.HSVToRGB(h2,.8f,.8f));
		}
		else{

		if(reakt){

			colorObject.GetComponent<MeshRenderer>().material.SetColor(col1Name,_color1);
			colorObject.GetComponent<MeshRenderer>().material.SetColor(col2Name,reaktC2);
		}
		else{
		if(!particles){
		colorObject.GetComponent<MeshRenderer>().material.SetColor(col1Name,HSVtoRGB.HSVToRGB(h1,.8f,.8f));
		colorObject.GetComponent<MeshRenderer>().material.SetColor(col2Name,HSVtoRGB.HSVToRGB(h2,.8f,.8f));
		}
		else{
			colorObject.GetComponent<ParticleSystemRenderer>().material.SetColor(col1Name,HSVtoRGB.HSVToRGB(h1,.8f,.8f));
			colorObject.GetComponent<ParticleSystemRenderer>().material.SetColor(col2Name,HSVtoRGB.HSVToRGB(h2,.8f,.8f));
		//_Color_copy _Color
		}
		}
		}
	}
}
