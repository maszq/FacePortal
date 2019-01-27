using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacePortal
{
    public class Character
    {

        public int id;
        public int twarz_wys;
        public int twarz_szer;
        public int oko_lewe_wys;
        public int oko_lewe_szer;
        public int oko_prawe_wys;
        public int oko_prawe_szer;
        public int usta_wys;
        public int usta_szer;
        public int odl_oczy;
        public int nos_wys;
        public int polik_lewy_szer;
        public int polik_prawy_szer;
        public int czolo_wys;
        public int czolo_szer;
        public int broda_wys;
        public int broda_szer;
        public int wlosy_kolor;


        public Character(SqlDataReader dr)
        {
            id = dr.GetInt32(0);
            twarz_wys = dr.GetInt32(1);
            twarz_szer = dr.GetInt32(2);
            oko_lewe_wys = dr.GetInt32(3);
            oko_lewe_szer = dr.GetInt32(4);
            oko_prawe_wys = dr.GetInt32(5);
            oko_prawe_szer = dr.GetInt32(6);
            usta_wys = dr.GetInt32(7);
            usta_szer = dr.GetInt32(8);
            odl_oczy = dr.GetInt32(9);
            nos_wys = dr.GetInt32(10);
            polik_lewy_szer = dr.GetInt32(11);
            polik_prawy_szer = dr.GetInt32(12);
            czolo_wys = dr.GetInt32(13);
            czolo_szer = dr.GetInt32(14);
            broda_wys = dr.GetInt32(15);
            broda_szer = dr.GetInt32(16);
            wlosy_kolor = dr.GetInt32(17);

        }

    }
}