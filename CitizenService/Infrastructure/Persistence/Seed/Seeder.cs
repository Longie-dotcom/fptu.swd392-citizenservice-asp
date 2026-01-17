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
                new(Guid.NewGuid(), "Hà Nội", "VN-HN", 20.816, 21.308, 105.516, 106.037),
                new(Guid.NewGuid(), "Thành phố Hồ Chí Minh", "VN-HCM", 10.650, 10.950, 106.600, 106.950),
                new(Guid.NewGuid(), "Hải Phòng", "VN-HP", 20.700, 20.950, 106.600, 107.000),
                new(Guid.NewGuid(), "Đà Nẵng", "VN-DN", 16.000, 16.300, 108.150, 108.300),
                new(Guid.NewGuid(), "Cần Thơ", "VN-CT", 9.950, 10.100, 105.650, 105.800),
                new(Guid.NewGuid(), "Huế", "VN-HUE", 16.400, 16.500, 107.500, 107.600),

                // =========================
                // 28 PROVINCES (MERGED)
                // =========================

                // Northern
                new(Guid.NewGuid(), "Lai Châu", "VN-LC", 21.250, 22.500, 102.250, 103.500),
                new(Guid.NewGuid(), "Điện Biên", "VN-DB", 21.100, 22.400, 102.300, 103.200),
                new(Guid.NewGuid(), "Sơn La", "VN-SL", 20.800, 21.700, 103.200, 104.200),
                new(Guid.NewGuid(), "Lào Cai + Yên Bái", "VN-LCYB", 20.500, 22.300, 103.500, 105.000),
                new(Guid.NewGuid(), "Thái Nguyên + Bắc Kạn", "VN-TNBK", 20.700, 21.500, 105.700, 106.400),
                new(Guid.NewGuid(), "Tuyên Quang + Hà Giang", "VN-TQHG", 21.000, 22.800, 104.800, 106.200),
                new(Guid.NewGuid(), "Phú Thọ + Vĩnh Phúc + Hòa Bình", "VN-PTVPHB", 20.300, 21.500, 104.800, 105.700),
                new(Guid.NewGuid(), "Bắc Ninh + Bắc Giang", "VN-BNBG", 20.900, 21.200, 105.800, 106.400),
                new(Guid.NewGuid(), "Hưng Yên + Thái Bình", "VN-HYTB", 20.800, 21.200, 106.000, 106.500),
                new(Guid.NewGuid(), "Ninh Bình + Hà Nam + Nam Định", "VN-NBHNND", 20.300, 20.800, 105.800, 106.300),
                new(Guid.NewGuid(), "Quảng Ninh", "VN-QN", 20.500, 21.200, 107.500, 108.500),
                new(Guid.NewGuid(), "Lạng Sơn", "VN-LS", 21.400, 22.500, 105.500, 106.500),
                new(Guid.NewGuid(), "Cao Bằng", "VN-CB", 21.500, 22.700, 105.800, 106.800),

                // Central
                new(Guid.NewGuid(), "Thanh Hóa", "VN-TH", 19.000, 20.500, 104.800, 106.000),
                new(Guid.NewGuid(), "Nghệ An", "VN-NA", 18.600, 19.600, 105.500, 107.200),
                new(Guid.NewGuid(), "Hà Tĩnh", "VN-HT", 18.200, 19.000, 105.500, 107.200),
                new(Guid.NewGuid(), "Quảng Bình", "VN-QB", 17.500, 18.200, 105.300, 107.000),
                new(Guid.NewGuid(), "Quảng Trị", "VN-QT", 16.700, 17.500, 106.500, 107.500),
                new(Guid.NewGuid(), "Quảng Nam", "VN-QNM", 15.800, 16.700, 107.000, 108.500),
                new(Guid.NewGuid(), "Quảng Ngãi", "VN-QNG", 14.900, 15.800, 108.500, 109.300),
                new(Guid.NewGuid(), "Khánh Hòa + Ninh Thuận", "VN-KHNT", 11.800, 13.200, 108.500, 109.500),

                // Central Highlands
                new(Guid.NewGuid(), "Gia Lai + Bình Định", "VN-GLBD", 12.000, 14.000, 107.000, 109.500),
                new(Guid.NewGuid(), "Đắk Lắk + Phú Yên", "VN-DLPY", 12.500, 14.500, 107.000, 109.000),
                new(Guid.NewGuid(), "Lâm Đồng", "VN-LD", 11.500, 12.500, 107.500, 108.500),

                // Southern
                new(Guid.NewGuid(), "Đồng Nai + Bình Phước", "VN-DNBP", 10.500, 11.200, 106.500, 107.200),
                new(Guid.NewGuid(), "Tây Ninh + Long An", "VN-TNLA", 10.500, 11.000, 105.500, 106.500),
                new(Guid.NewGuid(), "Vĩnh Long + Bến Tre + Trà Vinh", "VN-VLBTTV", 9.500, 10.500, 105.500, 106.500),
                new(Guid.NewGuid(), "Đồng Tháp + Tiền Giang", "VN-DTTG", 9.500, 10.500, 105.000, 106.000),
                new(Guid.NewGuid(), "An Giang + Kiên Giang", "VN-AGKG", 9.000, 10.500, 104.500, 105.500),
                new(Guid.NewGuid(), "Cà Mau + Bạc Liêu", "VN-CMBL", 8.400, 9.500, 104.800, 105.500)
            };

            await context.CitizenAreas.AddRangeAsync(areas);
            await context.SaveChangesAsync();

            ServiceLogger.Logging(
                Level.Infrastructure,
                $"Seeded {areas.Count} citizen areas (VN administrative reform 2026)");
        }
    }
}
