using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Data.OleDb;
using CUST;
using System.Xml;
using CUST.Tools;
namespace CUST.Sys
{
    internal struct ZdType
    {
        public const string BKLBDM = "bklbdm";//报考类别
        public const string KSLB = "kslbdm";//考生类别代码
        public const string KSTZBZ = "kstzbz";//考生特征标志
        public const string MZ = "mzdm";//民族代码
        public const string MZWZ = "mzwzdm";//民族文字
        public const string WYYZ = "wyyzdm";//外语语种代码
        public const string XB = "xbdm";//性别代码
        public const string ZZMM = "zzmmdm";//政治面貌代码
        public const string ZJLXDM = "zjlxdm";
        public const string KSTZ = "kstzbz";//考生特征
        public const string TYXKKM = "tyxkkm3";//体育选考科目
        public const string CZFS = "czfsdm";  //操作方式 金温展    
        public const string RZLB = "rzlbdm";  //日志类别 金温展
        public const string MQLX = "mqlx";//母亲类型
        public const string FQLX = "fqlx";//父亲类型
        public const string ZW = "zw";//职位
        public const string KQZW = "kqzw";//考区职位
    }
    public struct Struct_ZYMC
    {
        public string dm;//代码
        public string mc;//名称
        public Struct_ZYMC(string dm, string mc)
        {
            this.dm = dm;
            this.mc = mc;
        }
    }
    public struct Struct_ZYZD
    {
        public string zd;
        public string mc;
        public bool sfdk;
        public Struct_ZYZD(string zd1, string mc1, bool sfdk1)
        {
            zd1 = "";
            zd = zd1;
            mc1 = "";
            mc = mc1;
            sfdk1 = false;
            sfdk = sfdk1;
        }
    }

    /// <summary>
    /// 字典结构体.
    /// </summary>
    public struct Struct_ZD
    {
        /// <summary>
        /// 字典码.
        /// </summary>
        public string zdm;

        /// <summary>
        /// 编码.
        /// </summary>
        public string bm;

        /// <summary>
        /// 名称.
        /// </summary>
        public string mc;

        /// <summary>
        /// 描述.
        /// </summary>
        public string ms;

        ///// <summary>
        ///// 
        ///// </summary>
        //public string gbkl;
    }

    /// <summary>
    /// 照顾代码结构体.
    /// </summary>
    public struct Struct_ZGDM
    {
        /// <summary>
        /// 照顾代码.
        /// </summary>
        public string zgdm;

        /// <summary>
        /// 照顾名称.
        /// </summary>
        public string zgmc;

        /// <summary>
        /// 分值.
        /// </summary>
        public int fz;
    }

    public class SysData
    {
        #region 数据库字典

        //普校字典
        private static DataTable InitZDData(string zdm)
        {
            lock (typeof(SysData))
            {
                DBClass dbc = new DBClass();
                try
                {
                    OracleParameter[] opara ={
                                             new OracleParameter("p_zdm",OracleType.VarChar),
                                             new OracleParameter("p_mycur",OracleType.Cursor)
                                         };
                    opara[0].Value = zdm;
                    opara[1].Direction = ParameterDirection.Output;
                    DataSet ds = dbc.RunProcedure("PACK_SYS.Sys_ZD_selectByZDM", opara, "table");

                    return ds.Tables[0];

                }
                finally
                {
                    dbc.Close();
                }
            }

        }



        /// <summary>
        /// 从静态表转化为哈希表.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static Hashtable FromZDDataTableToHashTable(DataTable dt)
        {
            lock (typeof(SysData))
            {
                Hashtable ht = new Hashtable();
                Struct_ZD zd;
                string bm;
                foreach (DataRow dr in dt.Rows)
                {
                    bm = dr["BM"].ToString();

                    //字典码.
                    zd.zdm = dr["ZDM"].ToString();

                    //编码.
                    zd.bm = dr["BM"].ToString();

                    //描述.
                    zd.mc = dr["MC"].ToString();

                    zd.ms = dr["MS"].ToString();

                    ht.Add(bm, zd);
                }
                return ht;
            }
        }

        //mqlx         .
        private static DataTable dt_mqlx = InitZDData(ZdType.MQLX);
        public static DataTable mqlx()
        {
            return dt_mqlx;
        }

