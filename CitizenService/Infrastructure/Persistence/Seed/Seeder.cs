using Application.Helper;
using Domain.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed
{
    public static class Seeder
    {
        public static async Task SeedAsync(CitizenDBContext context)
        {
            if (await context.CitizenAreas.AnyAsync())
            {
                ServiceLogger.Warning(
                    Level.Infrastructure,
                    "Citizen areas already seeded, skipping");
                return;
            }

            var areas = new List<CitizenArea>
            {
                // =========================
                // 6 MUNICIPALITIES (CENTRAL)
                // =========================
                new(Guid.NewGuid(), "Hà Nội", "VN-HN", true),
                new(Guid.NewGuid(), "Thành phố Hồ Chí Minh", "VN-HCM", true),
                new(Guid.NewGuid(), "Hải Phòng", "VN-HP", true),
                new(Guid.NewGuid(), "Đà Nẵng", "VN-DN", true),
                new(Guid.NewGuid(), "Cần Thơ", "VN-CT", true),
                new(Guid.NewGuid(), "Huế", "VN-HUE", true),

                // =========================
                // 28 PROVINCES (MERGED)
                // =========================

                // Northern
                new(Guid.NewGuid(), "Lai Châu", "VN-LC", true),
                new(Guid.NewGuid(), "Điện Biên", "VN-DB", true),
                new(Guid.NewGuid(), "Sơn La", "VN-SL", true),
                new(Guid.NewGuid(), "Lào Cai", "VN-LCYB", true),          // Lào Cai + Yên Bái
                new(Guid.NewGuid(), "Thái Nguyên", "VN-TNBK", true),      // Thái Nguyên + Bắc Kạn
                new(Guid.NewGuid(), "Tuyên Quang", "VN-TQHG", true),      // Tuyên Quang + Hà Giang
                new(Guid.NewGuid(), "Phú Thọ", "VN-PTVPHB", true),        // Phú Thọ + Vĩnh Phúc + Hòa Bình
                new(Guid.NewGuid(), "Bắc Ninh", "VN-BNBG", true),         // Bắc Ninh + Bắc Giang
                new(Guid.NewGuid(), "Hưng Yên", "VN-HYTB", true),         // Hưng Yên + Thái Bình
                new(Guid.NewGuid(), "Ninh Bình", "VN-NBHNND", true),      // Ninh Bình + Hà Nam + Nam Định
                new(Guid.NewGuid(), "Quảng Ninh", "VN-QN", true),
                new(Guid.NewGuid(), "Lạng Sơn", "VN-LS", true),
                new(Guid.NewGuid(), "Cao Bằng", "VN-CB", true),

                // Central
                new(Guid.NewGuid(), "Thanh Hóa", "VN-TH", true),
                new(Guid.NewGuid(), "Nghệ An", "VN-NA", true),
                new(Guid.NewGuid(), "Hà Tĩnh", "VN-HT", true),
                new(Guid.NewGuid(), "Quảng Bình", "VN-QB", true),
                new(Guid.NewGuid(), "Quảng Trị", "VN-QT", true),
                new(Guid.NewGuid(), "Quảng Nam", "VN-QNM", true),
                new(Guid.NewGuid(), "Quảng Ngãi", "VN-QNG", true),
                new(Guid.NewGuid(), "Khánh Hòa", "VN-KHNT", true),        // Khánh Hòa + Ninh Thuận

                // Central Highlands
                new(Guid.NewGuid(), "Gia Lai", "VN-GLBD", true),          // Gia Lai + Bình Định
                new(Guid.NewGuid(), "Đắk Lắk", "VN-DLPY", true),          // Đắk Lắk + Phú Yên
                new(Guid.NewGuid(), "Lâm Đồng", "VN-LD", true),

                // Southern
                new(Guid.NewGuid(), "Đồng Nai", "VN-DNBP", true),         // Đồng Nai + Bình Phước
                new(Guid.NewGuid(), "Tây Ninh", "VN-TNLA", true),         // Tây Ninh + Long An
                new(Guid.NewGuid(), "Vĩnh Long", "VN-VLBTTV", true),      // Vĩnh Long + Bến Tre + Trà Vinh
                new(Guid.NewGuid(), "Đồng Tháp", "VN-DTTG", true),        // Đồng Tháp + Tiền Giang
                new(Guid.NewGuid(), "An Giang", "VN-AGKG", true),         // An Giang + Kiên Giang
                new(Guid.NewGuid(), "Cà Mau", "VN-CMBL", true)            // Cà Mau + Bạc Liêu
            };

            await context.CitizenAreas.AddRangeAsync(areas);
            await context.SaveChangesAsync();

            ServiceLogger.Logging(
                Level.Infrastructure,
                $"Seeded {areas.Count} citizen areas (VN administrative reform 2026)");
        }
    }
}
