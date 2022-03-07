using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HR_Imformation
{
    public static class HR
    {
        public static Vector3 Index_PrePos = Vector3.zero;
        public static List<Vector3> Pos { get; set; }
        public static List<Hand_Node> Hand_Nodes { get; set; }
        public static List<Hand_Node> Thumb_Nodes { get; set; }
        public static List<Hand_Node> Index_Nodes { get; set; }
        public static List<Hand_Node> Middle_Nodes { get; set; }
        public static List<Hand_Node> Ring_Nodes { get; set; }
        public static List<Hand_Node> Pinky_Nodes { get; set; }


        static HR()
        {
            
            Pos = new List<Vector3>();
            Hand_Nodes = new List<Hand_Node>();
            Thumb_Nodes = new List<Hand_Node>();
            Index_Nodes = new List<Hand_Node>();
            Middle_Nodes = new List<Hand_Node>();
            Ring_Nodes = new List<Hand_Node>();
            Pinky_Nodes = new List<Hand_Node>();
            for (int i = 0; i < 21; i++)
            {
                Pos.Add(Vector2.zero);
                HR.Hand_Nodes.Add(new Hand_Node(i, Vector3.zero));
            }


        }

        public static Vector2 Get_Hand_Pos()
        {
            return (Hand_Nodes[0].Screen_Pos + Hand_Nodes[3].Screen_Pos + Hand_Nodes[5].Screen_Pos + Hand_Nodes[9].Screen_Pos + Hand_Nodes[13].Screen_Pos + Hand_Nodes[17].Screen_Pos) / 6;
        }

        public static Vector3 Get_Hand_World_Pos()
        {
            return (Hand_Nodes[0].World_Pos + Hand_Nodes[3].World_Pos + Hand_Nodes[5].World_Pos + Hand_Nodes[9].World_Pos + Hand_Nodes[13].World_Pos + Hand_Nodes[17].World_Pos) / 6;
        }

        public static void Set_Coords(double[] _Coords)
        {
            lock (Pos)
            {
                
                for (int i = 0; i < Pos.Count; i++)
                {
                    if(i == 8)
                    {
                        Index_PrePos = Pos[8];
                    }
                    Pos[i] = new Vector3((float)_Coords[i], (float)_Coords[i + 21], (float)_Coords[i + 21 * 2]);
                }
            }
        }

        public static Finger Get_Finger(int _Index)
        {
            if (_Index > 0 && _Index <= 4)
            {
                return Finger.Thumb;
            }
            else if (_Index > 4 && _Index <= 8)
            {
                return Finger.Index;
            }
            else if (_Index > 8 && _Index <= 12)
            {
                return Finger.Middle;
            }
            else if (_Index > 12 && _Index <= 16)
            {
                return Finger.Ring;
            }
            else if (_Index > 16 && _Index <= 20)
            {
                return Finger.Pinky;
            }
            return Finger.None;
        }

        public static bool Check_Hand_State(Hand_State _HS)
        {
            switch (_HS)
            {
                case Hand_State.Fist:
                    if (Index_Nodes[0].Finger_State != Finger_State.Straight && Middle_Nodes[0].Finger_State != Finger_State.Straight && Ring_Nodes[0].Finger_State != Finger_State.Straight && Pinky_Nodes[0].Finger_State != Finger_State.Straight)
                    {
                        return true;
                    }
                    return false;
                case Hand_State.One:
                    if (Index_Nodes[0].Finger_State != Finger_State.Bended && Middle_Nodes[0].Finger_State == Finger_State.Bended && Ring_Nodes[0].Finger_State == Finger_State.Bended && Pinky_Nodes[0].Finger_State == Finger_State.Bended && !Check_Hand_State(Hand_State.Not_Showing))
                    {
                        return true;
                    }
                    return false;
                case Hand_State.Two:
                    if (Thumb_Nodes[0].Finger_State == Finger_State.Bended && Index_Nodes[0].Finger_State != Finger_State.Bended && Middle_Nodes[0].Finger_State != Finger_State.Bended && Ring_Nodes[0].Finger_State == Finger_State.Bended && Pinky_Nodes[0].Finger_State == Finger_State.Bended)
                    {
                        return true;
                    }
                    return false;
                case Hand_State.HardTwo:
                    if (Thumb_Nodes[0].Finger_State == Finger_State.Bended && Index_Nodes[0].Finger_State == Finger_State.Straight && Middle_Nodes[0].Finger_State == Finger_State.Straight && Ring_Nodes[0].Finger_State == Finger_State.Bended && Pinky_Nodes[0].Finger_State == Finger_State.Bended)
                    {
                        return true;
                    }
                    return false;
                case Hand_State.SoftTwo:
                    if (Thumb_Nodes[0].Finger_State == Finger_State.Bended && (Index_Nodes[0].Finger_State == Finger_State.Bending || Middle_Nodes[0].Finger_State == Finger_State.Bending) && Ring_Nodes[0].Finger_State == Finger_State.Bended && Pinky_Nodes[0].Finger_State == Finger_State.Bended)
                    {
                        return true;
                    }
                    return false;
                case Hand_State.SoftFive:
                    if (Index_Nodes[0].Finger_State == Finger_State.Straight && Middle_Nodes[0].Finger_State == Finger_State.Straight && Ring_Nodes[0].Finger_State == Finger_State.Straight && Pinky_Nodes[0].Finger_State == Finger_State.Straight)
                    {
                        return true;
                    }
                    return false;
                    /*
                case Hand_State.SoftFive:
                    if (Thumb_Nodes[0].Finger_State != Finger_State.Bended && Index_Nodes[0].Finger_State != Finger_State.Bended && Middle_Nodes[0].Finger_State != Finger_State.Bended && Ring_Nodes[0].Finger_State != Finger_State.Bended && Pinky_Nodes[0].Finger_State != Finger_State.Bended)
                    {
                        return true;
                    }
                    return false;
                    */
                case Hand_State.Five:
                    if (Thumb_Nodes[0].Finger_State == Finger_State.Straight && Index_Nodes[0].Finger_State == Finger_State.Straight && Middle_Nodes[0].Finger_State == Finger_State.Straight && Ring_Nodes[0].Finger_State == Finger_State.Straight && Pinky_Nodes[0].Finger_State == Finger_State.Straight)
                    {
                        return true;
                    }
                    return false;
                case Hand_State.Partal_Below:
                    if (Pos[0].y >= 0.95f && !Check_Hand_State(Hand_State.Not_Showing))
                    {
                        return true;
                    }
                    return false;
                case Hand_State.Not_Showing:
                    if (Pos[8] == Index_PrePos)
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }


    public class Hand_Node
    {
        public int Index { get; set; }
        public Vector3 World_Pos { get; set; }
        public Vector2 Screen_Pos { get; set; }
        public Finger Finger { get; set; }
        public Finger_State Finger_State { get; set; }
        public Hand_Node(int _Index, Vector3 _Pos)
        {
            Index = _Index;
            World_Pos = _Pos;
            Finger = HR.Get_Finger(_Index);
            Finger_State = Finger_State.Straight;
            switch ((int)Finger)
            {
                case 1:
                    HR.Thumb_Nodes.Add(this);
                    break;
                case 2:
                    HR.Index_Nodes.Add(this);
                    break;
                case 3:
                    HR.Middle_Nodes.Add(this);
                    break;
                case 4:
                    HR.Ring_Nodes.Add(this);
                    break;
                case 5:
                    HR.Pinky_Nodes.Add(this);
                    break;
            }
        }
    }

    public enum Finger
    {
        None = 0,
        Thumb = 1,
        Index = 2,
        Middle = 3,
        Ring = 4,
        Pinky = 5
    }

    public enum Finger_State
    {
        Straight = 1,
        Bending = 2,
        Bended = 3,
    }

    public enum Hand_State
    {
        Not_Showing = -2,
        Partal_Below = -1,
        Fist = 0,
        One = 1,
        Two = 2,
        HardTwo = 6,
        SoftTwo = 7,
        Three = 3,
        Four = 4,
        SoftFive = 8,
        Five = 5,
    }
}

