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
    
    public partial class pregunta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pregunta()
        {
            this.respuesta_pregunta = new HashSet<respuesta_pregunta>();
        }
    
        public int pregunta_id { get; set; }
        public int subcategoria_id { get; set; }
        public int tipo_pregunta_id { get; set; }
        public string enunciado_pregunta { get; set; }
        public Nullable<double> valor_pregunta { get; set; }
        public Nullable<int> tiene_imagen { get; set; }
        public string imagen_url { get; set; }
    
        public virtual subcategoria subcategoria { get; set; }
        public virtual tipo_pregunta tipo_pregunta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<respuesta_pregunta> respuesta_pregunta { get; set; }
    }
}