        private static Hashtable ht_mqlx = FromZDDataTableToHashTable(dt_mqlx);
        public static Struct_ZD mqlxByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_mqlx[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        //fqlx         .
        private static DataTable dt_fqlx = InitZDData(ZdType.FQLX);
        public static DataTable fqlx()
        {
            return dt_fqlx;
        }

        private static Hashtable ht_fqlx = FromZDDataTableToHashTable(dt_fqlx);
        public static Struct_ZD fqlxByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_fqlx[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        //czfsdm 金温展
        private static DataTable dt_czfs = InitZDData(ZdType.CZFS);
        public static DataTable CZFS()
        {
            return dt_czfs;
        }
        private static Hashtable ht_czfs = FromZDDataTableToHashTable(dt_czfs);
        public static Struct_ZD CZFSByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_czfs[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        //rzlbdm 金温展
        private static DataTable dt_rzlb = InitZDData(ZdType.RZLB);
        public static DataTable RZLB()
        {
            return dt_rzlb;
        }
        private static Hashtable ht_rzlb = FromZDDataTableToHashTable(dt_rzlb);
        public static Struct_ZD RZLBByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_rzlb[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }


        //tyxkkm    .
        private static DataTable dt_tyxkkm = InitZDData(ZdType.TYXKKM);
        public static DataTable TYXKKM()
        {
            return dt_tyxkkm;
        }
        private static Hashtable ht_tyxkkm = FromZDDataTableToHashTable(dt_tyxkkm);
        public static Struct_ZD TYXKKMByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_tyxkkm[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        //ZJLXDM 
        private static DataTable dt_zjlxdm = InitZDData(ZdType.ZJLXDM);
        public static DataTable ZJLXDM()
        {
            return dt_zjlxdm;
        }

        private static Hashtable ht_zjlxdm = FromZDDataTableToHashTable(dt_zjlxdm);
        public static Struct_ZD ZJLXDMByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_zjlxdm[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }







        //bklbdm
        private static DataTable dt_bklbdm = InitZDData(ZdType.BKLBDM);
        public static DataTable BKLBDM()
        {
            return dt_bklbdm;
        }

        private static Hashtable ht_bklbdm = FromZDDataTableToHashTable(dt_bklbdm);
        public static Struct_ZD BKLBDMByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_bklbdm[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }




        //kslbdm  .
        private static DataTable dt_kslb = InitZDData(ZdType.KSLB);
        public static DataTable KSLB()
        {
            return dt_kslb;
        }

        private static Hashtable ht_kslb = FromZDDataTableToHashTable(dt_kslb);
        public static Struct_ZD KSLBByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_kslb[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }



        //kstzbz    .
        private static DataTable dt_kstzbz = InitZDData(ZdType.KSTZBZ);
        public static DataTable KSTZBZ()
        {
            return dt_kstzbz;
        }

        private static Hashtable ht_kstzbz = FromZDDataTableToHashTable(dt_kstzbz);
        public static Struct_ZD KSTZBZByKey(string key)   //根据键值,返回一个结构
        {
            //object obj = ht_kstzbz[key];
            //Struct_ZD struct_ZD = new Struct_ZD();
            //struct_ZD.bm = key;
            //return (obj != null ? (Struct_ZD)obj : struct_ZD);
            Struct_ZD struct_ZD = new Struct_ZD();
            if (key != "")
            {
                string kstz_jg = "";
                string kstz_zj = "";
                for (int j = 0; j < key.Length; j = j + 2)
                {
                    kstz_zj = key.Substring(j, 2);
                    if (j == 0)
                    {
                        kstz_jg = ((Struct_ZD)ht_kstzbz[kstz_zj]).mc;
                    }
                    else
                    {
                        kstz_jg = kstz_jg + "+" + ((Struct_ZD)ht_kstzbz[kstz_zj]).mc;
                    }
                }
                struct_ZD.mc = kstz_jg;
                struct_ZD.bm = key;
            }
            else
            {
                struct_ZD.mc = "";
                struct_ZD.bm = "";

            }

            return struct_ZD;
        }

        //mzdm     .
        private static DataTable dt_mz = InitZDData(ZdType.MZ);
        public static DataTable MZ()
        {
            return dt_mz;
        }

        private static Hashtable ht_mz = FromZDDataTableToHashTable(dt_mz);
        public static Struct_ZD MZByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_mz[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        //mzwzdm     .
        private static DataTable dt_mzwz = InitZDData(ZdType.MZWZ);
        public static DataTable MZWZ()
        {
            return dt_mzwz;
        }

        private static Hashtable ht_mzwz = FromZDDataTableToHashTable(dt_mzwz);
        public static Struct_ZD MZWZByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_mzwz[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }








        //wyyzdm        .
        private static DataTable dt_wyyz = InitZDData(ZdType.WYYZ);
        public static DataTable WYYZ()
        {
            return dt_wyyz;
        }

        private static Hashtable ht_wyyz = FromZDDataTableToHashTable(dt_wyyz);
        public static Struct_ZD WYYZByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_wyyz[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        //xbdm         .
        private static DataTable dt_xb = InitZDData(ZdType.XB);
        public static DataTable XB()
        {
            return dt_xb;
        }

        private static Hashtable ht_xb = FromZDDataTableToHashTable(dt_xb);
        public static Struct_ZD XBByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_xb[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        //zzmmdm         .
        private static DataTable dt_zzmm = InitZDData(ZdType.ZZMM);
        public static DataTable ZZMM()
        {
            return dt_zzmm;
        }

        private static Hashtable ht_zzmm = FromZDDataTableToHashTable(dt_zzmm);
        public static Struct_ZD ZZMMByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_zzmm[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        #endregion


        #region 各种状态
        private static DataTable FromHashTableToDataTable(Hashtable ht)//从哈希表转化为静态表
        {
            lock (typeof(SysData))
            {
                if (ht == null) return null;
                DataTable dt = new DataTable();

                dt.Columns.Add("bm", typeof(int));
                dt.Columns.Add("mc", typeof(string));
                DataRow dr;
                foreach (DictionaryEntry de in ht)
                {
                    dr = dt.NewRow();
                    dr["bm"] = de.Key;
                    dr["mc"] = de.Value;
                    dt.Rows.Add(dr);
                }
                dt.DefaultView.Sort = "bm asc";
                return dt.DefaultView.ToTable();
            }

        }
        private static DataTable FromHashTableToDataTableByDMstr(Hashtable ht)//从哈希表转化为静态表
        {
            lock (typeof(SysData))
            {
                if (ht == null) return null;
                DataTable dt = new DataTable();
                dt.Columns.Add("bm", typeof(string));
                dt.Columns.Add("mc", typeof(string));
                DataRow dr;
                foreach (DictionaryEntry de in ht)
                {
                    dr = dt.NewRow();
                    dr["bm"] = de.Key;
                    dr["mc"] = de.Value;
                    dt.Rows.Add(dr);
                }
                return dt;
            }

        }

        #region======考生报名状态=====
        private static Hashtable InitHKSBMZT()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                student.Add(0, "考生未保存");
                student.Add(1, "考生保存");
                student.Add(2, "考生提交");
                student.Add(3, "学校提交");
                student.Add(4, "县区提交");
                student.Add(5, "地区提交");
                return student;
            }
        }
        private static Hashtable ht_ksbmzt = InitHKSBMZT();
        public static string KSBMZTByKey(int key)
        {
            object obj = ht_ksbmzt[key];
            return (obj != null ? (string)obj : key.ToString());
        }

        private static DataTable dt_ksbmzt = FromHashTableToDataTable(ht_ksbmzt);
        public static DataTable KSBMZT()
        {
            return dt_ksbmzt;
        }
        #endregion


        #region======报名序号生成状态=========
        private static Hashtable InitBMXHSCZT()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                student.Add(0, "未生成");
                student.Add(1, "正在等待生成");
                student.Add(2, "已生成");
                student.Add(3, "生成失败");
                return student;
            }
        }
        private static Hashtable ht_bmxhsczt = InitBMXHSCZT();
        public static string BMXHSCZTByKey(int key)
        {
            object obj = ht_bmxhsczt[key];
            return (obj != null ? (string)obj : key.ToString());
        }

        private static DataTable dt_bmxhsczt = FromHashTableToDataTable(ht_bmxhsczt);
        public static DataTable BMXHSCZT()
        {
            return dt_bmxhsczt;
        }
        #endregion

        #endregion

        #region  字典重新绑值.

        public static void ReSetZdData()
        {

            //考生类别代码.
            ReSetKSLB();



            //考生特征标志.
            ReSetKSTZBZ();

            //民族代码.
            ReSetMZ();



            //民族文字
            ReSetMZWZ();



            //外语语种代码
            ReSetWYYZ();

            //性别代码.
            ReSetXB();

            //政治面貌代码.
            ReSetZZMM();
        }

        //考生类别代码.
        public static void ReSetKSLB()
        {
            dt_kslb = InitZDData(ZdType.KSLB);
            ht_kslb = FromZDDataTableToHashTable(dt_kslb);
        }


        //考生特征标志.
        public static void ReSetKSTZBZ()
        {
            dt_kstzbz = InitZDData(ZdType.KSTZBZ);
            ht_kstzbz = FromZDDataTableToHashTable(dt_kstzbz);
        }

        //民族代码.
        public static void ReSetMZ()
        {
            dt_mz = InitZDData(ZdType.MZ);
            ht_mz = FromZDDataTableToHashTable(dt_mz);
        }



        //民族文字
        public static void ReSetMZWZ()
        {
            dt_mzwz = InitZDData(ZdType.MZWZ);
            ht_mzwz = FromZDDataTableToHashTable(dt_mzwz);
        }


        //外语语种代码
        public static void ReSetWYYZ()
        {
            dt_wyyz = InitZDData(ZdType.WYYZ);
            ht_wyyz = FromZDDataTableToHashTable(dt_wyyz);
        }

        //性别代码.
        public static void ReSetXB()
        {
            dt_xb = InitZDData(ZdType.XB);
            ht_xb = FromZDDataTableToHashTable(dt_xb);
        }

        //政治面貌代码.
        public static void ReSetZZMM()
        {
            dt_zzmm = InitZDData(ZdType.ZZMM);
            ht_zzmm = FromZDDataTableToHashTable(dt_zzmm);
        }

        #endregion



        #region======科目、字典码============


        private static Hashtable InitZDM()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                student.Add("tyxkkm1", "体育科目一");
                student.Add("tyxkkm2", "体育科目二");
                student.Add("tyxkkm3", "体育科目三");
                student.Add("bylbdm", "毕业类别");
                student.Add("kslbdm", "考生类别代码");
                student.Add("kstzbz", "考生特征标志");
                student.Add("mzdm", "民族代码");
                student.Add("mzwzdm", "民族文字");
                student.Add("wyyzdm", "外语语种代码");
                student.Add("xbdm", "性别代码");
                student.Add("zzmmdm", "政治面貌代码");
                student.Add("czfsdm", "操作方式代码");
                student.Add("tyxkkm", "体育学考科目");
                student.Add("zhlbdm", "帐号类别代码");
                student.Add("zjlxdm", "证件类型代码");
                student.Add("bkkldm", "报考科类代码");
                student.Add("bklbdm", "报考类别代码");
                student.Add("fqlx", "监护人1代码");
                student.Add("mqlx", "监护人2代码");
                student.Add("rzlbdm", "日志类别代码");
                return student;
            }
        }
        private static Hashtable ht_zdm = InitZDM();
        public static string ZDMByKey(string key)
        {
            object obj = ht_zdm[key];
            return (obj != null ? (string)obj : key.ToString());
        }

        private static DataTable dt_zdm = FromHashTableToDataTableByDMstr(ht_zdm);
        public static DataTable ZDM()
        {
            return dt_zdm;
        }
        #endregion



        #region======考生种类（普校、对口）============
        private static Hashtable InitKSZL()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                student.Add("1", "普校");
                student.Add("2", "对口");
                return student;
            }
        }
        private static Hashtable ht_kszl = InitKSZL();
        public static string KSZLByKey(string key)
        {
            object obj = ht_kszl[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_kszl = FromHashTableToDataTableByDMstr(ht_kszl);
        public static DataTable KSZL()
        {
            return dt_kszl;
        }
        #endregion

        #region======日志类型、操作类型、操作来源（2013-11-09）===========
        //日志类型  rzlx(1考生报名，2考生体检，3照顾加分，4信息变更申请)
        //修改人:王永川
        //修改时间：2014-11-3

        private static Hashtable InitRZLX()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                student.Add("1", "考生报名");
                student.Add("2", "考生体检");
                student.Add("3", "照顾加分");
                student.Add("4", "信息变更申请");
                student.Add("5", "外语口试成绩");
                student.Add("6", "贫困生审核");
                student.Add("7", "恢复删除考生");
                return student;
            }
        }
        private static Hashtable ht_rzlx = InitRZLX();
        public static string RZLXByKey(string key)
        {
            object obj = ht_rzlx[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_rzlx = FromHashTableToDataTableByDMstr(ht_rzlx);
        public static DataTable RZLX()
        {
            return dt_rzlx;
        }
        //操作类型  czlx(操作：1、添加、2修改、4驳回)
        private static Hashtable InitCZLX()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                student.Add("1", "添加");
                student.Add("2", "修改");
                student.Add("4", "驳回");
                //添加时间2014-8-27
                student.Add("3", "删除");
                student.Add("5", "提交");
                student.Add("6", "同意变更");
                student.Add("7", "拒绝変更");
                student.Add("8", "导入");
                student.Add("9", "同意");
                student.Add("10", "未同意");

                return student;
            }
        }
        private static Hashtable ht_czlx = InitCZLX();
        public static string CZLXByKey(string key)
        {
            object obj = ht_czlx[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_czlx = FromHashTableToDataTableByDMstr(ht_czlx);
        public static DataTable CZLX()
        {
            return dt_czlx;
        }
        /*//操作来源  czly(操作来源：1信息变更申请，2修改，3驳回）
        private static Hashtable InitCZLY()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                student.Add("1", "信息变更申请");
                student.Add("2", "修改");
                student.Add("3", "驳回");
                return student;
            }
        }
        private static Hashtable ht_czly = InitCZLY();
        public static string CZLYByKey(string key)
        {
            object obj = ht_czly[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_czly = FromHashTableToDataTableByDMstr(ht_czly);
        public static DataTable CZLY()
        {
            return dt_czly;
        }*/
        #endregion

        #region======考场科目(2013-11-09)============
        private static Hashtable InitKCKM()
        {
            lock (typeof(SysData))
            {
                Hashtable kckm = new Hashtable();
                kckm.Add("0", "高考");
                kckm.Add("1", "美术类");
                kckm.Add("2", "MHK");
                return kckm;
            }
        }
        private static Hashtable ht_kckm = InitKCKM();
        public static string KCKMByKey(string key)
        {
            object obj = ht_kckm[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_kckm = FromHashTableToDataTableByDMstr(ht_kckm);
        public static DataTable KCKM()
        {
            return dt_kckm;
        }
        #endregion

        #region======考生号生成状态(2013-11-30)============
        private static Hashtable InitKSHZT()
        {
            lock (typeof(SysData))
            {
                Hashtable kckm = new Hashtable();
                kckm.Add("0", "等待生成");
                kckm.Add("2", "正在生成");
                return kckm;
            }
        }
        private static Hashtable ht_kshzt = InitKSHZT();
        public static string KSHZTByKey(string key)
        {
            object obj = ht_kshzt[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_kshzt = FromHashTableToDataTableByDMstr(ht_kshzt);
        public static DataTable KSHZT()
        {
            return dt_kshzt;
        }
        #endregion

        #region======方案类型(2014-1-14)============
        private static Hashtable InitFALX()
        {
            lock (typeof(SysData))
            {
                Hashtable falx = new Hashtable();
                falx.Add("0", "高考（普校）");
                falx.Add("3", "高考（对口）");
                falx.Add("1", "美术类");
                falx.Add("2", "MHK");
                falx.Add("4", "运动训练");
                return falx;
            }
        }
        private static Hashtable ht_falx = InitFALX();
        public static string FALXByKey(string key)
        {
            object obj = ht_falx[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        // 这个函数是对“方案”进行定制的，主要是为了改变“对口”的显示顺序，将其和“普校”放到一起
        private static DataTable FromHashTableToFATable(Hashtable htFalx)
        {
            DataTable dtTemp = FromHashTableToDataTableByDMstr(ht_falx);
            dtTemp.AcceptChanges();
            DataRow drTemp = dtTemp.Select("bm=3")[0];
            DataRow drNew = dtTemp.NewRow();
            drNew["bm"] = drTemp["bm"];
            drNew["mc"] = drTemp["mc"];
            dtTemp.Rows.Remove(drTemp);
            dtTemp.Rows.InsertAt(drNew, 1);
            dtTemp.AcceptChanges();

            return dtTemp;
        }
        private static DataTable dt_falx = FromHashTableToFATable(ht_falx);
        public static DataTable FALX()
        {
            return dt_falx;
        }
        #endregion

        #region======操作人类别（2014年2月15日）=========
        private static Hashtable InitYHLB()
        {
            lock (typeof(SysData))
            {
                Hashtable yh = new Hashtable();
                yh.Add(1, "市区");
                yh.Add(2, "县区");
                yh.Add(3, "学校");
                yh.Add(5, "高中");
                yh.Add(6, "中职");
                yh.Add(7, "中教科");
                yh.Add(8, "市级部门");
                return yh;
            }
        }
        private static Hashtable ht_yhlb = InitYHLB();
        public static string YHLBByKey(int key)
        {
            object obj = ht_yhlb[key];
            return (obj != null ? (string)obj : key.ToString());
        }

        private static DataTable dt_yhlb = FromHashTableToDataTable(ht_yhlb);
        public static DataTable YHLB()
        {
            return dt_yhlb;
        }
        #endregion

        #region 考场科目顺序
        private static Hashtable InitKMShunXu()
        {
            lock (typeof(SysData))
            {
                Hashtable kmsx = new Hashtable();
                //kmsx.Add(1, "第一天上午第一场");
                //kmsx.Add(2, "第一天上午第二场");
                //kmsx.Add(3, "第一天上午第三场");
                //kmsx.Add(4, "第一天下午第一场");
                //kmsx.Add(5, "第一天下午第二场");
                //kmsx.Add(6, "第一天下午第三场");
                //kmsx.Add(7, "第二天上午第一场");
                //kmsx.Add(8, "第二天上午第二场");
                //kmsx.Add(9, "第二天上午第三场");
                //kmsx.Add(10, "第二天下午第一场");
                //kmsx.Add(11, "第二天下午第二场");
                //kmsx.Add(12, "第二天下午第三场");
                //kmsx.Add(13, "第三天上午第一场");
                //kmsx.Add(14, "第三天上午第二场");
                //kmsx.Add(15, "第三天上午第三场");
                //kmsx.Add(16, "第三天下午第一场");
                //kmsx.Add(17, "第三天下午第二场");
                //kmsx.Add(18, "第三天下午第三场");
                kmsx.Add(11, "第一天上午第一场");
                kmsx.Add(12, "第一天上午第二场");
                kmsx.Add(13, "第一天上午第三场");
                kmsx.Add(14, "第一天下午第一场");
                kmsx.Add(15, "第一天下午第二场");
                kmsx.Add(16, "第一天下午第三场");
                kmsx.Add(21, "第二天上午第一场");
                kmsx.Add(22, "第二天上午第二场");
                kmsx.Add(23, "第二天上午第三场");
                kmsx.Add(24, "第二天下午第一场");
                kmsx.Add(25, "第二天下午第二场");
                kmsx.Add(26, "第二天下午第三场");
                kmsx.Add(31, "第三天上午第一场");
                kmsx.Add(32, "第三天上午第二场");
                kmsx.Add(33, "第三天上午第三场");
                kmsx.Add(34, "第三天下午第一场");
                kmsx.Add(35, "第三天下午第二场");
                kmsx.Add(36, "第三天下午第三场");
                return kmsx;
            }
        }
        private static Hashtable ht_kmsx = InitKMShunXu();
        public static string KMShunXuByKey(int key)
        {
            object obj = ht_kmsx[key];
            return (obj != null ? (string)obj : key.ToString());
        }

        private static DataTable dt_kmsx = FromHashTableToDataTable(ht_kmsx);
        public static DataTable KMShunXu()
        {
            return dt_kmsx;
        }
        #endregion

        #region======图像采集（2014年8月2日）=========
        private static Hashtable InitTXCJ()
        {
            lock (typeof(SysData))
            {
                Hashtable txcj = new Hashtable();
                txcj.Add(0, "未采集");
                txcj.Add(1, "已采集");
                return txcj;
            }
        }
        private static Hashtable ht_txcj = InitTXCJ();
        public static string TXCJByKey(int key)
        {
            object obj = ht_txcj[key];
            return (obj != null ? (string)obj : key.ToString());
        }

        private static DataTable dt_txcj = FromHashTableToDataTable(ht_txcj);
        public static DataTable TXCJZT()
        {
            return dt_txcj;
        }
        #endregion

        #region======民族状态=====
        private static Hashtable InitMZZT()
        {
            lock (typeof(SysData))
            {
                Hashtable mzzt = new Hashtable();
                mzzt.Add(0, "汉族");
                mzzt.Add(1, "非汉族");
                return mzzt;
            }
        }
        private static Hashtable ht_mzzt = InitMZZT();
        public static string MZZTByKey(int key)
        {
            object obj = ht_mzzt[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_mzzt = FromHashTableToDataTable(ht_mzzt);
        public static DataTable MZZT()
        {
            return dt_mzzt;
        }
        #endregion

        #region======是否是权限用户（周思硕）2014年10月30日18:17:44=====
        private static Hashtable InitSFSQXYH()
        {
            lock (typeof(SysData))
            {
                Hashtable sfsqxyh = new Hashtable();
                sfsqxyh.Add(1, "是");
                sfsqxyh.Add(0, "否");
                return sfsqxyh;
            }
        }
        private static Hashtable ht_sfsqxyh = InitSFSQXYH();
        public static string SFSQXYHByKey(int key)
        {
            object obj = ht_sfsqxyh[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_sfsqxyh = FromHashTableToDataTable(ht_sfsqxyh);
        public static DataTable SFSQXYH()
        {
            return dt_sfsqxyh;
        }
        #endregion

        #region======是否启用(杨宏达)=====
        private static Hashtable InitSFQY()
        {
            lock (typeof(SysData))
            {
                Hashtable sfqy = new Hashtable();
                sfqy.Add(0, "禁用");
                sfqy.Add(1, "启用");
                return sfqy;
            }
        }
        private static Hashtable ht_sfqy = InitSFQY();
        public static string SFQYByKey(int key)
        {
            object obj = ht_sfqy[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_sfqy = FromHashTableToDataTable(ht_sfqy);
        public static DataTable SFQY()
        {
            return dt_sfqy;
        }
        #endregion

        #region======贫困考生状态（段银兰）2014年9月28日16:28:34====
        //贫困考生状态
        private static Hashtable InitPKKSZT()
        {
            lock (typeof(SysData))
            {
                Hashtable pkks = new Hashtable();
                pkks.Add("0", "已保存");
                pkks.Add("1", "县区提交");
                pkks.Add("2", "地区提交");
                return pkks;
            }
        }
        private static Hashtable ht_pkkszt = InitPKKSZT();
        public static string PKKSZTByKey(string key)
        {
            object obj = ht_pkkszt[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_pkkszt = FromHashTableToDataTableByDMstr(ht_pkkszt);
        public static DataTable PKKSZT()
        {
            return dt_pkkszt;
        }
        #endregion

        #region======贫困考生审核状态（段银兰）2014年9月28日16:28:48====
        //贫困考生状态
        private static Hashtable InitPKKSSHZT()
        {
            lock (typeof(SysData))
            {
                Hashtable pkks = new Hashtable();
                pkks.Add("1", "合格");
                pkks.Add("2", "不合格");
                return pkks;
            }
        }
        private static Hashtable ht_pkksshzt = InitPKKSSHZT();
        public static string PKKSSHZTByKey(string key)
        {
            object obj = ht_pkksshzt[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_pkksshzt = FromHashTableToDataTableByDMstr(ht_pkksshzt);
        public static DataTable PKKSSHZT()
        {
            return dt_pkksshzt;
        }
        #endregion

        #region==========导入数据信息查看类型(段银兰)================
        private static Hashtable InitDRSJXXCKLX()
        {
            lock (typeof(SysData))
            {
                Hashtable drsjlx = new Hashtable();
                drsjlx.Add("1", "高二会考数据");
                drsjlx.Add("2", "高三会考数据");
                drsjlx.Add("3", "照顾代码");
                drsjlx.Add("4", "考生违纪数据");
                drsjlx.Add("5", "少数民族照顾加分");
                drsjlx.Add("6", "体育优胜者照顾加分(普校)");
                drsjlx.Add("7", "体育优胜者照顾加分(对口)");
                drsjlx.Add("8", "不参与考场编排考生数据");
                drsjlx.Add("9", "普通高考考点数据");
                drsjlx.Add("10", "对口高考考点数据");
                drsjlx.Add("11", "运动训练考生信息");
                drsjlx.Add("12", "运动训练考场信息");
                return drsjlx;
            }
        }
        private static Hashtable ht_drsjxxcklx = InitDRSJXXCKLX();
        public static string DRSJXXCKLXByKey(string key)
        {
            object obj = ht_drsjxxcklx[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_drsjxxcklx = FromHashTableToDataTableByDMstr(ht_drsjxxcklx);
        public static DataTable DRSJXXCKLX()
        {
            return dt_drsjxxcklx;
        }
        #endregion
        /// <summary>
        /// 操作类型
        /// </summary>
        private static DataTable dt_czlxAll = get_czlxAll_ht();

        public static DataTable get_czlxAll_dt()
        {
            return dt_czlxAll;
        }

        public static DataTable get_czlxAll_ht()
        {
            lock (typeof(SysData))
            {
                Hashtable student = new Hashtable();
                DataTable dt_ksbm = new DataTable();//考生报名
                dt_ksbm.TableName = "dt_ksbm";
                dt_ksbm.Columns.Add("bm", typeof(string));
                dt_ksbm.Columns.Add("mc", typeof(string));
                dt_ksbm.Columns.Add("ss", typeof(string));
                DataRow dr = dt_ksbm.NewRow();
                DataRow dr1 = dt_ksbm.NewRow();
                DataRow dr2 = dt_ksbm.NewRow();
                DataRow dr3 = dt_ksbm.NewRow();
                DataRow dr4 = dt_ksbm.NewRow();
                DataRow dr5 = dt_ksbm.NewRow();
                DataRow dr6 = dt_ksbm.NewRow();
                DataRow dr7 = dt_ksbm.NewRow();
                DataRow dr8 = dt_ksbm.NewRow();
                DataRow dr9 = dt_ksbm.NewRow();
                DataRow dr10 = dt_ksbm.NewRow();
                DataRow dr11 = dt_ksbm.NewRow();
                DataRow dr12 = dt_ksbm.NewRow();
                DataRow dr13 = dt_ksbm.NewRow();
                DataRow dr14 = dt_ksbm.NewRow();
                DataRow dr15 = dt_ksbm.NewRow();
                DataRow dr16 = dt_ksbm.NewRow();
                DataRow dr17 = dt_ksbm.NewRow();
                DataRow dr18 = dt_ksbm.NewRow();
                DataRow dr19 = dt_ksbm.NewRow();
                DataRow dr20 = dt_ksbm.NewRow();
                DataRow dr21 = dt_ksbm.NewRow();
                DataRow dr22 = dt_ksbm.NewRow();
                DataRow dr23 = dt_ksbm.NewRow();
                DataRow dr24 = dt_ksbm.NewRow();
                DataRow dr25 = dt_ksbm.NewRow();
                DataRow dr26 = dt_ksbm.NewRow();
                dr["bm"] = "1"; dr["mc"] = "添加"; dr["ss"] = "1";
                dr1["bm"] = "2"; dr1["mc"] = "修改"; dr1["ss"] = "1";
                dr2["bm"] = "3"; dr2["mc"] = "删除"; dr2["ss"] = "1";
                dr3["bm"] = "4"; dr3["mc"] = "驳回"; dr3["ss"] = "1";
                dr4["bm"] = "5"; dr4["mc"] = "提交"; dr4["ss"] = "1";

                dr5["bm"] = "1"; dr5["mc"] = "添加"; dr5["ss"] = "2";
                dr6["bm"] = "2"; dr6["mc"] = "修改"; dr6["ss"] = "2";
                dr7["bm"] = "3"; dr7["mc"] = "删除"; dr7["ss"] = "2";
                dr8["bm"] = "4"; dr8["mc"] = "驳回"; dr8["ss"] = "2";
                dr9["bm"] = "5"; dr9["mc"] = "提交"; dr9["ss"] = "2";

                dr10["bm"] = "1"; dr10["mc"] = "添加"; dr10["ss"] = "3";
                dr11["bm"] = "3"; dr11["mc"] = "删除"; dr11["ss"] = "3";
                dr12["bm"] = "4"; dr12["mc"] = "驳回"; dr12["ss"] = "3";
                dr13["bm"] = "5"; dr13["mc"] = "提交"; dr13["ss"] = "3";

                dr14["bm"] = "1"; dr14["mc"] = "添加"; dr14["ss"] = "4";
                dr15["bm"] = "6"; dr15["mc"] = "同意变更"; dr15["ss"] = "4";
                dr16["bm"] = "7"; dr16["mc"] = "拒绝変更"; dr16["ss"] = "4";
                dr17["bm"] = "5"; dr17["mc"] = "提交"; dr17["ss"] = "4";

                dr18["bm"] = "1"; dr18["mc"] = "添加"; dr18["ss"] = "5";

                dr19["bm"] = "3"; dr19["mc"] = "删除"; dr19["ss"] = "5";
                dr20["bm"] = "4"; dr20["mc"] = "驳回"; dr20["ss"] = "5";
                dr21["bm"] = "5"; dr21["mc"] = "提交"; dr21["ss"] = "5";
                dr22["bm"] = "1"; dr22["mc"] = "添加"; dr22["ss"] = "6";
                dr23["bm"] = "8"; dr23["mc"] = "导入"; dr23["ss"] = "6";
                dr24["bm"] = "5"; dr24["mc"] = "提交"; dr24["ss"] = "6";
                dr25["bm"] = "4"; dr25["mc"] = "驳回"; dr25["ss"] = "6";
                dr26["bm"] = "2"; dr26["mc"] = "修改"; dr26["ss"] = "6";

                DataRow dr27 = dt_ksbm.NewRow();
                DataRow dr28 = dt_ksbm.NewRow();
                DataRow dr29 = dt_ksbm.NewRow();
                dr27["bm"] = "5"; dr27["mc"] = "提交"; dr27["ss"] = "7";
                dr28["bm"] = "9"; dr28["mc"] = "同意"; dr28["ss"] = "7";
                dr29["bm"] = "10"; dr29["mc"] = "未同意"; dr29["ss"] = "7";
                dt_ksbm.Rows.Add(dr); dt_ksbm.Rows.Add(dr1); dt_ksbm.Rows.Add(dr2); dt_ksbm.Rows.Add(dr3); dt_ksbm.Rows.Add(dr4);
                dt_ksbm.Rows.Add(dr5); dt_ksbm.Rows.Add(dr6); dt_ksbm.Rows.Add(dr7); dt_ksbm.Rows.Add(dr8); dt_ksbm.Rows.Add(dr9);
                dt_ksbm.Rows.Add(dr10); dt_ksbm.Rows.Add(dr11); dt_ksbm.Rows.Add(dr12); dt_ksbm.Rows.Add(dr13); dt_ksbm.Rows.Add(dr14);
                dt_ksbm.Rows.Add(dr15); dt_ksbm.Rows.Add(dr16); dt_ksbm.Rows.Add(dr17); dt_ksbm.Rows.Add(dr18); dt_ksbm.Rows.Add(dr19);
                dt_ksbm.Rows.Add(dr20); dt_ksbm.Rows.Add(dr21);
                dt_ksbm.Rows.Add(dr22); dt_ksbm.Rows.Add(dr23); dt_ksbm.Rows.Add(dr24); dt_ksbm.Rows.Add(dr25); dt_ksbm.Rows.Add(dr26);
                dt_ksbm.Rows.Add(dr27); dt_ksbm.Rows.Add(dr28); dt_ksbm.Rows.Add(dr29);
                dt_ksbm.AcceptChanges();
                return dt_ksbm;

            }
        }

        #region====恢复删除考生申请状态
        private static Hashtable InitHFSCKSSQZT()
        {
            lock (typeof(SysData))
            {
                Hashtable SQZT = new Hashtable();
                SQZT.Add("0", "未申请");
                SQZT.Add("1", "市招办提交");
                SQZT.Add("2", "省招办已处理");
                return SQZT;
            }
        }
        private static Hashtable ht_HFSCKSSQZT = InitHFSCKSSQZT();
        public static string HFSCKSSQZTByKey(string key)
        {
            object obj = ht_HFSCKSSQZT[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_HFSCKSSQZT = FromHashTableToDataTableByDMstr(ht_HFSCKSSQZT);
        public static DataTable HFSCKSSQZT()
        {
            dt_HFSCKSSQZT.TableName = "dt_HFSCKSSQZT";
            return dt_HFSCKSSQZT;
        }
        #endregion

        #region ===恢复删除考生处理状态==
        private static Hashtable InitHFSCKSCLZT()
        {
            lock (typeof(SysData))
            {
                Hashtable CLZT = new Hashtable();
                CLZT.Add("0", "未处理");
                CLZT.Add("1", "已同意");
                CLZT.Add("2", "未同意");
                return CLZT;
            }
        }
        private static Hashtable ht_HFSCKSCLZT = InitHFSCKSCLZT();
        public static string HFSCKSCLZTByKey(string key)
        {
            object obj = ht_HFSCKSCLZT[key];
            return (obj != null ? (string)obj : key.ToString());
        }
        private static DataTable dt_HFSCKSCLZT = FromHashTableToDataTableByDMstr(ht_HFSCKSCLZT);
        public static DataTable HFSCKSCLZT()
        {
            dt_HFSCKSCLZT.TableName = "dt_HFSCKSCLZT";
            return dt_HFSCKSCLZT;
        }
        #endregion


        //ZW
        private static DataTable dt_zw = InitZDData(ZdType.ZW);
        public static DataTable ZW()
        {
            return dt_zw;
        }

        private static Hashtable ht_zw = FromZDDataTableToHashTable(dt_zw);
        public static Struct_ZD ZWByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_zw[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }
        //kqzw
        private static DataTable dt_kqzw = InitZDData(ZdType.KQZW);
        public static DataTable KQZW()
        {
            return dt_kqzw;
        }

        private static Hashtable ht_kqzw = FromZDDataTableToHashTable(dt_kqzw);
        public static Struct_ZD KQZWByKey(string key)   //根据键值,返回一个结构
        {
            object obj = ht_kqzw[key];
            Struct_ZD struct_ZD = new Struct_ZD();
            struct_ZD.bm = key;
            return (obj != null ? (Struct_ZD)obj : struct_ZD);
        }

        public static List<ConfigModel> GetConfigs()
        {
            List<ConfigModel> dic = new List<ConfigModel>();
            XmlDocument oXmlDocument = new XmlDocument();
            string _strFileName = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "#Sys.config";
            try
            {
                oXmlDocument.Load(_strFileName);
                XmlNodeList oXmlNodeList = oXmlDocument.DocumentElement.ChildNodes;
                foreach (XmlElement oXmlElement in oXmlNodeList)
                {
                    if (oXmlElement.Name.ToLower() == "appsettings")
                    {
                        XmlNodeList _node = oXmlElement.ChildNodes;
                        if (_node.Count > 0)
                        {
                            foreach (XmlElement _el in _node)
                            {
                                dic.Add(new ConfigModel(_el.Attributes["key"].InnerXml.ToLower(), _el.Attributes["value"].Value));
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            return dic;
        }
        private static DataTable dt_xqdm = GetXQ();
        private static DataTable GetXQ()
        {
            lock (typeof(SysData))
            {
                DBClass dbc = new DBClass();
                try
                {
                    OracleParameter[] opara ={
                                             
                                             new OracleParameter("mycur",OracleType.Cursor)
                                         };

                    opara[0].Direction = ParameterDirection.Output;
                    DataSet ds = dbc.RunProcedure("PACK_WEB_ZYXX.Sys_XQ_select", opara, "table");

                    return ds.Tables[0];

                }
                finally
                {
                    dbc.Close();
                }
            }
        }
        private static DataSet ShowZYZD(string xqdm, int pc)//根据县区检索出操作字段
        {
            lock (typeof(SysData))
            {
                DBClass dbc = new DBClass();
                DataSet ds = new DataSet();
                try
                {
                    OracleParameter[] parameter = {
                                                  new OracleParameter("zy_xqdm",OracleType.VarChar),
                                                  new OracleParameter("zy_pc",OracleType.Int32),
                                                  new OracleParameter("mycur",OracleType.Cursor)
                                              };
                    parameter[0].Value = xqdm;
                    parameter[1].Value = pc;
                    parameter[2].Direction = ParameterDirection.Output;
                    ds = dbc.RunProcedure("PACK_WEB_ZYXX.Sys_ZYPZ_selectByXQDM", parameter, "table");
                    return ds;
                }
                finally
                {
                    dbc.Close();
                }
            }

        }

        private static Hashtable InitHashZYZDTable(string xqdm, int pc)//初始化哈希表 ——键值对为“ZY_ZD——志愿字段结构体”
        {
            lock (typeof(SysData))
            {
                Hashtable ht = new Hashtable();
                Struct_ZYZD zyzd;
                string zd;
                DBClass dbc = new DBClass();
                DataSet ds = new DataSet();
                try
                {
                    ds = ShowZYZD(xqdm, pc);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        zd = dr["ZY_ZD"].ToString();
                        zyzd.zd = dr["ZY_ZD"].ToString();
                        zyzd.mc = dr["ZDMC"].ToString();
                        zyzd.sfdk = (dr["SFDK"].ToString() == "1") ? true : false;
                        ht.Add(zd, zyzd);
                    }
                    return ht;
                }
                finally
                {
                    dbc.Close();
                }
            }
        }

        private static Hashtable InitHashZYZDPC1()//初始化哈希表 ——键值对为“XQDM——ZYZD哈希表”--第一批次
        {
            lock (typeof(SysData))
            {
                Hashtable zyzd = new Hashtable();
                string xqdm;
                foreach (DataRow dr in dt_xqdm.Rows)
                {
                    xqdm = dr["V_DM"].ToString();
                    zyzd.Add(xqdm, InitHashZYZDTable(xqdm, 1));
                }
                return zyzd;
            }
        }
        private static Hashtable InitTableZYZDPC2()//初始化志愿字段静态表--第二批次
        {
            lock (typeof(SysData))
            {
                Hashtable zyzd = new Hashtable();
                string xqdm;
                foreach (DataRow dr in dt_xqdm.Rows)
                {
                    xqdm = dr["V_DM"].ToString();
                    zyzd.Add(xqdm, ShowZYZD(xqdm, 2).Tables[0]);
                }
                return zyzd;
            }
        }
        private static Hashtable zyczzdpc1 = InitHashZYZDPC1();
        private static Hashtable zyczzdpc2 = InitTableZYZDPC2();
        public static Struct_ZYZD ZYZDByKey(string xqdm, string key)//根据键值,返回一个结构
        {
            object obj = new object();
            if (Convert.ToInt32(Config.pc) == 1)
            {
                obj = zyczzdpc1[xqdm];
            }
            else
            {
                obj = zyczzdpc2[xqdm];
            }
            Hashtable zyzd = (obj != null ? (Hashtable)obj : new Hashtable());
            obj = zyzd[key];
            return (obj != null ? (Struct_ZYZD)obj : new Struct_ZYZD());
        }
        private static Hashtable InitHWZ()
        {
            lock (typeof(SysData))
            {
                Hashtable weizhi = new Hashtable();
                weizhi.Add("GZZY1", "01");
                weizhi.Add("GZZY2", "02");
                weizhi.Add("GZZY3", "03");
                weizhi.Add("GZZY4", "04");
                weizhi.Add("GZZY5", "05");
                weizhi.Add("GZZY6", "06");
                weizhi.Add("GZZY7", "07");
                weizhi.Add("GZZY8", "08");
                weizhi.Add("GZZY9", "09");
                weizhi.Add("GZZY10", "10");
                weizhi.Add("GZZY11", "11");
                weizhi.Add("GZZY12", "12");
                weizhi.Add("GZZY13", "13");
                weizhi.Add("GZZY14", "14");
                weizhi.Add("GZZY15", "15");
                weizhi.Add("GZZY16", "16");
                //  weizhi.Add("ZGZY1", "41");
                //  weizhi.Add("ZGZY2", "42");
                // weizhi.Add("PTZZZY1", "51");
                // weizhi.Add("PTZZZY2", "52");
                // weizhi.Add("PTZZZY3", "53");
                weizhi.Add("ZGZY1", "31");
                weizhi.Add("ZGZY2", "32");
                weizhi.Add("PTZZZY1", "41");
                weizhi.Add("PTZZZY2", "42");
                weizhi.Add("PTZZZY3", "43");
                weizhi.Add("ZYZY1", "61");
                weizhi.Add("ZYZY2", "62");
                return weizhi;
            }
        }
        private static Hashtable _HWZ = InitHWZ();
        private static DataTable InitZYMCTable()
        {
            OracleConnection con = new OracleConnection(Config.connectionString);
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                OracleCommand com = new OracleCommand(@"select distinct XQ_DM,ZY_ZD,ZDMC from ZYPZ", con);
                OracleDataAdapter da = new OracleDataAdapter(com);
                da.Fill(ds);
                return ds.Tables[0];
            }
            finally
            {
                con.Close();
            }
        }
        private static Hashtable InitZYMCHash()
        {
            DataTable dt_zymcTemp = InitZYMCTable();
            lock (typeof(SysData))
            {
                Hashtable ht_zymc = new Hashtable();
                object objTemp = "";
                string strKeyTemp = "";
                foreach (DataRow drTemp in dt_zymcTemp.Rows)
                {
                    objTemp = _HWZ[drTemp["zy_zd"].ToString().Trim()];
                    if (objTemp != null)
                    {
                        strKeyTemp = drTemp["xq_dm"].ToString().Trim() + objTemp.ToString().Trim();
                        Struct_ZYMC zymc = new Struct_ZYMC(drTemp["ZY_ZD"].ToString(), drTemp["zdmc"].ToString());
                        ht_zymc.Add(strKeyTemp, zymc);
                    }
                }
                return ht_zymc;
            }
        }
        public static string ZYMCByKey(string strXQDM, string strWZ)
        {
            string strKey = strXQDM.Trim() + strWZ.Trim();
            object obj = ht_zymc[strKey];
            return (obj != null ? ((Struct_ZYMC)obj).mc : "");
        }

        #region====================检索志愿学校及专业==========================================
        private static Hashtable InitHashZYXXTable()//初始化志愿学校哈希表——键值对为“DM——志愿学校结构体”
        {
            lock (typeof(SysData))
            {
                Hashtable ht = new Hashtable();
                Struct_ZYXX zd;
                string dm;
                DBClass dbc = new DBClass();
                try
                {
                    OracleParameter[] opara ={ 
                                             new OracleParameter("mycur",OracleType.Cursor)
                                         };
                    opara[0].Direction = ParameterDirection.Output;
                    OracleDataReader Reader = dbc.RunProcedure("PACK_WEB_ZYXX.Sys_JHK_select", opara);
                    while (Reader.Read())
                    {
                        dm = Reader["DM"].ToString();
                        zd.dm = Reader["DM"].ToString();
                        zd.mc = Reader["MC"].ToString();
                        ht.Add(dm, zd);
                    }
                    Reader.Close();
                    return ht;
                }
                finally
                {
                    dbc.Close();
                }
            }
        }

        private static DataTable InitZYXXTable()//初始化志愿学校静态表
        {
            lock (typeof(SysData))
            {
                DBClass dbc = new DBClass();
                try
                {
                    DataSet ds = new DataSet();
                    OracleParameter[] opara ={ 
                                             new OracleParameter("mycur",OracleType.Cursor)
                                         };
                    opara[0].Direction = ParameterDirection.Output;
                    ds = dbc.RunProcedure("PACK_WEB_ZYXX.Sys_JHK_select", opara, "table");
                    return ds.Tables[0];
                }
                finally
                {
                    dbc.Close();
                }
            }
        }

        private static DataTable dt_zyxx = InitZYXXTable();
        public static DataTable TZYXX()//返回志愿学校静态表
        {
            return dt_zyxx;
        }

        private static Hashtable zyxx = InitHashZYXXTable();
        public static Struct_ZYXX ZYXXByKey(string key)//根据键值,返回一个结构
        {
            object obj = zyxx[key];
            return (obj != null ? (Struct_ZYXX)obj : new Struct_ZYXX());
        }

        private static DataTable InitTableZYZY(string xxdm)//初始化志愿专业静态表
        {
            lock (typeof(SysData))
            {
                DBClass dbc = new DBClass();
                DataSet ds = new DataSet();
                try
                {
                    OracleParameter[] opara ={
                                             new OracleParameter("zy_xxdm",OracleType.VarChar),
                                             new OracleParameter("mycur",OracleType.Cursor)
                                         };
                    opara[0].Value = xxdm;
                    opara[1].Direction = ParameterDirection.Output;
                    ds = dbc.RunProcedure("PACK_WEB_ZYXX.Sys_JHK_selectByXXDM", opara, "table");
                    return ds.Tables[0];
                }
                finally
                {
                    dbc.Close();
                }
            }
        }

        private static Hashtable InitTableZYZY()//初始化哈希表——键值对“县区——志愿专业静态表”
        {
            lock (typeof(SysData))
            {
                Hashtable zyzy = new Hashtable();
                string zyxx;
                foreach (DataRow dr in dt_zyxx.Rows)
                {
                    zyxx = dr["DM"].ToString();
                    zyzy.Add(zyxx, InitTableZYZY(zyxx));
                }
                return zyzy;
            }

        }

        private static Hashtable dt_zyzy = InitTableZYZY();//根据学校返回志愿专业静态表
        public static DataTable ZYZY(string xxdm)
        {
            object obj = dt_zyzy[xxdm];
            return (obj != null ? (DataTable)obj : new DataTable());
        }

        private static Hashtable InitHashZYZYTable(string xxdm)//初始化志愿专业哈希表，键值对为“专业代码——志愿专业结构体”
        {
            lock (typeof(SysData))
            {
                Hashtable ht = new Hashtable();
                Struct_ZY zd;
                string dm;
                DBClass dbc = new DBClass();
                try
                {
                    OracleParameter[] opara ={
                                             new OracleParameter("zy_xxdm",OracleType.VarChar),
                                             new OracleParameter("mycur",OracleType.Cursor)
                                         };
                    opara[0].Value = xxdm;
                    opara[1].Direction = ParameterDirection.Output;
                    OracleDataReader Reader = dbc.RunProcedure("PACK_WEB_ZYXX.Sys_JHK_selectByXXDM", opara);
                    while (Reader.Read())
                    {
                        dm = Reader["DM"].ToString();
                        zd.dm = Reader["DM"].ToString();
                        zd.mc = Reader["MC"].ToString();
                        ht.Add(dm, zd);
                    }
                    Reader.Close();
                    return ht;
                }
                finally
                {
                    dbc.Close();
                }
            }
        }

        private static Hashtable InitHashZYZY()//初始化哈希表——键值对“志愿学校——志愿专业哈希表”
        {
            lock (typeof(SysData))
            {
                Hashtable zyzy = new Hashtable();
                string zyxx;
                foreach (DataRow dr in dt_zyxx.Rows)
                {
                    zyxx = dr["DM"].ToString();
                    zyzy.Add(zyxx, InitHashZYZYTable(zyxx));
                }
                return zyzy;
            }

        }

        private static Hashtable hash_zyzy = InitHashZYZY();
        public static Struct_ZY ZYZYByKey(string xxdm, string key)//根据键值,返回一个结构
        {
            object obj = hash_zyzy[xxdm];
            Hashtable zyzy = (obj != null ? (Hashtable)obj : new Hashtable());
            obj = zyzy[key];
            return (obj != null ? (Struct_ZY)obj : new Struct_ZY());
        }
        #endregion
        private static DataTable ZYXX_SelectTCSJHK()
        {
            lock (typeof(SysData))
            {
                DBClass dbc = new DBClass();
                try
                {
                    OracleParameter[] para ={
                                             new OracleParameter("mycur",OracleType.Cursor)
                                         };
                    para[0].Direction = ParameterDirection.Output;
                    return dbc.RunProcedure("PACK_WEB_ZYXX.Sys_SelectTCSJHK", para, "table").Tables[0];
                }
                finally
                {
                    dbc.Close();
                }
            }
        }
        private static DataTable _tcsjhk = ZYXX_SelectTCSJHK();
        public static DataTable TCSJHK
        {
            get
            {
                return _tcsjhk;
            }
        }

        private static DataTable dt_bklb = InitZDData("kslbdm");
        private static Hashtable bklb = FromZDDataTableToHashTable(dt_bklb);
        public static Struct_ZD BKLBByKey(string key)//根据键值,返回一个结构
        {
            object obj = bklb[key];
            return (obj != null ? (Struct_ZD)obj : new Struct_ZD());
        }
        private static DataTable dt_ZyzjZYMC = InitZDData("kstzbz");
        public static DataTable ZYZJ_ZYMC()
        {
            return dt_ZyzjZYMC;
        }
        private static Hashtable zyzjzymc = FromZDDataTableToHashTable(dt_ZyzjZYMC);
        public static Struct_ZD ZYZJ_ZYMCByKey(string key)//根据键值,返回一个结构
        {
            object obj = zyzjzymc[key];
            return (obj != null ? (Struct_ZD)obj : new Struct_ZD());
        }

        private static Hashtable ht_zymc = InitZYMCHash();
        private static DataTable InitKSTZ()
        {
            DataTable dt = new DataTable();
            dt = InitZDData("kstzbz");
            //dt == null ? new DataTable() : dt;
            DataRow dr = dt.NewRow();
            dr["BM"] = "";
            dr["ZDM"] = "kstzbz";
            dr["MC"] = "无";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
            return dt;
        }
        private static DataTable dt_kstz = InitKSTZ();
        private static Hashtable kstz = FromZDDataTableToHashTable(dt_kstz);
        public static Struct_ZD KSTZByKey(string key)//根据键值,返回一个结构
        {
            object obj = kstz[key];
            return (obj != null ? (Struct_ZD)obj : new Struct_ZD());
        }

        private static DataTable ShowZYXX(string xqdm, string wz, int pc)
        {
            lock (typeof(SysData))
            {
                DBClass dbc = new DBClass();
                DataSet ds = new DataSet();
                try
                {
                    OracleParameter[] parameter = {
                                                  new OracleParameter("zy_xqdm",OracleType.VarChar),
                                                  new OracleParameter("zy_wz",OracleType.VarChar),
                                                  new OracleParameter("zy_pc",OracleType.Int32),
                                                  new OracleParameter("mycur",OracleType.Cursor)
                                              };
                    parameter[0].Value = xqdm;
                    parameter[1].Value = wz;
                    parameter[2].Value = pc;
                    parameter[3].Direction = ParameterDirection.Output;

                    ds = dbc.RunProcedure("PACK_WEB_ZYXX.Sys_XQ_JHK_selectByXQXXLBWZPC", parameter, "table");
                    return ds.Tables[0];
                }
                finally
                {
                    dbc.Close();
                }
            }
        }
        private static DataTable InitTableWZ()
        {
            lock (typeof(SysData))
            {
                OracleConnection con = new OracleConnection(Config.connectionString);
                DataSet ds = new DataSet();
                try
                {
                    con.Open();
                    OracleCommand com = new OracleCommand(@"select distinct WZ from XQ_JHK order by wz", con);
                    OracleDataAdapter da = new OracleDataAdapter(com);
                    da.Fill(ds);
                    return ds.Tables[0];
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private static DataTable dt_wz = InitTableWZ();

        private static Hashtable InitHashZYXXPC1()
        {
            lock (typeof(SysData))
            {
                Hashtable hashzyxx = new Hashtable();
                foreach (DataRow dr in dt_xqdm.Rows)
                {
                    Hashtable hash_zyxx = new Hashtable();
                    foreach (DataRow dw in dt_wz.Rows)
                    {
                        hash_zyxx.Add(dw["WZ"].ToString(), ShowZYXX(dr["V_DM"].ToString(), dw["WZ"].ToString(), 1));
                    }
                    hashzyxx.Add(dr["V_DM"].ToString(), hash_zyxx);
                }
                return hashzyxx;
            }
        }

        private static Hashtable InitHashZYXXPC2()
        {
            lock (typeof(SysData))
            {
                Hashtable hashzyxx = new Hashtable();
                foreach (DataRow dr in dt_xqdm.Rows)
                {
                    Hashtable hash_zyxx = new Hashtable();
                    foreach (DataRow dw in dt_wz.Rows)
                    {
                        hash_zyxx.Add(dw["WZ"].ToString(), ShowZYXX(dr["V_DM"].ToString(), dw["WZ"].ToString(), 2));
                    }
                    hashzyxx.Add(dr["V_DM"].ToString(), hash_zyxx);
                }
                return hashzyxx;
            }
        }

        private static Hashtable dt_zyxxtablepc1 = InitHashZYXXPC1();
        private static Hashtable dt_zyxxtablepc2 = InitHashZYXXPC2();
        public static DataTable ZYXXBYXQWZ(string xqdm, string wz, string strKslbdm)
        {
            object obj = new object();
            if (Convert.ToInt32(Config.pc) == 1)
            {
                obj = dt_zyxxtablepc1[xqdm];
            }
            else
            {
                obj = dt_zyxxtablepc2[xqdm];
            }
            Hashtable hash_zyxx = (obj != null ? (Hashtable)obj : new Hashtable());
            obj = hash_zyxx[wz];
            // return (obj != null ? (DataTable)obj : new DataTable());
            // 通过考生类别，过滤DataTable
            if (obj != null)
            {
                DataTable dtTemp = (DataTable)obj;
                // 表示“全部”
                if (strKslbdm == "-1")
                {
                    return dtTemp;
                }
                else
                {
                    DataTable dtTempFinal = dtTemp.Clone();
                    DataRow[] arrDrTemp = dtTemp.Select("kslb='" + strKslbdm + "'");
                    foreach (DataRow drTemp in arrDrTemp)
                    {
                        dtTempFinal.Rows.Add(drTemp.ItemArray);
                    }
                    dtTempFinal.AcceptChanges();

                    return dtTempFinal;
                }
            }
            else
            {
                return new DataTable();
            }
        }
        public static DataTable ZYXXBYXQWZ(string xqdm, string wz, int pc, string strKslbdm)
        {
            object obj = new object();
            if (pc == 1)
            {
                obj = dt_zyxxtablepc1[xqdm];
            }
            else
            {
                obj = dt_zyxxtablepc2[xqdm];
            }
            Hashtable hash_zyxx = (obj != null ? (Hashtable)obj : new Hashtable());
            obj = hash_zyxx[wz];
            // return (obj != null ? (DataTable)obj : new DataTable());
            // 通过考生类别，过滤DataTable
            if (obj != null)
            {
                DataTable dtTemp = (DataTable)obj;
                // 表示全部
                if (strKslbdm == "-1")
                {
                    return dtTemp;
                }
                else
                {
                    DataTable dtTempFinal = dtTemp.Clone();
                    DataRow[] arrDrTemp = dtTemp.Select("kslb='" + strKslbdm + "'");
                    foreach (DataRow drTemp in arrDrTemp)
                    {
                        dtTempFinal.Rows.Add(drTemp.ItemArray);
                    }
                    dtTempFinal.AcceptChanges();

                    return dtTempFinal;
                }
            }
            else
            {
                return new DataTable();
            }
        }
    }
    public struct Struct_ZYXX
    {
        public string dm;
        public string mc;
        public Struct_ZYXX(string dm1, string mc1)
        {
            mc1 = "";
            dm1 = "";
            dm = dm1;
            mc = mc1;
        }
    }

    public struct Struct_ZY
    {
        public string dm;
        public string mc;
        public Struct_ZY(string dm1, string mc1)
        {
            mc1 = "";
            dm1 = "";
            dm = dm1;
            mc = mc1;
        }
    }
}
