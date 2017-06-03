namespace TeamLeasing.Services.AppConfigurationService.CheckAccesToActionService
{
    public interface ICheckAccesToActionService
    {
        bool AcceptOfferByEmployee(string statusOffer, string negotiationStatus);
        bool ResignOfferByEmployee(string status);
        bool NegotiateOfferByEmployee(string statusOffer, string negotiationStatus);
        bool NegotiateOfferByDeveloper(string statusOffer, string negotiationStatus);
        bool AcceptOfferByDeveloper(string statusOffer, string negotiationStatus);
        bool RejectOfferByDeveloper(string status);
    }
}