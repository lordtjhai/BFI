using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BFI
{
    public class TextGenerator
    {
        private const int _INTMAXCHAR = 62;
        private readonly List<string> _STRSEED;

        private RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();

        public TextGenerator()
        {
            int m_intCount = 0;
            _STRSEED = new List<string>();
            while (m_intCount < _INTMAXCHAR)
            {
                _STRSEED.Add(((char)(m_intCount + (m_intCount < 26 ? 65 : (m_intCount < 52 ? 71 : -4)))).ToString());
                m_intCount++;
            }
        }

        public string ByteArrayToString(byte[] InputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < InputArray.Length; i++)
            {
                output.Append(_STRSEED[InputArray[i] % _INTMAXCHAR]);
            }
            return output.ToString();
        }

        public string RandomText(int EvenLength)
        {
            byte[] m_bytRandomNo = new byte[(int)Math.Ceiling((decimal)EvenLength)];
            _rngProvider.GetBytes(m_bytRandomNo);
            return ByteArrayToString(m_bytRandomNo).Insert(7, DateTime.Now.ToString("dd")) + DateTime.Now.ToString("MM");
        }
    }
}
