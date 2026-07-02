using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace TradingPlatform.Infrastructure.Persistence
{
    public class TradingPlatformDbContext: DbContext
    {
        public TradingPlatformDbContext(DbContextOptions<TradingPlatformDbContext> options)
            :base(options)
        {

        }
    }
}
