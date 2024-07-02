using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Admin> _admin { get; set; }
    public DbSet<Proprietaire> _proprietaire { get; set; }
    public DbSet<Client> _client { get; set; }
    public DbSet<TypeDeBien> _typesDeBien { get; set; }
    public DbSet<Region> _region { get; set; }
    public DbSet<Bien> _bien { get; set; }
    public DbSet<Location> _location { get; set; }
    public DbSet<Photo> _photo { get; set; }
    public DbSet<ViewChiffreAffaire> _vchiffaffaire { get; set; }
    public DbSet<Viewca> _viewca { get; set; }
    public DbSet<DetailLocation> _detail { get; set; }
    public DbSet<ViewListBien> _vlist { get; set; }
    public DbSet<ViewPaye> _vloyer { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);
        // Configuration de la séquence pour TypeProduits

        modelBuilder.Entity<Admin>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR admin_id_seq");        
           
        modelBuilder.Entity<Proprietaire>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR proprietaire_seq");

        modelBuilder.Entity<Client>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR client_seq");

        modelBuilder.Entity<TypeDeBien>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR type_de_bien_seq");

        modelBuilder.Entity<Region>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR region_seq");

        modelBuilder.Entity<Bien>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR bien_seq");

        modelBuilder.Entity<Location>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR location_seq");
        
        modelBuilder.Entity<Photo>()
            .Property(p => p.Id)
            .HasDefaultValueSql($"NEXT VALUE FOR photo_seq");
    
        modelBuilder.Entity<ViewChiffreAffaire>().HasNoKey();
        modelBuilder.Entity<Viewca>().HasNoKey();
        modelBuilder.Entity<DetailLocation>().HasNoKey();
        modelBuilder.Entity<ViewListBien>().HasNoKey();
        modelBuilder.Entity<ViewPaye>().HasNoKey();
            
    }
}