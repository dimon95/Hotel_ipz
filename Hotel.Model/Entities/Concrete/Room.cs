using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model.Entities.Concrete
{
    public enum SearchCriteria: byte { Freedge = 0x01, TV = 0x02, WiFi = 0x04, Vault = 0x08, Count };

    public class Room : Abstract.Place
    {
        /*public bool HasFreedge { get; set; }
        public bool HasTV { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasVault { get; set; }*/

        //public virtual IDictionary<string, bool> SearchCriterias { get; private set; }
        
        public int SearchCriterias { get; set; }

        public int BedCount { get; set; }

        protected Room () { }

        public Room ( Guid id, int number, int personsCount, decimal price, string description, int bedCount )
            : base( id, number, personsCount, price, description )
        {
            BedCount = bedCount;

            /*SearchCriterias = new Dictionary<string, bool>();

            SearchCriterias.Add( "HasFreedge", false );
            SearchCriterias.Add( "HasTV", false );
            SearchCriterias.Add( "HasWiFi", false );
            SearchCriterias.Add( "HasVault", false );*/

            SearchCriterias = 0x00;
        }

        public void SetCriteria ( SearchCriteria criteria )
        {
            /*if ( !SearchCriterias.ContainsKey( name ) )
                throw new ArgumentException("criteria no found");

            SearchCriterias [ name ] = true;*/

            if ( criteria == SearchCriteria.Count )
                throw new ArgumentException("Invalid value");

            SearchCriterias |= ( byte ) criteria;
        }

        public void ResetCriteria ( SearchCriteria criteria )
        {
            /*if ( !SearchCriterias.ContainsKey( name ) )
                throw new ArgumentException( "criteria no found" );

            SearchCriterias [ name ] = false;*/

            if ( criteria == SearchCriteria.Count )
                throw new ArgumentException( "Invalid value" );

            SearchCriterias &= ( byte ) criteria;
        }

        public IList<string> GetAllCriterias ()
        {
            IList<string> res = new List<string>();

            //for(SearchCriteria sc = (SearchCriteria)0x01; sc < SearchCriteria.Count;  sc )

            byte sc = 0x01;

            while ( sc < (byte)SearchCriteria.Count)
            {
                res.Add( (( SearchCriteria ) sc).ToString() );



                sc = (byte)(sc * 2);                
            }

            return res;
            //return SearchCriterias.Keys.ToList();
        }
    }
}
