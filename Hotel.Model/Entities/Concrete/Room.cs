using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model.Entities.Concrete
{
    public class Room : Abstract.Place
    {
        /*public bool HasFreedge { get; set; }
        public bool HasTV { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasVault { get; set; }*/

        public virtual IDictionary<string, bool> SearchCriterias { get; private set; }
        
        public int BedCount { get; set; }

        protected Room () { }

        public Room ( Guid id, int number, int personsCount, decimal price, string description, int bedCount )
            : base( id, number, personsCount, price, description )
        {
            BedCount = bedCount;

            SearchCriterias = new Dictionary<string, bool>();

            SearchCriterias.Add( "HasFreedge", false );
            SearchCriterias.Add( "HasTV", false );
            SearchCriterias.Add( "HasWiFi", false );
            SearchCriterias.Add( "HasVault", false );
        }

        public void SetCriteria ( string name )
        {
            if ( !SearchCriterias.ContainsKey( name ) )
                throw new ArgumentException("criteria no found");

            SearchCriterias [ name ] = true;
        }

        public void ResetCriteria ( string name )
        {
            if ( !SearchCriterias.ContainsKey( name ) )
                throw new ArgumentException( "criteria no found" );

            SearchCriterias [ name ] = false;
        }

        public IList<string> GetAllCriterias ()
        {
            return SearchCriterias.Keys.ToList();
        }
    }
}
