using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Data;
using System.IO;
using System.Text.Json;

namespace Server.Database
{
    public class ServerdbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<OrderData> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<VisitLog> VisitLogs { get; set; }

        public ServerdbContext()
        {
            _connectionString = GetConnectionString();
        }

        public ServerdbContext(DbContextOptions<ServerdbContext> options) : base(options)
        {
            _connectionString = GetConnectionString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_connectionString,
                    new MySqlServerVersion(new Version(10, 11, 11)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 테이블 상태 ENUM 설정
            modelBuilder.Entity<Table>()
                .Property(t => t.Status)
                .HasConversion<string>();

            // 예약 상태 ENUM 설정  
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Status)
                .HasConversion<string>();
        }

        private string GetConnectionString()
        {
            // 환경변수에서 먼저 확인
            var envConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (!string.IsNullOrEmpty(envConnectionString))
            {
                return envConnectionString;
            }

            // JSON 파일에서 연결 문자열 가져오기
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Database", "Connect_Account.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"DB 연결 설정 파일을 찾을 수 없습니다: {path}");
            }

            string json = File.ReadAllText(path);
            var account = JsonSerializer.Deserialize<ConnectAccount>(json);
            return account?.DB ?? throw new InvalidOperationException("DB 연결 문자열이 null입니다.");
        }

        public IDbConnection CreateConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}