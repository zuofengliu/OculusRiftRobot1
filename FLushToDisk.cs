using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using DZ.MediaPlayer;
using DZ.MediaPlayer.Vlc;
using DZ.MediaPlayer.Vlc.Io;
using DZ.MediaPlayer.Vlc.Deployment;


public class vlcFactoryPlayer : MonoBehaviour {

	//declare the variables 
	VlcMediaLibraryFactory  factory;
	MediaInput input;
	Player player ;
	PlayerOutput output;
	DirectoryInfo directory ;
	string filenameString;
	
	 void Start () {

			// Use this for initialization
			VlcDeployment deployment = VlcDeployment.Default;
			// install library if it doesn't exist
			if (!deployment.CheckVlcLibraryExistence (false, false)) {
			// install library
					deployment.Install (true);
			}   
			
			// set up the vlcMedialibraryFactory
			string path = Path.Combine ("C:\\Users\\LZF\\Documents\\vlcWindowFromTry\\Assets\\Pulgins", "plugins");
			print (path);
			factory = new VlcMediaLibraryFactory (new string[] { "--reset-config", 
				"--no-snapshot-preview", 
				"--aspect-ratio=16:9", 
				"--ignore-config", 
				"--intf", "rc", 
				"--no-osd", 
				"--plugin-path", path });
			factory.CreateSinglePlayers = true;
			
			//set up the input and start to play
			input = new MediaInput (MediaInputType.File, //@"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");
				                        //@"C:\Users\Public\Music\Sample Music\Sleep Away.mp3"
			                     				 @"C:\Users\LZF\Desktop\1348239491.mov"						);
		  //input = new MediaInput (MediaInputType.NetworkStream,"udp://@141.44.234.220:8008");
			output = new PlayerOutput();
			player = factory.CreatePlayer (output);
			player.SetMediaInput (input);
			player.Play();
			if (player.State == PlayerState.Playing) {
						print ("player is playing");
				}
		                     
		}

		// Update is called once per frame
	void Update () {
		if (player.State != PlayerState.Playing)
		{	player.Play();
			print ("not playing");
		}
		if (player.State == PlayerState.Playing ) {
			try {
				//take snapshot and save it to disk as png, read png according to the file created time
				directory = new DirectoryInfo("C:\\Users\\LZF\\Documents");
				player.TakeSnapshot ("C:\\Users\\LZF\\Documents", 300, 300);
				var filename = directory.GetFiles ("*.png").OrderByDescending (f => f.CreationTime).First ();
				print (filename);		 
				filenameString = filename.ToString ();

				//read the png image frome the disk, put it to the texture
				Bitmap bitmap = new Bitmap (filenameString);
				MemoryStream m = new MemoryStream ();
				bitmap.Save (m, bitmap.RawFormat);
				Texture2D camera = new Texture2D (300, 300);
				camera.LoadImage (m.ToArray ());
				renderer.material.mainTexture = camera;	
			}
				
			catch (Exception ex) {
				print ("there is an exception");	
			}
			print ("outside if");
		}
	}

	void OnGUI()
	{
		if(GUI.Button (new Rect (10, 10, 100, 100), "set one image to texture")){


		//	FileInfo[] files = directory.GetFiles();
		//	FileInfo fileInfo = new FileInfo(@"C:\Users\LZF\Documents\vlcsnap-2015-04-16-11h42m15s243.png"); ;
		//	print (fileInfo.CreationTime );
		//	FileInfo fileInfo = new FileInfo() ;
		//	Array.Sort<File>(file ,dele);
		//	player.Stop ();
		}

		if (GUI.Button (new Rect (200, 10, 100, 100), "SnapShot")) {
			player.TakeSnapshot ("C:\\Users\\LZF\\Documents\\vlcWindowFromTry\\Assets\\Pulgins", 500, 500);
			print ("snapshot taken");
		}
	}

}