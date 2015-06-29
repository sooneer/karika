using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSS.Karika.Business
{
    public static class KarikaBusiness
    {

        public static string ConvertToImageName(decimal id)
        {
            String result = string.Empty;

            if (id > 9999)
            {
                result = id.ToString() + ".jpg";
            }
            else
            {
                if (id > 999)
                {
                    result = "0" + id.ToString() + ".jpg";
                }
                else
                {
                    if (id > 99)
                    {
                        result = "00" + id.ToString() + ".jpg";
                    }
                    else
                    {
                        if (id > 9)
                        {
                            result = "000" + id.ToString() + ".jpg";
                        }
                        else
                        {
                            if (id > 0)
                            {
                                result = "0000" + id.ToString() + ".jpg";
                            }
                        }
                    }
                }
            }

            return result;

        }

    }
}
