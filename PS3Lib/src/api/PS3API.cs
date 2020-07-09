// ************************************************* //
//    --- Copyright (c) 2015 iMCS Productions ---    //
// ************************************************* //
//              PS3Lib v4 By FM|T iMCSx              //
//                                                   //
// Features v4.5 :                                   //
// - Support CCAPI v2.60+ C# by iMCSx.               //
// - Read/Write memory as 'double'.                  //
// - Read/Write memory as 'float' array.             //
// - Constructor overload for ArrayBuilder.          //
// - Some functions fixes.                           //
//                                                   //
// Credits : Enstone, Buc-ShoTz                      //
//                                                   //
// Follow me :                                       //
//                                                   //
// FrenchModdingTeam.com                             //
// Twitter.com/iMCSx                                 //
// Facebook.com/iMCSx                                //
//                                                   //
// ************************************************* //

using System;
using System.Collections.Generic;
using System.Reflection;

namespace PS3Lib
{
    public delegate void OnError(ErrorCodes code);

    public static class APISingleton
    {
        public static CCAPI CC_API;
        public static TMAPI TM_API;
    }

    public class PS3API
    {
        public static string TargetName { get; private set; }
        public static string TargetIp { get; private set; }

        public static OnError OnError;

        private SelectAPI CurrentAPI { get; set; }

        public PS3API(SelectAPI API = SelectAPI.TargetManager)
        {
            CurrentAPI = API;
            MakeInstanceAPI(API);
        }

        private void MakeInstanceAPI(SelectAPI API)
        {
            if (API == SelectAPI.TargetManager)
                if (APISingleton.TM_API == null)
                    APISingleton.TM_API = new TMAPI();
            else
                if (APISingleton.CC_API == null)
                    APISingleton.CC_API = new CCAPI();
            // implement PS3MAPI
        }

       /// <summary>init again the connection if you use a Thread or a Timer.</summary>
        public void InitTarget()
        {
            if (CurrentAPI == SelectAPI.TargetManager)
                APISingleton.TM_API.InitComms();
            else
                OnError?.Invoke(ErrorCodes.TMAPI_NOT_SELECTED);
        }

        public List<CCAPI.ConsoleInfo> GetConsoleList()
        {
            return APISingleton.CC_API.GetConsoleList();
        }

        /// <summary>Connect your console with selected API.</summary>
        public bool ConnectTarget(int target = 0)
        {
            // We'll check again if the instance has been done, else you'll got an exception error.
            MakeInstanceAPI(CurrentAPI);

            if (CurrentAPI == SelectAPI.TargetManager)
                return APISingleton.TM_API.ConnectTarget(target);
            else
            {
                var consoleList = GetConsoleList();
                if (ConnectTarget(consoleList[target].Ip))
                {
                    TargetName = consoleList[target].Name;
                    return true;
                }
            }

            return false;
        }

        /// <summary>Connect your console with CCAPI.</summary>
        public bool ConnectTarget(string ip)
        {
            // We'll check again if the instance has been done.
            MakeInstanceAPI(CurrentAPI);
            if (APISingleton.CC_API.SUCCESS(APISingleton.CC_API.ConnectTarget(ip)))
            {
                TargetIp = ip;
                return true;
            }
            else return false;
        }

        /// <summary>Disconnect Target with selected API.</summary>
        public void DisconnectTarget()
        {
            if (CurrentAPI == SelectAPI.TargetManager)
                APISingleton.TM_API.DisconnectTarget();
            else APISingleton.CC_API.DisconnectTarget();
        }

        /// <summary>Attach the current process (current Game) with selected API.</summary>
        public bool AttachProcess()
        {
            // We'll check again if the instance has been done.
            MakeInstanceAPI(CurrentAPI);

            if (CurrentAPI == SelectAPI.TargetManager)
                return APISingleton.TM_API.AttachProcess();
            else
                return APISingleton.CC_API.SUCCESS(APISingleton.CC_API.AttachProcess());
        }

        public string GetConsoleName()
        {
            if (CurrentAPI == SelectAPI.TargetManager)
                return APISingleton.TM_API.SCE.GetTargetName();
            else
            {
                if (TargetName != string.Empty)
                    return TargetName;

                if (TargetIp != string.Empty)
                {
                    List<CCAPI.ConsoleInfo> Data = APISingleton.CC_API.GetConsoleList();
                    if (Data.Count > 0)
                    {
                        for (int i = 0; i < Data.Count; i++)
                            if (Data[i].Ip == TargetIp)
                                return Data[i].Name;
                    }
                }
                return TargetIp;
            }
        }

        /// <summary>Set memory to offset with selected API.</summary>
        public void SetMemory(uint offset, byte[] buffer)
        {
            if (CurrentAPI == SelectAPI.TargetManager)
                APISingleton.TM_API.SetMemory(offset, buffer);
            else
                APISingleton.CC_API.SetMemory(offset, buffer);
        }

        /// <summary>Get memory from offset using the Selected API.</summary>
        public void GetMemory(uint offset, byte[] buffer)
        {
            if (CurrentAPI == SelectAPI.TargetManager)
                APISingleton.TM_API.GetMemory(offset, buffer);
            else
                APISingleton.CC_API.GetMemory(offset, buffer);
        }

        /// <summary>Get memory from offset with a length using the Selected API.</summary>
        public byte[] GetBytes(uint offset, int length)
        {
            byte[] buffer = new byte[length];
            if (CurrentAPI == SelectAPI.TargetManager)
                APISingleton.TM_API.GetMemory(offset, buffer);
            else
                APISingleton.CC_API.GetMemory(offset, buffer);
            return buffer;
        }

        /// <summary>Return selected API into string format.</summary>
        public string GetCurrentAPIName()
        {
            if (CurrentAPI == SelectAPI.TargetManager)
                return Enum.GetName(typeof(SelectAPI), SelectAPI.TargetManager).Replace("Manager", " Manager");
            else if (CurrentAPI == SelectAPI.ControlConsole)
                return Enum.GetName(typeof(SelectAPI), SelectAPI.ControlConsole).Replace("Console", " Console");
            else
                return Enum.GetName(typeof(SelectAPI), SelectAPI.ManagerAPI).Replace("API", " API");
        }

        /// <summary>This will find the dll ps3tmapi_net.dll for TMAPI.</summary>
        public Assembly PS3TMAPI_NET()
        {
            return APISingleton.TM_API.PS3TMAPI_NET();
        }

        /// <summary>Use the extension class with your selected API.</summary>
        public Extension Extension
        {
            get 
            { 
                return new Extension(CurrentAPI); 
            }
        }

        /// <summary>Access to all TMAPI functions.</summary>
        public TMAPI TMAPI
        {
            get { return new TMAPI(); }
        }

        /// <summary>Access to all CCAPI functions.</summary>
        public CCAPI CCAPI
        {
            get { return new CCAPI(); }
        }

        /// <summary>Access to all MAPI functions.</summary>
        public CCAPI MAPI
        {
            get { return new CCAPI(); }
        }
    }
}
