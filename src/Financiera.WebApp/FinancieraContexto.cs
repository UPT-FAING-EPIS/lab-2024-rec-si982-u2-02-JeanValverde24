using Financiera.Dominio.Modelos;
using Financiera.WebApp.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Financiera.WebApp;

/// <summary>
/// Clase que contiene las Modelos y configuraciones de persistencia
/// del contexto Financiera
/// </summary>
public class FinancieraContexto(IConfiguration configuration) : DbContext
{
    private readonly string _connectionString = configuration.GetConnectionString("FinancieraBD");

    // INICIO: Comentar o eliminar esta sección luego de la migración
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
      //  if (!optionsBuilder.IsConfigured)
        //{
          //  optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        //}
   // }
    // FIN

    /// <summary>
    /// Conjunto de datos cliente
    /// </summary>
    public DbSet<Cliente> Clientes { get; set; }
    /// <summary>
    /// Conjunto de datos TiposMovimiento
    /// </summary>
    public DbSet<TipoMovimiento> TiposMovimiento { get; set; }
    /// <summary>
    /// Conjunto de datos Cuentas de Ahorro
    /// </summary>
    public DbSet<CuentaAhorro> CuentasAhorro { get; set; }
    /// <summary>
    /// Conjunto de Datos de Movimientos de Cuentas
    /// </summary>
    public DbSet<MovimientoCuenta> MovimientosCuenta { get; set; }
    /// <summary>
    /// Configuración de cada entidad hacia la base de datos
    /// </summary>
    /// <param name="modelBuilder">Constructor del modelo</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Mapeos.ClienteConfiguracion());
        modelBuilder.ApplyConfiguration(new Mapeos.TipoMovimientoConfiguracion());
        modelBuilder.ApplyConfiguration(new Mapeos.CuentaAhorroConfiguracion());
        modelBuilder.ApplyConfiguration(new Mapeos.MovimientoCuentaConfiguracion());
    }
}
