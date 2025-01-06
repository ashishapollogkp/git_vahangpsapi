using vahangpsapi.Data;
using Microsoft.EntityFrameworkCore;

namespace vahangpsapi.Context
{
    public class JwtContext:DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options) :base(options) { }

        public DbSet<RoleMaster> RoleMaster { get; set; } 
        public DbSet<Department> DepartmentMaster { get; set; }
      
        public DbSet<IdProofType> IdProofTypes { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<GeoFence> GeoFenceArea { get; set; } 
        public DbSet<VisitorPurpose> VisitorPurpose { get; set; }
        public DbSet<VisitorMaster> VisitorMaster { get; set; }
        public DbSet<VisitorsStatus> VisitorStatus { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; } 
        public DbSet<AssetSubType> AssetSubTypes { get; set; } 
        public DbSet<DeviceMaster> Device_Master { get; set; }
        public DbSet<VehicleGeoFenceAssignment> VehicleGeoFenceAssignment { get; set; }
        public DbSet<CurrentGpsData> CURRENT_GPS_DATA { get; set; }
        public DbSet<GpsData> GPS_DATA { get; set; }
        public DbSet<InOutGeoFenceData> InOutGeoFenceData { get; set; }
        public DbSet<donation_type> donation_type { get; set; }
        public DbSet<customer_payment> customer_payment { get; set; }
        public DbSet<payment_category> payment_category { get; set; }
        public DbSet<customer_payment_history> customer_payment_history { get; set; }

        public DbSet<EmployeeMaster> EmployeeMaster { get; set; }

        public DbSet<Backend_Master> Backend_Master { get; set; }
        public DbSet<State_Master> State_Master { get; set; }
        public DbSet<Manager_Master> Manager_Master { get; set; }
        public DbSet<RTO_Master> RTO_Master { get; set; }

        public DbSet<district_master> district_master { get; set; }

        public DbSet<product_master> product_master { get; set; }

        public DbSet<manufacturer_product> manufacturer_product { get; set; }

        public DbSet<authority_Master> authority_Master { get; set; }

        public DbSet<vahan_device_master> vahan_device_master { get; set; }
        public DbSet<sim_status_sensorise> sim_status_sensorise { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InOutGeoFenceData>().HasNoKey();
            modelBuilder.Entity<VehicleGeoFenceAssignment>().HasNoKey();
            //modelBuilder.Entity<VehicleGeoFenceAssignment>().HasNoKey();
        }
    }
}
