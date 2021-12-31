﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTV_Core.SystemAssembly.ConfigModule
{
    public class RoomConfigClass
    {
       public class RoomList
        {
            public List<RoomCard> data { set; get; } = new List<RoomCard>();
        }
        public class RoomListDiscard
        {
            public List<RoomCardDiscard> data { set; get; } = new List<RoomCardDiscard>();
        }
        public class RoomCard
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string name;
            /// <summary>
            /// 描述
            /// </summary>
            public string Description;
            /// 房间号
            /// </summary>
            public int RoomId;
            /// <summary>
            /// 用户UID(mid)
            /// </summary>
            public long UID;
            /// <summary>
            /// 自动录制
            /// </summary>
            public bool IsAutoRec;
            /// <summary>
            /// 开播提醒
            /// </summary>
            public bool IsRemind;
            /// <summary>
            /// 是否录制弹幕
            /// </summary>
            public bool IsRecDanmu;
            /// <summary>
            /// 优先标记
            /// </summary>
            public bool Like;
        }
        public class RoomCardDiscard: RoomCard
        {  
            public bool Like;
            /// <summary>
            /// (已弃用！！)(请使用name)名称
            /// </summary>
            public string Name;
            /// <summary>
            /// (已弃用！！)(请使用)房间号
            /// </summary>
            public string RoomNumber;
            /// <summary>
            /// (已弃用！！)(请使用IsAutoRec)是否自动录制
            /// </summary>
            public bool VideoStatus;
            /// <summary>
            /// (已弃用！！)(请使用IsRemind)是否开播提醒
            /// </summary>
            public bool RemindStatus;
        }
    }
}