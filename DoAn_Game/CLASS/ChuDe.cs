using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn_Game.CLASS
{
    class ChuDe
    {
        private ConnectionCreation conc = new ConnectionCreation();
        #region"Thuoc tinh va properties"
        private int maChuDe;

        public int MaChuDe
        {
            get { return maChuDe; }
            set { maChuDe = value; }
        }
        private string tenChuDe;

        public string TenChuDe
        {
            get { return tenChuDe; }
            set { tenChuDe = value; }
        }
        private string noiDung;

        public string NoiDung
        {
            get { return noiDung; }
            set { noiDung = value; }
        }
        private string moTa;

        public string MoTa
        {
            get { return moTa; }
            set { moTa = value; }
        }
        private int thoiGian;

        public int ThoiGian
        {
            get { return thoiGian; }
            set { thoiGian = value; }
        }
        private int diemToiDa;

        public int DiemToiDa
        {
            get { return diemToiDa; }
            set { diemToiDa = value; }
        }
        private int capDoKho;

        public int CapDoKho
        {
            get { return capDoKho; }
            set { capDoKho = value; }
        }
        private bool tinhTrang;

        public bool TinhTrang
        {
            get { return tinhTrang; }
            set { tinhTrang = value; }
        }
        #endregion
        #region"phuong thuc"
        public DataTable GetTopic(int id)
        {
            DataTable dt = new DataTable();
            string sql = "select * from CHUDE where MaChuDe=" + id.ToString();
            dt = conc.ExecuteQuery(sql);
            return dt;
        }

        public List<ChuDe> GetLessonListByLevel(int level)
        {
            List<ChuDe> LessonList = new List<ChuDe>();
            string sql = "select * from CHUDE where TinhTrang=1 and CapDoKho=" + level.ToString();
            DataTable dt = conc.ExecuteQuery(sql);
            ChuDe lesson;
            foreach (DataRow row in dt.Rows)
            {
                lesson = new ChuDe();
                lesson.maChuDe = int.Parse(row["MaChuDe"].ToString());
                lesson.tenChuDe = row["TenChuDe"].ToString();
                lesson.noiDung = row["NoiDung"].ToString();
                lesson.moTa = row["MoTa"].ToString();
                lesson.thoiGian = int.Parse(row["ThoiGian"].ToString());
                lesson.diemToiDa = int.Parse(row["DiemToiDa"].ToString());
                lesson.capDoKho = int.Parse(row["CapDoKho"].ToString());
                lesson.TinhTrang = Boolean.Parse(row["TinhTrang"].ToString());
                LessonList.Add(lesson);
            }
            return LessonList;
        }
        //Ham lấy 1 chủ đề
        public ChuDe GetTopicByID(int id)
        {
            ChuDe result = new ChuDe();
            DataTable dt = GetTopic(id);
            DataRow dr = dt.Rows[0];
            result.maChuDe = int.Parse(dr["MaChuDe"].ToString());
            result.tenChuDe = dr["TenChuDe"].ToString();
            result.noiDung = dr["NoiDung"].ToString();
            result.moTa = dr["MoTa"].ToString();
            result.thoiGian = int.Parse(dr["ThoiGian"].ToString());
            result.diemToiDa = int.Parse(dr["DiemToiDa"].ToString());
            result.capDoKho = int.Parse(dr["CapDoKho"].ToString());
            result.tinhTrang = bool.Parse(dr["TinhTrang"].ToString());
            return result;
        }

        //Hàm đếm số lượng từ trong bài tập

        public static int WordsInLesson(ChuDe chude)
        {
            int wordCount = 0;
            string[] str = chude.noiDung.Split(new string[] { "[P]" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < str.Length; i++)
            {
                string str1 = str[i].Trim();
                for (int j = 0; j < str1.Length; j++)
                {
                    if (str1[j] == ' ')
                        wordCount++;
                }
            }
            return wordCount;
        }

        public DataTable LoadDanhSachBaiTap(int type)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql += " Select MaBaiTap,TenBaiTap ";
            sql += " From BaiTap ";
            sql += " Where TinhTrang=True and LoaiBaiTap =" + type.ToString();
            dt = conc.ExecuteQuery(sql);
            return dt;
        }
        public static string RemoveQuote(string text)
        {
            return text.Replace('\'', 'a');
        }
        #endregion
    }
}
