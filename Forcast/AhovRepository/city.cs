//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AhovRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class city
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public city()
        {
            this.citybuilding = new HashSet<citybuilding>();
            this.organization = new HashSet<organization>();
            this.user = new HashSet<user>();
        }
    
        public int CityId { get; set; }
        public string Name { get; set; }
        public double Lenght { get; set; }
        public double Width { get; set; }
        public int Population { get; set; }
        public int ChildPercent { get; set; }
        public double Au_ch { get; set; }
        public double Au { get; set; }
        public double Aw_ch { get; set; }
        public double Aw { get; set; }
        public double Aa_ch { get; set; }
        public double Aa { get; set; }
        public double Apr { get; set; }
        public double Apr_ch { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<citybuilding> citybuilding { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<organization> organization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user> user { get; set; }
    }
}
