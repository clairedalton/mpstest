using System;

namespace InvestmentAppProd.Services
{
    public interface IWallClock
    {
        DateTime Now { get; }
    }

    public class WallClock : IWallClock
    {
        public DateTime Now => DateTime.Now;
    }
}