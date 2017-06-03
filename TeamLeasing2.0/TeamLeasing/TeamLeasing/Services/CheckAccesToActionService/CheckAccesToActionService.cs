using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Models;

namespace TeamLeasing.Services.AppConfigurationService.CheckAccesToActionService
{
    public class CheckAccesToActionService : ICheckAccesToActionService
    {
        public bool ResignOfferByEmployee(string status)
        {
            if (status == Enums.OfferStatus.Canceled.GetAttribute().Name)
                return false;
            return true;
        }


        public bool AcceptOfferByEmployee(string statusOffer, string negotiationStatus)
        {
            if (string.IsNullOrEmpty(negotiationStatus))
            {
                if (statusOffer == Enums.OfferStatus.Negotiation.GetAttribute().Name)
                    return true;
                return false;
            }
            if (statusOffer == Enums.OfferStatus.Negotiation.GetAttribute().Name
                && negotiationStatus == Enums.NegotiationStatus.Consider.GetAttribute().Name)
            {
                return true;
            }
            return false;
        }

        public bool NegotiateOfferByEmployee(string statusOffer, string negotiationStatus)
        {
            if (string.IsNullOrEmpty(negotiationStatus))
                return false;
            if (statusOffer == Enums.OfferStatus.Negotiation.GetAttribute().Name
                && negotiationStatus == Enums.NegotiationStatus.Consider.GetAttribute().Name)
                return true;
            return false;
        }



        public bool RejectOfferByDeveloper(string status)
        {
            if (status == Enums.OfferStatus.Negotiation.GetAttribute().Name
                || status == Enums.OfferStatus.New.GetAttribute().Name
                || status == Enums.OfferStatus.InProgress.GetAttribute().Name)
                return true;
            return false;
        }

        public bool AcceptOfferByDeveloper(string statusOffer, string negotiationStatus)
        {
            if (string.IsNullOrEmpty(negotiationStatus))
            {
                if (statusOffer == Enums.OfferStatus.New.GetAttribute().Name)
                    return true;
                return false;
            }
            if (statusOffer == Enums.OfferStatus.Negotiation.GetAttribute().Name
                && negotiationStatus == Enums.NegotiationStatus.Consider.GetAttribute().Name)
            {
                return true;
            }
            return false;
        }

        public bool NegotiateOfferByDeveloper(string statusOffer, string negotiationStatus)
        {
            if (string.IsNullOrEmpty(negotiationStatus))
            {
                if (statusOffer == Enums.OfferStatus.New.GetAttribute().Name)
                {
                   return  true;
                }
                return false;
            }
      
            if (statusOffer == Enums.OfferStatus.Negotiation.GetAttribute().Name
                && negotiationStatus == Enums.NegotiationStatus.Consider.GetAttribute().Name)
                return true;
            return false;
        }

    }
}