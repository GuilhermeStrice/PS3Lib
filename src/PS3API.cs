using System;
using System.Collections.Generic;

namespace PS3_MAPI
{
    public delegate void OnError(ErrorCodes code);

    public static class APISingleton
    {
        public static MAPI M_API;
    }

    public class PS3API
    {
        public static string TargetName { get; internal set; }
        public static string TargetIp { get; internal set; }

        public static OnError OnError;

        public bool IsAttached
        {
            get;
            internal set;
        }

        internal bool isConnected()
        {
            return MAPI.IsConnected;
        }

        public bool IsConnected
        {
            get => isConnected();
        }

        private void MakeInstanceAPI()
        {
            APISingleton.M_API = new MAPI();
        }

       /// <summary>init again the connection if you use a Thread or a Timer.</summary>
        public void InitTarget()
        {
            OnError?.Invoke(ErrorCodes.TMAPI_NOT_SELECTED);
        }


        /// <summary>Connect your console with CCAPI.</summary>
        public bool ConnectTarget(string ip)
        {
            if (APISingleton.M_API.ConnectTarget(ip))
            {
                TargetIp = ip;
                return true;
            }
            
            return false;
        }

        /// <summary>Disconnect Target with selected API.</summary>
        public void DisconnectTarget()
        {
            APISingleton.M_API.DisconnectTarget();
        }

        /// <summary>Attach the current process (current Game) with selected API.</summary>
        public bool AttachProcess()
        {
            return IsAttached = APISingleton.M_API.AttachProcess();
        }

        /// <summary>Set memory to offset with selected API.</summary>
        public void SetMemory(uint offset, byte[] buffer)
        {
            APISingleton.M_API.Process.Memory.Set(APISingleton.M_API.Process.Process_Pid, offset, buffer);
        }

        /// <summary>Get memory from offset using the Selected API.</summary>
        public void GetMemory(uint offset, byte[] buffer)
        {
            APISingleton.M_API.Process.Memory.Get(APISingleton.M_API.Process.Process_Pid, offset, buffer);
        }

        /// <summary>Get memory from offset with a length using the Selected API.</summary>
        public byte[] GetBytes(uint offset, int length)
        {
            byte[] buffer = new byte[length];
            APISingleton.M_API.Process.Memory.Get(APISingleton.M_API.Process.Process_Pid, offset, buffer);
            return buffer;
        }

        public MAPI MAPI
        {
            get
            {
                return APISingleton.M_API;
            }
        }
    }
}
