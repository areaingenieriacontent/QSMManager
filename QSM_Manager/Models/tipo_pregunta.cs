//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QSM_Manager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tipo_pregunta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipo_pregunta()
        {
            this.pregunta = new HashSet<pregunta>();
        }
    
        public int tipo_pregunta_id { get; set; }
        public string tipo_pregunta_nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pregunta> pregunta { get; set; }
    }
}
