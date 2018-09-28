using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BFI
{
    public class XYZNumber
    {
        private const int _INTMAXCHAR = 62;
        private readonly List<string> _STRSEED;

        private RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();

        //public TextGenerator()
        //{
        //    int m_intCount = 0;
        //    _STRSEED = new List<string>();
        //    while (m_intCount < _INTMAXCHAR)
        //    {
        //        _STRSEED.Add(((char)(m_intCount + (m_intCount < 26 ? 65 : (m_intCount < 52 ? 71 : -4)))).ToString());
        //        m_intCount++;
        //    }
        //}

        //public string ByteArrayToString(byte[] InputArray)
        //{
        //    StringBuilder output = new StringBuilder("");
        //    for (int i = 0; i < InputArray.Length; i++)
        //    {
        //        output.Append(_STRSEED[InputArray[i] % _INTMAXCHAR]);
        //    }
        //    return output.ToString();
        //}

        private string ConvertToXYZ(int Dividend, int Divisor)
        {
            if (Dividend % Divisor == 0)
                switch (Divisor)
                {
                    case 3: return "X";
                    case 5: return "Y";
                    case 7: return "Z";
                }
            return string.Empty;
        }
        public string GenerateXYZNumber(int Number)
        {
            List<string> m_lstReturn = new List<string>();
            int m_intCount = 1;
            while (m_intCount <= Number)
            {
                string m_strConvertedXYZNumber = ConvertToXYZ(m_intCount, 3) + ConvertToXYZ(m_intCount, 5) + ConvertToXYZ(m_intCount, 7);
                m_lstReturn.Add(m_strConvertedXYZNumber == string.Empty ? m_intCount.ToString() : m_strConvertedXYZNumber);
                m_intCount++;
            }

            return String.Join(" ", m_lstReturn);
        }
    }
}
