using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Python.Runtime;
using System.Threading;
using HR_Imformation;
using UnityEngine.SceneManagement;

public class Import_HR : MonoBehaviour
{
    bool Using_Thread = true;
    System.IntPtr Macro;
    Thread HR_Thread;
    string DataPath;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Scene1")
        {
            try
            {
                DataPath = Application.dataPath;
                Runtime.PythonDLL = DataPath + "/StreamingAssets/python-3.7.8-embed-amd64/python37.dll";
                PythonEngine.Initialize(mode: ShutdownMode.Soft);
                if (Using_Thread)
                {
                    Macro = PythonEngine.BeginAllowThreads();
                }
            }
            catch
            {
                Debug.Log("Failed0");
            }

            if (Using_Thread)
            {
                HR_Thread = new Thread(Init_HR_PYD);
                HR_Thread.Start();
            }
            else
            {
                Init_HR_PYD();
            }
        }
        
    }


    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit_Flag = true;
            if (PythonEngine.IsInitialized)
            {
                HR_Thread.Abort();
                PythonEngine.EndAllowThreads(Macro);
                PythonEngine.Shutdown();
            }
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        */
    }


    double[] _Test = new double[21 * 3];
    void Init_HR_PYD()
    {
        
        using (Py.GIL())
        {
            try
            {
                dynamic sys = PyModule.Import("sys");
                //Debug.Log(DataPath + "/Plugins/PYD");
                //sys.path.append(DataPath + "/Plugins/PYD");
                sys.path.append(DataPath + "/StreamingAssets/HR");
                dynamic np = Py.Import("HR_PYD");
                if (Using_Thread)
                {
                    Calculating(np);
                }
                else
                {
                    StartCoroutine(Updating_HR(np));
                }
            }
            catch
            {
                Debug.Log("Failed1");
            }
        }
    }

    bool Exit_Flag = false;
    void Calculating(dynamic _np)
    {
        while (!Exit_Flag)
        {
            lock (HR.Pos)
            {
                _np.Calculate_Hand();
                HR.Set_Coords((double[])_np.Get_Coords());
            }
        }
    }

    IEnumerator Updating_HR(dynamic _np)
    {
        while (!Exit_Flag)
        {
            yield return new WaitForSeconds(0.05f);
            _np.Calculate_Hand();
            HR.Set_Coords((double[])_np.Get_Coords());
        }
    }

    public void OnApplicationQuit()
    {
        if (PythonEngine.IsInitialized)
        {
            Exit_Flag = true;
            if (PythonEngine.IsInitialized)
            {
                HR_Thread.Abort();
                PythonEngine.EndAllowThreads(Macro);
                PythonEngine.Shutdown();
            }
        }
    }

    public void ExitPlayMode()
    {
        Exit_Flag = true;
        if (PythonEngine.IsInitialized)
        {
            HR_Thread.Abort();
            PythonEngine.EndAllowThreads(Macro);
            PythonEngine.Shutdown();
        }
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
