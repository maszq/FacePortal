using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacePortal
{
    
    public class Result
    {
        public int id_user;
        public int id_celebrite;
        public int percent_result;
        public bool ranking;

        public Result(int id, int id_cel, int percent, bool rank)
        {
            id_user = id;
            id_celebrite = id_cel;
            percent_result = percent;
            ranking = rank;
        }

    }

}