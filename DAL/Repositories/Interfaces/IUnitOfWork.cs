namespace DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IBookingRepository Booking { get; }
        public IComboRepository Combo { get; }
        public IComboItemRepository ComboItem { get; }
        public IFinancialReportRepository FinancialReport { get; }
        public IHomestayAvailabilityRepository HomestayAvailability { get; }
        public IHomestayBookingRepository HomestayBooking { get; }
        public IHomestayRoomRepository HomestayRoom { get; }
        public IHomestayServiceRepository HomestayService { get; }
        public ILocationRepository Location { get; }
        public IMessageRepository Message { get; }
        public IPartnerDocumentRepository PartnerDocument { get; }
        public IPartnerLocationRepository PartnerLocation { get; }
        public IPartnerRepository Partner { get; }
        public IPaymentRepository Payment { get; }
        public IRefundRepository Refund { get; }
        public IRevenueRepository Revenue { get; }
        public ISavedLocationRepository SavedLocation { get; }
        public IServiceFeedbackRepository ServiceFeedback { get; }
        public IServicePromotionRepository ServicePromotion { get; }
        public IServiceRatingRepository ServiceRating { get; }
        public IServiceRepository Service { get; }
        public ITourBookingRepository TourBooking { get; }
        public ITourItineraryRepository TourItinerary { get; }
        public ITourScheduleRepository TourSchedule { get; }
        public ITourServiceRepository TourService { get; }
        public IUserBankAccountRepository UserBankAccount { get; }
        public IUserRepository User { get; }
        public IVehicleCategoryRepository VehicleCategory { get; }
        public IVehicleRentalBookingRepository VehicleRentalBooking { get; }
        public IVehicleRentalServiceRepository VehicleRentalService { get; }
        public IVehicleRepository Vehicle { get; }
        public IVoucherRepository Voucher { get; }
        Task SaveChangesAsync();
        void Dispose();       
    }
}
