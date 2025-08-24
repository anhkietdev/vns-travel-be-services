using DAL.Context;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IBookingRepository Booking { get; private set; }
        public IComboRepository Combo { get; private set; }
        public IComboItemRepository ComboItem { get; private set; }
        public IFinancialReportRepository FinancialReport { get; private set; }
        public IHomestayAvailabilityRepository HomestayAvailability { get; private set; }
        public IHomestayBookingRepository HomestayBooking { get; private set; }
        public IHomestayRoomRepository HomestayRoom { get; private set; }
        public IHomestayServiceRepository HomestayService { get; private set; }
        public ILocationRepository Location { get; private set; }
        public IMessageRepository Message { get; private set; }
        public IPartnerDocumentRepository PartnerDocument { get; private set; }
        public IPartnerLocationRepository PartnerLocation { get; private set; }
        public IPartnerRepository Partner { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IRefundRepository Refund { get; private set; }
        public IRevenueRepository Revenue { get; private set; }
        public ISavedLocationRepository SavedLocation { get; private set; }
        public IServiceFeedbackRepository ServiceFeedback { get; private set; }
        public IServicePromotionRepository ServicePromotion { get; private set; }
        public IServiceRatingRepository ServiceRating { get; private set; }
        public IServiceRepository Service { get; private set; }
        public ITourBookingRepository TourBooking { get; private set; }
        public ITourItineraryRepository TourItinerary { get; private set; }
        public ITourScheduleRepository TourSchedule { get; private set; }
        public ITourServiceRepository TourService { get; private set; }
        public IUserBankAccountRepository UserBankAccount { get; private set; }
        public IUserRepository User { get; private set; }
        public IVehicleCategoryRepository VehicleCategory { get; private set; }
        public IVehicleRentalBookingRepository VehicleRentalBooking { get; private set; }
        public IVehicleRentalServiceRepository VehicleRentalService { get; private set; }
        public IVehicleRepository Vehicle { get; private set; }
        public IVoucherRepository Voucher { get; private set; }        

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Booking = new BookingRepository(_context);
            Combo = new ComboRepository(_context);
            ComboItem = new ComboItemRepository(_context);
            FinancialReport = new FinancialReportRepository(_context);
            HomestayAvailability = new HomestayAvailabilityRepository(_context);
            HomestayBooking = new HomestayBookingRepository(_context);
            HomestayRoom = new HomestayRoomRepository(_context);
            HomestayService = new HomestayServiceRepository(_context);
            Location = new LocationRepository(_context);
            Message = new MessageRepository(_context);
            PartnerDocument = new PartnerDocumentRepository(_context);
            PartnerLocation = new PartnerLocationRepository(_context);
            Partner = new PartnerRepository(_context);
            Payment = new PaymentRepository(_context);
            Refund = new RefundRepository(_context);
            Revenue = new RevenueRepository(_context);
            SavedLocation = new SavedLocationRepository(_context);
            ServiceFeedback = new ServiceFeedbackRepository(_context);
            ServicePromotion = new ServicePromotionRepository(_context);
            ServiceRating = new ServiceRatingRepository(_context);
            Service = new ServiceRepository(_context);
            TourBooking = new TourBookingRepository(_context);
            TourItinerary = new TourItineraryRepository(_context);
            TourSchedule = new TourScheduleRepository(_context);
            TourService = new TourServiceRepository(_context);
            UserBankAccount = new UserBankAccountRepository(_context);
            User = new UserRepository(_context);
            VehicleCategory = new VehicleCategoryRepository(_context);
            VehicleRentalBooking = new VehicleRentalBookingRepository(_context);
            VehicleRentalService = new VehicleRentalServiceRepository(_context);
            Vehicle = new VehicleRepository(_context);
            Voucher = new VoucherRepository(_context);
        }        
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
