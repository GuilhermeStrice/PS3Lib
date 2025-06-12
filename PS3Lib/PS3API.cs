using System;
using System.Collections.Generic;

namespace PS3_MAPI
{
    public delegate void OnError(ErrorCodes code);

    public class PS3API
    {
        public static string TargetName { get; internal set; }
        public static string TargetIp { get; internal set; }

        public static OnError OnError;

        MAPI api;

        public bool IsAttached
        {
            get;
            internal set;
        }

        internal bool isConnected()
        {
            return api.IsConnected;
        }

        public bool IsConnected
        {
            get => isConnected();
        }

        private void MakeInstanceAPI()
        {
            api = new MAPI();
        }

       /// <summary>init again the connection if you use a Thread or a Timer.</summary>
        public void InitTarget()
        {
            OnError?.Invoke(ErrorCodes.TMAPI_NOT_SELECTED);
        }


        /// <summary>Connect your console with CCAPI.</summary>
        public bool ConnectTarget(string ip)
        {
            if (api.ConnectTarget(ip))
            {
                TargetIp = ip;
                return true;
            }
            
            return false;
        }

        /// <summary>Disconnect Target with selected API.</summary>
        public void DisconnectTarget()
        {
            api.DisconnectTarget();
        }

        /// <summary>Attach the current process (current Game) with selected API.</summary>
        public bool AttachProcess()
        {
            return IsAttached = api.AttachProcess();
        }

        /// <summary>Set memory to offset with selected API.</summary>
        public void SetMemory(uint offset, byte[] buffer)
        {
            api.Process.Memory.Set(api.Process.Process_Pid, offset, buffer);
        }

        /// <summary>Get memory from offset using the Selected API.</summary>
        public void GetMemory(uint offset, byte[] buffer)
        {
            api.Process.Memory.Get(api.Process.Process_Pid, offset, buffer);
        }

        /// <summary>Get memory from offset with a length using the Selected API.</summary>
        public byte[] GetBytes(uint offset, int length)
        {
            byte[] buffer = new byte[length];
            api.Process.Memory.Get(api.Process.Process_Pid, offset, buffer);
            return buffer;
        }
    }
}
