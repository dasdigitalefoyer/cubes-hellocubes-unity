using System.Collections;
using System.Collections.Generic;
using PuzzleCubes.Controller;
using PuzzleCubes.Models;
using UnityEngine;

public class HelloCubesAppController : AppController
{

	public FloatEvent orientationEvent;

    public void HandleHelloCubes(HelloCubes helloCubes)
    {
        Debug.Log(helloCubes.Message);
    }

    public void HandleCubeControl(CubeControl cubeControl) {
			if (cubeControl == null) return;

		
			
			orientationEvent?.Invoke(cubeControl.Orientation.GetValueOrDefault());
			
		}

    
}
