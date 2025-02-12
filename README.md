[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/0lo6gDOJ)
[![Open in Codespaces](https://classroom.github.com/assets/launch-codespace-2972f46106e565e64193e422d61a12cf1da4916b45550586e14ef0a7c637dd04.svg)](https://classroom.github.com/open-in-codespaces?assignment_repo_id=18151716)
# SESION DE LABORATORIO N° 03: Construyendo una aplicación Web utilizando Blazor Server

### Nombre: Jean Pier ELias Valverde Zamora

## OBJETIVOS
  * Desarrolla una aplicación web utilizando Blazor Server.

## REQUERIMIENTOS
  * Conocimientos: 
    - Bash o Powershell.
    - AWS CLI.
    - Net / Blazor
  * Hardware:
    - Virtualization activada en el BIOS.
    - CPU SLAT-capable feature.
    - Al menos 4GB de RAM.
  * Software:
    - Docker Desktop 
    - AWS CLI 2.x.x
    - Python 3.10 o superior
    - EB CLI
    - Dotnet 8
## CONSIDERACIONES INICIALES
  * Obtener y copiar los valores de acceso a nube (CLOUD ACCESS - AWS CLI) ubicados en el curso de LEARNER LAB de AWS Academy
    ```
    [default]
    aws_access_key_id=.....
    aws_secret_access_key=....
    aws_session_token=......
    ```
    1. Iniciar la aplicación Powershell o Windows Terminal en modo administrador
    2. En el terminal, ejecutar el siguiente comando para crear la carpeta y archivos de conexion a AWS
    ```Powershell
    md $env:USERPROFILE\.aws
    New-Item -Path $env:USERPROFILE\.aws -Name credentials -ItemType File
    New-Item -Path $env:USERPROFILE\.aws -Name config -ItemType File
    ```
    3. En el terminal, ejecutar el siguiente comando para editar el archivo de credenciales.
    ```Powershell
    notepad $env:USERPROFILE\.aws\credentials
    ```
    > Pegar los valores previamente obtenidos CLOUD ACCESS - AWS CLI y guardar los cambios en el archivos
    4. En el terminal, ejecutar el siguiente comando para editar el archivo de configuracion.
    ```Powershell
    notepad $env:USERPROFILE\.aws\config
    ```
    > Pegar el siguiente contenido y guardar los cambios.
    ```
    [default]
    region=us-east-1
    ```
    5. Cerrar y volver a abrir el terminal de Powershell
  * Clonar el repositorio mediante git para tener los recursos necesarios en una ubicación que no sea del sistema.
  * Colocar su nombre en el archivo   
    
## DESARROLLO
1. Iniciar la aplicación Powershell o Windows Terminal en modo administrador.

![alt text](image.png)

2. En el terminal, ubicarse en un ruta que no sea del sistema y ejecutar los siguientes comandos.
```Bash
md src
cd src
dotnet new blazor -o Financiera.WebApp
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.AspNetCore.Components.QuickGrid --version 8.0.0
dotnet add package Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter --version 8.0.0
dotnet tool install -g dotnet-ef --version 8.0.0
dotnet tool install -g dotnet-aspnet-codegenerator --version 8.0.0
```
![alt text](image-1.png)

1. Abrir Visual Studio Code y elegir la carpeta del proyecto, dentro del proyecto Financiera.WebApp, crear la carpeta Modelos, y dentro de esta crear los siguientes archivos con lo siguientes contenidos:
> Cliente.cs
```CSharp
namespace Financiera.WebApp.Modelos;
/// <summary>
/// Entidad de Dominio que representa al Cliente
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador único del cliente
    /// </summary>
    public int IdCliente { get; set; }
    /// <summary>
    /// Nombre completo del cliente
    /// </summary>
    public string NombreCliente { get; set; }
    
    /// <summary>
    /// Permite registrar a un nuevo cliente
    /// </summary>
    /// <param name="_nombre">Nombre completo del cliente</param>
    /// <returns>Instancia nueva de la clase Cliente</returns>
    public static Cliente Registrar(string _nombre)
    {
        return new Cliente(){
            NombreCliente = _nombre
        };
    }
    /// <summary>
    /// Permite modificar el nombre del cliente
    /// </summary>
    /// <param name="_nombre"></param>
    public void ModificarNombre(string _nombre)
    {
        NombreCliente = _nombre;
    }

}
```
> CuentaAhorro.cs
```CSharp
namespace Financiera.WebApp.Modelos;
/// <summary>
/// Clase de Dominio que representa una cuenta de ahorro
/// </summary>
public class CuentaAhorro
{
    /// <summary>
    /// 
    /// </summary>
    public int IdCuenta { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public string NumeroCuenta { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual Cliente Propietario { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public int IdPropietario { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal Tasa { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal Saldo { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime FechaApertura { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public bool Estado { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public ICollection<MovimientoCuenta> Movimientos { get; private set; }
    
    /// <summary>
    /// Primer constructor
    /// </summary>
    public CuentaAhorro()
    {
        Movimientos = new List<MovimientoCuenta>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_numeroCuenta"></param>
    /// <param name="_propietario"></param>
    /// <param name="_tasa"></param>
    /// <returns></returns>
    public static CuentaAhorro Aperturar(string _numeroCuenta, Cliente _propietario, decimal _tasa)
    {
        return new CuentaAhorro()
        {
            NumeroCuenta = _numeroCuenta,
            Propietario = _propietario,
            IdPropietario = _propietario.IdCliente,
            Tasa = _tasa,
            Saldo = 0
        };
    }

    public const string ERROR_MONTO_MENOR_IGUAL_A_CERO = "El monto no puede ser menor o igual a 0";
    
    public void Depositar(decimal monto)
    {
        if (monto <= 0)
            throw new Exception (ERROR_MONTO_MENOR_IGUAL_A_CERO);
        Saldo += monto;
    }
    
    public void Retirar(decimal monto)
    {
        if (monto <= 0)
            throw new Exception (ERROR_MONTO_MENOR_IGUAL_A_CERO);
        Saldo -= monto;
    }
}
```
> MovimientoCuenta.cs
```CSharp
namespace Financiera.Dominio.Modelos;
/// <summary>
/// Clase de WebApp que representa el movimientos de la cuenta
/// </summary>
public class MovimientoCuenta
{
    /// <summary>
    /// 
    /// </summary>
    public int NumeroMovimiento { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public CuentaAhorro Cuenta { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public int IdCuenta { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public TipoMovimiento Tipo { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public int IdTipoMovimiento { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime FechaMovimiento { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public decimal MontoMovimiento { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    public bool Estado { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_cuenta"></param>
    /// <param name="_tipo"></param>
    /// <param name="_monto"></param>
    /// <returns></returns>
    public static MovimientoCuenta Crear(CuentaAhorro _cuenta, TipoMovimiento _tipo, decimal _monto)
    {
        return new MovimientoCuenta() {
            Cuenta = _cuenta,
            IdCuenta = _cuenta.IdCuenta,
            Tipo = _tipo,
            IdTipoMovimiento = _tipo.IdTipo,
            FechaMovimiento = DateTime.Now,
            MontoMovimiento = _monto,
            Estado = true
        };
    }
    /// <summary>
    /// 
    /// </summary>
    public void Anular()
    {
        Estado = false;
    }
}
```
> TipoMovimiento.cs
```CSharp
namespace Financiera.WebApp.Modelos;
/// <summary>
/// Entidad de Dominio que representa un tipo de movimiento de cuenta de ahorro
/// </summary>
public class TipoMovimiento
{
    /// <summary>
    /// Representa el identificador del tipo de movimiento
    /// </summary>
    public int IdTipo { get; private set; }
    /// <summary>
    /// Representa la descripcion del tipo de movimiento
    /// </summary>
    public string DescripcionTipo { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_descripcion"></param>
    /// <returns></returns>
    public static TipoMovimiento Registrar(string _descripcion)
    {
        return new TipoMovimiento(){
            DescripcionTipo = _descripcion
        };
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_descripcion"></param>
    /// <returns></returns>
    public static TipoMovimiento Crear(int _id, string _descripcion)
    {
        return new TipoMovimiento(){
            IdTipo = _id,
            DescripcionTipo = _descripcion
        };
    }
}
```
![alt text](image-2.png)

4. En Visual Studio Code, dentro del proyecto Financiera.WebApp, crear la carpeta Mapeos, y dentro de esta crear los siguientes archivos con lo siguientes contenidos:
> ClienteConfiguracion.cs
```CSharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Financiera.WebApp.Modelos;
namespace Financiera.WebApp.Mapeos;
public class ClienteConfiguracion : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("CLIENTES");
        builder.HasKey(c => c.IdCliente);
        builder.Property(c => c.IdCliente).HasColumnName("ID_CLIENTE").HasComment("Identificador unico del cliente");
        builder.Property(c => c.NombreCliente).HasColumnName("NOM_CLIENTE").HasComment("Nombre del cliente").HasMaxLength(200).IsRequired();
    }
}
```
> CuentaAhorroConfiguracion.cs
```CSharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Financiera.WebApp.Modelos;
namespace Financiera.WebApp.Mapeos;
public class CuentaAhorroConfiguracion : IEntityTypeConfiguration<CuentaAhorro>
{
    public void Configure(EntityTypeBuilder<CuentaAhorro> builder)
    {
        builder.ToTable("CUENTAS_AHORRO");
        builder.HasKey(c => c.IdCuenta);
        builder.Property(c => c.IdCuenta).HasColumnName("ID_CUENTA").IsRequired();
        builder.Property(c => c.NumeroCuenta).HasColumnName("NUM_CUENTA").HasMaxLength(10).IsRequired();
        builder.Property(c => c.IdPropietario).HasColumnName("ID_CLIENTE").IsRequired();
        builder.HasOne(c => c.Propietario).WithMany().HasForeignKey(f => f.IdPropietario);
    }
}
```
> MovimientoCuentaConfiguracion.cs
```CSharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Financiera.WebApp.Modelos;
namespace Financiera.WebApp.Mapeos;
public class MovimientoCuentaConfiguracion : IEntityTypeConfiguration<MovimientoCuenta>
{
    public void Configure(EntityTypeBuilder<MovimientoCuenta> builder)
    {
        builder.ToTable("MOVIMIENTOS_CUENTA");
        builder.HasKey(c => c.NumeroMovimiento);
        builder.Property(c => c.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO").IsRequired();
        builder.Property(c => c.IdCuenta).HasColumnName("ID_CUENTA").IsRequired();
        builder.Property(c => c.IdTipoMovimiento).HasColumnName("ID_TIPO");
        builder.Property(c => c.MontoMovimiento).HasColumnName("MON_MOVIMIENTO").IsRequired();
        builder.HasOne(c => c.Cuenta).WithMany().HasForeignKey(f => f.IdCuenta);
        builder.HasOne(c => c.Tipo).WithMany().HasForeignKey(f => f.IdTipoMovimiento);
    }
}
```
> TipoMovimientoConfiguracion.cs
```CSharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Financiera.WebApp.Modelos;
namespace Financiera.WebApp.Mapeos;
public class TipoMovimientoConfiguracion : IEntityTypeConfiguration<TipoMovimiento>
{
    public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
    {
        builder.ToTable("TIPOS_MOVIMIENTO");
        builder.HasKey(c => c.IdTipo);
        builder.Property(c => c.IdTipo).HasColumnName("ID_TIPO").UseMySqlIdentityColumn();
        builder.Property(c => c.DescripcionTipo).HasColumnName("DES_TIPO").HasMaxLength(100).IsRequired();
    }
}
```
![alt text](image-3.png)

7. En Visual Studio Code, dentro del proyecto Financiera.WebApp, en la raiz crear el siguiente archivo y contenido:
> FinancieraContexto.cs
```CSharp
using Financiera.WebApp.Modelos;
using Microsoft.EntityFrameworkCore;
namespace Financiera.WebApp;
/// <summary>
/// Clase que contiene las Modelos y configuraciones de persistencia
/// del contexto Financiera
/// </summary>
public class FinancieraContexto : DbContext 
{
    // INICIO: Comentar o eliminar esta seccion luego de la migracion
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
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
    /// <value></value>
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
```

8. En el terminal, ejecutar el siguiente comando para iniciar una base de datos MariaDB:
```Bash
md site && cs site
eb init Financiera.WebApp -r us-east-1 -p 64bit-amazon-linux-2023-v3.3.0-running-.net-8
eb create dev-env -s -sr LabRole -ip LabInstanceProfile -db
```
> Nota: para la base de datos se utilizar las credenciales usuario: sqladmin y password: upt.2025

![alt text](image-4.png)

```Bash
Enter an RDS DB username (default is "ebroot"): sqladmin
Enter an RDS DB master password:
Retype password to confirm:
Creating application version archive "app-250211_164411034297".
Uploading Financiera.WebApp/app-250211_164411034297.zip to S3. This may take a while.
Upload Complete.
Environment details for: dev-env
  Application name: Financiera.WebApp
  Region: us-east-1
  Deployed Version: app-250211_164411034297
  Environment ID: e-5davmvupac
  Platform: arn:aws:elasticbeanstalk:us-east-1::platform/.NET 8 running on 64bit Amazon Linux 2023/3.3.0
  Tier: WebServer-Standard-1.0
  CNAME: UNKNOWN
  Updated: 2025-02-11 21:44:16.872000+00:00
Printing Status:
2025-02-11 21:44:15    INFO    createEnvironment is starting.
2025-02-11 21:44:17    INFO    Using elasticbeanstalk-us-east-1-895176160814 as Amazon S3 storage bucket for environment data.
2025-02-11 21:44:44    INFO    Created security group named: awseb-e-5davmvupac-stack-AWSEBSecurityGroup-9bNKKBjB6ZNe
2025-02-11 21:44:59    INFO    Creating RDS database named: awseb-e-5davmvupac-stack-awsebrdsdatabase-l0qgj4q0bdos. This may take a few minutes.
2025-02-11 21:44:59    INFO    Created EIP: 3.231.45.110
 -- Events -- (safe to Ctrl+C)
```

9. En el terminal, verificar las instancias de base de datos creadas.
```Bash
aws rds describe-db-instances
```
![alt text](image-5.png)

```Bash
{
    "DBInstances": [
        {
            "DBInstanceIdentifier": "awseb-e-5davmvupac-stack-awsebrdsdatabase-l0qgj4q0bdos",
            "DBInstanceClass": "db.t3.small",
            "Engine": "mysql",
            "DBInstanceStatus": "creating",
            "MasterUsername": "sqladmin",
            "DBName": "ebdb",
            "AllocatedStorage": 5,
            "PreferredBackupWindow": "06:16-06:46",
            "BackupRetentionPeriod": 1,
            "DBSecurityGroups": [
                {
                    "DBSecurityGroupName": "awseb-e-5davmvupac-stack-awsebrdsdbsecuritygroup-1ujed33zndgwf",
                    "Status": "active"
                }
            ],
            "VpcSecurityGroups": [
                {
                    "VpcSecurityGroupId": "sg-0987c5d885486d5c3",
                    "Status": "active"
                }
            ],
            "DBParameterGroups": [
                {
                    "DBParameterGroupName": "default.mysql8.0",
                    "ParameterApplyStatus": "in-sync"
                }
            ],
            "AvailabilityZone": "us-east-1d",
            "DBSubnetGroup": {
                "DBSubnetGroupName": "default",
                "DBSubnetGroupDescription": "default",
                "VpcId": "vpc-08cb492e195299b66",
                "SubnetGroupStatus": "Complete",
                "Subnets": [
                    {
                        "SubnetIdentifier": "subnet-042e705e36e7860fa",
                        "SubnetAvailabilityZone": {
                            "Name": "us-east-1a"
                        },
                        "SubnetOutpost": {},
                        "SubnetStatus": "Active"
                    },
                    {
                        "SubnetIdentifier": "subnet-0bc61b511b77a088f",
                        "SubnetAvailabilityZone": {
                            "Name": "us-east-1c"
                        },
                        "SubnetOutpost": {},
                        "SubnetStatus": "Active"
                    },
                    {
                        "SubnetIdentifier": "subnet-09c0bd1316f06c0cf",
                        "SubnetAvailabilityZone": {
                            "Name": "us-east-1f"
                        },
                        "SubnetOutpost": {},
                        "SubnetStatus": "Active"
                    },
                    {
                        "SubnetIdentifier": "subnet-0a5aca1e0832f6524",
                        "SubnetAvailabilityZone": {
                            "Name": "us-east-1d"
                        },
                        "SubnetOutpost": {},
                        "SubnetStatus": "Active"
                    },
                    {
                        "SubnetIdentifier": "subnet-0f8ec7f5e8cd0cbdf",
                        "SubnetAvailabilityZone": {
                            "Name": "us-east-1b"
                        },
                        "SubnetOutpost": {},
                        "SubnetStatus": "Active"
                    },
                    {
                        "SubnetIdentifier": "subnet-0b4350d2c94c3a513",
                        "SubnetAvailabilityZone": {
                            "Name": "us-east-1e"
                        },
                        "SubnetOutpost": {},
                        "SubnetStatus": "Active"
                    }
                ]
            },
            "PreferredMaintenanceWindow": "sat:04:18-sat:04:48",
            "PendingModifiedValues": {
                "MasterUserPassword": "****"
            },
            "MultiAZ": false,
            "EngineVersion": "8.0.40",
            "AutoMinorVersionUpgrade": true,
            "ReadReplicaDBInstanceIdentifiers": [],
            "LicenseModel": "general-public-license",
            "OptionGroupMemberships": [
                {
                    "OptionGroupName": "default:mysql-8-0",
                    "Status": "in-sync"
                }
            ],
            "PubliclyAccessible": true,
            "StorageType": "gp2",
            "DbInstancePort": 0,
            "StorageEncrypted": false,
            "DbiResourceId": "db-FG7W5FWF7T6BJEHNOIQT4ATAGY",
            "CACertificateIdentifier": "rds-ca-rsa2048-g1",
            "DomainMemberships": [],
            "CopyTagsToSnapshot": false,
            "MonitoringInterval": 0,
            "DBInstanceArn": "arn:aws:rds:us-east-1:895176160814:db:awseb-e-5davmvupac-stack-awsebrdsdatabase-l0qgj4q0bdos",
            "IAMDatabaseAuthenticationEnabled": false,
            "DatabaseInsightsMode": "standard",
            "PerformanceInsightsEnabled": false,
            "DeletionProtection": false,
            "AssociatedRoles": [],
            "TagList": [],
            "CustomerOwnedIpEnabled": false,
            "ActivityStreamStatus": "stopped",
            "BackupTarget": "region",
            "NetworkType": "IPV4",
            "StorageThroughput": 0,
            "CertificateDetails": {
                "CAIdentifier": "rds-ca-rsa2048-g1"
            },
            "DedicatedLogVolume": false,
            "IsStorageConfigUpgradeAvailable": false,
            "EngineLifecycleSupport": "open-source-rds-extended-support"
        }
    ]
}
```
10.  En el terminal, ejecutar el siguiente comando para añadir el puerto de entrada a la base de datos (1521) el cual servira para la comunicación, reemplazar el valor de group-id por el valor de VpcSecurityGroupId obtenido en el paso anterior
```Bash
aws ec2 authorize-security-group-ingress --group-id sg-XXXXXXXXXXXXXX --protocol tcp --port 1521 --cidr 0.0.0.0/0
```
![alt text](image-6.png)

11.  En Visual Studio Code, editar el archivo appsetting.json, que se encuentra en el proyecto Financiera.WebApp, y adicionar lo siguiente despues de la apertura de la primera llave.
```JSON
  "ConnectionStrings": {
    "FinancieraBD": "Server=XXXXXXXXXXXXXXXX; User ID=sqlamin; Password=upt.2025; Database=ebdb"
  },
```
> Nota: Reemplazar las XXXXXXXXXX con el nombre del servidor de base de datos generado previamente.
12.  En Visual Studio Code, en el proyecto Financiera.WebApp modificar el archivo program.cs, al inicio del archivo agregar
```C#
using Financiera.WebApp;
using Microsoft.EntityFrameworkCore;
```
13.  En Visual Studio Code, en el proyecto Financiera.WebApp modificar el archivo program.cs, debajo de la linea que indica `// Add services to the container`.
```C#
string connectionString = builder.Configuration.GetConnectionString("FinancieraBD")??"";
builder.Services.AddDbContext<FinancieraContexto>(
    opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddQuickGridEntityFrameworkAdapter();
```

14.  En el terminal, esperar un minuto que inicie correctamente el contenedor y ejecutar el siguiente comando para efectuar la migración:
```Bash
cd Financiera.WebApp
dotnet ef migrations add CrearFinancieraBD
dotnet ef database update
```

![alt text](image-7.png)

15.  En el terminal, proceder a generar la interfaz del cliente con el siguiente comando:
```Bash
cd Financiera.WebApp
dotnet aspnet-codegenerator blazor CRUD -m Cliente -dc FinancieraContexto
cd ..
```
![alt text](image-8.png)

16.  En Visual Studio Code, en el proyecto Financiera.WebApp en la ruta Components\Layout modificar el archivo NavMenu.razor
> dice
```Razor
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="weather">
                <span class="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Weather
            </NavLink>
        </div>
```
> debe decir
```Razor
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="clientes">
                <span class="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Custormers
            </NavLink>
        </div>
```

![alt text](image-9.png)

17.  En el terminal, para revisar la aplicación utilizar el siguiente comando:
```
eb open
```

---
## Actividades Encargadas
1. Construir el archivo terraform (archivo main.tf) para aprovisionar la insfraestructura mediante Github Actions (archivo infra.yml) en AWS (4ptos)
2. Completar las vistas para los demas modelos Cuenta, Movimiento y Tipo. (2ptos)
3. Completar la documentación de las clases (DocFX). (2ptos)
4. Generar el diagrama Mermaid correspondiente al proyecto (dotnet tool install --global dll2mmd) (2ptos)
5. Generar la automatización del despliegue de los cambios en la aplicación (archivo deploy.yml) (4 ptos)
