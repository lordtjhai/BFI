using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BFI
{
    public class Cities
    {
        private const string _STRDATAADDRESS = "Data\\cities.json";
        private const decimal _DECMINSIMILARITY = (decimal)0.7;

        public class City
        {
            public string country;
            public string name;
            public string lat;
            public string lng;
        }

        public string Message { get; set; }
        public List<City> CitiesList { get; set; }

        private decimal Similarity(string CityName, string CityToCompare)
        {
            decimal m_decPercentage = 0.0M;
            string m_strSource = CityName.Length >= CityToCompare.Length ? CityName : CityToCompare;
            string m_strDestination = CityName.Length >= CityToCompare.Length ? CityToCompare : CityName;

            for (int m_intPosition = 0; m_intPosition < m_strSource.Length + m_strDestination.Length - 1; m_intPosition++)
            {
                int m_intOccurance = 0;
                string m_strFragmentedDestination = m_strDestination.Substring(m_intPosition <= m_strSource.Length ? 0 : m_intPosition - m_strSource.Length + 1,
                    m_intPosition < m_strDestination.Length ? m_intPosition + 1 :
                    (m_strSource.Length + m_strDestination.Length - 1 - m_intPosition > m_strDestination.Length ? m_strDestination.Length : m_strSource.Length + m_strDestination.Length - 1 - m_intPosition));
                string m_strFragmentedSource = m_strSource.Substring(m_intPosition >= m_strSource.Length - 1 ? 0 : m_strSource.Length - m_intPosition - 1, m_strFragmentedDestination.Length);

                for (int m_intFragmentedPosition = 0; m_intFragmentedPosition < m_strFragmentedSource.Length; m_intFragmentedPosition++)
                    m_intOccurance += (m_strFragmentedSource[m_intFragmentedPosition] == m_strFragmentedDestination[m_intFragmentedPosition] ? 1 : 0);

                decimal m_decPercentageTemp = (decimal)m_intOccurance / (m_strSource.Length + m_strDestination.Length - m_strFragmentedSource.Length);
                m_decPercentage = m_decPercentage > m_decPercentageTemp ? m_decPercentage : m_decPercentageTemp;
            }
            return m_decPercentage;
        }

        public void PrepareCities(string Country)
        {
            try
            {
                string m_strCities = File.ReadAllText(_STRDATAADDRESS);

                CitiesList = JsonConvert.DeserializeObject<List<City>>(m_strCities);
                if (CitiesList != null && CitiesList.Count > 0)
                    CitiesList = CitiesList.FindAll(m_objCity => m_objCity.country.Contains(Country));
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public List<string> Find(string City)
        {
            City = City.ToLower();
            List<string> m_lstCities = new List<string>();
            SortedDictionary<string, decimal> m_sdiCities = new SortedDictionary<string, decimal>();

            foreach (City m_objCity in CitiesList)
            {
                if (m_sdiCities.Any(m_objCities => m_objCities.Key == m_objCity.name))
                    continue;

                decimal m_decPercentage = Similarity(City, m_objCity.name.ToLower());
                if (m_decPercentage >= _DECMINSIMILARITY)
                    m_sdiCities.Add(m_objCity.name, m_decPercentage);
            }
            if (m_sdiCities.Count > 0)
            {
                foreach (KeyValuePair<string, decimal> m_kvpCities in m_sdiCities.OrderByDescending(m_objOrder => m_objOrder.Value))
                    m_lstCities.Add(m_kvpCities.Key);
            }

            return m_lstCities;
        }
    }
}
