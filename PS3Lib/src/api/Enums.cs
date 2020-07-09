using System;

namespace PS3Lib
{
    public enum ErrorCodes
    {
        CCAPI_LOAD_FAILED,
        CCAPI_NOT_FOUND,
        CCAPI_NOT_INSTALLED,

        MAPI_LOAD_FAILED,
        MAPI_NOT_FOUND,

        TMAPI_NOT_FOUND,
        TMAPI_NOT_SELECTED,

        CID_NULL,
        PSID_NULL,
    }

    public enum SelectAPI
    {
        ControlConsole,
        TargetManager,
        ManagerAPI,
    }

    #region CCAPI
    public enum CCAPIFunctions
    {
        ConnectConsole,
        DisconnectConsole,
        GetConnectionStatus,
        GetConsoleInfo,
        GetDllVersion,
        GetFirmwareInfo,
        GetNumberOfConsoles,
        GetProcessList,
        GetMemory,
        GetProcessName,
        GetTemperature,
        VshNotify,
        RingBuzzer,
        SetBootConsoleIds,
        SetConsoleIds,
        SetConsoleLed,
        SetMemory,
        ShutDown
    }

    public enum IdType
    {
        IDPS,
        PSID
    }

    public enum NotifyIcon
    {
        INFO,
        CAUTION,
        FRIEND,
        SLIDER,
        WRONGWAY,
        DIALOG,
        DIALOGSHADOW,
        TEXT,
        POINTER,
        GRAB,
        HAND,
        PEN,
        FINGER,
        ARROW,
        ARROWRIGHT,
        PROGRESS,
        TROPHY1,
        TROPHY2,
        TROPHY3,
        TROPHY4
    }

    public enum ConsoleType
    {
        CEX = 1,
        DEX = 2,
        TOOL = 3
    }

    public enum ProcessType
    {
        VSH,
        SYS_AGENT,
        CURRENTGAME
    }

    public enum RebootFlags
    {
        ShutDown = 1,
        SoftReboot = 2,
        HardReboot = 3
    }

    public enum BuzzerMode
    {
        Continuous,
        Single,
        Double,
        Triple
    }

    public enum LedColor
    {
        Green = 1,
        Red = 2
    }

    public enum LedMode
    {
        Off,
        On,
        Blink
    }
    #endregion

    #region TMAPI
    public enum SNRESULT
    {
        SN_E_BAD_ALIGN = -28,
        SN_E_BAD_MEMSPACE = -18,
        SN_E_BAD_PARAM = -21,
        SN_E_BAD_TARGET = -3,
        SN_E_BAD_UNIT = -11,
        SN_E_BUSY = -22,
        SN_E_CHECK_TARGET_CONFIGURATION = -33,
        SN_E_COMMAND_CANCELLED = -36,
        SN_E_COMMS_ERR = -5,
        SN_E_COMMS_EVENT_MISMATCHED_ERR = -39,
        SN_E_CONNECT_TO_GAMEPORT_FAILED = -35,
        SN_E_CONNECTED = -38,
        SN_E_DATA_TOO_LONG = -26,
        SN_E_DECI_ERROR = -23,
        SN_E_DEPRECATED = -27,
        SN_E_DLL_NOT_INITIALISED = -15,
        SN_E_ERROR = -2147483648,
        SN_E_EXISTING_CALLBACK = -24,
        SN_E_FILE_ERROR = -29,
        SN_E_HOST_NOT_FOUND = -8,
        SN_E_INSUFFICIENT_DATA = -25,
        SN_E_LICENSE_ERROR = -32,
        SN_E_LOAD_ELF_FAILED = -10,
        SN_E_LOAD_MODULE_FAILED = -31,
        SN_E_MODULE_NOT_FOUND = -34,
        SN_E_NO_SEL = -20,
        SN_E_NO_TARGETS = -19,
        SN_E_NOT_CONNECTED = -4,
        SN_E_NOT_IMPL = -1,
        SN_E_NOT_LISTED = -13,
        SN_E_NOT_SUPPORTED_IN_SDK_VERSION = -30,
        SN_E_OUT_OF_MEM = -12,
        SN_E_PROTOCOL_ALREADY_REGISTERED = -37,
        SN_E_TARGET_IN_USE = -9,
        SN_E_TARGET_RUNNING = -17,
        SN_E_TIMEOUT = -7,
        SN_E_TM_COMMS_ERR = -6,
        SN_E_TM_NOT_RUNNING = -2,
        SN_E_TM_VERSION = -14,
        SN_S_NO_ACTION = 6,
        SN_S_NO_MSG = 3,
        SN_S_OK = 0,
        SN_S_PENDING = 1,
        SN_S_REPLACED = 5,
        SN_S_TARGET_STILL_REGISTERED = 7,
        SN_S_TM_VERSION = 4
    }

    public enum UnitType
    {
        PPU,
        SPU,
        SPURAW
    }

    [Flags]
    public enum ResetParameter : ulong
    {
        Hard = 1L,
        Quick = 2L,
        ResetEx = 9223372036854775808L,
        Soft = 0L
    }

    /// <summary>Enum of flag reset.</summary>
    public enum ResetTarget
    {
        Hard,
        Quick,
        ResetEx,
        Soft
    }
    #endregion

    #region TMAPI_NET
    public enum ConnectStatus
    {
        Connected,
        Connecting,
        NotConnected,
        InUse,
        Unavailable
    }

    [Flags]
    public enum TargetInfoFlag : uint
    {
        Boot = 0x20,
        FileServingDir = 0x10,
        HomeDir = 8,
        Info = 4,
        Name = 2,
        TargetID = 1
    }

    [Flags]
    public enum BootParameter : ulong
    {
        BluRayEmuOff = 4L,
        BluRayEmuUSB = 0x20L,
        DebugMode = 0x10L,
        Default = 0L,
        DualNIC = 0x80L,
        HDDSpeedBluRayEmu = 8L,
        HostFSTarget = 0x40L,
        MemSizeConsole = 2L,
        ReleaseMode = 1L,
        SystemMode = 0x11L
    }
    #endregion
}
