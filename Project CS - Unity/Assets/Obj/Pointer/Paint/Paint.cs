using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HR_Imformation;
using Management;
using MLNet;

public class Paint : MonoBehaviour
{
    public bool Is_Painting = false;
    [SerializeField] Camera Conjuring_Camera;
    Spell_Case SpC;
    Mana Mn;
    Particle Particle;
    float Starting_Time, Ended_Time;
    /// <summary>
    /// z is painted time
    /// </summary>
    List<Vector3> Paint_Points;
    List<string> Paint_Info;



    void Start()
    {
        Particle = GameObject.Find("Particle_Trail").GetComponent<Particle>();
        SpC = GameObject.Find("Spell_Case").GetComponent<Spell_Case>();
        Mn = GameObject.Find("Mana").GetComponent<Mana>();
        Particle.GetComponent<Particle>().Stop_Particle();
        /*
        Color _Temp = GetComponent<SpriteRenderer>().color;
        _Temp.a = 1;
        GetComponent<SpriteRenderer>().color = _Temp;
        */
        Paint_Points = new List<Vector3>();
        Paint_Info = new List<string>();
        //StartCoroutine(Follow_Index_Finger_For_Data());

        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Classification = "Circle";
            Debug.Log("Circle!!!");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Classification = "X";
            Debug.Log("X!!!");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Classification = "l";
            Debug.Log("l!!!");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            File_Manager.File_Out("DataSet_1.txt", Get_PaintInfo());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Enable_Painting = !Enable_Painting;
        }
        //transform.position += Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime + Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime;
    }

    bool Enable_Painting = false;
    private void FixedUpdate()
    {
        if (!Enable_Painting)
        {
            if (Particle.Is_Playing)
            {
                Particle.Stop_Particle();
                Particle.Is_Playing = false;
            }
            Is_Painting = false;
            Paint_Points = new List<Vector3>();
        }
            
        //Debug.Log("8: " + HR.Pos[8].z + ", 0: " + HR.Pos[0].z);
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Enable_Painting = !Enable_Painting;
            if (!Enable_Painting)
            {
                if (Particle.Is_Playing)
                {
                    Particle.Stop_Particle();
                    Particle.Is_Playing = false;
                }
                Is_Painting = false;
                Paint_Points = new List<Vector3>();
                //Color _Temp = GetComponent<SpriteRenderer>().color;
                //_Temp.a = 0;
                //GetComponent<SpriteRenderer>().color = _Temp;
            }
            else
            {
                //Color _Temp = GetComponent<SpriteRenderer>().color;
                //_Temp.a = 1;
                //GetComponent<SpriteRenderer>().color = _Temp;
            }
        }
        */
        if (Enable_Painting)
        {
            Follow_Index_Finger();
        }
        else
        {
            Follow_Hand();
        }
        /*
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Particle.Is_Playing)
            {
                Particle.Stop_Particle();
                Particle.Is_Playing = false;
            }
            Is_Painting = false;
            Paint_Points = new List<Vector3>();
            Color _Temp = GetComponent<SpriteRenderer>().color;
            _Temp.a = 0;
            GetComponent<SpriteRenderer>().color = _Temp;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Color _Temp = GetComponent<SpriteRenderer>().color;
            _Temp.a = 1;
            GetComponent<SpriteRenderer>().color = _Temp;
        }
        */

    }

    void Follow_Hand()
    {
        Vector3 _Temp = HR.Get_Hand_World_Pos();
        _Temp.z = transform.position.z;
        transform.position = _Temp;
    }

    void Follow_Index_Finger()
    {
        if (HR.Check_Hand_State(Hand_State.One))
        {
            if (Mn.Amount <= 5)
            {
                Ended_Time = Time.time;

                if (Is_Painting)
                {
                    if (Particle.Is_Playing)
                    {
                        Particle.Stop_Particle();
                        Particle.Is_Playing = false;
                    }
                    Is_Painting = false;
                    Debug.Log("Predict!");
                    //Paint_Spell(GetComponent<TR>().Prediction(Get_PaintSample()));
                    Paint_Points = new List<Vector3>();
                }
            }

            Stay_Fist_Time -= 2;
            Stay_Fist_Time = Stay_Fist_Time < 0 ? 0 : Stay_Fist_Time;
            if (!Is_Painting && Stay_Fist_Time < 6)
            {
                Is_Painting = true;
                //Conjuring_Camera.clearFlags = CameraClearFlags.Nothing;
                Starting_Time = Time.time;
            }
            if (!Particle.Is_Playing)
            {
                Particle.Play_Particle();
                Particle.Is_Playing = true;
            }
            Vector3 _Temp = HR.Index_Nodes[3].World_Pos;
            _Temp.z = transform.position.z;
            transform.position = _Temp;
            Paint_Points.Add(new Vector3(HR.Index_Nodes[3].Screen_Pos.x, HR.Index_Nodes[3].Screen_Pos.y, Time.time));
            if (Paint_Points.Count > 2)
            {
                Use_Mana(Paint_Points[Paint_Points.Count - 1], Paint_Points[Paint_Points.Count - 2]);
            }

        }
        else
        {
            if (HR.Check_Hand_State(Hand_State.Fist))
            {
                Ended_Time = Time.time;
                Stay_Fist_Time++;
                Stay_Fist_Time = Stay_Fist_Time >= 7 ? 7 : Stay_Fist_Time;
                if (Is_Painting && Stay_Fist_Time >= 6)
                {
                    if (Particle.Is_Playing)
                    {
                        Particle.Stop_Particle();
                        Particle.Is_Playing = false;
                    }
                    //transform.position += Vector3.right * 10000;
                    Is_Painting = false;
                    //Conjuring_Camera.clearFlags = CameraClearFlags.SolidColor;
                    //Write_PaintInfo();
                    Debug.Log("Predict!");
                    Paint_Spell(GetComponent<TR>().Prediction(Get_PaintSample()));
                    //GetComponent<TR>().Prediction("1, 0.9686731, 0.9523479, 1, 0.7914791, 0.9324815, 0.6462393, 0.8768992, 0.599112, 0.6803841, 0.5144461, 0.4710103, 0.3762567, 0.3833182, 0.1191066, 0.2305107, 0.2839159, 0.1370896, 0, 0.08328822, 0.667127, 0.09760185, 0.5296371, 0, 0.4541819, 0.02794142, 0.5407243, 0.04053071, 0.6175475, 0.03260385, 0.6623191, 0.02975081, 0.7009506, 0.03131289, 0.7453511, 0.02164938, 0.7917601, 0.05585627, 0.7917601, 0.05585627, 0.667127, 0.09760185");
                    Paint_Points = new List<Vector3>();
                }
            }
            else if (HR.Check_Hand_State(Hand_State.Partal_Below))
            {
                if (Is_Painting)
                {
                    Vector3 _Temp = HR.Index_Nodes[3].World_Pos;
                    _Temp.z = transform.position.z;
                    transform.position = _Temp;
                    Paint_Points.Add(new Vector3(HR.Index_Nodes[3].Screen_Pos.x, HR.Index_Nodes[3].Screen_Pos.y, Time.time));
                    if (Paint_Points.Count > 2)
                    {
                        Use_Mana(Paint_Points[Paint_Points.Count - 1], Paint_Points[Paint_Points.Count - 2]);
                    }
                }
            }
            else
            {
                if (Particle.Is_Playing)
                {
                    //Particle.Stop_Particle();
                    //Particle.Is_Playing = false;
                }
            }
        }
    }


    void Paint_Spell(string _Name)
    {
        switch (_Name)
        {
            case "Ball":
                SpC.Creating_Spell(1);
                break;
            case "Lighting":
                SpC.Creating_Spell(0);
                break;
            case "Tornado":
                SpC.Creating_Spell(2);
                break;
        }
    }

    [SerializeField]int Stay_Fist_Time = 0; 
    IEnumerator Follow_Index_Finger_For_Data()
    {
        while (true)
        {
            if (HR.Check_Hand_State(Hand_State.One))
            {
                Stay_Fist_Time -= 2;
                Stay_Fist_Time = Stay_Fist_Time < 0 ? 0 : Stay_Fist_Time;
                if (!Is_Painting && Stay_Fist_Time < 6)
                {
                    Is_Painting = true;
                    //Conjuring_Camera.clearFlags = CameraClearFlags.Nothing;
                    Starting_Time = Time.time;
                }
                Vector3 _Temp = HR.Index_Nodes[3].World_Pos;
                Paint_Points.Add(new Vector3(HR.Index_Nodes[3].Screen_Pos.x, HR.Index_Nodes[3].Screen_Pos.y, Time.time));
                _Temp.z = transform.position.z;
                transform.position = _Temp;
            }
            else
            {
                if (HR.Check_Hand_State(Hand_State.Fist))
                {
                    Ended_Time = Time.time;
                    Stay_Fist_Time++;
                    Stay_Fist_Time = Stay_Fist_Time >= 7 ? 7 : Stay_Fist_Time;
                    if (Is_Painting && Stay_Fist_Time >= 6)
                    {
                        int _Confirmed = 0;
                        while (_Confirmed == 0)
                        {
                            if (Input.GetKeyDown(KeyCode.C))
                            {
                                _Confirmed = 1;
                            }
                            else if (Input.GetKeyDown(KeyCode.D))
                            {
                                _Confirmed = -1;
                            }
                            yield return new WaitForFixedUpdate();
                        }
                        if (_Confirmed == -1)
                        {
                            Stay_Fist_Time -= 2;
                            Stay_Fist_Time = Stay_Fist_Time < 0 ? 0 : Stay_Fist_Time;
                            continue;
                        }
                        //transform.position += Vector3.right * 10000;
                        Is_Painting = false;
                        //Conjuring_Camera.clearFlags = CameraClearFlags.SolidColor;
                        Write_PaintInfo();
                        Paint_Points = new List<Vector3>();
                        File_Index++;
                    }
                }
                else if (HR.Check_Hand_State(Hand_State.Partal_Below))
                {
                    if (Is_Painting)
                    {
                        Vector3 _Temp = HR.Index_Nodes[3].World_Pos;
                        Paint_Points.Add(new Vector3(HR.Index_Nodes[3].Screen_Pos.x, HR.Index_Nodes[3].Screen_Pos.y, Time.time));
                        _Temp.z = transform.position.z;
                        transform.position = _Temp;
                    }
                }
                else
                {
                    //transform.position += Vector3.right * 10000;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    
    void Use_Mana(Vector3 _VecA, Vector3 _VecB)
    {
        Mn.Reduce_Mana(Vector3.Distance(_VecA, _VecB) * 0.033f);
    }

    string Get_PaintSample()
    {
        float _Duration = Ended_Time - Starting_Time;
        string _Line = "";
        float Max_x = float.MinValue;
        float Min_x = float.MaxValue;
        float Max_y = float.MinValue;
        float Min_y = float.MaxValue;

        for (int i = 0; i < Paint_Points.Count; i++)
        {
            if (Paint_Points[i].x > Max_x)
            {
                Max_x = Paint_Points[i].x;
            }
            if (Paint_Points[i].x < Min_x)
            {
                Min_x = Paint_Points[i].x;
            }
        }
        for (int i = 0; i < Paint_Points.Count; i++)
        {
            if (Paint_Points[i].y > Max_y)
            {
                Max_y = Paint_Points[i].y;
            }
            if (Paint_Points[i].y < Min_y)
            {
                Min_y = Paint_Points[i].y;
            }
        }

        for (int i = 0; i <= 100; i += 5)
        {
            for (int j = 0; j < Paint_Points.Count - 1; j++)
            {
                float _P = (Paint_Points[j].z - Starting_Time) / _Duration * 100;
                float _P2 = (Paint_Points[j + 1].z - Starting_Time) / _Duration * 100;
                if (j == 0 && i == 0)
                {
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].x - Min_x) / (Max_x - Min_x));
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].y - Min_y) / (Max_y - Min_y));
                    break;
                }
                else if (_P < i && _P2 > i)
                {
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].x - Min_x) / (Max_x - Min_x));
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].y - Min_y) / (Max_y - Min_y));
                    break;
                }
                else if (j == Paint_Points.Count - 2)
                {
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[Paint_Points.Count - 1].x - Min_x) / (Max_x - Min_x));
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[Paint_Points.Count - 1].y - Min_y) / (Max_y - Min_y));
                    break;
                }
            }
        }
        return _Line;
    }

    List<string> Out_Put_CSV = new List<string>();
    void Write_PaintInfo()
    {
        float _Duration = Ended_Time - Starting_Time;
        string _Line = "";

        float Max_x = float.MinValue;
        float Min_x = float.MaxValue;
        float Max_y = float.MinValue;
        float Min_y = float.MaxValue;

        for (int i = 0; i < Paint_Points.Count; i++)
        {
            if (Paint_Points[i].x > Max_x)
            {
                Max_x = Paint_Points[i].x;
            }
            if (Paint_Points[i].x < Min_x)
            {
                Min_x = Paint_Points[i].x;
            }
        }
        for (int i = 0; i < Paint_Points.Count; i++)
        {
            if (Paint_Points[i].y > Max_y)
            {
                Max_y = Paint_Points[i].y;
            }
            if (Paint_Points[i].y < Min_y)
            {
                Min_y = Paint_Points[i].y;
            }
        }

        for (int i = 0; i <= 100; i += 5)
        {
            for (int j = 0; j < Paint_Points.Count - 1; j++)
            {
                float _P = (Paint_Points[j].z - Starting_Time) / _Duration * 100;
                float _P2 = (Paint_Points[j + 1].z - Starting_Time) / _Duration * 100;
                if (j == 0 && i == 0)
                {
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].x - Min_x) / (Max_x - Min_x));
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].y - Min_y) / (Max_y - Min_y));
                    break;
                }
                else if (_P < i && _P2 > i)
                {
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].x - Min_x) / (Max_x - Min_x));
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[j].y - Min_y) / (Max_y - Min_y));
                    break;
                }
                else if (j == Paint_Points.Count - 2)
                {
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[Paint_Points.Count - 1].x - Min_x) / (Max_x - Min_x));
                    _Line = File_Manager.Add_to_Line(_Line, (Paint_Points[Paint_Points.Count - 1].y - Min_y) / (Max_y - Min_y));
                    break;
                }
            }
        }
        _Line = File_Manager.Add_to_Line(_Line, Classification);
        Out_Put_CSV.Add(_Line);
        Debug.Log("OK!");
    }

    List<string> Get_PaintInfo()
    {
        string _Line = "";
        for (int i = 0; i <= 100; i += 5)
        {
            _Line = File_Manager.Add_to_Line(_Line, "X" + i);
            _Line = File_Manager.Add_to_Line(_Line, "Y" + i);
        }
        _Line = File_Manager.Add_to_Line(_Line, "Symbol");
        List<string> _Temp = new List<string>();
        _Temp.Add(_Line);
        _Temp.AddRange(Out_Put_CSV);
        return _Temp;
    }

    int File_Index;
    string Classification = "Tornado";
    List<string> Get_PaintInfo_Old()
    {
        List<string> _Info = new List<string>();
        string _Line, _Line2;
        /*
        _Line = "Starting_Time";
        File_Manager.Add_to_Line(_Line, Starting_Time);
        _Info.Add(_Line);

        _Line = "Ended_Time";
        File_Manager.Add_to_Line(_Line, Ended_Time);
        _Info.Add(_Line);
        */
        float _Duration = Ended_Time - Starting_Time;
        float Max_x = float.MinValue;
        float Min_x = float.MaxValue;
        float Max_y = float.MinValue;
        float Min_y = float.MaxValue;

        for (int i = 0; i < Paint_Points.Count; i++)
        {
            if (Paint_Points[i].x > Max_x)
            {
                Max_x = Paint_Points[i].x;
            }
            if (Paint_Points[i].x < Min_x)
            {
                Min_x = Paint_Points[i].x;
            }
        }
        for (int i = 0; i < Paint_Points.Count; i++)
        {
            if (Paint_Points[i].y > Max_y)
            {
                Max_y = Paint_Points[i].y;
            }
            if (Paint_Points[i].y < Min_y)
            {
                Min_y = Paint_Points[i].y;
            }
        }

        _Line = "X";
        _Line2 = "Y";
        for (int i = 0; i <= 100; i += 5)
        {
            for (int j = 0; j < Paint_Points.Count - 1; j++)
            {
                float _P = (Paint_Points[j].z - Starting_Time) / _Duration * 100;
                float _P2 = (Paint_Points[j + 1].z - Starting_Time) / _Duration * 100;
                if (j == Paint_Points.Count - 2 && i == 100)
                {
                    _Line = File_Manager.Add_to_Line(_Line, Paint_Points[Paint_Points.Count - 1].x);
                    _Line2 = File_Manager.Add_to_Line(_Line2, Paint_Points[Paint_Points.Count - 1].y);
                }
                else if (_P < i && _P2 > i)
                {
                    _Line = File_Manager.Add_to_Line(_Line, Paint_Points[j].x);
                    _Line2 = File_Manager.Add_to_Line(_Line2, Paint_Points[j].y);
                }
            }
        }
        /*
        for(int i = 0; i < Paint_Points.Count; i++)
        {
            File_Manager.Add_to_Line(_Line, Paint_Points[i].x);
            if(Paint_Points[i].x > Max_x)
            {
                Max_x = Paint_Points[i].x;
            }
            if (Paint_Points[i].x < Min_x)
            {
                Max_x = Paint_Points[i].x;
            }
        }
        */
        _Info.Add(_Line);
        _Info.Add(_Line2);

        return _Info;
        /*
        _Line = "Normalized_X";
        for (int i = 0; i < Paint_Points.Count; i++)
        {
            File_Manager.Add_to_Line(_Line, Paint_Points[i].x / Max_x - Min_x);
        }
        _Info.Add(_Line);

        _Line = "Normalized_Y";
        for (int i = 0; i < Paint_Points.Count; i++)
        {
            File_Manager.Add_to_Line(_Line, Paint_Points[i].y / Max_y - Min_y);
        }
        _Info.Add(_Line);
        */

    }
}
