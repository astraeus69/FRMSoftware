using System;
using System.Collections.Generic;
using APIdbConection.Models.Catalogos;
using APIdbConection.Models.Movimientos;
using APIdbConection.Models.Utilidades;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APIdbConection.Models;

public partial class HistoricoDbContext : DbContext
{
    public HistoricoDbContext()
    {
    }

    public HistoricoDbContext(DbContextOptions<HistoricoDbContext> options)
        : base(options)
    {
    }

    // HISTÓRICOS - CATÁLOGOS
    public virtual DbSet<UsuariosHistorico> UsuariosHistorico { get; set; }
    public virtual DbSet<EmpleadosHistorico> EmpleadosHistorico { get; set; }
    public virtual DbSet<CultivosHistorico> CultivosHistorico { get; set; }
    public virtual DbSet<ViverosHistorico> ViverosHistorico { get; set; }
    public virtual DbSet<RanchosHistorico> RanchosHistorico { get; set; }
    public virtual DbSet<VehiculosHistorico> VehiculosHistorico { get; set; }

    // HISTÓRICOS - MOVIMIENTOS
    public virtual DbSet<LlavesHistorico> LlavesHistorico { get; set; }
    public virtual DbSet<PlantacionesHistorico> PlantacionesHistorico { get; set; }
    public virtual DbSet<ReplantesHistorico> ReplantesHistorico { get; set; }
    public virtual DbSet<PodasHistorico> PodasHistorico { get; set; }
    public virtual DbSet<CosechasHistorico> CosechasHistorico { get; set; }
    public virtual DbSet<ProduccionHistorico> ProduccionHistorico { get; set; }
    public virtual DbSet<PersonalCosechaHistorico> PersonalCosechaHistorico { get; set; }
    public virtual DbSet<ViajesHistorico> ViajesHistorico { get; set; }
    //public virtual DbSet<ConductoresHistorico> ConductoresHistorico { get; set; }
    public virtual DbSet<TarimasHistorico> TarimasHistorico { get; set; }
    public virtual DbSet<ProcesoHistorico> ProcesoHistorico { get; set; }
    public virtual DbSet<VentasHistorico> VentasHistorico { get; set; }
    public virtual DbSet<VentasDetallesHistorico> VentasDetallesHistorico { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        // CATÁLOGOS HISTÓRICOS

        // UsuariosHistorico
        modelBuilder.Entity<UsuariosHistorico>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios_historico");

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
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // EmpleadosHistorico
        modelBuilder.Entity<EmpleadosHistorico>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PRIMARY");

            entity.ToTable("empleados_historico");

            entity.Property(e => e.IdEmpleado).HasColumnType("int(11)");
            entity.Property(e => e.Colonia).HasMaxLength(30);
            entity.Property(e => e.CP)
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
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // CultivosHistorico
        modelBuilder.Entity<CultivosHistorico>(entity =>
        {
            entity.HasKey(e => e.IdCultivo).HasName("PRIMARY");

            entity.ToTable("cultivos_historico");

            entity.Property(e => e.IdCultivo).HasColumnType("int(11)");
            entity.Property(e => e.TipoBerry).HasMaxLength(30);
            entity.Property(e => e.Variedad).HasMaxLength(60);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // ViverosHistorico
        modelBuilder.Entity<ViverosHistorico>(entity =>
        {
            entity.HasKey(e => e.IdVivero).HasName("PRIMARY");

            entity.ToTable("viveros_historico");

            entity.Property(e => e.IdVivero).HasColumnType("int(11)");
            entity.Property(e => e.CodigoVivero).HasMaxLength(30);
            entity.Property(e => e.NombreVivero).HasMaxLength(60);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // RanchosHistorico
        modelBuilder.Entity<RanchosHistorico>(entity =>
        {
            entity.HasKey(e => e.IdRancho).HasName("PRIMARY");

            entity.ToTable("ranchos_historico");

            entity.Property(e => e.IdRancho).HasColumnType("int(11)");
            entity.Property(e => e.CP)
                .HasMaxLength(5)
                .HasColumnName("CP");
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(30);
            entity.Property(e => e.Municipio).HasMaxLength(30);
            entity.Property(e => e.NombreRancho).HasMaxLength(60);
            entity.Property(e => e.NumeroRancho).HasMaxLength(60);
            entity.Property(e => e.SuperficieAcres).HasPrecision(18, 2);
            entity.Property(e => e.SuperficieHA)
                .HasPrecision(18, 2)
                .HasColumnName("SuperficieHA");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // VehiculosHistorico
        modelBuilder.Entity<VehiculosHistorico>(entity =>
        {
            entity.HasKey(e => e.IdVehiculo).HasName("PRIMARY");

            entity.ToTable("vehiculos_historico");

            entity.Property(e => e.IdVehiculo).HasColumnType("int(11)");
            entity.Property(e => e.Marca).HasMaxLength(30);
            entity.Property(e => e.Modelo).HasMaxLength(30);
            entity.Property(e => e.Placas).HasMaxLength(7);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // MOVIMIENTOS HISTÓRICOS

        // LlavesHistorico
        modelBuilder.Entity<LlavesHistorico>(entity =>
        {
            entity.HasKey(e => e.IdLlave).HasName("PRIMARY");

            entity.ToTable("llaves_historico");

            entity.Property(e => e.IdLlave).HasColumnType("int(11)");
            entity.Property(e => e.CantidadTuneles).HasColumnType("int(11)");
            entity.Property(e => e.Disponibilidad);
            entity.Property(e => e.IdRancho).HasColumnType("int(11)");
            entity.Property(e => e.NombreLlave).HasMaxLength(60);
            entity.Property(e => e.SuperficieAcres).HasPrecision(18, 2);
            entity.Property(e => e.SuperficieHA)
                .HasPrecision(18, 2)
                .HasColumnName("SuperficieHA");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // PlantacionesHistorico
        modelBuilder.Entity<PlantacionesHistorico>(entity =>
        {
            entity.HasKey(e => e.IdPlantacion).HasName("PRIMARY");

            entity.ToTable("plantaciones_historico");

            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.CantidadPlantas).HasColumnType("int(11)");
            entity.Property(e => e.EstatusPlantacion).HasMaxLength(15);
            entity.Property(e => e.IdCultivo).HasColumnType("int(11)");
            entity.Property(e => e.IdLlave).HasColumnType("int(11)");
            entity.Property(e => e.IdVivero).HasColumnType("int(11)");
            entity.Property(e => e.PlantasPorMetro).HasPrecision(18, 2);
            entity.Property(e => e.FechaPlantacion).HasColumnType("datetime");
            entity.Property(e => e.NumSemPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // ReplantesHistorico
        modelBuilder.Entity<ReplantesHistorico>(entity =>
        {
            entity.HasKey(e => e.IdReplante).HasName("PRIMARY");

            entity.ToTable("replantes_historico");

            entity.Property(e => e.IdReplante).HasColumnType("int(11)");
            entity.Property(e => e.CantidadReplante).HasColumnType("int(11)");
            entity.Property(e => e.IdCultivo).HasColumnType("int(11)");
            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.IdVivero).HasColumnType("int(11)");
            entity.Property(e => e.FechaReplante).HasColumnType("datetime");
            entity.Property(e => e.NumSemReplante).HasColumnType("int(11)");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // PodasHistorico
        modelBuilder.Entity<PodasHistorico>(entity =>
        {
            entity.HasKey(e => e.IdPoda).HasName("PRIMARY");

            entity.ToTable("podas_historico");

            entity.Property(e => e.IdPoda).HasColumnType("int(11)");
            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.TipoPoda).HasMaxLength(20);
            entity.Property(e => e.FechaPoda).HasColumnType("datetime");
            entity.Property(e => e.NumSemPoda).HasColumnType("int(11)");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // CosechasHistorico
        modelBuilder.Entity<CosechasHistorico>(entity =>
        {
            entity.HasKey(e => e.IdCosecha).HasName("PRIMARY");

            entity.ToTable("cosechas_historico");

            entity.Property(e => e.IdCosecha).HasColumnType("int(11)");
            entity.Property(e => e.IdPlantacion).HasColumnType("int(11)");
            entity.Property(e => e.FechaCosecha).HasColumnType("date");
            entity.Property(e => e.NumSemCosecha).HasColumnType("int(11)");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // ProduccionHistorico
        modelBuilder.Entity<ProduccionHistorico>(entity =>
        {
            entity.HasKey(e => e.IdProduccion).HasName("PRIMARY");

            entity.ToTable("produccion_historico");

            entity.Property(e => e.IdProduccion).HasColumnType("int(11)");
            entity.Property(e => e.IdCosecha).HasColumnType("int(11)");
            entity.Property(e => e.TipoCaja).HasMaxLength(30);
            entity.Property(e => e.CantidadCajas).HasColumnType("int(11)");
            entity.Property(e => e.KilosProceso).HasPrecision(18, 2);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // PersonalCosechaHistorico
        modelBuilder.Entity<PersonalCosechaHistorico>(entity =>
        {
            entity.HasKey(e => e.IdPersonalCosecha).HasName("PRIMARY");

            entity.ToTable("personal_cosecha_historico");

            entity.Property(e => e.IdPersonalCosecha).HasColumnType("int(11)");
            entity.Property(e => e.IdCosecha).HasColumnType("int(11)");
            entity.Property(e => e.IdEmpleado).HasColumnType("int(11)");
            entity.Property(e => e.Jarras).HasColumnType("int(11)");
            entity.Property(e => e.PrecioJarra).HasPrecision(18, 2);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // ViajesHistorico
        modelBuilder.Entity<ViajesHistorico>(entity =>
        {
            entity.HasKey(e => e.IdViaje).HasName("PRIMARY");

            entity.ToTable("viajes_historico");

            entity.Property(e => e.IdViaje).HasColumnType("int(11)");
            entity.Property(e => e.IdConductor).HasColumnType("int(11)");
            entity.Property(e => e.IdVehiculo).HasColumnType("int(11)");
            entity.Property(e => e.FechaSalida).HasColumnType("date");
            entity.Property(e => e.NumSemViaje).HasColumnType("int(11)");
            entity.Property(e => e.EstadoAprobacion).HasMaxLength(30);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // ConductoresHistorico
        /*
        modelBuilder.Entity<ConductoresHistorico>(entity =>
        {
            entity.HasKey(e => e.IdConductor).HasName("PRIMARY");

            entity.ToTable("conductores_historico");

            entity.Property(e => e.IdConductor).HasColumnType("int(11)");
            entity.Property(e => e.IdEmpleado).HasColumnType("int(11)");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });
        
        */

        // TarimasHistorico
        modelBuilder.Entity<TarimasHistorico>(entity =>
        {
            entity.HasKey(e => e.IdTarima).HasName("PRIMARY");

            entity.ToTable("tarimas_historico");

            entity.Property(e => e.IdTarima).HasColumnType("int(11)");
            entity.Property(e => e.IdProduccion).HasColumnType("int(11)");
            entity.Property(e => e.IdViaje).HasColumnType("int(11)");
            entity.Property(e => e.CantidadCajasViaje).HasColumnType("int(11)");
            entity.Property(e => e.Licencia).HasColumnType("int(11)");
            entity.Property(e => e.KilosProcesoViaje).HasPrecision(18, 2);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // ProcesoHistorico
        modelBuilder.Entity<ProcesoHistorico>(entity =>
        {
            entity.HasKey(e => e.IdProceso).HasName("PRIMARY");

            entity.ToTable("proceso_historico");

            entity.Property(e => e.IdProceso).HasColumnType("int(11)");
            entity.Property(e => e.IdViaje).HasColumnType("int(11)");
            entity.Property(e => e.ClaseAkg).HasPrecision(10, 2);
            entity.Property(e => e.ClaseBkg).HasPrecision(10, 2);
            entity.Property(e => e.ClaseCkg).HasPrecision(10, 2);
            entity.Property(e => e.Rechazo).HasPrecision(10, 2);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // VentasHistorico
        modelBuilder.Entity<VentasHistorico>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.ToTable("ventas_historico");

            entity.Property(e => e.IdVenta).HasColumnType("int(11)");
            entity.Property(e => e.FechaFacturacion).HasColumnType("date");
            entity.Property(e => e.Total).HasPrecision(18, 2);
            entity.Property(e => e.PrecioDolar).HasPrecision(18, 2);
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        // VentasDetallesHistorico
        modelBuilder.Entity<VentasDetallesHistorico>(entity =>
        {
            entity.HasKey(e => new { e.IdVenta, e.IdTarima }).HasName("PRIMARY");

            entity.ToTable("ventasdetalles_historico");

            entity.Property(e => e.IdVenta).HasColumnType("int(11)");
            entity.Property(e => e.IdTarima).HasColumnType("int(11)");
            entity.Property(e => e.PrecioVentaTarima).HasPrecision(18, 2);
            entity.Property(e => e.FechaRecepcion).HasColumnType("date");
            entity.Property(e => e.FechaTraspaso).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
