using UnityEngine;
using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

using DZ.MediaPlayer.Vlc.Io;
using DZ.MediaPlayer.Vlc.Deployment;
using DZ.MediaPlayer.Vlc.WindowsForms;


public class NewBehaviourScript : MonoBehaviour {

		//declare variables
		VlcPlayerControl vlcPlayerControl;
		VlcPlayerControl vlcPlayerControl2;
		
		MediaInput input;
		MediaInput input2;

		Bitmap bitmap;
		System.Drawing.Rectangle rec;
	
		public void Start () {
			// Use this for initialization
			VlcDeployment deployment = VlcDeployment.Default;
			
			// install library if it doesn't exist
			if (!deployment.CheckVlcLibraryExistence(false, false))
			{
				// install library
				deployment.Install(true);
			}   
				
			// set up vlcPlayerControl and play it
				vlcPlayerControl2 = new VlcPlayerControl ();
				vlcPlayerControl2.Initialize();
				vlcPlayerControl2.Visible = true;
					input2 = new MediaInput (MediaInputType.File, //@"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");
				//@"C:\Users\Public\Music\Sample Music\Sleep Away.mp3"
						@"C:\Users\LZF\Desktop\1348239491.mov");
				//	input2 = new MediaInput (MediaInputType.NetworkStream,"udp://@141.44.234.220:8008");
				vlcPlayerControl2.Play(input2); 
				
				//display the video in winforms
				Form form2 = new Form ();
				this.vlcPlayerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
				this.vlcPlayerControl2.Location = new System.Drawing.Point(0, 0);
				this.vlcPlayerControl2.Size = new System.Drawing.Size(426, 319);
				form2.Controls.Add (vlcPlayerControl2);
				this.vlcPlayerControl2.Time = System.TimeSpan.Parse("00:00:00");
				form2.Show ();

				
				bitmap = new Bitmap(vlcPlayerControl2.Width,vlcPlayerControl2.Height);
				rec = new Rectangle();
	}	
	// Update is called once per frame
		void Update () 
			{

				//save the bitmap to MemoryStream then feed it to texture
				try{	
					vlcPlayerControl2.DrawToBitmap(bitmap,rec);
					print(bitmap.GetPixel (25,25));
				//	bitmap.Save ("C:\\Users\\LZF\\Documents\\bitmapSnapshot",System.Drawing.Imaging.ImageFormat.Png);
			        MemoryStream m = new MemoryStream();
			        bitmap.Save(m,bitmap.RawFormat);
					Texture2D camera = new Texture2D(426, 319);
					camera.LoadImage(m.ToArray());
					renderer.material.mainTexture = camera;	
					
				}
				
				catch (Exception ex)
				{
					print ("there is an exception");
				}
	/*
			if (Input.GetKey (KeyCode.UpArrow))
				{
					vlcPlayerControl.Play (input);
					Debug.Log ("A UpArrow click has been detected");
				}
			
			if (Input.GetKey(KeyCode.DownArrow))
				{
					vlcPlayerControl.Stop();
					Debug.Log ("A DownArrow click has been detected");
			
				}
	*/

			}


	/*
		void OnGUI()
		{
				if(GUI.Button (new Rect (10, 100, 50, 50), "Draw bitmap to texture"))
				{	

				}

			if(GUI.Button (new Rect (10, 10, 50, 50), "Play the video in winform"))
				{
				//	vlcPlayerControl.Stop ();
				vlcPlayerControl = new VlcPlayerControl ();
				vlcPlayerControl.Initialize();
				vlcPlayerControl.Visible = true;
			//	input = new MediaInput (MediaInputType.File, //@"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");
				                        //@"C:\Users\Public\Music\Sample Music\Sleep Away.mp3"
			//                       				 @"C:\Users\LZF\Desktop\1348239491.mov"						);
				
				input = new MediaInput (MediaInputType.NetworkStream,"udp://@141.44.234.220:8008");

				vlcPlayerControl.Play(input); 
				//if (Input.GetKey(KeyCode.UpArrow))
				Form form1 = new Form ();
				this.vlcPlayerControl.Dock = System.Windows.Forms.DockStyle.Fill;
				this.vlcPlayerControl.Location = new System.Drawing.Point(0, 0);
				this.vlcPlayerControl.Size = new System.Drawing.Size(426, 319);
				form1.Controls.Add (vlcPlayerControl);
				this.vlcPlayerControl.Time = System.TimeSpan.Parse("00:00:00");

				form1.Show ();
				}

		

			if (GUI.Button (new Rect (100, 100, 50, 50), "Test"))
				{
				
						//	vlcPlayerControl.Stop ();
						vlcPlayerControl2 = new VlcPlayerControl ();
						vlcPlayerControl2.Initialize ();
						vlcPlayerControl2.Visible = true;
						input2 = new MediaInput (MediaInputType.File, @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");
						//@"C:\Users\Public\Music\Sample Music\Sleep Away.mp3"
						// @"C:\Users\LZF\Desktop\1348239491.mov"						);

						vlcPlayerControl2.Play (input2); 

			     
					
				
		}

		

			if(GUI.Button (new Rect(500,50,50,50),"stop all video")){
					if (vlcPlayerControl2.State == VlcPlayerControlState.Playing){
						vlcPlayerControl2.Stop ();
						print("video2 stoped");
					}
					if (vlcPlayerControl.State == VlcPlayerControlState.Playing){
						vlcPlayerControl.Stop ();
						print("video1 stoped");
						}
			}
					
				
 		}
		

		void OnDisable() { 
		//	if (vlcPlayerControl.State == VlcPlayerControlState.Playing){
		//		vlcPlayerControl.Stop ();
		//		}
		//	if (vlcPlayerControl2.State == VlcPlayerControlState.Playing){
				//vlcPlayerControl2.Stop ();
		//		}
		}


		*/


}