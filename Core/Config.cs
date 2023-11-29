﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Linq;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Config
    {
        #region Private Properties

        private static Dictionary<string, FieldInfo> varMap = new Dictionary<string, FieldInfo>();

        /// <summary>
        /// 构造函数，将这个类下的private参数给生成字典用于配置文件的读写
        /// </summary>
        static Config()
        {
            var fieldInfos = typeof(Config.Core).GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            lock (varMap)
                foreach (var fieldInfo in fieldInfos)
                    varMap.Add(fieldInfo.Name, fieldInfo);
        }
        #endregion

        #region public Method

        /// <summary>
        /// 从持久化配置文件读取配置刷新本地配置
        /// </summary>
        public static void ReadConfiguration()
        {
            string[] A = File.ReadAllLines(Core.ConfigurationFile);
            lock (varMap)
            {
                foreach (var item in A)
                {
                    if (item.Split('=').Length == 2)
                    {
                        varMap[item.Split('=')[0]].SetValue(null, item.Split('=')[1]);
                    }
                }
            } 
        }
        /// <summary>
        /// 把当前配置写入到持久化配置文件
        /// </summary>
        public static void WriteConfiguration()
        {
            using (StreamWriter file = new StreamWriter(Core.ConfigurationFile))
            {
                lock (varMap)
                {
                    foreach (var item in varMap)
                    {
                        file.WriteLine($"{item.Key}={item.Value.GetValue(null) as string}");
                    }
                }
            }
        }

        #endregion

        #region public Properties Method

        public class Core
        {
              private static string ConfigDirectory = "./Config/";
        /// <summary>
        /// 配置文件路径（字符串）
        /// 默认值：./Config/
        /// </summary>
        public static string _ConfigDirectory { get { return ConfigDirectory; } }

        internal static string ConfigurationFile = $"{ConfigDirectory}DDTV_Config.ini";
        /// <summary>
        /// 默认的配置文件路径（字符串）
        /// 默认值：./Config/DDTV_Config.ini
        /// </summary>
        public static string _ConfigurationFile { get { return ConfigurationFile; } }

        internal static string Key = "34D3D99D3489446191AB9B8582454669";
        /// <summary>
        /// 默认的AES加密秘钥（字符串）
        /// 默认值：34D3D99D3489446191AB9B8582454669
        /// </summary>
        public static string _Key { get { return Key; } }

        internal static string IV = "B3FF40627013F53F";
        /// <summary>
        /// 默认的AES加密初始化向量（字符串）
        /// 默认值：B3FF40627013F53F
        /// </summary>
        public static string _IV { get { return IV; } }

        internal static string UserInfoCoinfFileExtension = ".Duser";
        /// <summary>
        /// 用户配置文件拓展名（字符串）
        /// 默认值：.Duser
        /// </summary>
        public static string _UserInfoCoinfFileExtension { get { return UserInfoCoinfFileExtension; } }

        internal static string LogFileDirectory = "./Logs/";
        /// <summary>
        /// 日志文件路径（字符串）
        /// 默认值：./Logs/
        /// </summary>
        public static string _LogFileDirectory { get { return LogFileDirectory; } }

        internal static string LiveDomainName = "https://api.live.bilibili.com";
        /// <summary>
        /// 默认使用的直播API域名（字符串）
        /// 默认值：https://api.live.bilibili.com
        /// </summary>
        public static string _LiveDomainName { get { return LiveDomainName; } set { LiveDomainName = value; } }

        internal static string MainDomainName = "https://api.bilibili.com";
        /// <summary>
        /// 默认使用的主站API域名（字符串）
        /// 默认值：https://api.bilibili.com
        /// </summary>
        public static string _MainDomainName { get { return MainDomainName; } set { MainDomainName = value; } }
        }

      

        #endregion
    }
}
