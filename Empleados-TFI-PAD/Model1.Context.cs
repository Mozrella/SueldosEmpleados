//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Empleados_TFI_PAD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EmpleadosTFIEntities : DbContext
    {
        public EmpleadosTFIEntities()
            : base("name=EmpleadosTFIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<departamento> departamento { get; set; }
        public virtual DbSet<detalle_recibo> detalle_recibo { get; set; }
        public virtual DbSet<empleado> empleado { get; set; }
        public virtual DbSet<recibo_sueldo> recibo_sueldo { get; set; }
        public virtual DbSet<rol> rol { get; set; }
    }
}
