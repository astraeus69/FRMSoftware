using System;
using System.Collections.Generic;
using APIdbConection.Models.Catalogos;
using APIdbConection.Models.Movimientos;
using APIdbConection.Models.Utilidades;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APIdbConection.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // CATÁLOGOS
    public virtual DbSet<Catalogos.Usuarios> Usuarios { get; set; }
    public virtual DbSet<Catalogos.Empleados> Empleados { get; set; }
    public virtual DbSet<Catalogos.Cultivos> Cultivos { get; set; }
    public virtual DbSet<Catalogos.Viveros> Viveros { get; set; }
    public virtual DbSet<Catalogos.Ranchos> Ranchos { get; set; }
    public virtual DbSet<Catalogos.Vehiculos> Vehiculos { get; set; }


    // MOVIMIENTOS
    public virtual DbSet<Movimientos.Llaves> Llaves { get; set; }
    public virtual DbSet<Movimientos.Plantaciones> Plantaciones { get; set; }
    public virtual DbSet<Movimientos.Replantes> Replantes { get; set; }
    public virtual DbSet<Movimientos.Podas> Podas { get; set; }
    public virtual DbSet<Movimientos.Cosechas> Cosechas { get; set; }
    public virtual DbSet<Movimientos.Produccion> Producciones { get; set; }
    public virtual DbSet<Movimientos.PersonalCosecha> PersonalCosecha { get; set; }
    public virtual DbSet<Movimientos.Viajes> Viajes { get; set; }
    public virtual DbSet<Movimientos.CalificacionTarima> DetallesViajes { get; set; }
    public virtual DbSet<Movimientos.Tarimas> Tarimas { get; set; }
    public virtual DbSet<Movimientos.Conductores> Conductores { get; set; }
    public virtual DbSet<Movimientos.RecepcionViajes> RecepcionViajes { get; set; }
    public virtual DbSet<Movimientos.Proceso> Procesos { get; set; }
    public virtual DbSet<Movimientos.Ventas> Ventas { get; set; }
    public virtual DbSet<Movimientos.VentasDetalles> VentasDetalles { get; set; }


    // UTILIDADES
    public virtual DbSet<Utilidades.ErrorLog> ErrorLogs { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        // CATÁLOGOS

        // Usuarios
        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.Contrasena).HasMaxLength(60);
            entity.Property(e => e.Email).HasMaxLength(40);
            entity.Property(e => e.Estatus).HasMaxLength(20);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(15);
            entity.Property(e => e.Telefono).HasMaxLength(12);
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .HasColumnName("Usuario");
        });


        // Empleados
        modelBuilder.Entity<Empleados>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PRIMARY");

            entity.ToTable("empleados");

            entity.Property(e => e.IdEmpleado).HasColumnType("int(11)");
            entity.Property(e => e.Colonia).HasMaxLength(30);
            entity.Property(e => e.Cp)
                .HasMaxLength(5)
                .HasColumnName("CP");
            entity.Property(e => e.Departamento).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(40);
            entity.Property(e => e.Estado).HasMaxLength(30);
            entity.Property(e => e.Estatus).HasMaxLength(20);
            entity.Property(e => e.Municipio).HasMaxLength(30);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Puesto).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(12);
        });

        // Cultivos
        modelBuilder.Entity<Cultivos>(entity =>
        {
            entity.HasKey(e => e.IdCultivo).HasName("PRIMARY");

            entity.ToTable("cultivos");

            entity.Property(e => e.IdCultivo).HasColumnType("int(11)");
            entity.Property(e => e.TipoBerry).HasMaxLength(30);
            entity.Property(e => e.Variedad).HasMaxLength(60);
        });

        // Viveros
        modelBuilder.Entity<Viveros>(entity =>
        {
            entity.HasKey(e => e.IdVivero).HasName("PRIMARY");

            entity.ToTable("viveros");

            entity.Property(e => e.IdVivero).HasColumnType("int(11)");
            entity.Property(e => e.CodigoVivero).HasMaxLength(30);
            entity.Property(e => e.NombreVivero).HasMaxLength(60);
        });

        // Ranchos
        modelBuilder.Entity<Ranchos>(entity =>
        {
            entity.HasKey(e => e.IdRancho).HasName("PRIMARY");

            entity.ToTable("ranchos");

            entity.Property(e => e.IdRancho).HasColumnType("int(11)");
            entity.Property(e => e.Cp)
                .HasMaxLength(5)
                .HasColumnName("CP");
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(30);
            entity.Property(e => e.Municipio).HasMaxLength(30);
            entity.Property(e => e.NombreRancho).HasMaxLength(60);
            entity.Property(e => e.NumeroRancho).HasMaxLength(60);
            entity.Property(e => e.SuperficieAcres).HasPrecision(18, 2);
            entity.Property(e => e.SuperficieHa)
                .HasPrecision(18, 2)
                .HasColumnName("SuperficieHA");
        });

        // Vehiculos
        modelBuilder.Entity<Vehiculos>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo).HasName("PRIMARY");

            entity.ToTable("vehiculos");

            entity.Property(e => e.IdVehiculo).HasColumnType("int(11)");
            entity.Property(e => e.Marca).HasMaxLength(30);
            entity.Property(e => e.Modelo).HasMaxLength(30);
            entity.Property(e => e.Placas).HasMaxLength(7);
        });



        // MOVIMIENTOS

        // Llaves
        modelBuilder.Entity<Llaves>(entity =>
        {
            entity.HasKey(e => e.IdLlave).HasName("PRIMARY");

            entity.ToTable("llaves");

            entity.Property(e => e.IdLlave).HasColumnType("int(11)");
            entity.Property(e => e.CantidadTuneles).HasColumnType("int(11)");
            entity.Property(e => e.Disponibilidad);
            entity.Property(e => e.IdRancho).HasColumnType("int(11)");
            entity.Property(e => e.NombreLlave).HasMaxLength(60);
            entity.Property(e => e.SuperficieAcres).HasPrecision(18, 2);
            entity.Property(e => e.SuperficieHa)
                .HasPrecision(18, 2)
                .HasColumnName("SuperficieHA");
        });

        // Plantaciones
        modelBuilder.Entity<Plantaciones>(entity =>
        {
            entity.HasKey(e => e.IdPlantacion).HasName("PRIMARY");

            entity.ToTable("plantaciones");

            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.CantidadPlantas).HasColumnType("int(11)");
            entity.Property(e => e.EstatusPlantacion).HasMaxLength(15);
            entity.Property(e => e.IdCultivo).HasColumnType("int(11)");
            entity.Property(e => e.IdLlave).HasColumnType("int(11)");
            entity.Property(e => e.IdVivero).HasColumnType("int(11)");
            entity.Property(e => e.PlantasPorMetro).HasPrecision(18, 2);

            // Configuración para la propiedad FechaPlantacion
            entity.Property(e => e.FechaPlantacion).HasColumnType("datetime");
            entity.Property(e => e.NumSemPlantacion).HasColumnType("int(11)");

        });


        // Replantes
        modelBuilder.Entity<Replantes>(entity =>
        {
            entity.HasKey(e => e.IdReplante).HasName("PRIMARY");

            entity.ToTable("replantes");

            entity.Property(e => e.IdReplante).HasColumnType("int(11)");
            entity.Property(e => e.CantidadReplante).HasColumnType("int(11)");
            entity.Property(e => e.IdCultivo).HasColumnType("int(11)");
            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.IdVivero).HasColumnType("int(11)");
            entity.Property(e => e.FechaReplante).HasColumnType("datetime");
            entity.Property(e => e.NumSemReplante).HasColumnType("int(11)");

        });


        // Podas
        modelBuilder.Entity<Podas>(entity =>
        {
            entity.HasKey(e => e.IdPoda).HasName("PRIMARY");

            entity.ToTable("podas");

            entity.Property(e => e.IdPoda).HasColumnType("int(11)");
            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.TipoPoda).HasMaxLength(20);
            entity.Property(e => e.FechaPoda).HasColumnType("datetime");
            entity.Property(e => e.NumSemPoda).HasColumnType("int(11)");

        });

        // Cosechas
        modelBuilder.Entity<Cosechas>(entity =>
        {
            entity.HasKey(e => e.IdCosecha).HasName("PRIMARY");

            entity.ToTable("cosechas");

            entity.Property(e => e.IdCosecha).HasColumnType("int(11)");
            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.FechaCosecha).HasColumnType("date");
            entity.Property(e => e.NumSemCosecha).HasColumnType("int(11)");

        });


        // Produccion
        modelBuilder.Entity<Produccion>(entity =>
        {
            entity.HasKey(e => e.IdProduccion).HasName("PRIMARY");

            entity.ToTable("produccion");

            entity.Property(e => e.IdProduccion).HasColumnType("int(11)");
            entity.Property(e => e.IdCosecha).HasColumnType("int(11)");
            entity.Property(e => e.TipoCaja).HasMaxLength(30);
            entity.Property(e => e.CantidadCajas).HasColumnType("int(11)");
            entity.Property(e => e.KilosProceso).HasPrecision(18, 2);
        });


        // PersonalCosecha
        modelBuilder.Entity<PersonalCosecha>(entity =>
        {
            entity.HasKey(e => e.IdPersonalCosecha).HasName("PRIMARY");

            entity.ToTable("personal_cosecha");

            entity.Property(e => e.IdPersonalCosecha).HasColumnType("int(11)");
            entity.Property(e => e.IdCosecha).HasColumnType("int(11)");
            entity.Property(e => e.IdEmpleado).HasColumnType("int(11)");
            entity.Property(e => e.Jarras).HasColumnType("int(11)");
            entity.Property(e => e.PrecioJarra).HasPrecision(18, 2);
        });

        // Viajes
        modelBuilder.Entity<Viajes>(entity =>
        {
            entity.HasKey(e => e.IdViaje).HasName("PRIMARY");

            entity.ToTable("viajes");

            entity.Property(e => e.IdViaje).HasColumnType("int(11)");
            entity.Property(e => e.IdConductor).HasColumnType("int(11)");
            entity.Property(e => e.IdVehiculo).HasColumnType("int(11)");
            entity.Property(e => e.FechaSalida).HasColumnType("date");
            entity.Property(e => e.NumSemViaje).HasColumnType("int(11)");
            entity.Property(e => e.EstadoAprobacion).HasMaxLength(30);

        });

        // Conductores
        modelBuilder.Entity<Conductores>(entity =>
        {
            entity.HasKey(e => e.IdConductor).HasName("PRIMARY");

            entity.ToTable("conductores");

            entity.Property(e => e.IdConductor).HasColumnType("int(11)");
            entity.Property(e => e.IdEmpleado).HasColumnType("int(11)");
        });

        // Tarimas
        modelBuilder.Entity<Tarimas>(entity =>
        {
            entity.HasKey(e => e.IdTarima).HasName("PRIMARY");

            entity.ToTable("tarimas");

            entity.Property(e => e.IdTarima).HasColumnType("int(11)");
            entity.Property(e => e.IdProduccion).HasColumnType("int(11)");
            entity.Property(e => e.IdViaje).HasColumnType("int(11)");
            entity.Property(e => e.CantidadCajasViaje).HasColumnType("int(11)");
            entity.Property(e => e.Licencia).HasColumnType("int(11)");
            entity.Property(e => e.KilosProcesoViaje).HasPrecision(18, 2);
        });

        // Clasificar proceso
        modelBuilder.Entity<Proceso>(entity =>
        {
            entity.HasKey(e => e.IdProceso).HasName("PRIMARY");

            entity.ToTable("proceso");

            entity.Property(e => e.IdProceso).HasColumnType("int(11)");
            entity.Property(e => e.IdViaje).HasColumnType("int(11)");
            entity.Property(e => e.ClaseAkg).HasPrecision(10, 2);
            entity.Property(e => e.ClaseBkg).HasPrecision(10, 2);
            entity.Property(e => e.ClaseCkg).HasPrecision(10, 2);
            entity.Property(e => e.Rechazo).HasPrecision(10, 2);
        });

        // RecepcionViajes
        modelBuilder.Entity<RecepcionViajes>(entity =>
        {
            entity.HasKey(e => e.IdRecepcion).HasName("PRIMARY");

            entity.ToTable("recepcion_viaje");

            entity.Property(e => e.IdRecepcion).HasColumnType("int(11)");
            entity.Property(e => e.IdViaje).HasColumnType("int(11)");
            entity.Property(e => e.FechaRecepcion).HasColumnType("date");
            entity.Property(e => e.NumSemRecepcion).HasColumnType("int(11)");
            entity.Property(e => e.HoraRecepcion).HasColumnType("time");
            entity.Property(e => e.HoraInspeccion).HasColumnType("time");
        });

        // CalificacionTarima
        modelBuilder.Entity<CalificacionTarima>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("PRIMARY");

            entity.ToTable("calificacion_tarima");

            entity.Property(e => e.IdCalificacion).HasColumnType("int(11)");
            entity.Property(e => e.IdTarima).HasColumnType("int(11)");
            entity.Property(e => e.EstadoAprobacion)
                .HasMaxLength(30);
        });


        // Ventas
        modelBuilder.Entity<Ventas>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.ToTable("ventas");

            entity.Property(e => e.IdVenta).HasColumnType("int(11)");
            entity.Property(e => e.FechaFacturacion).HasColumnType("date");
            entity.Property(e => e.Total).HasPrecision(18, 2);
            entity.Property(e => e.PrecioDolar).HasPrecision(18, 2);
        });

        // DetallesVentas
        modelBuilder.Entity<VentasDetalles>(entity =>
        {
            entity.HasKey(e => new { e.IdVenta, e.IdTarima }).HasName("PRIMARY");

            entity.ToTable("ventasdetalles");

            entity.Property(e => e.IdVenta).HasColumnType("int(11)");
            entity.Property(e => e.IdTarima).HasColumnType("int(11)");
            entity.Property(e => e.PrecioVentaTarima).HasPrecision(18, 2);
            entity.Property(e => e.FechaRecepcion).HasColumnType("date");
        });


        // UTILIDADES
        // ErrorLogs/Mantenimiento
        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id);  // Llave primaria simple

            entity.ToTable("errorlogs");  // Nombre de la tabla

            entity.Property(e => e.Id).HasColumnType("int(11)").ValueGeneratedOnAdd(); // Columna Id
            entity.Property(e => e.UserName)
                .HasMaxLength(50)  // Limita el tamaño de la columna UserName
                .HasColumnType("varchar(50)");

            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(500)  // Limita el tamaño del mensaje de error
                .HasColumnType("varchar(500)");

            entity.Property(e => e.ErrorProcedure)
                .HasMaxLength(200)  // Limita el tamaño del nombre de la procedura
                .HasColumnType("varchar(200)");

            entity.Property(e => e.ErrorLine)
                .HasMaxLength(500)  // Lo tratamos como texto, ya que puede ser largo
                .HasColumnType("varchar(500)");

            entity.Property(e => e.ErrorTime)
                .HasColumnType("datetime");  // Definimos la columna como fecha y hora
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
